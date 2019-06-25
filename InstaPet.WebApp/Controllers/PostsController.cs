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

namespace InstaPet.WebApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly InstaPetDbContext _context;
        private readonly IBlobStorage _blobStorage;

        public PostsController()
        {
            _context = new InstaPetDbContext();
            _blobStorage = new AzureBlobService();
        }

        // GET: Posts/5
        [HttpGet]
        [Route("posts/{id}")]
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.Creator)
                .Where(m => m.Creator.Id == id)
                .ToListAsync();

            if (posts == null)
            {
                return NotFound();
            }

            ViewBag.Creator = await _context.Pets.FindAsync(id);

            return View(posts);
        }

        // GET: Posts/Create
        [HttpGet]
        [Route("posts/create/{creatorId}")]
        public IActionResult Create(Guid? creatorId)
        {
            ViewBag.Creator = _context.Pets.Find(creatorId);

            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Content,PhotoUrl,PublishDateTime,Id")] Post post, Guid creatorId)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                post.Creator = await _context.Pets.FindAsync(creatorId);

                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    post.PhotoUrl = _blobStorage.UploadFile(
                        file.FileName, file.OpenReadStream(),
                        "posts",
                        file.ContentType);
                }

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = creatorId });
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Content,PhotoUrl,PublishDateTime,Id")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
