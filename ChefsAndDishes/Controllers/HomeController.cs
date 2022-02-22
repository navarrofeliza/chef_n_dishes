using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ChefsAndDishes.Models;

namespace ChefsAndDishes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Chef> AllChefs = _context.Chefs.Include(dish => dish.Dishes).ToList();
            ViewBag.allchefs = AllChefs;
            return View();
        }

        [HttpGet]
        [Route("Dishes")]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = _context.Dishes.Include(chef => chef.Creator).ToList();
            ViewBag.alldishes = AllDishes;
            return View("Dishes");
        }

        [HttpGet]
        [Route("ChefView")]
        public IActionResult ChefView()
        {
            return View("AddChef");
        }

        [HttpGet]
        [Route("")]
        public IActionResult DishView()
        {
            List<Chef> AllChefs = _context.Chefs.ToList();
            ViewBag.allchefs = AllChefs;
            return View("AddDish");
        }

        [HttpPost]
        [Route("AddChef")]
        public IActionResult AddChef(Chef chef)
        {
            if (ModelState.IsValid)
            {
                if (chef.Birthday >= DateTime.Today)
                {
                    ModelState.AddModelError("Birthday", "Birthday cannot be from the future!");
                    return View("AddChef");
                }
                Chef newChef = new Chef
                {
                    FirstName = chef.FirstName,
                    LastName = chef.LastName,
                    Birthday = chef.Birthday,
                };
                _context.Add(newChef);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddChef");
            }
        }

        [HttpPost]
        [Route("AddDish")]
        public IActionResult AddDish(Dish dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                _context.SaveChanges();
                return RedirectToAction("DishView");
            }
            else
            {
                List<Chef> AllChefs = _context.Chefs.ToList();
                ViewBag.allchefs = AllChefs;
                return View("AddDish", dish);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
