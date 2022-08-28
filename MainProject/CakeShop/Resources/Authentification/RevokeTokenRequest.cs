using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Authentification
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }
    }
}
