using ClassroomSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomSystem.ViewModels.CourseViewModels
{
    public class DetailsCourseViewModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime CreatedOn { get; set; }

        public required string CreatorId { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new HashSet<Assignment>();
        
    }
}
