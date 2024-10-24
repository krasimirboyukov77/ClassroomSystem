using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomSystem.Data.Models
{
    public class AssignmentMaterial
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AssignmentId { get; set; }

        [ForeignKey(nameof(AssignmentId))]
        public Assignment Assignment { get; set; } = null!;

        [Required]
        public string FilePath { get; set; } = null!;

        [Required]
        public string ContentType { get; set; } = null!;

        [Required]
        public long Size { get; set; }

        [Required]
        public string FileName = null!;
    }
}
