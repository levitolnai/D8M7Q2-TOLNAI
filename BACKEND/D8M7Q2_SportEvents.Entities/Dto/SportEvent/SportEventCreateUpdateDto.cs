using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8M7Q2_SportEvents.Entities.Dto.SportEvent
{
    public class SportEventCreateUpdateDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Date { get; set; } = "";
        public string Location { get; set; } = "";
        public int CompetitorLimit { get; set; }
    }
}
