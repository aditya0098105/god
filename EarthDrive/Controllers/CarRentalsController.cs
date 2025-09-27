using System;
using System.Linq;
using System.Threading.Tasks;
using EarthDrive.Data;
using EarthDrive.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EarthDrive.Controllers
{
    public class CarRentalsController : Controller
    {
        private readonly EarthDriveContext _context;

        public CarRentalsController(EarthDriveContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars.Where(car => car.Available), "Id", "Model");

            var today = DateTime.Today;
            return View(new CarRental
            {
                PickupDate = today,
                ReturnDate = today.AddDays(1)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,CustomerName,CustomerEmail,CustomerPhone,PickupLocation,DropoffLocation,PickupDate,ReturnDate")] CarRental carRental)
        {
            var selectedCar = await _context.Cars.FindAsync(carRental.CarId);
            if (selectedCar == null)
            {
                ModelState.AddModelError("CarId", "Selected car could not be found.");
            }

            if (ModelState.IsValid)
            {
                if (selectedCar != null)
                {
                    var rentalDays = Math.Max(1, (carRental.ReturnDate - carRental.PickupDate).Days);
                    carRental.EstimatedCost = rentalDays * selectedCar.RatePerDay;
                }

                _context.Add(carRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), new { id = carRental.Id });
            }

            ViewData["CarId"] = new SelectList(_context.Cars.Where(car => car.Available), "Id", "Model", carRental.CarId);
            return View(carRental);
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var carRental = await _context.CarRentals
                .Include(rental => rental.Car)
                .FirstOrDefaultAsync(rental => rental.Id == id);

            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }
    }
}
