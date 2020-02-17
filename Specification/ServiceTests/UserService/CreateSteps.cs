using DevItUp.Grain.API.Helpers;
using DevItUp.Grain.API.Models.Users;
using DevItUp.Grain.API.Models.Users.Responses;
using DevItUp.Grain.API.Specification;
using DevItUp.Grain.API.Specification.Helpers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace DevItUp.Grain.API.Specification.ServiceTests.UserService
{
    [Binding]
    class CreateSteps
    {
        private readonly UserContext _userContext;
        private DataContext _dataContext;

        public CreateSteps(UserContext userContext)
        {
            _userContext = userContext;
        }

        [Given(@"I have configured a connection string")]
        public void GivenIHaveConfiguredAConnectionString()
        {
            _userContext.Test_Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _dataContext = new DataContext(_userContext.Test_Configuration);
        }

        [Given(@"I prepare a RegisterModel parameter")]
        public void GivenIPrepareARegisterModelParameter()
        {
            _userContext.Test_RegisterModel = new RegisterModel();
        }

        [Given(@"the prepared RegisterModel parameter has a ""(.*)"" parameter whose value is ""(.*)""")]
        public void GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs(string parameterName, string parameterValue)
        {
            PropertyHelper.SetProperty(_userContext.Test_RegisterModel, parameterName, parameterValue);
        }

        [When(@"I call the UserService\.Create method with the prepared parameters")]
        public void WhenICallTheUserService_CreateMethodWithThePreparedParameters()
        {
            _userContext.Test_UserService = new Services.UserService(_dataContext);
            _userContext.Test_CreateResponse = _userContext.Test_UserService.Create(_userContext.Test_RegisterModel);
        }

        [Then(@"a CreateResponse object is returned")]
        public void ThenACreateResponseObjectIsReturned()
        {
            Assert.IsInstanceOf(typeof(CreateResponse), _userContext.Test_CreateResponse);
        }

        [Then(@"the CreateResponse object has a User object parameter")]
        public void ThenTheCreateResponseObjectHasAUserObjectParameter()
        {
            Assert.IsTrue(PropertyHelper.HasProperty(_userContext.Test_CreateResponse, "User", UserModel));
        }

    }
}
