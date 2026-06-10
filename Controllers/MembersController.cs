using GymManagment.BLL.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoAsp.Net8_Session01_.Controllers
{
    public class MembersController : Controller
    {
        //Database Connection 
        //MemberService


        private readonly IMemberService _memService;
        public MembersController(IMemberService memService)
        {
            _memService = memService;
        }


        #region GetMember
        //GET::Base URL/Member /Index => List All Members


        public async Task<IActionResult> Index(CancellationToken ct )    
        {
            var members = await _memService.GetAllAsync(ct);
            return View(members);
        }

        #endregion


    }
}
