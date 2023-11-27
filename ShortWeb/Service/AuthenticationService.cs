
using ShortWeb.Model.Models.Dtos;
using ShortWeb.Service.IService;
using ShortWeb.Utility;
using System.Security.Policy;

namespace ShortWeb.Service
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IBaseService _baseService;
        
        public AuthenticationService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto, 
                Url = StaticData.AuthenticationApiBase + "api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new()
            {
                ApiType = StaticData.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticData.AuthenticationApiBase + "api/auth/login"
            });
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.AuthenticationApiBase + "api/auth/register"
            });
        }
    }
}
