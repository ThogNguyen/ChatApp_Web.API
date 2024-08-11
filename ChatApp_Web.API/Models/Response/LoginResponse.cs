namespace ChatApp_Web.API.Models.Response
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }
}
