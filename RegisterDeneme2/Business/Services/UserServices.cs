using Business.Interface;
using DataAccess.Concrete;
using DataAccess.DTOS;
using DataAccess.Entity;
using DataAccess.Security;
using Microsoft.AspNetCore.Identity;
using Response.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Business.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IJWT _jwt;
        private readonly UserManager<User> _userAdress;
        
        public UserServices(UserManager<User> userManager, IJWT jwt)

        {
            _userManager = userManager;
            _jwt = jwt;

         }
        public async Task<Response<User>> Register(DtoClass dto)
        {
            User datas = new User
            {
                UserName = dto.UserName,
                Email = dto.Email

            };
           IdentityResult result = await _userManager.CreateAsync(datas, dto.Password);
           if (result.Succeeded)
                return Response<User>.Success(datas);
            return Response<User>.Fail(result.Errors.Select(e => e.Description));

            


        }

        public async Task<string> Login(loginDto Ldto)
        {
            TokenM tokenm = new TokenM();
            User UserName = await _userManager.FindByEmailAsync(Ldto.Email);
            var userPassword = await _userManager.CheckPasswordAsync(UserName, Ldto.Password);

            if (UserName != null && userPassword == true)
            {
                tokenm.token = _jwt.CreateToken(UserName);
                var token = tokenm.token;
                return token;
            }
            else
            {
                throw new Exception("Boyle bir kayıt bulunamamaktadır."); 
            }

        }

       public async Task<string> ForgetPassword(EmailDto emailDto)
       {
            Random random = new Random();
            string rndm = random.Next(1000, 9999).ToString();
            User user = await _userManager.FindByEmailAsync(emailDto.Email);
          
            if (user != null)
            {
                    user.conformationCode = rndm;
                    await _userManager.UpdateAsync(user);
                    var jwt = _jwt.CreateToken(user);
                    var body = jwt + " " + rndm;
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("yarenyldz654@gmail.com");
                    mail.To.Add(emailDto.Email);
                    mail.Subject = "Şifre Güncelleme Talebi";
                    mail.Body= body;    


                    SmtpClient smp = new SmtpClient();
                    smp.Host = "smtp.gmail.com";
                    smp.UseDefaultCredentials = false; 
                    smp.Port = 587;
                    smp.Credentials = new NetworkCredential("yarenyldz654@gmail.com", "ifql tcpo fswp cygm");
                    smp.EnableSsl = true;
                    smp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smp.SendMailAsync(mail);
                    await _userManager.UpdateAsync(user);
                    return jwt;
            

            }

            throw new Exception();
       }

        public async Task<string> UpdatePassword(string token, DtoConfirmationcode dtoConfirmationcode)
        {
            User user = await _userManager.FindByEmailAsync(token);
            if (user != null)
            {
                if (user.conformationCode == dtoConfirmationcode.DConfirmationcode)
                {
                    var removePasswordResult = await _userManager.RemovePasswordAsync(user);

                    var addPasswordResult = await _userManager.AddPasswordAsync(user, dtoConfirmationcode.NewPassword);

                        if (addPasswordResult.Succeeded)
                        {
                            await _userManager.UpdateAsync(user);
                            throw new Exception("başarılı");
                        }
                }   

            }
           throw new Exception("hatalı");
            
        }
    }


}
