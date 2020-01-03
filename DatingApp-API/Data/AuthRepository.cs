using System;
using System.Threading.Tasks;
using DatingApp_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        //inject our DataContext once this class will be responsible for
        //querying our database via EntityFramework
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //anything here will be disposed once we finish with it
                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for ( int i = 0; i < computedHash.Length; i++){
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password) //user and pw entities
        {
            //declare byte arrays: password hash and password salt
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); //method to create passwordHash and salt

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //as it's async, we need to add await
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //straight forward process
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //anything here will be disposed once we finish with it
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync( x => x.Username == username))
                return true;

            return false;
        }
    }
}