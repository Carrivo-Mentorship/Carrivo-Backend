using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Mentor_System
{

    // Request للـ POST /api/sessions/request
    public class RequestSessionRequest
    {
        public Guid MentorId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
