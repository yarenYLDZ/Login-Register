using Business.Interface;
using DataAccess.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RegisterDeneme2.Controllers
{
     
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IUserServices _dataServices;

        public ValuesController(IUserServices dataServices)
        {
            _dataServices = dataServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(DtoClass dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _dataServices.Register(dto);
                if (!result.IsSuccess)


                    return BadRequest(result);
            }

            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login(loginDto Ldto)
        {
          var token =  await _dataServices.Login(Ldto);
            return Ok(token);
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult>ForgetPasswordVerification(EmailDto emailDto)
        {


           var verı= await _dataServices.ForgetPassword(emailDto);
            return Ok(verı);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePassword(DtoConfirmationcode dtoConfirmationcode)
        {
            var veri= User.FindFirst(ClaimTypes.Email)?.Value;
            var newpass = await _dataServices.UpdatePassword(veri,dtoConfirmationcode);
            return Ok(newpass); 
        }
    }
}
