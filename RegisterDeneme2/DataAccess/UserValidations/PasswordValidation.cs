using DataAccess.DTOS;
using DataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UserValidations
{
    public class PasswordValidation : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
           
                List<IdentityError> errors = new List<IdentityError>();
                if (password.Length < 5) 
                    errors.Add(new IdentityError { Code = "PasswordLength", Description = "Lütfen şifreyi en az 5 karakter giriniz." });
                if (password.ToLower().Contains(user.UserName.ToLower())) 
                    errors.Add(new IdentityError { Code = "PasswordContainsUserName", Description = "Lütfen şifre içerisinde kullanıcı adını yazmayınız." });

                if (!errors.Any())
                    return Task.FromResult(IdentityResult.Success);
                else
                    return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            
        }

      
    }
}
