using AutoMapper;
using DevItUp.Grain.API.Controllers;
using DevItUp.Grain.API.Specification.Model;
using DevItUp.Grain.API.Specification.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DevItUp.Grain.API.Specification
{
    public static class ScenarioProperties
    {
        public static RegisterRequestData Register_RequestData
        {
            get
            {
                return ScenarioContext.Current.Get<RegisterRequestData>("Register_RequestData");
            }
            set
            {
                ScenarioContext.Current.Set<RegisterRequestData>(value, "Register_RequestData");
            }
        }

        public static UsersController Controller_Users
        {
            get
            {
                return ScenarioContext.Current.Get<UsersController>("Controller_Users");
            }
            set
            {
                ScenarioContext.Current.Set<UsersController>(value, "Controller_Users");
            }
        }

        public static MockUserService Mock_UsersService
        {
            get
            {
                return ScenarioContext.Current.Get<MockUserService>("Mock_UsersService");
            }
            set
            {
                ScenarioContext.Current.Set<MockUserService>(value, "Mock_UsersService");
            }
        }

        public static IMapper Mock_Mapper
        {
            get
            {
                return ScenarioContext.Current.Get<IMapper>("Mock_Mapper");
            }
            set
            {
                ScenarioContext.Current.Set<IMapper>(value, "Mock_Mapper");
            }
        }

        public static IActionResult TheEndpointResponse
        {
            get
            {
                return ScenarioContext.Current.Get<IActionResult>("TheEndpointResponse");
            }
            set
            {
                ScenarioContext.Current.Set<IActionResult>(value, "TheEndpointResponse");
            }
        }
    }
}
