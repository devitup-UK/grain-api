using DevItUp.Grain.API.Entities;
using DevItUp.Grain.API.Models.Users;
using DevItUp.Grain.API.Models.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevItUp.Grain.API.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        CreateResponse Create(RegisterModel user);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
