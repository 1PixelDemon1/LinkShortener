using ShortWeb.AuthService.Models.Dtos;

namespace ShortWeb.AuthService.Service.IService
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
