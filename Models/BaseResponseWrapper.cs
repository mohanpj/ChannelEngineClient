namespace Models
{
    public class BaseResponseWrapper
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}