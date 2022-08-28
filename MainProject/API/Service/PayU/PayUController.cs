using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.API.PayU
{
    public class PayUController
    {
        private static readonly HttpClient client = new HttpClient();
        private static string OAuthToken; 
        private async void GetOAuthToken()
        {
            var values = new Dictionary<string, string>
            {
              { "grant_type", "client_credentials" },
              { "client_id", "434989" },
              { "client_secret", "b9b3a65563b938b038504a08498bba3f" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://secure.snd.payu.com/pl/standard/user/oauth/authorize", content);
            var stringContent = await response.Content.ReadAsStreamAsync();
            var jsonContent = await JsonSerializer.DeserializeAsync<OAuthTemplate>(stringContent);
            OAuthToken = jsonContent.access_token;
        }
        [HttpPost]
        public void SendOrder()
        {

            string values = @"{
        ""notifyUrl"": ""https://your.eshop.com/notify"",
        ""customerIp"": ""127.0.0.1"",
        ""merchantPosId"": ""434989"",
        ""description"": ""Cake shop"",
        ""currencyCode"": ""PLN"",
        ""totalAmount"": ""21000"",
        ""buyer"": {
            ""email"": ""john.doe@example.com"",
            ""phone"": ""654111654"",
            ""firstName"": ""John"",
            ""lastName"": ""Doe"",
            ""language"": ""pl""
        },
        ""products"": [
            {
                ""name"": ""Wireless Mouse for Laptop"",
                ""unitPrice"": ""15000"",
                ""quantity"": ""1""
            },
            {
                ""name"": ""HDMI cable"",
                ""unitPrice"": ""6000"",
                ""quantity"": ""1""
            }
        ]
    }";
        }
    }
}
