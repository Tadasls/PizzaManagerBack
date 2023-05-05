using CompetitionEventsManager.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace CompetitionEventsManager.Services
{
    public class UserService : IUserService
    {

        /// <summary>
        /// Uzkoduojam/uz'hashinam slaptazodi
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        /// <summary>
        /// patikrinam slaptazodi
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var cumputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return cumputedHash.SequenceEqual(passwordHash);
            }
        }
    }
}