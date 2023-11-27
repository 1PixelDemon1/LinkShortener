namespace ShortWeb.Model.Models.Dtos
{
    // When user has logged in successfully
    // we send this model as a result.
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
