using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _ctx;
        private readonly IVegaSharedRepository _vegaRepo;
        public AuthRepository(DataContext context, IVegaSharedRepository vegaSharedRepository)
        {
            _ctx = context;
            _vegaRepo = vegaSharedRepository;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, key;
            CreateHashPassword(password, out passwordHash, out key);

            // now set the password fields
            user.PasswordHash = passwordHash;
            user.PasswordSalt = key;
            _vegaRepo.Add(user);
            await _vegaRepo.SaveAll();
            return user;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] key)
        {
            using ( var hashedPassword = new System.Security.Cryptography.HMACSHA512() )
            {
                passwordHash = hashedPassword.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                key = hashedPassword.Key;
            }
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _ctx.Users.SingleOrDefaultAsync(u => u.Name == userName);
            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using ( var hashPassword = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var enteredPassword = hashPassword.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < enteredPassword.Length; i++)
                {
                    if (passwordHash[i] != enteredPassword[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<bool> UserExist(string name)
        {
            name = name.ToLower();
            var user = await _ctx.Users.SingleOrDefaultAsync(u => u.Name == name);
            if (user == null)
                return false;

            return true;
        }
    }
}
