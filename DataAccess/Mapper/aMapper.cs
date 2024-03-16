using AutoMapper;
using DataAccess.DTOS;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class aMapper : Profile
    {
        public aMapper()
        {
         CreateMap<User,DtoClass>().ReverseMap();
        }

    }
}
