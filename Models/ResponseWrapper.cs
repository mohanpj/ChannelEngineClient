using System.Collections.Generic;

namespace Models
{
    public class ResponseWrapper<T>
    {
        public IEnumerable<T> Content { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}