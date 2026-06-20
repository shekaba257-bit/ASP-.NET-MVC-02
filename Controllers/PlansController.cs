using DemoAsp.Net8_Session01_.Contexts;
using DemoAsp.Net8_Session01_.Models;
using GymManagment.DAL.Repostories.Classes;
using GymManagment.DAL.Repostories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAsp.Net8_Session01_.Controllers
{
    public class PlansController : Controller
    {
        //[1] DataBase Connection => PlanRepository
        //   private readonly GymDbContext Context;
        // Dependancy Injection To Killled=>> new


        private readonly IGenaricRepository<Plan> _planRepostory ;
        public PlansController(IGenaricRepository<Plan> planRepostory)
        {
            _planRepostory = planRepostory;
        }
        //[2] Get :: URL/Plan/Index


        public async Task<IActionResult> Index(CancellationToken ct)
        {
            //Async => As Treat With Database 
            var Plans = await _planRepostory.GetAllAsync(ct: ct); //Pass By Name
            return View(Plans);
        }

        //[3] Get :: BaseURL/Plan/Detais/{id}
                                      //Detection ==> ([From Route] int id)
        public async Task<IActionResult> Details(int id,CancellationToken ct)
        {
            //FindAsync ==> If Data is  Cashed  Will Return it if not Return From Database
            var plan = await _planRepostory.GetByIdAsync(id,ct);  
            if(plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan); 
           
        }

    }
}
