using System.Collections.Generic;

namespace Models
{
    public class ResponseWrapper<T> : BaseResponseWrapper
    {
        public IEnumerable<T> Content { get; set; }
        public int Count { get; set; }
    }
}