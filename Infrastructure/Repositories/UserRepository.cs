using LearnApi.Domain.Models;

namespace LearnApi.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ConnectionContext _context;

    public UserRepository(ConnectionContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetByUsername(string username)
    {
        return _context.Users
            .Where(
                u => u.Username.Equals(username)
            ).FirstOrDefault();
    }

}