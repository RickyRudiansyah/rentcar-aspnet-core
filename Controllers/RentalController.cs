using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Rental/Create
        public async Task<IActionResult> Create(string carId, DateTime pickupDate, DateTime returnDate)
        {
            // Cek apakah user sudah login
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Ambil data mobil
            var car = await _context.Cars
                .Include(c => c.CarImages)
                .FirstOrDefaultAsync(c => c.CarId == carId);

            if (car == null)
            {
                return NotFound();
            }

            // Ambil data customer
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            // Hitung total hari dan harga
            var totalDays = (returnDate - pickupDate).Days;
            if (totalDays <= 0) totalDays = 1;

            var totalPrice = totalDays * car.PricePerDay;

            ViewBag.Car = car;
            ViewBag.Customer = customer;
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;
            ViewBag.TotalDays = totalDays;
            ViewBag.TotalPrice = totalPrice;

            return View();
        }

        // POST: /Rental/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string carId, DateTime pickupDate, DateTime returnDate, string confirm)
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null)
            {
                return NotFound();
            }

            // Hitung total
            var totalDays = (returnDate - pickupDate).Days;
            if (totalDays <= 0) totalDays = 1;
            var totalPrice = totalDays * car.PricePerDay;

            // Buat rental baru
            var rental = new TrRental
            {
                RentalId = Guid.NewGuid().ToString(),
                RentalDate = pickupDate,
                ReturnDate = returnDate,
                TotalPrice = totalPrice,
                PaymentStatus = false,
                CustomerId = customerId,
                CarId = carId
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Penyewaan berhasil! Silakan lakukan pembayaran.";

            return RedirectToAction("History");
        }

        // GET: /Rental/History
        public async Task<IActionResult> History()
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.RentalDate)
                .ToListAsync();

            return View(rentals);
        }

        // POST: /Rental/Pay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(string rentalId)
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var rental = await _context.Rentals
                .FirstOrDefaultAsync(r => r.RentalId == rentalId && r.CustomerId == customerId);

            if (rental == null)
            {
                return NotFound();
            }

            // Update status pembayaran
            rental.PaymentStatus = true;

            // Buat record pembayaran
            var payment = new LtPayment
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.Now,
                Amount = rental.TotalPrice,
                PaymentMethod = "Transfer Bank",
                RentalId = rental.RentalId
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Pembayaran berhasil!";

            return RedirectToAction("History");
        }
    }
}