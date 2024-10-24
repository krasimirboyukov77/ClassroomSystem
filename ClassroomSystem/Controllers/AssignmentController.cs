using ClassroomSystem.Data;
using ClassroomSystem.Data.Models;
using ClassroomSystem.ViewModels.AssignmentViewModels;
using ClassroomSystem.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace ClassroomSystem.Controllers
{
    
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public AssignmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            bool isValidId = Guid.TryParse(id, out var assignmentId);

            if (!isValidId)
            {
                return BadRequest();
            }

            var assignment = await _context.Assignments.Include(a=> a.Materials).Include(a=> a.User).Include(a=> a.Materials).FirstOrDefaultAsync(a=> a.AssignmentId == assignmentId);

            if(assignment == null)
            {
                return NotFound();
            }

            var viewModel = new AssignmentDetailsViewModel()
            {
                Title = assignment.Title,
                Description = assignment.Description,
                CreatedBy = assignment.User?.Email ?? string.Empty,
                DueDate = assignment.DueDate,
                FilePaths = assignment.Materials.Select(am=> am.FilePath).ToArray()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create(string courseId)
        {

            AssignmentAndMaterialAddViewModel viewModel = new AssignmentAndMaterialAddViewModel()
            {
                CourseId = courseId,
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AssignmentAndMaterialAddViewModel viewModel)
        {
            var assignmentMaterialOriginal = new AssignmentMaterial();
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "HomeController");
            }

            bool isValidDate = DateTime.TryParseExact(viewModel.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime assDate);

            if (!isValidDate)
            {
                return View(viewModel);
            }

            bool isValidCourseId = Guid.TryParse(viewModel.CourseId, out Guid newCourseId);

            if (!isValidCourseId)
            {
                return View("Index");
            }

            Assignment newAssignment = new Assignment()
            {
                Title = viewModel.Title,
                Description = viewModel.Description ?? string.Empty,
                UserId = currentUser.Id,
                CourseId = newCourseId,
                DueDate = assDate,
                
            };

           var currentCourse = await _context.Courses.FirstOrDefaultAsync(c=> c.Id == newAssignment.CourseId);
            if (currentCourse == null)
            {
                return View(viewModel);
            }
            if (viewModel.File != null && viewModel.File.Length > 0)
            {

                var fileName = Path.GetFileName(viewModel.File.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.File.CopyToAsync(stream);
                }

                var assignmentMaterial = new AssignmentMaterial
                {
                    FileName = fileName,
                    FilePath = filePath,
                    ContentType = viewModel.File.ContentType,
                    AssignmentId = viewModel.AssignmentId,
                    Size = viewModel.File.Length,
                    Id = Guid.NewGuid()
                };

                assignmentMaterialOriginal = assignmentMaterial;
                _context.Add(assignmentMaterialOriginal);
                newAssignment.Materials.Add(assignmentMaterialOriginal);

            }
            
             currentCourse.Assignments.Add(newAssignment);
            _context.Assignments.Add(newAssignment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Course", new {id = currentCourse.Id});
        }

        [Authorize]
        public IActionResult DownloadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return NotFound();
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var mimeType = "application/octet-stream";
            return File(System.IO.File.ReadAllBytes(filePath), mimeType, filePath);
        }
    }
}
