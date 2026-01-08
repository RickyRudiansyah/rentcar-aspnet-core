using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using RentCar.ViewModels;
using System.Diagnostics;

namespace RentCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(CarSearchViewModel search)
        {
            var viewModel = new HomeViewModel
            {
                Search = search,
                AvailableYears = await _context.Cars
                    .Select(c => c.Year)
                    .Distinct()
                    .OrderByDescending(y => y)
                    .ToListAsync()
            };

            // Jika user sudah melakukan pencarian
            if (search.PickupDate.HasValue && search.ReturnDate.HasValue)
            {
                viewModel.HasSearched = true;

                // Query mobil yang tersedia status = true
                var query = _context.Cars
                    .Include(c => c.CarImages)
                    .Where(c => c.Status == true);

                // Filter berdasarkan tahun jika dipilih
                if (search.YearFilter.HasValue && search.YearFilter.Value > 0)
                {
                    query = query.Where(c => c.Year == search.YearFilter.Value);
                }

                // Cek mobil yang tidak sedang disewa pada periode tersebut
                var rentedCarIds = await _context.Rentals
                    .Where(r => 
                        (search.PickupDate <= r.ReturnDate && search.ReturnDate >= r.RentalDate))
                    .Select(r => r.CarId)
                    .ToListAsync();

                query = query.Where(c => !rentedCarIds.Contains(c.CarId));

                // Sorting
                query = search.SortBy switch
                {
                    "price_asc" => query.OrderBy(c => c.PricePerDay),
                    "price_desc" => query.OrderByDescending(c => c.PricePerDay),
                    "year_asc" => query.OrderBy(c => c.Year),
                    "year_desc" => query.OrderByDescending(c => c.Year),
                    _ => query.OrderBy(c => c.PricePerDay) // Default: harga terendah
                };

                // Pagination
                var totalCars = await query.CountAsync();
                viewModel.TotalPages = (int)Math.Ceiling(totalCars / (double)search.PageSize);
                viewModel.CurrentPage = search.Page;

                viewModel.Cars = await query
                    .Skip((search.Page - 1) * search.PageSize)
                    .Take(search.PageSize)
                    .ToListAsync();
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}