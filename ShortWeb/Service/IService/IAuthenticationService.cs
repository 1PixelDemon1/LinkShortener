using ShortWeb.Model.Models.Dtos;

namespace ShortWeb.Service.IService
{
    public interface IAuthenticationService
    {

        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
