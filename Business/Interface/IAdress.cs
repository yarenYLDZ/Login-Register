using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IAdress
    {
        Task<Address>CreateAdress(Address address);
        Task<Address>GetAllAdress(Address address);
    }
}
