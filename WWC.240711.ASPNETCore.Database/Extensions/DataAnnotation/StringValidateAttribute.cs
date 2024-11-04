using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Extensions.DataAnnotation
{
    /// <summary>
    /// Long 类型验证
    /// </summary>
    public class StringValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is long longValue && longValue == 0)
            {
                return new ValidationResult($"{validationContext.DisplayName} cannot be 0.");
            }

            return ValidationResult.Success;
        }
    }
}
