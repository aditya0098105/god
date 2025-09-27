using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EarthDrive.Data;
using EarthDrive.Models;

namespace EarthDrive.Controllers
{
    public class HiresController : Controller
    {
        private readonly EarthDriveContext _context;

        public HiresController(EarthDriveContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var earthDriveContext = _context.Hires.Include(h => h.Car);
            return View(await earthDriveContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hire = await _context.Hires
                .Include(h => h.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hire == null)
            {
                return NotFound();
            }

            return View(hire);
        }

        
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Model");

            
            ViewData["CarRates"] = _context.Cars.ToDictionary(c => c.Id, c => c.RatePerDay);

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,CustomerName,PickupAddress,DropoffAddress,HireDate,ReturnDate,TotalAmount")] Hire hire)
        {
            if (ModelState.IsValid)
            {
                // Auto-calculate TotalAmount (backend safety)
                var car = await _context.Cars.FindAsync(hire.CarId);
                if (car != null)
                {
                    var days = (hire.ReturnDate - hire.HireDate).Days;
                    if (days < 1) days = 1; // at least 1 day charge
                    hire.TotalAmount = days * car.RatePerDay;
                }

                _context.Add(hire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Model", hire.CarId);
            ViewData["CarRates"] = _context.Cars.ToDictionary(c => c.Id, c => c.RatePerDay);
            return View(hire);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hire = await _context.Hires.FindAsync(id);
            if (hire == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Model", hire.CarId);
            return View(hire);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,CustomerName,PickupAddress,DropoffAddress,HireDate,ReturnDate,TotalAmount")] Hire hire)
        {
            if (id != hire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var car = await _context.Cars.FindAsync(hire.CarId);
                    if (car != null)
                    {
                        var days = (hire.ReturnDate - hire.HireDate).Days;
                        if (days < 1) days = 1;
                        hire.TotalAmount = days * car.RatePerDay;
                    }

                    _context.Update(hire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HireExists(hire.Id))
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

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Model", hire.CarId);
            return View(hire);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hire = await _context.Hires
                .Include(h => h.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hire == null)
            {
                return NotFound();
            }

            return View(hire);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hire = await _context.Hires.FindAsync(id);
            if (hire != null)
            {
                _context.Hires.Remove(hire);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HireExists(int id)
        {
            return _context.Hires.Any(e => e.Id == id);
        }
    }
}
