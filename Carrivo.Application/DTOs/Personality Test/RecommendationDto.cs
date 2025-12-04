using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.Personality_Test
{
    public class RecommendationDto
    {
        public Guid TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public decimal CompatibilityScore { get; set; }
        public int Rank { get; set; }
    }
}
