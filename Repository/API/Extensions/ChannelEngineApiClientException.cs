using System;
using System.Net;

namespace Repository.API.Extensions
{
    public class ChannelEngineApiClientException : Exception
    {
        public readonly object AdditionalInfo;
        public readonly HttpStatusCode StatusCode;

        public ChannelEngineApiClientException(
            HttpStatusCode statusCode,
            string message,
            object additionalInfo = null)
            : base(message)
        {
            StatusCode = statusCode;
            AdditionalInfo = additionalInfo;
        }
    }
}