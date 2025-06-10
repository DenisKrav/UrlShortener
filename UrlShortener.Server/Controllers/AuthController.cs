using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.DTOs.Auth.Request;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.InterfacesServices;
using UrlShortener.Server.Utilities;
using UrlShortener.Server.ViewModels.Auth.Request;

namespace UrlShortener.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<GeneralResultModel>> Login([FromBody] LoginRequestViewModel request)
        {
            GeneralResultModel generalResult = new GeneralResultModel();

            try
            {
                await _authService.LoginAsync(_mapper.Map<LoginRequestDto>(request));

                var userDto = await _userService.GetUserByEmailAsync(request.Email);

                var token = await _authService.GenerateToken(userDto.Id.ToString(), userDto.Email);

                generalResult.Result = token;

                return Ok(generalResult);
            }
            catch (InvalidLoginPasswordException ex)
            {
                generalResult.Errors.Add(ex.Message);

                return Unauthorized(generalResult);
            }
            catch (InvalidLoginEmailException ex)
            {
                generalResult.Errors.Add(ex.Message);

                return Unauthorized(generalResult);
            }
            catch (UserArgumentException ex)
            {
                generalResult.Errors.Add(ex.Message);

                return Unauthorized(generalResult);
            }
            catch (Exception ex)
            {
                generalResult.Errors.Add(ex.Message);
                return StatusCode(500, generalResult);
            }
        }
    }
}
