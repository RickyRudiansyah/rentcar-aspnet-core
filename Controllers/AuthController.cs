using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using RentCar.ViewModels;

namespace RentCar.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("CustomerId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine($"Login attempt - Email: {model.Email}, Password: {model.Password}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState tidak valid");
                return View(model);
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == model.Email);

            if (customer == null)
            {
                Console.WriteLine("Customer tidak ditemukan");
                ModelState.AddModelError("", "Email belum terdaftar");
                return View(model);
            }

            Console.WriteLine($"Customer ditemukan: {customer.Name}");
            Console.WriteLine($"Password di DB: {customer.Password}");

            if (customer.Password != model.Password)
            {
                Console.WriteLine("Password tidak cocok");
                ModelState.AddModelError("", "Password salah");
                return View(model);
            }

            HttpContext.Session.SetString("CustomerId", customer.CustomerId);
            HttpContext.Session.SetString("CustomerName", customer.Name);
            HttpContext.Session.SetString("CustomerEmail", customer.Email);

            Console.WriteLine("Login berhasil, redirect ke Home");

            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("CustomerId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == model.Email);

            if (existingCustomer != null)
            {
                ModelState.AddModelError("Email", "Email sudah terdaftar");
                return View(model);
            }

            var customer = new MsCustomer
            {
                CustomerId = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registrasi berhasil! Silakan login.";

            return RedirectToAction("Login");
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}