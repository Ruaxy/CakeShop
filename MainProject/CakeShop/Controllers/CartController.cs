using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CartController : Controller
    {
        private readonly CakesAPPContext _context;

        public CartController(CakesAPPContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cartwithcake = await (from cart in _context.Carts
                                      join cake in _context.Cakes
                                      on cart.CakeId equals cake.Id
                                      where cart.UserIdentification == User.Identity.Name
                                      select new CartCakeNameModel
                                      {
                                          Id = cart.Id,
                                          CakeId = cake.Id,
                                          UserIdentification = User.Identity.Name,
                                          Name = cake.Name,
                                          Description = cake.Description,
                                          Size = cake.Size,
                                          Price = cake.Price,
                                          ImageUrl = cake.ImageUrl
                                      }
                                              ).ToListAsync();
            return View(cartwithcake);
        }

        public IActionResult DetailsAsync(bool isPaymentActive, int TotalPrice)
        {

            string temp = String.Format("continueUrl={0}&" +
                "currencyCode={1}&customerIp={2}&" +
                "description={3}&" +
                "merchantPosId={4}&notifyUrl={5}&" +
                "products[0].name=ciasto&products[0].quantity=1&" +
                "products[0].unitPrice=1000&" +
                "totalAmount={6}&2fa2abf55266e4e8b723293fc819b322", "https%3A%2F%2Flocalhost%3A44313%2FCart%2FFinishedPayment", "PLN", "123.123.123.123",
                "Opis+zam%C3%B3wienia", "434989", "https%3A%2F%2Flocalhost%3A44313%2F", TotalPrice.ToString());
            ViewBag.IsPaymentActive = isPaymentActive;
            ViewBag.hashToPayment = sha256_hash(temp);
            ViewBag.totalAmount = TotalPrice.ToString();

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CakeId,UserIdentification")] CartModel cartModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartModel);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartModel = await _context.Carts.FindAsync(id);
            if (cartModel == null)
            {
                return NotFound();
            }
            return View(cartModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CakeId,UserIdentification")] CartModel cartModel)
        {
            if (id != cartModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartModelExists(cartModel.Id))
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
            return View(cartModel);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartModel = await _context.Carts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartModel == null)
            {
                return NotFound();
            }

            return View(cartModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cartModel = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cartModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartModelExists(long id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
        public async Task<ActionResult> FinishedPayment()
        {
            var customerid = _context.Customers.FirstOrDefault(x => x.Email == User.Identity.Name).Id;

            await _context.Database.ExecuteSqlRawAsync("update [order] set IsPaid = 1 where customerid = " + customerid.ToString());
            await _context.SaveChangesAsync(); 
            return View();
        }
        public async Task<ActionResult> MakeOrder(string PhoneNumber, string Name, string Surname, string Email, DateTime DateOrder)
        {
            var customer = new CustomerModel
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                PhoneNumber = PhoneNumber
            };
            var isCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            if (isCustomer is null)
            {
                await _context.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                isCustomer.Email = customer.Email;
                isCustomer.Surname = customer.Surname;
                isCustomer.PhoneNumber = customer.PhoneNumber;
                await _context.SaveChangesAsync();
            }
            var customerId = _context.Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber).Id;
            var carts = await _context.Carts.Where(x => x.UserIdentification == User.Identity.Name).ToListAsync();
            List<decimal> temp2 = new List<decimal>();
            foreach (var i in carts)
            {
                temp2.Add(_context.Cakes.Where(x => x.Id == i.CakeId).Select(x => x.Price).FirstOrDefault());
            }
            var totalAmount = (int)temp2.Sum() * 100;
            long uniqueOrderId = 0;
            if (_context.Orders.FirstOrDefault(x => x.Id == 1) is null)
            {
                uniqueOrderId = 0;
            }
            else
            {
                uniqueOrderId = await _context.Orders.MaxAsync(x => x.Id);
            }
            uniqueOrderId += 1;

            if (!(carts is null))
            {
                foreach (var item in carts)
                {
                    var temp = new OrderModel
                    {
                        OrderId = uniqueOrderId,
                        CakeId = item.CakeId,
                        CustomerId = (long)customerId,
                        Quantity = 1,
                        OrderDate = DateOrder
                    };
                    try
                    {
                        await _context.Orders.AddAsync(temp);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return View(ex);
                    }
                }
                _context.Carts.RemoveRange(carts);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Details", new { isPaymentActive = true, TotalPrice = totalAmount.ToString() });
        }
        public string sha256_hash(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();


            }
        }
    }
}
