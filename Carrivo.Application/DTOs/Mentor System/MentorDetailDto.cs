using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Mentor_System
{


    // Response للـ GET /api/mentors/:id
    public class MentorDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public List<ReviewDto> Reviews { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public string ExperienceSummary { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public decimal Rating { get; set; }
        public string? Image { get; set; }
    }
}
