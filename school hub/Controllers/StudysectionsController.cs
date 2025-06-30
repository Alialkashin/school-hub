using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;
using school_hub.ViewModels;
using System.Security.Claims;

namespace school_hub.Areas.Public.Controllers
{
    public class StudySectionsController : Controller
    {
        private readonly AppDBContext _context;
        public StudySectionsController(AppDBContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var studysection1 = await _context.Sections
                .OfType<StudySection>()
                .ToListAsync();
            return View(studysection1);
        }


             public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var section = await _context.Sections
                .OfType<StudySection>()
                .Include(s => s.StudyPlans)
                .FirstOrDefaultAsync(s => s.SectionId == id);

            if (section == null)
                return NotFound();

            var viewModel = new StudySectionDetailsViewModel
            {
                Section = section
            };

            if (User.Identity.IsAuthenticated)
            {
                int studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                viewModel.StudentId = studentId;
                viewModel.SubscribedPlanIds = await _context.StudentSubscriptions
                    .Where(s => s.StudentId == studentId)
                    .Select(s => s.PlanId)
                    .ToListAsync();
            }

            return View(viewModel);
        }
        [HttpGet]
        [AllowAnonymous] // أو احذفها إذا تريد السماح للمستخدمين المسجلين فقط
        public async Task<IActionResult> Buy(short planId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // عدّلها حسب صفحة تسجيل الدخول لديك
            }

            int studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool alreadyExists = await _context.StudentSubscriptions
                .AnyAsync(s => s.StudentId == studentId && s.PlanId == planId);

            if (!alreadyExists)
            {
                var subscription = new StudentStudyPlan
                {
                    StudentId = studentId,
                    PlanId = planId,
                    PaymentStatus = enPaymentStatus.notComplete
                };
                _context.StudentSubscriptions.Add(subscription);
                await _context.SaveChangesAsync();
            }

         
            var plan = await _context.StudyPlans.FirstOrDefaultAsync(p => p.StudyPlanId == planId);
            if (plan != null)
            {
                return RedirectToAction("Details", "StudySections", new { area = "Public", id = plan.StudySectionId });
            }

            return RedirectToAction("Index", "StudySections", new { area = "Public" });
        }

    }
}