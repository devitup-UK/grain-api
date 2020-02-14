using DevItUp.Grain.API.Entities;
using DevItUp.Grain.API.Helpers;
using DevItUp.Grain.API.Interfaces;
using DevItUp.Grain.API.Models.Users;
using DevItUp.Grain.API.Models.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevItUp.Grain.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordManagement.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public CreateResponse Create(RegisterModel registerModel)
        {
            CreateResponse response = new CreateResponse();

            // validation
            if (string.IsNullOrWhiteSpace(registerModel.Password))
            {
                response.Error = new Error(1, "Password was not supplied.");
                return response;
            }

            if (_context.Users.Any(x => x.Username == registerModel.Username))
            {
                response.Error = new Error(2, "That username is taken.");
                return response;
            }

            if(registerModel.Password != registerModel.ConfirmPassword)
            {
                response.Error = new Error(3, "Password and ConfirmPassword did not match.");
                return response;
            }

            response.User = new User();
            response.User.FirstName = registerModel.FirstName;
            response.User.LastName = registerModel.LastName;
            response.User.Username = registerModel.Username;

            PasswordManagement.CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            response.User.PasswordHash = passwordHash;
            response.User.PasswordSalt = passwordSalt;

            _context.Users.Add(response.User);
            _context.SaveChanges();

            return response;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");

                user.Username = userParam.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                PasswordManagement.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}