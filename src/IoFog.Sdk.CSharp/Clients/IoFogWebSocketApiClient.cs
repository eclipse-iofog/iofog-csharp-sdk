using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

using IoFog.Sdk.CSharp.Dto;
using IoFog.Sdk.CSharp.Handlers;
using IoFog.Sdk.CSharp.Utils;

namespace IoFog.Sdk.CSharp.Clients
{
    public class IoFogWebSocketApiClient : IoFogClientBase, IDisposable
    {
        private readonly ClientWebSocket _client;
        private readonly IIoFogWebSocketApiHandler _handler;

        private readonly Dictionary<IoFogWebSocketEndpointEnum, string> _endpoints;

        public IoFogWebSocketApiClient(IIoFogWebSocketApiHandler handler, string host = null, int? port = null) : base("ws", host, port)
        {
            _handler = handler;
            _client = new ClientWebSocket();

            _endpoints = new Dictionary<IoFogWebSocketEndpointEnum, string>
            {
                {IoFogWebSocketEndpointEnum.ControlSocket, $"v2/control/socket/id/{ContainerId}"},
                {IoFogWebSocketEndpointEnum.MessageSocket, $"v2/message/socket/id/{ContainerId}"},
            };
        }

        public WebSocketState State => _client.State;

        public async Task ConnectAsync(IoFogWebSocketEndpointEnum endpoint)
        {
            try
            {
                var uri = BuildUri(_endpoints[endpoint]);
                Console.WriteLine($"{DateTime.Now}: " + $"connecting to enpoint: {uri}");
                await _client.ConnectAsync(uri, CancellationToken.None);
            }
            catch (Exception exception)
            {
                _handler.OnException(exception);
            }
        }

        public async Task CloseConnectionAsync()
        {
            try
            {
                await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal closure", CancellationToken.None);
            }
            catch (Exception exception)
            {
                _handler.OnException(exception);
            }
        }

        public void AbortConnection()
        {
            try
            {
                _client.Abort();
            }
            catch (Exception exception)
            {
                _handler.OnException(exception);
            }
        }

        public async Task SendMessageAsync(IoMessage ioMessage)
        {
            ioMessage.Publisher = Environment.GetEnvironmentVariable("SELFNAME");
            Console.WriteLine($"{DateTime.Now}: " + "sending message over WS:");
            Console.WriteLine($"{DateTime.Now}: " + ioMessage.ToString());
            Console.WriteLine();
            byte[] messageRawBytes = ioMessage.GetBytes();
            ArraySegment<byte> requestBuffer;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    byte[] header = new byte[5];
                    byte[] bLenArr = ByteUtils.IntegerToBytes(messageRawBytes.Length);
                    header[0] = (byte)IoFogWebSocketOpCodeEnum.Message;
                    header[1] = bLenArr[0];
                    header[2] = bLenArr[1];
                    header[3] = bLenArr[2];
                    header[4] = bLenArr[3];

                    byte[] message = new byte[header.Length + messageRawBytes.Length];

                    Array.Copy(header, 0, message, 0, header.Length);
                    Array.Copy(messageRawBytes, 0, message, header.Length, messageRawBytes.Length);

                    writer.Write(message);
                }

                requestBuffer = new ArraySegment<byte>(stream.ToArray());
            }

            await SendInternalAsync(requestBuffer);
        }

        public async Task SendAcknowledgeAsync()
        {
            Console.WriteLine($"{DateTime.Now}: " +"Sending acknowlenge");
            ArraySegment<byte> requestBuffer;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write((byte)IoFogWebSocketOpCodeEnum.Ack);
                }

                requestBuffer = new ArraySegment<byte>(stream.ToArray());
            }

            await SendInternalAsync(requestBuffer);
        }

        public async Task StartListenAsync()
        {
            Console.WriteLine($"{DateTime.Now}: " +"starting listen WS endpoint");
            while (_client.State == WebSocketState.Open)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        var buffer = new ArraySegment<byte>(new byte[1024]);
                        WebSocketReceiveResult result;
                        do
                        {
                            result = await _client.ReceiveAsync(buffer, CancellationToken.None);
                            stream.Write(buffer.Array, buffer.Offset, buffer.Count);
                        } while (result != null && !result.EndOfMessage);

                        await HandleRecievedMessage(stream.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    _handler.OnException(exception);
                }
            }
            Console.WriteLine($"{DateTime.Now}: " +"ending of listening WS endpoint");
        }

        private async Task SendInternalAsync(ArraySegment<byte> buffer)
        {
            try
            {
                await _client.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            catch (Exception exception)
            {
                _handler.OnException(exception);
            }
        }

        private async Task HandleRecievedMessage(byte[] content)
        {
            var opСode = (IoFogWebSocketOpCodeEnum)content[0];

            switch (opСode)
            {
                case IoFogWebSocketOpCodeEnum.Ping:
                    Console.WriteLine($"{DateTime.Now}: " +"PING received");
                    break;
                case IoFogWebSocketOpCodeEnum.Pong:
                    Console.WriteLine($"{DateTime.Now}: " +"PONG received");
                    break;
                case IoFogWebSocketOpCodeEnum.Ack:
                    Console.WriteLine($"{DateTime.Now}: " +"ACK received");
                    break;
                case IoFogWebSocketOpCodeEnum.ControlSignal:
                    Console.WriteLine($"{DateTime.Now}: " +"ControlSignal received");
                    await SendAcknowledgeAsync();
                    _handler.OnNewConfigSignal();
                    break;
                case IoFogWebSocketOpCodeEnum.Message:
                    Console.WriteLine($"{DateTime.Now}: " +"MESSAGE received");
                    int totalMsgLength = ByteUtils.BytesToInteger(ByteUtils.CopyOfRange(content, 1, 5));
                    int msgLength = totalMsgLength + 5;
                    var message = new IoMessage(ByteUtils.CopyOfRange(content, 5, msgLength));
                    await SendAcknowledgeAsync();
                    _handler.OnMessage(message);
                    break;
                case IoFogWebSocketOpCodeEnum.Receipt:
                    Console.WriteLine($"{DateTime.Now}: " +"RECEIPT received");
                    int size = content[1];
                    int pos = 3;
                    string messageId = string.Empty;
                    if (size > 0)
                    {
                        messageId = StringUtils.NewString(ByteUtils.CopyOfRange(content, pos, pos + size));
                        pos += size;
                    }
                    size = content[2];
                    long timestamp = 0L;
                    if (size > 0)
                    {
                        timestamp = ByteUtils.BytesToLong(ByteUtils.CopyOfRange(content, pos, pos + size));
                    }
                    await SendAcknowledgeAsync();
                    _handler.OnReceipt(messageId, timestamp);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
