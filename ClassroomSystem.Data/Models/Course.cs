using ClassroomSystem.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomSystem.Data.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CourseValidation.CourseNameMaxLength)]
        public string Name { get; set; } = null!;


        [MaxLength(CourseValidation.CourseDescriptionMaxLength)]
        public string? Description { get; set; }

        public string? Password { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;


        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public virtual ICollection<UserCourse> UsersCourses { get; set; } = new HashSet<UserCourse>();
    }
}
