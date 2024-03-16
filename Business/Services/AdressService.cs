using Business.Interface;
using DataAccess.Context;
using DataAccess.DTOS;
using DataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AdressService : IAdress
    {
        private readonly ContextClass _dbContext;

        public AdressService (ContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task<Address> GetAllAdress(Address address)
        {
            var adres = await _dbContext.adresses.ToListAsync();
            return address;
        }

        public async Task<Address> CreateAdress(Address address)
        {
            var adres = new Address
            {
                Sehir = address.Sehir,
                Ilce = address.Ilce,
                Mahalle = address.Mahalle,
                Cadde = address.Cadde,
                Sokak = address.Sokak,
                Apartman = address.Apartman,
                Kat_Daire = address.Kat_Daire,
            };
            return adres;
         
        }
    }
}
