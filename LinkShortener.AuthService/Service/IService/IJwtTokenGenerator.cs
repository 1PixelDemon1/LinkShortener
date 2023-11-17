using ShortWeb.AuthService.Models;

namespace ShortWeb.AuthService.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
