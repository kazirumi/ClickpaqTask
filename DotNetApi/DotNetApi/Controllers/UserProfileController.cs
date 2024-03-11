
using DotNetApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private IHttpContextAccessor _contextAccessor;

        public UserProfileController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        [HttpGet]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            //string userId = User.Claims.First(c => c.Type == "UserID").Value;
            //var user = await _userManager.FindByIdAsync(userId);
            User user=(User)_contextAccessor.HttpContext.Items["User"];
            return new
            {
                user.Email,
                UserName=user.Name,
                
            };
        }
    }
}
