namespace ShowApi.Models
{
    public class BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public BaseResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
