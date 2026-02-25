using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Model validation using ValidationContext and throws ArugmentException in case of any validation error
        /// </summary>
        /// <param name="obj">Model object to validate</param>
        /// <exception cref="ArgumentException">When one or more validation errors found</exception>
        internal static void ModelValidation(object obj)
        {
            //Model validations
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            //validate the model object and get errors
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }            
        }
    }
}
