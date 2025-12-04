using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class UserMentorDto
    {
        public bool HasMentor { get; set; }
        public MentorBasicDto? Mentor { get; set; }
    }
}
