using AutoMapper;
using DevItUp.Grain.API.Controllers;
using DevItUp.Grain.API.Specification.Model;
using DevItUp.Grain.API.Specification.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;
using DevItUp.Grain.API.Models.Users;
using DevItUp.Grain.API.Services;
using DevItUp.Grain.API.Models.Users.Responses;

namespace DevItUp.Grain.API.Specification
{
    public class UserContext
    {
        public RegisterRequestData Register_RequestData { get; set; }
        public UsersController Controller_Users { get; set; }
        public MockUserService Mock_UsersService { get; set; }
        public IMapper Mock_Mapper { get; set; }
        public IActionResult TheEndpointResponse { get; set; }
        public IConfiguration Test_Configuration { get; set; }
        public RegisterModel Test_RegisterModel { get; set; }
        public UserService Test_UserService { get; set; }
        public CreateResponse Test_CreateResponse { get; set; }
    }
}
