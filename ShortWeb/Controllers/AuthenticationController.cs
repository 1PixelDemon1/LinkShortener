using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShortWeb.Model.Models.Dtos;
using ShortWeb.Service.IService;
using ShortWeb.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShortWeb.Areas.User.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly Service.IService.IAuthenticationService _authenticationService;
        private readonly ITokenProvider _tokenProvider;

        public AuthenticationController(Service.IService.IAuthenticationService authenticationService, ITokenProvider tokenProvider)
        {
            _authenticationService = authenticationService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authenticationService.LoginAsync(obj);

            if (responseDto is not null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                if (loginResponseDto?.Token is not null && !string.IsNullOrEmpty(loginResponseDto.Token))
                {
                    await SignInUser(loginResponseDto);
                    _tokenProvider.SetToken(loginResponseDto.Token);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomerError", responseDto.Message);
                return View(obj);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new() {Text = StaticData.RoleAdmin, Value=StaticData.RoleAdmin},
                new() {Text = StaticData.RoleUser, Value=StaticData.RoleUser},
            };

            ViewBag.RoleList = roleList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authenticationService.RegisterAsync(obj);
            if(result is not null && result.IsSuccess)
            {
                if(string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = StaticData.RoleUser;
                }
                ResponseDto assignRole = await _authenticationService.AssignRoleAsync(obj);
                if(assignRole is not null && assignRole.IsSuccess)
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                ModelState.AddModelError("RegistrationError", result.Message);
            }
            var roleList = new List<SelectListItem>()
            {
                new() {Text = StaticData.RoleAdmin, Value=StaticData.RoleAdmin},
                new() {Text = StaticData.RoleUser, Value=StaticData.RoleUser},
            };

            ViewBag.RoleList = roleList;

            return View(obj);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            // Signing in user using default .net identity.
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));            
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, 
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));            
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, 
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            // Very important asp.net integration.
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
            

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
