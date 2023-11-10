namespace LearnApi.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int WORK_FACTOR = 13;

        public string HashPassword(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password, WORK_FACTOR);

        public bool ComparePassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}