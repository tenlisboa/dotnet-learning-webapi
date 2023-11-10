namespace LearnApi.Domain.Models;

public interface IUserRepository
{
    void Add(User user);
    User? GetByUsername(string username);
}