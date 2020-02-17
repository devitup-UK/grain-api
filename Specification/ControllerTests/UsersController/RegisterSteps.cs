using AutoMapper;
using DevItUp.Grain.API;
using DevItUp.Grain.API.Controllers;
using DevItUp.Grain.API.Helpers;
using DevItUp.Grain.API.Interfaces;
using DevItUp.Grain.API.Models.Users;
using DevItUp.Grain.API.Services;
using DevItUp.Grain.API.Specification;
using DevItUp.Grain.API.Specification.Helpers;
using DevItUp.Grain.API.Specification.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DevItUp.Grain.API.Specification.ControllerTests.UsersController
{
    [Binding]
    class RegisterSteps
    {
        private readonly UserContext _userContext;

        public RegisterSteps(UserContext userContext)
        {
            _userContext = userContext;
        }

        [Given(@"I have created the endpoint ""(.*)""")]
        public void GivenIHaveCreatedTheEndpoint(string endpointRelativeUrl)
        {
            switch(endpointRelativeUrl)
            {
                case "/users/register":
                    // Build up the options.
                    IOptions<AppSettings> someOptions = Options.Create<AppSettings>(new AppSettings());

                    // Setup the user mock.
                    _userContext.Mock_UsersService = new MockUserService();

                    // Setup the mapper.
                    _userContext.Mock_Mapper = MappingProfile.InitializeAutoMapper().CreateMapper();

                    // Initialise the controller.
                    _userContext.Controller_Users = new Controllers.UsersController(_userContext.Mock_UsersService,
                        _userContext.Mock_Mapper,
                        someOptions);


                    break;
                default:
                    throw new NotImplementedException("The supplied endpoint is not defined in the testing steps.");
            }
        }


        [Given(@"I have prepared a ""(.*)"" request")]
        public void GivenIHavePreparedARequest(string requestType)
        {
            _userContext.Register_RequestData = new Model.RegisterRequestData();
            _userContext.Register_RequestData.RequestType = requestType;
        }

        [Given(@"the prepared request has a RegisterModel parameter")]
        public void GivenThePreparedRequestHasARegisterModelParameter()
        {
            _userContext.Register_RequestData.RequestObject = new RegisterModel();
        }

        [Given(@"the prepared RegisterModel parameter in the request has a ""(.*)"" parameter whose value is ""(.*)""")]
        public void GivenThePreparedRegisterModelParameterInTheRequestHasAParameterWhoseValueIs(string parameterName, string parameterValue)
        {
            PropertyHelper.SetProperty(_userContext.Register_RequestData.RequestObject, parameterName, parameterValue);
        }


        [When(@"I submit the prepared request to ""(.*)""")]
        public void WhenISubmitThePreparedRequestTo(string endpointRelativeUrl)
        {
            IActionResult theEndpointResponse = null;

            // Throw an error if the request data hasn't been prepared.
            if (_userContext.Register_RequestData == null)
            {
                throw new ArgumentNullException("Register_RequestData");
            }

            // Error if the HTTP method hasn't been set.
            if (String.IsNullOrEmpty(_userContext.Register_RequestData.RequestType))
            {
                throw new ArgumentOutOfRangeException("httpMethod");
            }

            switch (endpointRelativeUrl)
            {
                case "/users/register":
                    if(_userContext.Register_RequestData.RequestType.Equals("POST"))
                    {
                        ControllerHelper.SimulateValidation(_userContext.Register_RequestData.RequestObject, _userContext.Controller_Users);
                        theEndpointResponse = _userContext.Controller_Users.Register(_userContext.Register_RequestData.RequestObject);
                    }
                    break;
                default:
                    throw new NotImplementedException("The supplied endpoint is not defined in the testing steps.");
            }

            if (theEndpointResponse != null)
            {
                _userContext.TheEndpointResponse = theEndpointResponse;
            }
        }

        [Then(@"the endpoint will return a status code of ""(.*)""")]
        public void ThenTheEndpointWillReturnAStatusCodeOf(int statusCode)
        {
            object status;

            try
            {
                status = _userContext.TheEndpointResponse?.GetType().GetProperty("StatusCode")?.GetValue(_userContext.TheEndpointResponse);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("statusCode", "The returned ActionResult did not have a Status Code.");
            }

            if (status != null)
            {
                Assert.AreEqual(statusCode, (int)status);
            }
        }

        [Then(@"the endpoint will return a body of '(.*)'")]
        public void ThenTheEndpointWillReturnABodyOf(string expectedResponse)
        {
            var responseData = _userContext.TheEndpointResponse.GetType().GetProperty("Value")?.GetValue(_userContext.TheEndpointResponse);
            Assert.AreEqual(expectedResponse, JsonConvert.SerializeObject(responseData));
        }

        [Given(@"I have registered a user with the details \(FirstName: ""(.*)"", LastName: ""(.*)"", Username: ""(.*)"", Email Address: ""(.*)"", Password: ""(.*)""\)")]
        public void GivenIHaveRegisteredAUserWithTheDetailsFirstNameLastNameUsernameEmailAddressPassword(string firstName, string lastName, string username, string emailAddress, string password)
        {
            GivenIHavePreparedARequest("POST");
            GivenThePreparedRequestHasARegisterModelParameter();
            GivenThePreparedRegisterModelParameterInTheRequestHasAParameterWhoseValueIs("FirstName", firstName);
            GivenThePreparedRegisterModelParameterInTheRequestHasAParameterWhoseValueIs("LastName", lastName);
            GivenThePreparedRegisterModelParameterInTheRequestHasAParameterWhoseValueIs("Username", username);
            GivenThePreparedRegisterModelParameterInTheRequestHasAParameterWhoseValueIs("EmailAddress", emailAddress);
            GivenThePreparedRegisterModelParameterInTheRequestHasAParameterWhoseValueIs("Password", password);
            WhenISubmitThePreparedRequestTo("/users/register");
        }






    }
}
