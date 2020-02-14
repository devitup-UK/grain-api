using DevItUp.Grain.API.Entities;
using DevItUp.Grain.API.Helpers;
using DevItUp.Grain.API.Interfaces;
using DevItUp.Grain.API.Models.Users;
using DevItUp.Grain.API.Models.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevItUp.Grain.API.Specification.Mocks
{
    public class MockUserService : IUserService
    {
        public static Dictionary<int, User> Users = new Dictionary<int, User>();
        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public CreateResponse Create(RegisterModel registerModel)
        {
            CreateResponse response = new CreateResponse();

            if (string.IsNullOrWhiteSpace(registerModel.Password))
            {
                response.Error = new Error(1, "Password was blank.");
                return response;
            }

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                response.Error = new Error(3, "Password and ConfirmPassword do not match.");
                return response;
            }

            if (Users.Any(x => x.Value.Username == registerModel.Username))
            {
                response.Error = new Error(2, "That username is taken.");
                return response;
            }

            response.User = new User();
            response.User.FirstName = registerModel.FirstName;
            response.User.LastName = registerModel.LastName;
            response.User.Username = registerModel.Username;

            if (Users.Count > 0)
            {
                response.User.Id = Users.Keys.Max() + 1;
            }
            else
            {
                response.User.Id = 1;
            }

            Users.Add(response.User.Id, response.User);

            return response;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
