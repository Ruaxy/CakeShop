using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class UserModel
    {
            public long Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            [JsonIgnore]
            public string Password { get; set; }
            [JsonIgnore]
            public List<Token> RefreshTokens { get; set; }
    }
}