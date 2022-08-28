using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static string OAuthToken;
        private readonly CakesAPPContext _context;

        public OrderController(CakesAPPContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,CakeId,CustomerId,Quantity,OrderDate")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                SendOrder();
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,OrderId,CakeId,CustomerId,Quantity,OrderDate")] OrderModel orderModel)
        {
            if (id != orderModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderModelExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

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
            var stringContent = await response.Content.ReadAsStringAsync();
        }
        public async void SendOrder()
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
                ""name"": ""Ciasta"",
                ""unitPrice"": ""10000"",
                ""quantity"": ""5""
            }
        ]
          }";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://secure.snd.payu.com/api/v2_1/orders");
            httpRequest.Content = new StringContent(values, Encoding.UTF8, "application/json");
            GetOAuthToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuthToken);

            var response = await client.SendAsync(httpRequest);
            var contentresponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(contentresponse);
        }
        public IActionResult Test()
        {
            return View();
        }
        
    }
}
