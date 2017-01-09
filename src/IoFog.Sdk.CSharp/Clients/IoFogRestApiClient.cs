﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using IoFog.Sdk.CSharp.Dto;
using IoFog.Sdk.CSharp.Handlers;
using IoFog.Sdk.CSharp.Utils;

using Newtonsoft.Json.Linq;

namespace IoFog.Sdk.CSharp.Clients
{
    public class IoFogRestApiClient : IoFogClientBase, IDisposable
    {
        private readonly HttpClient _client;
        private readonly IIoFogRestApiHandler _handler;

        public IoFogRestApiClient(IIoFogRestApiHandler handler, string host = null, int? port = null) : base("http", host, port)
        {
            _handler = handler;
            _client = new HttpClient
            {
                BaseAddress = BuildUri()
            };
        }

        public async Task<JObject> GetContainerConfigAsync()
        {
            var content = new JObject
            {
                new JProperty("id", ContainerId)
            };

            var response = await ExecutePost("v2/config/get", content);
            return response;
        }

        public async Task<JObject> GetContainerNextUnreadMessagesConfigAsync()
        {
            var content = new JObject
            {
                new JProperty("id", ContainerId)
            };

            var response = await ExecutePost("v2/messages/next", content);
            return response;
        }

        public async Task<JObject> PostMessageAsync(IoMessage ioMessage)
        {
            ioMessage.Publisher = Environment.GetEnvironmentVariable("selfname");
            var content = ioMessage.GetJson();

            var response = await ExecutePost("v2/messages/new", content);
            return response;
        }

        public async Task<JObject> GetMessagesFromPublishersWithinTimeframeAsync(long timeframeStart, long timeframeEnd, string[] publishers)
        {
            var content = new JObject
            {
                new JProperty("id", ContainerId),
                new JProperty("timeframestart", timeframeStart),
                new JProperty("timeframeend", timeframeEnd),
                new JProperty("publishers", publishers)
            };

            var response = await ExecutePost("v2/messages/query", content);
            return response;
        }

        private async Task<JObject> ExecutePost(string url, JObject content)
        {
            try
            {
                using (var message = await _client.PostAsync(url, new JsonContent(content.ToString(), Encoding.UTF8, "application/json")))
                {
                    await CheckErrorCode(message);
                    var result = await message.Content.ReadAsStringAsync();

                    return JObject.Parse(result);
                }
            }
            catch (Exception exception)
            {
                _handler.OnException(exception);
            }

            return null;
        }

        private async Task CheckErrorCode(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                if (message.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessage = await message.Content.ReadAsStringAsync();
                    _handler.OnBadRequest(errorMessage);
                }
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
