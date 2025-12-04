using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Mentor_System
{
    // Response للـ GET /api/mentors
    public class MentorListDto
    {
        public List<MentorCardDto> Mentors { get; set; } = new();
    }
}
