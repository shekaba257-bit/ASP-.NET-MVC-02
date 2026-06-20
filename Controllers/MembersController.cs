using GymManagment.BLL.Services.interfaces;
using GymManagment.BLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoAsp.Net8_Session01_.Controllers
{
    public class  MembersController : Controller
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
        #region Create
        // Get :: BaseUrl / Member/Create =>Show Empty Form
        [HttpGet]
        public IActionResult Create() 
            => View();

        // Post :: BaseUrl /Members/Create/{member} =>Submit form
        // CreateMember
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model,CancellationToken ct)
        {
            //Check ModelState if is invalid and valid
            if (!ModelState.IsValid) return View(nameof(Create), model);
            //If Valid
            var result =await _memService.CreateMemberAsync(model, ct);
            if (result)
                TempData["Success Message"] = "Member Create Successfully ";
            else
                TempData["Error Message"] = "Member Failed To Create ! ";

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> MemberDetails(int id ,CancellationToken ct)
        {
            var member = await _memService.GetMemberDetailsByIdAsync(id, ct);

            if(member is null)
            {
                TempData["Error Message"] = "Member Not Found !";
            }

            return View(member);
        }

        #endregion
        //Get ::BaseUrl/Members/HealthRecordDetails/{Id} => Get HealthRecord For Specific Members
        public async Task<IActionResult> HealthRecordDetails(int id ,CancellationToken ct)
        {
            var record =await _memService.GetMemberHealthRecord(id, ct);

            //Check if record is null
            if(record is null)
            {
                TempData["Error Message"] = "No HealthRecord Found !";
                return RedirectToAction(nameof(Index));
            }

            return View(record);
        }


        #region Edit
        //Get :: BaseUrl/Members/Edit/{id} => Show Edit Form 

        [HttpGet]
        public async Task<IActionResult> EditMember(int id ,CancellationToken ct)
        {
            var member =await _memService.GetMemberToUpdateAsync(id, ct);
            //Check if member is null
            if(member is null)
            {
                TempData["Error Message"] = "Member Not Failed ";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }


        // Post :: BaseUrl/Members/Edit/{member}=>submit Edit Form 
        [HttpPost]
        public async Task<IActionResult> EditMember([FromRoute] int id, CancellationToken ct, MemberToUpdateViewModel model)
        {
            //(1)Check if ModelState Is Not Valid  
            if (!ModelState.IsValid)return View(model);

            //if valid 
            var result = await _memService.UpdateMemberAsync(id, model, ct);

            if (result)
            {
                TempData["Success Message"] = "Member Update Successfully";
            }
            else
            {
                TempData["Error Message"] = "Failed To Update Member ";
            }
            return RedirectToAction(nameof(Index));
             
        }

        #endregion
        //Get BaseUrl /Members/Delete /{id} =>Show Validation Page

        public async Task<IActionResult> Delete (int id,CancellationToken ct)
        {
            var member =await _memService.GetMemberDetailsByIdAsync(id, ct);
            //Check if member is null 
            if( member is null)
            {
                TempData["Error Message"] = "Member NOT Found";
                return RedirectToAction(nameof(Index));
            }
            return View();
        } 


        // Action Delete 
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id, CancellationToken ct)
        {
            var result =await _memService.DeleteMemberAsync(id, ct);

            if(result)
                TempData["Success Message"] = "Member Deleted Successfully";

            TempData["Error Message"] = "Failed To Deleted Member";

            return RedirectToAction(nameof(Index));
        }
    }
}
