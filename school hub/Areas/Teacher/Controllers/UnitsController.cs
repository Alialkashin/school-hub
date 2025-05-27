using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using school_hub.Data;
using school_hub.Models;
using Microsoft.EntityFrameworkCore;
using school_hub.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace school_hub.Areas.Tetcher.Controllers
{
    public class UnitsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly int techerId;
        public UnitsController(AppDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostEnvironment;
            techerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        }


        public IActionResult Index()
        {
            ICollection<Unit> units = (ICollection<Unit>)_context.Subjects.Where(s => s.TeacherId == techerId).SelectMany(s => s.Units);
            return View(units);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(InputDisplayInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Unit unit = new Unit();
                if (model.File!.Length > 0)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/Units");
                    string uniqueFileName = Guid.NewGuid().ToString() + model.File.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    Directory.CreateDirectory(uploadFolder);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyToAsync(fileStream);
                    }
                    unit.ImagePath = "~/images/Units" + uniqueFileName;
                }
                unit.SubjectId = _context.Subjects.FirstOrDefault(s => s.TeacherId == techerId)!.SubjectId;
                unit.Name = model.Name;
                unit.Description = model.Description;
                return RedirectToAction("Index");
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Delete(short unitId)
        {
            Unit? unit = _context.Units.FirstOrDefault(u => u.UnitId == unitId);
            if (unit == null)
            {
                return RedirectToAction("Index");
            }
            return Ok(unit);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(short unitId)
        {
            Unit? unit = _context.Units.FirstOrDefault(u => u.UnitId == unitId);
            if (unit == null)
            {
                return Json("faild");
            }
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return Json("done");

        }
    
            

        
        
        
        
    }
}