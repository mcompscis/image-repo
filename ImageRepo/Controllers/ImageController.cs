using ImageRepo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepo.Controllers
{
    public class ImageController : Controller
    {
       /* private readonly ImageContext _context;

        public ImageController(ImageContext context)
        {
            _context = context;
        }*/

        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(ImageModel image, List<IFormFile> ProductImage)
        {
            foreach (var item in ProductImage)
            {
                if(item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        image.ProductImage = stream.ToArray();
                    }
                }
            }
            using (var db = new ImageContext())
            {
                db.Add(image);
                db.SaveChanges();
            }
            /*_context.Image.Add(image);
            _context.SaveChanges();*/
            return View();
        }
    }
}
