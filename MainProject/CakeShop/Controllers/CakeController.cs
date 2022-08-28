using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CakeController : Controller
    {
        private readonly CakesAPPContext _context;
        IAuthorizationService _authorizationService;
        public CakeController(CakesAPPContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index(string ConfirmMessage)
        {
            var cakeswithtypes = await (from cake in _context.Cakes
                                        join type in _context.Types
                                        on cake.TypeId equals type.Id
                                        select new CakeWithTypeModel
                                        {
                                            Id = cake.Id,
                                            Name = cake.Name,
                                            Description = cake.Description,
                                            Size = cake.Size,
                                            Price = cake.Price,
                                            TypeName = type.Name,
                                            OwnerSpecification = cake.OwnerSpecification,
                                            ImageUrl = cake.ImageUrl
                                        }
                                  ).ToListAsync();
            ViewBag.ConfirmMessage = ConfirmMessage;
            return View(cakeswithtypes);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeModel = await _context.Cakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cakeModel == null)
            {
                return NotFound();
            }

            return View(cakeModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Size,Price,TypeId,ImageUrl")] CakeModel cakeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cakeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cakeModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cakeModel = await _context.Cakes.FindAsync(id);
            if (cakeModel == null)
            {
                return NotFound();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, cakeModel, new EditRes());
            if (!authorizationResult.Succeeded)
            {
                return View(cakeModel);
            }
            else
            {
                return NotFound();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description,Size,Price,TypeId")] CakeModel cakeModel)
        {
            if (id != cakeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cakeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeModelExists(cakeModel.Id))
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
            return View(cakeModel);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeModel = await _context.Cakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cakeModel == null)
            {
                return NotFound();
            }

            return View(cakeModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cakeModel = await _context.Cakes.FindAsync(id);
            _context.Cakes.Remove(cakeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeModelExists(long id)
        {
            return _context.Cakes.Any(e => e.Id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(long Id)
        {
            var cakeModel = await _context.Cakes.FindAsync(Id);
            string ConfirmMessage =  cakeModel.Name + " zostało dodane do koszyka.";
            CartModel cartitem = new CartModel();
            cartitem.CakeId = cakeModel.Id;
            cartitem.UserIdentification = User.Identity.Name;
            if (cartitem.UserIdentification is null)
            {
                cartitem.UserIdentification = "useradmin";
            }
            await _context.Carts.AddAsync(cartitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { ConfirmMessage });
        }
    }
}
