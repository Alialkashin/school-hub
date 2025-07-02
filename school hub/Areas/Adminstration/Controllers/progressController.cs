using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_hub.Areas.Adminstration.ViewModels;
using school_hub.Data;
using school_hub.Models;
using System.Security.Claims;

namespace school_hub.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class ProgressController : Controller
    {
        private readonly AppDBContext _context;

        public ProgressController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.StudentSubscriptions
                .Include(r => r.Student)
                .Include(r => r.StudyPlan)
                .Where(r => r.PaymentStatus != enPaymentStatus.Complete)
                .ToListAsync();

            var requests = data
          .Select((r, index) => new InputStudyPlanRequestViewModel
          {
              RequestNumber = index + 1,
              StudentName = r.Student.UserName,
              StudyPlanName = r.StudyPlan.Name,
              StudentId = r.StudentId,
              StudyPlanId = r.PlanId,
              PaymentStatus = r.PaymentStatus
          }).ToList();



            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int StudentId, short PlanId)
        {
            var request = await _context.StudentSubscriptions
                .FirstOrDefaultAsync(r => r.StudentId == StudentId && r.PlanId == PlanId);

            if (request != null)
            {
                request.PaymentStatus = enPaymentStatus.Complete;
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int StudentId, short PlanId)
        {
            var request = await _context.StudentSubscriptions
                .FirstOrDefaultAsync(r => r.StudentId == StudentId && r.PlanId == PlanId);

            if (request != null)
            {
                request.PaymentStatus = enPaymentStatus.Rejected;  // هنا تغير لـ Rejected
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequest(int StudentId, short PlanId)
        {
            var request = await _context.StudentSubscriptions
                .FirstOrDefaultAsync(r => r.StudentId == StudentId && r.PlanId == PlanId);

            if (request != null)
            {
                _context.StudentSubscriptions.Remove(request);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }




    }
}
