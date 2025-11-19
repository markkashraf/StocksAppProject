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


            if (val != null && val > Convert.ToDateTime("01-01-2000"))
                return ValidationResult.Success;
            else
                return new ValidationResult("Date must be newer than 2000-01-01.");
        }

    }
}
