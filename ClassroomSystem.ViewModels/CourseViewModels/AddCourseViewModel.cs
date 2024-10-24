using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomSystem.ViewModels.CourseViewModels
{
    public class AddCourseViewModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [MinLength(2, ErrorMessage = "Title is too short!")]
        [MaxLength(150, ErrorMessage = "Title is too long!")]
        public string Name { get; set; } = null!;

        [MaxLength(250, ErrorMessage ="Description is too long!")]
        public string? Description { get; set; }

        [MaxLength(100, ErrorMessage ="Password too long!")]
        public string? Password {  get; set; }

        


    }
}
