﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSeguridadConJWT.Models
{
    public class UsuarioLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}