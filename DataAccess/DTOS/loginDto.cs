﻿using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOS
{
    public class loginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
