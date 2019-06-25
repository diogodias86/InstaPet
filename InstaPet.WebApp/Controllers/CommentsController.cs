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
    public class CommentsController : Controller
    {
        private readonly InstaPetDbContext _context;
        private readonly IBlobStorage _blobStorage;

        public CommentsController()
        {
            _context = new InstaPetDbContext();
            _blobStorage = new AzureBlobService();
        }

        // GET: Comments
        [HttpGet]
        [Route("comments/{id}")]
        public async Task<IActionResult> Index(Guid? id)
        {
            var comments = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.Creator)
                .Where(c => c.Post.Id == id)
                .ToListAsync();

            ViewBag.Post = await _context.Posts.FindAsync(id);

            return View(comments);
        }

        // GET: Comments/Create
        [HttpGet]
        [Route("comments/create/{postId}")]
        public IActionResult Create(Guid postId)
        {
            ViewBag.Pets = _context.Pets.ToList();
            ViewBag.PostId = postId;

            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Content,PhotoUrl,PublishDateTime,Id")] Comment comment, Guid creatorId, Guid postId)
        {
            if (ModelState.IsValid)
            {
                comment.Id = Guid.NewGuid();
                comment.Creator = await _context.Pets.FindAsync(creatorId);
                comment.Post = await _context.Posts.FindAsync(postId);

                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    comment.PhotoUrl = _blobStorage.UploadFile(
                        file.FileName, file.OpenReadStream(),
                        "comments",
                        file.ContentType);
                }

                _context.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { id = postId });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Guid id, [Bind("Content,PhotoUrl,PublishDateTime,Id")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
