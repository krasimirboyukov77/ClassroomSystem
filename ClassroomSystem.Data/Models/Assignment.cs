using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomSystem.Common;
using Microsoft.AspNetCore.Identity;

namespace ClassroomSystem.Data.Models
{
    public class Assignment
    {
        [Key]
        public Guid AssignmentId { get; set; }
        [Required]
        [MaxLength(AssignmentValidation.AssignmentTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(AssignmentValidation.AssignmentDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        public Guid CourseId { get; set; } 

        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; } = null!;

        public ICollection<AssignmentMaterial> Materials { get; set; } = new HashSet<AssignmentMaterial>();

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>(); 
    }
}
