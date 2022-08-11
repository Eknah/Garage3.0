using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Core;
using Garage3._0.Data;
using Garage3._0.Web.Models.ViewModels;

namespace Garage3._0.Web.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly GarageContext _context;

        public MembershipsController(GarageContext context)
        {
            _context = context;
        }

        // GET: Memberships
        public async Task<IActionResult> Overview()
        {
			return _context.Membership != null ?
						View(await _context.Membership.Select(m => new MembershipsOverviewViewModel() { MembershipId = m.Id, FullName = $"{m.Name} {m.LastName}", NumRegVehicles = m.Vehicles.Count, MembershipType = m.Pro ? "Pro" : "Regular"}).ToListAsync()) :
						Problem("Entity set 'GarageContext.Membership'  is null.");
		}

		public async Task<IActionResult> Filter(string fullName)
		{
			if (string.IsNullOrWhiteSpace(fullName))
				return View(nameof(Overview));

			var allMemberViewModels = await _context.Membership.Select(m => new MembershipsOverviewViewModel() { MembershipId = m.Id, FullName = $"{m.Name} {m.LastName}", NumRegVehicles = m.Vehicles.Count, MembershipType = m.Pro ? "Pro" : "Regular" }).ToListAsync();

			var filteredMemberViewModels = allMemberViewModels.Where(m => m.FullName.StartsWith(fullName));


			return View(nameof(Overview), filteredMemberViewModels);
		}

		// GET: Memberships/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // GET: Memberships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonNumber,Name,LastName,Email,RegistrationDate,Pro")] Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Overview));
            }
            return View(membership);
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonNumber,Name,LastName,Email,RegistrationDate,Pro")] Membership membership)
        {
            if (id != membership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Overview));
            }
            return View(membership);
        }

        // GET: Memberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membership == null)
            {
                return Problem("Entity set 'GarageContext.Membership'  is null.");
            }
            var membership = await _context.Membership.FindAsync(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Overview));
        }

        private bool MembershipExists(int id)
        {
          return (_context.Membership?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
