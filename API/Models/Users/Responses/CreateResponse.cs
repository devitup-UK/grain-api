using DevItUp.Grain.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevItUp.Grain.API.Models.Users.Responses
{
    public class CreateResponse
    {
        public User User { get; set; }
        public Error Error { get; set; }
    }
}
