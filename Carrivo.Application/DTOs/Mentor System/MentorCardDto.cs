using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Mentor_System
{
    public class MentorCardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public string ExperienceLevel { get; set; } = string.Empty; // junior, mid, senior
        public string Availability { get; set; } = string.Empty; // online, offline, busy
        public string Track { get; set; } = string.Empty;
    }
}
