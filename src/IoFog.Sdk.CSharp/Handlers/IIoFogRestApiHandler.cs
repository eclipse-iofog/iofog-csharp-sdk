using System;

namespace IoFog.Sdk.CSharp.Handlers
{
    public interface IIoFogRestApiHandler
    {
        /// <summary>
        /// Method is triggered when Container catches an exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        void OnException(Exception exception);

        /// <summary>
        /// Method is triggered when Container receives Bad Request (400) response from ioFog.
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        void OnBadRequest(string errorMessage);
    }
}
