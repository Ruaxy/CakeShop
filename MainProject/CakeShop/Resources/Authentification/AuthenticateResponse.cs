using System.Text.Json.Serialization;
using CakeShop.Models;

namespace API.Authentification
{
        public class AuthenticateResponse
        {
            public long Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string JwtToken { get; set; }

            [JsonIgnore] // refresh token is returned in http only cookie
            public string RefreshToken { get; set; }

            public AuthenticateResponse(UserModel user, string jwtToken, string refreshToken)
            {
                Id = user.Id;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Username = user.Username;
                JwtToken = jwtToken;
                RefreshToken = refreshToken;
            }
        }
}
