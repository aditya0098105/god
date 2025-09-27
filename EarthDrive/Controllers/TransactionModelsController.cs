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
    public class TransactionModelsController : Controller
    {
        private readonly EarthDriveContext _context;

        public TransactionModelsController(EarthDriveContext context)
        {
            _context = context;
        }

        // GET: TransactionModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionModel.ToListAsync());
        }

        // GET: TransactionModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.TransactionModel
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // GET: TransactionModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,CustomerID,TransactionDate,CarID,CarPlateNumber,DurationOfRental,PickupLocation,PickUpTime,DropoffLocation,ReturnTime,VehicalType,DailyRate,LateFees,TotalDue,TransactionStatus,ConfirmationStatus,ExtraNotes")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionModel);
        }

        // GET: TransactionModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.TransactionModel.FindAsync(id);
            if (transactionModel == null)
            {
                return NotFound();
            }
            return View(transactionModel);
        }

        // POST: TransactionModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionID,CustomerID,TransactionDate,CarID,CarPlateNumber,DurationOfRental,PickupLocation,PickUpTime,DropoffLocation,ReturnTime,VehicalType,DailyRate,LateFees,TotalDue,TransactionStatus,ConfirmationStatus,ExtraNotes")] TransactionModel transactionModel)
        {
            if (id != transactionModel.TransactionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionModelExists(transactionModel.TransactionID))
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
            return View(transactionModel);
        }

        // GET: TransactionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.TransactionModel
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: TransactionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.TransactionModel.FindAsync(id);
            if (transactionModel != null)
            {
                _context.TransactionModel.Remove(transactionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionModelExists(int id)
        {
            return _context.TransactionModel.Any(e => e.TransactionID == id);
        }
    }
}
