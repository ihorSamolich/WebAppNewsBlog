using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppNewsBlog.Constants;
using WebAppNewsBlog.Data.Entities.Identity;
using WebAppNewsBlog.Helpers;
using WebAppNewsBlog.Interfaces;
using WebAppNewsBlog.Models.User;

namespace WebAppNewsBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private IJwtTokenService _jwtTokenService;

        public UserController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return BadRequest("Невірно введені дані! Спробуйте ще раз!");
                };

                var roles = await _userManager.GetRolesAsync(user);

                var token = _jwtTokenService.CreateToken(user, roles);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest("Помилка авторизації: " + ex);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                string imageName = string.Empty;
                if (!string.IsNullOrEmpty(model.ImageBase64))
                {
                    imageName = await ImageWorker.SaveImageAsync(model.ImageBase64);
                }
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    Image = imageName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, Roles.User);
                }
                else
                {
                    return BadRequest("Помилка реєстрації!");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var token = _jwtTokenService.CreateToken(user, roles);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest("Помилка авторизації: " + ex);
            }
        }
    }
}
