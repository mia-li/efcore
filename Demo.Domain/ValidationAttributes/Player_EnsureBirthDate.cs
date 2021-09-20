using Demo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class Player_EnsureBirthDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var player = (Player)validationContext.ObjectInstance;
            //其实可以用来检查所有的属性，不仅限于birthdate
            if(player!=null&&!string.IsNullOrWhiteSpace(player.Name))
            {
                
                if (!player.ValidateDateOfBirth())  //调player对象中validation的方法
                    return new ValidationResult("please provide a proper Birthdate");
            }
            return ValidationResult.Success;
        }
    }
}
