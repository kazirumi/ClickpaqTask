using DotNetApi.Model;

namespace DotNetApi.AuthServices
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(User user);
        public string? ValidateJwtToken(string token);
    }
}