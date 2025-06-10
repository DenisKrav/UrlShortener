using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.DTOs.Auth.Request;
using UrlShortener.BLL.DTOs.User.Request;
using UrlShortener.BLL.DTOs.User.Response;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.InterfacesServices;
using UrlShortener.DAL.Enums;
using UrlShortener.DAL.Models.Identities;
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
        private readonly RoleManager<ApplicationRole> _roleManager;


        public AuthController(IUserService userService, IAuthService authService, IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
            _roleManager = roleManager;

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

        ///////////////////////////////////////////////////////////////////////////////////
        // For manual register user
        //[HttpPost("register")]
        //public async Task RegisterUserTest()
        //{
        //    //Comment and uncomment user one by one and launch method

        //    // Admin user
        //    //var newUserDto = new NewUserDTO();
        //    //newUserDto.Email = "admin@gmail.com";
        //    //newUserDto.Password = "Admin1234!";
        //    //newUserDto.Phone = "1234567890";
        //    //newUserDto.FirstName = "Admin";
        //    //newUserDto.LastName = "Adminov";
        //    //newUserDto.Role = UserRole.Admin;

        //    // Admin user
        //    var newUserDto = new NewUserDTO();
        //    newUserDto.Email = "manager@gmail.com";
        //    newUserDto.Password = "Manager1234!";
        //    newUserDto.Phone = "3334567890";
        //    newUserDto.FirstName = "Manager";
        //    newUserDto.LastName = "Managerov";
        //    newUserDto.Role = UserRole.Manager;

        //    var (result, userId) = await _userService.CreateUserAsync(newUserDto);
        //}

        // For manual deliting user
        //[HttpDelete("delete")]
        //public async Task DeleteUserTest()
        //{
        //    var res = await _userService.DeleteUserAsync(1);
        //}

        // For manual adding user role
        //[HttpPost("add-role")]
        //public async Task<IActionResult> AddRole()
        //{
        //    //Comment and uncomment role one by one and launch method

        //    // Role 1
        //    string roleName = UserRole.User.ToString();

        //    // Role 2
        //    //string roleName = UserRole.Manager.ToString();

        //    // Role 3
        //    //string roleName = UserRole.Admin.ToString();

        //    if (await _roleManager.RoleExistsAsync(roleName))
        //    {
        //        return Conflict($"Role '{roleName}' already exists.");
        //    }

        //    var role = new ApplicationRole { Name = roleName };
        //    var result = await _roleManager.CreateAsync(role);

        //    if (result.Succeeded)
        //    {
        //        return Ok($"Role '{roleName}' created successfully.");
        //    }

        //    return BadRequest(result.Errors);
        //}
        ///////////////////////////////////////////////////////////////////////////////////
    }
}
