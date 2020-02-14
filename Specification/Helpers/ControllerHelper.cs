using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DevItUp.Grain.API.Specification.Helpers
{
    public static class ControllerHelper
    {
        public static void SimulateValidation(object model, ControllerBase controller)
        {
            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var validator = new DataAnnotationsValidator.DataAnnotationsValidator();
            validator.TryValidateObject(model, validationResults);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }


        }
    }
}
