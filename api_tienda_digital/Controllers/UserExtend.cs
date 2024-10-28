using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using api_tienda_digital.Context.Models;
using AutoMapper;
using api_tienda_digital.Context.CreatedUserExtendDTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_tienda_digital.DTOs.LoginDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using api_tienda_digital.DTOs.UserExtendDTO;

namespace api_tienda_digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExtendController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserExtend> _userManager;
        private readonly SignInManager<UserExtend> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserExtendController(UserManager<UserExtend> userManager, SignInManager<UserExtend> signInManager, IMapper mapper, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        // GET: api/UserExtend
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserExtend>>> GetUserExtends()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/UserExtend/5
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserExtend>> GetUserExtend(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/UserExtend/Register
        [HttpPost("register")]
        public async Task<ActionResult<UserExtendDTO>> Register(CreatedUserExtendDTO createdUserExtendDTO)
        {
            if (createdUserExtendDTO.Role != "admin" && createdUserExtendDTO.Role != "user")
            {
                return BadRequest("Invalid role. Only 'admin' and 'user' roles are allowed.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(createdUserExtendDTO.Role);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(createdUserExtendDTO.Role));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Error creating the role.");
                }
            }

            var userExtend = _mapper.Map<UserExtend>(createdUserExtendDTO);

            userExtend.CreatedDate = DateTime.Now;
            userExtend.UpdatedDate = DateTime.Now;
            userExtend.IsActive = true;
            userExtend.EmailConfirmed = true;
            userExtend.PhoneNumberConfirmed = true;
            userExtend.TwoFactorEnabled = true;
            userExtend.LockoutEnabled = true;
            userExtend.AccessFailedCount = 0;

            var result = await _userManager.CreateAsync(userExtend, createdUserExtendDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(userExtend, createdUserExtendDTO.Role);

            var createdUserDTO = _mapper.Map<UserExtendDTO>(userExtend);

            return CreatedAtAction(nameof(GetUserExtend), new { id = userExtend.Id }, createdUserDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSettingsKey = _configuration.GetValue<string>("JwtSettings:Key");

            if (string.IsNullOrEmpty(jwtSettingsKey))
            {
                throw new InvalidOperationException("The JWT signing key is missing in the configuration.");
            }

            var key = Encoding.UTF8.GetBytes(jwtSettingsKey);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration.GetValue<string>("JwtSettings:Issuer"),
                Audience = _configuration.GetValue<string>("JwtSettings:Audience")
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token)
            });
        }

        // DELETE: api/UserExtend/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserExtend(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
