using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8M7Q2_SportEvents.Entities.Dto.Competitor
{
    public class CompetitorCreateDto
    {
        public required string SportEventId { get; set; } = "";

        [MinLength(5)]
        [MaxLength(30)]
        public required string Name { get; set; } = "";

        [MinLength(5)]
        [MaxLength(40)]
        public required string Email { get; set; } = "";
        [MinLength(10)]
        [MaxLength(15)]
        public required string Phone { get; set; } = "";
    }
}
