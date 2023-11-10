using LearnApi.Models;

namespace LearnApi.Services
{
    public interface ITokenService
    {
        public object GenerateToken(Employee employee);
    }
}