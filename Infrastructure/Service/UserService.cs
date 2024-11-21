using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;

namespace Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieService _movieService;
        public UserService(IUserRepository userRepository, IMovieService movieService )
        {
            _userRepository = userRepository;
            _movieService = movieService;   
        }
        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // first check if the email user entered exists in the database
            // if yes, throw an exception or send a message saying email exists
            var user = await _userRepository.GetUserByEmail(requestModel.Email);
            if (user != null)
            {
                // email exist in the database
                throw new Exception($"Email {requestModel.Email} exists, please try again.");
            }

            // continue => Emmail doesn't exist in the DB
            // create a random salt and has the password with the salt
            // Using Library:  Microsoft.AspNetCore.Cryptography.KeyDerivation
            var salt = GenerateSalt();
            var hashedPassword = GenerateHashedPassword(requestModel.Password, salt);

            // create user entity object and call user repo to save
            var newUser = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            // Save in DB -> Repository
            var createdUser = await _userRepository.AddAsync(newUser);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;
        }
//登录login
        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // Get the user info from database by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                // Don't have the email in the database
                return null;
            }

            // Hash the user entered password along with salt from database
            var hashedPassword = GenerateHashedPassword(password, user.Salt);
            if (hashedPassword == user.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth ?? DateTime.UtcNow
                };
                //密码对了的话把信息传回前端 
                return userLoginResponseModel;
            }
            return null;
        }

        // copy from documentation
        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        /* *
         * Hashing Rules:
         * NEVER create your own hashing algorithm -> KeyDerivation.Pbkdf2; Argon2
         * */

        // copy from documentation
        private string GenerateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }
    }
}
