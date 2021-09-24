using ImageRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // temp delete method
            //var user = new UserModel() { Id = 7 };
            //using (var db = new ImageContext())
            //{
             //   //bascially finds the user in the db with id 7 and removes it
              //  db.Attach(user);
               // db.Remove(user);
                //db.SaveChanges();
            //}
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost]
        public IActionResult UpdateUser(UserModel user)
        {

            using (var db = new ImageContext())
            {
                var userTemp = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                TempData["userTemp"] = userTemp;
                //db.SaveChanges();
            }

            return View("UpdateUser");
        }
        public IActionResult UpdateUserFinal(UserModel user)
        {
            using(var db = new ImageContext())
            {
                var UpdateUser = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                UpdateUser.Name = user.Name;
                UpdateUser.UserName = user.UserName;
                UpdateUser.Age = user.Age;
                UpdateUser.Email = user.Email;

                db.SaveChanges();
            }

            return View("AddUsers");
        }
        public IActionResult SearchUser(string searchTerm)
        {
            List<UserModel> users = new List<UserModel>();

            using (var db = new ImageContext())
            {
                users = db.Users.Where(user => user.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            TempData["users"] = users;

            return View("AddUsers");
        }
        public IActionResult AddUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using(var db = new ImageContext())
            {
                users = db.Users.ToList();
            }

            TempData["users"] = users;

            return View();
        }

        [HttpPost]
        public IActionResult AddUsers(UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ImageContext())
                {
                    db.Add(user);
                    db.SaveChanges();
                }
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult ImageView()
        {
            List<ImageModel> images = new List<ImageModel>();

            using (var db = new ImageContext())
            {
                images = db.Image.ToList();
            }

            TempData["images"] = images;

            return View("ImageView");
        }

        [HttpPost]
        public IActionResult ImageView(ImageModel image)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ImageContext())
                {
                    db.Add(image);
                    db.SaveChanges();
                }
            }

            return View("ImageView");
        }
    }
}
