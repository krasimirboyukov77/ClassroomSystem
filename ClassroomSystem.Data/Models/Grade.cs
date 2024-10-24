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
    public class Grade
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; } = null!;

        [Required]
        public Guid AssignmentId { get; set; }

        [ForeignKey(nameof(AssignmentId))]
        public Assignment Assignment { get; set; } = null!;

        [Required]
       
        public int GradeValue { get; set; }

        public string? Description { get; set; } 

    }
}
