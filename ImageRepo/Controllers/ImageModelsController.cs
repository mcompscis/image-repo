using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImageRepo.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ImageRepo.Controllers
{
    public class ImageModelsController : Controller
    {
        private readonly ImageContext _context;

        public ImageModelsController(ImageContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RenderImage(int Id)
        {
            ImageModel image = await _context.Image.FindAsync(Id);
            byte[] imageArray = image.ProductImage;
            return File(imageArray, "image/png");
        }

        // GET: ImageModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Image.ToListAsync());
        }

        // GET: ImageModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Image
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // GET: ImageModels/Create
        public IActionResult Create()
        {
            // returns Create.cshtml
            return View();
        }

        // POST: ImageModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProductImage")] ImageModel imageModel, List<IFormFile> ProductImage)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in ProductImage)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            imageModel.ProductImage = stream.ToArray();
                        }
                    }
                }
                _context.Add(imageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: ImageModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Image.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }
            return View(imageModel);
        }

        // POST: ImageModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProductImage")] ImageModel imageModel, List<IFormFile> ProductImage)
        {
            /*if (id != imageModel.Id)
            {
                return NotFound();
            }*/

            var newImageName = imageModel.Name;

            imageModel = await _context.Image
                .FirstOrDefaultAsync(m => m.Id == id);

            if (imageModel == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                // modularize this code into another method-----------------------
                foreach (var item in ProductImage)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            imageModel.ProductImage = stream.ToArray();
                        }
                    }
                }
                // modularize ----------------------------------------------------
                
                imageModel.Name = newImageName;

                try
                {
                    _context.Update(imageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModelExists(imageModel.Id))
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
            return View(imageModel);
        }

        // GET: ImageModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Image
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: ImageModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModel = await _context.Image.FindAsync(id);
            _context.Image.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageModelExists(int id)
        {
            return _context.Image.Any(e => e.Id == id);
        }
    }
}
