using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class MentorBasicDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;
        public string? Image { get; set; }
    }
}
