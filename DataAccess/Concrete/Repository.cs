using AutoMapper;
using DataAccess.Context;
using DataAccess.DTOS;
using DataAccess.Entity;
using DataAccess.Inteface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Repository : Interface1
    {
       
        public Task<User> Post(DtoClass dto)
        {
            throw new NotImplementedException();
        }

        

        public Task<User> Post2(DtoClass dto)
        {
            throw new NotImplementedException();
        }
    }
}
