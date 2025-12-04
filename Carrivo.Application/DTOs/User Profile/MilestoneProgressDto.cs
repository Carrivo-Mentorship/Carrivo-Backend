using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrivo.Application.DTOs.User_Profile
{
    public class MilestoneProgressDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Progress { get; set; }
    }
}
