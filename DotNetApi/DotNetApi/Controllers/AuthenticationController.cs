using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotNetApi.Data;
using DotNetApi.DTO;
using DotNetApi.Model;
using DotNetApi.AuthServices;


namespace DotNetApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IJwtUtils _jwtUtils;

        public AuthenticationController(ApplicationDBContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("api/[controller]/Login")]
        public ActionResult<string> Login(LoginDTO loginDTO)
        {
            var user =_context.users.FirstOrDefault(c=>c.Name==loginDTO.UserName);
            if (user==null)
            {
                return BadRequest("Email not found");
            }

            var PasswordMatched = BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password,user.Password);
            if (!PasswordMatched)
            {
                return BadRequest("Incorrect password");
            }

            var token=_jwtUtils.GenerateJwtToken(user);
            return  Ok(new { user, token });
        }

        [HttpPost("api/[controller]/Register")]
        public async Task<ActionResult<UserDTO>> Register(UserDTO user)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.users'  is null.");
            }
            var userNameCheck = _context.users.FirstOrDefault(x => x.Name == user.Name);

            if (userNameCheck != null)
            {
                return BadRequest("User Name Already Exist");
            }

            var emailCheck = _context.users.FirstOrDefault(x => x.Email == user.Email);
            
            if(emailCheck != null)
            {
                return BadRequest("Email Already Exist");
            }
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);

            var userEntity = user.Adapt<User>();
            _context.users.Add(userEntity);
            await _context.SaveChangesAsync();

            return Ok(userEntity.Adapt<UserDTO>());
        }
    }
}
