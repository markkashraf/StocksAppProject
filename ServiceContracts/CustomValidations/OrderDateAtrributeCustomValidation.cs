using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CustomValidations
{
    public class OrderDateAtrributeCustomValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? val = (DateTime?) value;


            if (val is not null && val > Convert.ToDateTime("2000-01-01"))
                return ValidationResult.Success;
            else
                return new ValidationResult("Date must be newer than 2000-01-01.");
        }

    }
}
