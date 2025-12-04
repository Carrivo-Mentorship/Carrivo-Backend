using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Response للـ GET /api/test/progress/:userId
    public class TestProgressDto
    {
        public Dictionary<string, int> Answers { get; set; } = new();
        public int CurrentPage { get; set; }
    }
}
