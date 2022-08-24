namespace ShowApi.Models
{
    public class BaseResponse<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public BaseResponse(string code = null, string message = null)
        {
            Code = code;
            Message = message;
        }
    }
}
