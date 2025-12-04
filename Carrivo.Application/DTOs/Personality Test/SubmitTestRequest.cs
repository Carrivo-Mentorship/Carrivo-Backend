using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Request للـ POST /api/test/submit
    public class SubmitTestRequest
    {
        public Guid UserId { get; set; }
        public Dictionary<string, int> Answers { get; set; } = new();
    }

}
