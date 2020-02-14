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

namespace Grain.API.Specification.Authentication
{
    [Binding]
    class RegisterSteps
    {
        [Given(@"I have created the endpoint ""(.*)""")]
        public void GivenIHaveCreatedTheEndpoint(string endpointRelativeUrl)
        {
            switch(endpointRelativeUrl)
            {
                case "/users/register":
                    // Build up the options.
                    IOptions<AppSettings> someOptions = Options.Create<AppSettings>(new AppSettings());

                    // Setup the user mock.
                    ScenarioProperties.Mock_UsersService = new MockUserService();

                    // Setup the mapper.
                    ScenarioProperties.Mock_Mapper = MappingProfile.InitializeAutoMapper().CreateMapper();

                    // Initialise the controller.
                    ScenarioProperties.Controller_Users = new UsersController(ScenarioProperties.Mock_UsersService,
                        ScenarioProperties.Mock_Mapper,
                        someOptions);


                    break;
                default:
                    throw new NotImplementedException("The supplied endpoint is not defined in the testing steps.");
            }
        }


        [Given(@"I have prepared a ""(.*)"" request")]
        public void GivenIHavePreparedARequest(string requestType)
        {
            ScenarioProperties.Register_RequestData = new DevItUp.Grain.API.Specification.Model.RegisterRequestData();
            ScenarioProperties.Register_RequestData.RequestType = requestType;
        }

        [Given(@"the prepared request has a RegisterModel parameter")]
        public void GivenThePreparedRequestHasARegisterModelParameter()
        {
            ScenarioProperties.Register_RequestData.RequestObject = new RegisterModel();
        }

        [Given(@"the prepared RegisterModel parameter has a ""(.*)"" parameter whose value is ""(.*)""")]
        public void GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs(string parameterName, string parameterValue)
        {
            Type registerModel = typeof(RegisterModel);
            PropertyInfo myPropInfo = registerModel.GetProperty(parameterName);
            myPropInfo.SetValue(ScenarioProperties.Register_RequestData.RequestObject, parameterValue, null);
            
        }

        [When(@"I submit the prepared request to ""(.*)""")]
        public void WhenISubmitThePreparedRequestTo(string endpointRelativeUrl)
        {
            IActionResult theEndpointResponse = null;

            // Throw an error if the request data hasn't been prepared.
            if (ScenarioProperties.Register_RequestData == null)
            {
                throw new ArgumentNullException("Register_RequestData");
            }

            // Error if the HTTP method hasn't been set.
            if (String.IsNullOrEmpty(ScenarioProperties.Register_RequestData.RequestType))
            {
                throw new ArgumentOutOfRangeException("httpMethod");
            }

            switch (endpointRelativeUrl)
            {
                case "/users/register":
                    if(ScenarioProperties.Register_RequestData.RequestType.Equals("POST"))
                    {
                        ControllerHelper.SimulateValidation(ScenarioProperties.Register_RequestData.RequestObject, ScenarioProperties.Controller_Users);
                        theEndpointResponse = ScenarioProperties.Controller_Users.Register(ScenarioProperties.Register_RequestData.RequestObject);
                    }
                    break;
                default:
                    throw new NotImplementedException("The supplied endpoint is not defined in the testing steps.");
            }

            if (theEndpointResponse != null)
            {
                ScenarioProperties.TheEndpointResponse = theEndpointResponse;
            }
        }

        [Then(@"the endpoint will return a status code of ""(.*)""")]
        public void ThenTheEndpointWillReturnAStatusCodeOf(int statusCode)
        {
            object status;

            try
            {
                status = ScenarioProperties.TheEndpointResponse?.GetType().GetProperty("StatusCode")?.GetValue(ScenarioProperties.TheEndpointResponse);
            }
            catch (Exception ex)
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
            var responseData = ScenarioProperties.TheEndpointResponse.GetType().GetProperty("Value")?.GetValue(ScenarioProperties.TheEndpointResponse);
            Assert.AreEqual(expectedResponse, JsonConvert.SerializeObject(responseData));
        }

        [Given(@"I have registered a user with the details \(FirstName: ""(.*)"", LastName: ""(.*)"", Username: ""(.*)"", Email Address: ""(.*)"", Password: ""(.*)""\)")]
        public void GivenIHaveRegisteredAUserWithTheDetailsFirstNameLastNameUsernameEmailAddressPassword(string firstName, string lastName, string username, string emailAddress, string password)
        {
            GivenIHavePreparedARequest("POST");
            GivenThePreparedRequestHasARegisterModelParameter();
            GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs("FirstName", firstName);
            GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs("LastName", lastName);
            GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs("Username", username);
            GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs("EmailAddress", emailAddress);
            GivenThePreparedRegisterModelParameterHasAParameterWhoseValueIs("Password", password);
            WhenISubmitThePreparedRequestTo("/users/register");
        }






    }
}
