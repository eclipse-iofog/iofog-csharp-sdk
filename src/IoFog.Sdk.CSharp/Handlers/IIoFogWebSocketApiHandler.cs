using System;

using IoFog.Sdk.CSharp.Dto;

namespace IoFog.Sdk.CSharp.Handlers
{

    /// <summary>
    /// Listener's Interface for requests to ioFog.
    /// </summary>
    public interface IIoFogWebSocketApiHandler
    {

        /// <summary>
        /// Method is triggered when Container receives messages.
        /// </summary>
        /// <param name="message">Received message</param>
        void OnMessage(IoMessage message);

        /// <summary>
        /// Method is triggered when Container catches an exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        void OnException(Exception exception);

        /// <summary>
        /// Method is triggered when Container receives a signal about new configuration.
        /// </summary>
        void OnNewConfigSignal();

        /// <summary>
        /// Method is triggered when Container receives a signal just sended message.
        /// </summary>
        /// <param name="messageId">Message id</param>
        /// <param name="timestamp">Timestamp</param>
        void OnReceipt(string messageId, long timestamp);
    }
}
