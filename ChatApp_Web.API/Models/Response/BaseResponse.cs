namespace ChatApp_Web.API.Models.Response
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string? Errors { get; set; }
    }
}
