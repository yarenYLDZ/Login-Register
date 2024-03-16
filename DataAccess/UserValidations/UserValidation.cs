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
    public class UserValidation : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (int.TryParse(user.UserName[0].ToString(), out int _)) 
                errors.Add(new IdentityError { Code = "UserNameNumberStartWith", Description = "Kullanıcı adı sayısal ifadeyle başlayamaz..." });
            if (user.UserName.Length < 5 && user.UserName.Length > 15) 
                errors.Add(new IdentityError { Code = "UserNameLenhth", Description = "Kullanıcı adı 5 - 15 karakter arasında olmalıdır..." });
            if (user.Email.Length > 25) 
                errors.Add(new IdentityError { Code = "EmailLenhth", Description = "Email 25 karakterden fazla olamaz..." });
            if (!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }

    }
}
