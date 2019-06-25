using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstaPet.Infrastructure.DataAccess.Contexts;
using InstaPet.Infrastructure.DataAccess.Contexts.Models;
using InstaPet.DomainModel.ValueObjects;

namespace InstaPet.WebApp.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly InstaPetDbContext _context;

        public SpeciesController()
        {
            _context = new InstaPetDbContext();
        }

        // GET: Species
        public async Task<IActionResult> Index()
        {
            return View(await _context.Species.ToListAsync());
        }        

        // GET: Species/Create
        public IActionResult Create()
        {
            return View();
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Specie")] DbSpecie dbSpecie, string specie)
        {
            if (ModelState.IsValid)
            {
                dbSpecie.Id = Guid.NewGuid();
                dbSpecie.Specie = new Specie(specie);

                _context.Add(dbSpecie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbSpecie);
        }

        // GET: Species/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbSpecie = await _context.Species.FindAsync(id);
            if (dbSpecie == null)
            {
                return NotFound();
            }
            return View(dbSpecie);
        }

        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Specie")] DbSpecie dbSpecie, string specie)
        {
            if (id != dbSpecie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbSpecie.Specie = new Specie(specie);

                    _context.Update(dbSpecie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbSpecieExists(dbSpecie.Id))
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
            return View(dbSpecie);
        }

        // GET: Species/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbSpecie = await _context.Species
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbSpecie == null)
            {
                return NotFound();
            }

            return View(dbSpecie);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dbSpecie = await _context.Species.FindAsync(id);
            _context.Species.Remove(dbSpecie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbSpecieExists(Guid id)
        {
            return _context.Species.Any(e => e.Id == id);
        }
    }
}
