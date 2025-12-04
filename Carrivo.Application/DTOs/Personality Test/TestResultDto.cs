using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Personality_Test
{
    // Response للـ POST /api/test/submit
    public class TestResultDto
    {
        public string CareerPath { get; set; } = string.Empty;
        public Dictionary<string, decimal> Score { get; set; } = new();
        public List<RecommendationDto> Recommendations { get; set; } = new();
    }
}
