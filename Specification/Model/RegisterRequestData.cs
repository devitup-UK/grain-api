using DevItUp.Grain.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevItUp.Grain.API.Specification.Model
{
    public class RegisterRequestData
    {
        public string RequestType { get; set; }
        public RegisterModel RequestObject { get; set; }
    }
}
