using DemoAsp.Net8_Session01_.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAsp.Net8_Session01_.Controllers
{
    public class PlanController : Controller
    {
        //[1] DataBase Connection
        private readonly GymDbContext Context;

        public PlanController()
        {
            Context = new GymDbContext();
        }
        //[2] Get :: URL/Plan/Index


        public async Task<IActionResult> Index()
        {  
            //Async => As Treat With Database 
            var Plans =await Context.Plans.ToListAsync();
            return View(Plans);
        }

        //[3] Get :: BaseURL/Plan/Detais/{id}
                                      //Detection ==> ([From Route] int id)
        public async Task<IActionResult> Details(int id)
        {
            //FindAsync ==> If Data is  Cashed  Will Return it if not Return From Database
            var plans = await Context.Plans.FindAsync(id);  
            if(plans == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plans); 
           
        }

    }
}
