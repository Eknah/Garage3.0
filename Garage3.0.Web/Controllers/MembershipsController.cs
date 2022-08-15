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
    public class MembershipsController : Controller
    {
        private readonly GarageContext _context;

        public MembershipsController(GarageContext context)
        {
            _context = context;
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            return _context.Membership != null ? View(await _context.Membership.ToListAsync()) : Problem("Entity set 'GarageContext.Membership'  is null.");
           
        }

        public IActionResult CheckMember()
        {
            return View();
        }

        
       public IActionResult SearchMember(Membership ms)
        {
            if (_context.Membership?.FirstOrDefault(v => v.PersonNumber == ms.PersonNumber) == null && ms.PersonNumber.Length == 12)
            {
                return View("Create");
            }
            else if (_context.Membership?.FirstOrDefault(v => v.PersonNumber == ms.PersonNumber) != null && ms.PersonNumber.Length == 12)
            {
                //var membership = await _context.Membership.FindAsync(ms.Id);
                var membership = _context.Membership
                    .Where(v => v.PersonNumber == ms.PersonNumber);
                //.ToList();
                string FName="" , LName="",Id="";
                foreach (var name in membership)
                {
                    FName = name.Name;
                    LName = name.LastName;
                    Id = name.Id.ToString();

                }
                TempData["Message"] = "WELCOME " + FName.ToUpper()+ " " + LName.ToUpper();
                TempData["PersonId"] = Id;
                return View("CheckMember");
            }
            else
            {
                return View("CheckMember");
            }
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
                if (_context.Membership?.FirstOrDefault(v => v.PersonNumber == membership.PersonNumber.ToUpper()) != null)
                {
                    ViewBag.LicenseNumberExists = true;
                    return View(membership);
                }
                else
                {
                    membership.RegistrationDate = DateTime.Now;
                    membership.PersonNumber = membership.PersonNumber.ToUpper();
                    _context.Add(membership);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = membership.Name.ToUpper()+" "+membership.LastName + " has been added as member.";
                    //return RedirectToAction(nameof(Index));
                }
            }
            return View(membership);

            //if (ModelState.IsValid)
            //{
            //    _context.Add(membership);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(membership);
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(int id)
        {
          return (_context.Membership?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
