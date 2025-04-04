namespace TipMeBackend.Models
{
    public class Response<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public Response() { }

        public Response(T data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
            Message = "";
        }

        public Response(string message, int statusCode)
        {
            Data = default;
            Message = message;
            StatusCode = statusCode;
        }
        
    }
}
