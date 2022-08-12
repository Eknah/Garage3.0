using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Core;
using Garage3._0.Data;

namespace Garage3._0.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly GarageContext _context;

        public VehiclesController(GarageContext context)
        {
            _context = context;
        }
        public IActionResult RegisterVehicle()
        {
            return View();
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garageContext = _context.Vehicle.Include(v => v.VehicleType);
            return View(await garageContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        //public IActionResult Create()
        //{
        //    ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id");
        //    return View();
        //}

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            var member  = await _context.Membership.FirstOrDefaultAsync();
            vehicle.MembershipId = member.Id;

            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeId);
            return View(nameof(RegisterVehicle), vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MembershipId,VehicleTypeId,Fuel,Wheels,Brand,RegistrationNumber,Colour")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string registrationNumber)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'GarageContext.Vehicle'  is null.");
            }
			//var vehicle = await _context.Vehicle.FindAsync(123);
			var vehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.RegistrationNumber == registrationNumber);
			if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return (_context.Vehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
