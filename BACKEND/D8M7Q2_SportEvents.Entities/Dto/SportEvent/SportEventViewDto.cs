using D8M7Q2_SportEvents.Entities.Dto.Competitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8M7Q2_SportEvents.Entities.Dto.SportEvent
{
    public class SportEventViewDto
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Date { get; set; } = "";
        public string Location { get; set; } = "";
        public int CompetitorLimit { get; set; }

        
        public IEnumerable<CompetitorViewDto>? Competitors { get; set; }

        public int CompetitorCount => Competitors?.Count() ?? 0;

    }
}
