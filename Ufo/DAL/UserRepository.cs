using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class UserRepository : InterfaceUserRepository
    {
        private ObservationContext _db;
        private ILogger<UserRepository> _log;

        public UserRepository(ObservationContext db, ILogger<UserRepository> log)
        {
            _db = db;
            _log = log;
        }

        public static byte[] CreateHash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
        }

        public static byte[] CreateSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> Login(User user)
        {
            try
            {
                Users userFound = await _db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
                byte[] hash = CreateHash(user.Password, userFound.Salt);
                bool result = hash.SequenceEqual(userFound.Password);
                if (result) { return true; }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
