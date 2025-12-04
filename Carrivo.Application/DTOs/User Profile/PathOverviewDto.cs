using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class PathOverviewDto
    {
        public List<MilestoneProgressDto> Overview { get; set; } = new();
    }
}
