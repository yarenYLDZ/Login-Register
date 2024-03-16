using DataAccess.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class ContextClass : IdentityDbContext<User>
    {
        public ContextClass(DbContextOptions<ContextClass> options):base(options)
        {
            
        }
        public DbSet<Address>adresses { get; set; }


    }
}
