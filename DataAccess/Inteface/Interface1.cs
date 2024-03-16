using DataAccess.DTOS;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Inteface
{
     public interface Interface1
    {
        Task<User> Post(DtoClass dto);
        Task<User> Post2(DtoClass dto);
    }
}
