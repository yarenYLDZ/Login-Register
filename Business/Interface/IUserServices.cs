using DataAccess.DTOS;
using DataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Response.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserServices
    {
        Task<Response<User>> Register(DtoClass dto);
        Task<string> Login(loginDto Ldto);
        Task<string> ForgetPassword(EmailDto emailDto);
        Task<string> UpdatePassword(string token , DtoConfirmationcode dtoConfirmationcode);
        
    }
}
