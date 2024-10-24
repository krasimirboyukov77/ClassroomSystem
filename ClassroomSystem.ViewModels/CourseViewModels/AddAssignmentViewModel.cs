using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomSystem.ViewModels.CourseViewModels
{
    public class AddAssignmentViewModel
    {
        [Required(ErrorMessage ="Title is required!")]
        [StringLength(100, ErrorMessage ="The {0} must be between {2} and {1} characters long!",MinimumLength =2 )]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage ="There must be a Due Date!")]
        public string DueDate { get; set; } = null!;

        [Required]
        public string CourseId { get; set; } = null!;
    }
}
