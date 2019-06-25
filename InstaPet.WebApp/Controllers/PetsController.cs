using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstaPet.DomainModel.Entities;
using InstaPet.Infrastructure.DataAccess.Contexts;
using InstaPet.DomainService.Interfaces;
using InstaPet.Infrastructure.BlobStorage;
using InstaPet.DomainModel.ValueObjects;

namespace InstaPet.WebApp.Controllers
{
    public class PetsController : Controller
    {
        private readonly InstaPetDbContext _context;
        private readonly IBlobStorage _blobStorage;

        public PetsController()
        {
            _context = new InstaPetDbContext();
            _blobStorage = new AzureBlobService();
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var result = await _context.Pets.ToListAsync();

            return View(result);
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewBag.Profiles = _context.Profiles.Select(p => new { p.Id, p.Name }).ToList();
            ViewBag.Species = _context.Species.Select(s => new { s.Specie.Name }).ToList();

            return View();
        }

        // POST: Pets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhotoUrl,Breed,Id")] Pet pet, Guid owner, string specie, string breed)
        {
            if (ModelState.IsValid)
            {
                pet.Id = Guid.NewGuid();
                pet.Owner = _context.Profiles.Find(owner);
                pet.Breed = new Breed(specie, breed);

                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    pet.PhotoUrl = _blobStorage.UploadFile(
                        file.FileName, file.OpenReadStream(),
                        "pets",
                        file.ContentType);
                }

                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Owner)
                .Where(p => p.Id.Equals(id))
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                return NotFound();
            }

            ViewBag.Profiles = _context.Profiles.Select(p => new { p.Id, p.Name }).ToList();
            ViewBag.Species = _context.Species.Select(s => new { s.Specie.Name }).ToList();

            return View(pet);
        }

        // POST: Pets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,PhotoUrl,Breed,Id")] Pet pet, 
            Guid owner, string specie, string breed)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pet.Owner = _context.Profiles.Find(owner);
                    pet.Breed = new Breed(specie, breed);

                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        pet.PhotoUrl = _blobStorage.UploadFile(
                            file.FileName, file.OpenReadStream(),
                            "pets",
                            file.ContentType);
                    }

                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
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
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(Guid id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}
