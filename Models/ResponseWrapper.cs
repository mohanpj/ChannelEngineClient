using System.Collections.Generic;

namespace Models
{
    public class BaseResponseWrapper
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ResponseWrapper<T> : BaseResponseWrapper
    {
        public IEnumerable<T> Content { get; set; }
        public int Count { get; set; }
    }
}