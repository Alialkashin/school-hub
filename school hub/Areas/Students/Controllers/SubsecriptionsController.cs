using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_hub.Data;
using school_hub.Models;
using school_hub.ViewModles;
using System.Security.Claims;

namespace school_hub.Areas.Students.Controllers
{

    // إذا كنت تستخدم int كـ Id
    [Authorize]
    public class SubsecriptionsController : Controller
    {
        public int StudentId { get; set; } 
        private readonly AppDBContext _context;
        public SubsecriptionsController(AppDBContext context)
        {
            _context = context;
            if(User.Identity.IsAuthenticated)
                StudentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        }

        public IActionResult Index()
        {

            List<StudentStudyPlan> studentsub = _context.StudentSubscriptions.Where(ss => ss.StudentId == StudentId).ToList();
            List<SubsecriptionViewModel> sub = new List<SubsecriptionViewModel>();
            foreach (var itme in studentsub)
            {
                sub.Add(new SubsecriptionViewModel()
                {
                    studyPlanId = itme.PlanId,
                    paymentStatus = itme.PaymentStatus
                });
            }
            return View(sub);
        }
        public IActionResult AddCourse(int studyPlanId)
        {
            throw new NotImplementedException();
        }




    }
}