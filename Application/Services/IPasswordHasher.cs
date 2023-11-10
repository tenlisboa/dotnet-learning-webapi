namespace LearnApi.Application.Services
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool ComparePassword(string password, string hashedPassword);
    }
}