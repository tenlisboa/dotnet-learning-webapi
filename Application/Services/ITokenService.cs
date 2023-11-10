using LearnApi.Domain.Models;

namespace LearnApi.Application.Services
{
    public interface ITokenService
    {
        public object GenerateToken(User user);
    }
}