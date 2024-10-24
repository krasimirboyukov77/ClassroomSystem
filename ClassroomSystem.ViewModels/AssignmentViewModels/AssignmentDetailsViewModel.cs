using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomSystem.ViewModels.AssignmentViewModels
{
    public class AssignmentDetailsViewModel
    { 
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public ICollection<string> FilePaths { get; set; } = new List<string>();

        
    }
}
