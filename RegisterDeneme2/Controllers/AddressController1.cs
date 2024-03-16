using Business.Interface;
using DataAccess.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegisterDeneme2.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AddressController1 : ControllerBase
    {
        private readonly IAdress _adress;

        public AddressController1(IAdress adress)
        {
            _adress = adress;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(Address address)
        {
            address.UserId=User.Claims.First(x=>x.Type=="UserId").Value;
            await _adress.CreateAdress(address);
            return Ok(address);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAdress(Address address)
        {
            address.UserId=User.Claims.First(y=>y.Type=="UserId").Value;
            await _adress.GetAllAdress(address);
            return Ok(address);
        }
    }
}
