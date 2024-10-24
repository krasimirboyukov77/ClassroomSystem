using ClassroomSystem.Data;
using ClassroomSystem.Data.Models;
using ClassroomSystem.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClassroomSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CourseController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();

            return View(courses);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddCourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Course? course = new Course()
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                Description = viewModel.Description,
                Password = viewModel.Password,
                DateCreated = DateTime.Now,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty
            };

            if(course == null)
            {
                return View(viewModel);
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = course.Id });
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {

            bool IsCourseIdValid = Guid.TryParse(id, out Guid courseId);

            if (!IsCourseIdValid) {
                return View("Index");
            }

            Course? course = await  _context.Courses.Include(c=> c.Assignments).FirstOrDefaultAsync(c=> c.Id == courseId);

            if (course == null)
            {
                return View("Index");
            }

            DetailsCourseViewModel courseDetails = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description ?? string.Empty,
                CreatedOn = course.DateCreated,
                CreatorId = course.UserId,
                Assignments = course.Assignments,
            };

            return View(courseDetails);
        }

        
    }
}
