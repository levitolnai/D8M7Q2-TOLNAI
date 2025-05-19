using D8M7Q2_SportEvents.Data;
using D8M7Q2_SportEvents.Entities.Dto.Competitor;
using D8M7Q2_SportEvents.Entities.Dto.SportEvent;
using D8M7Q2_SportEvents.Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace D8M7Q2_SportEvents.Endpoint.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CompetitorController: ControllerBase
    {
        CompetitorLogic logic;
        UserManager<AppUser> userManager;

        public CompetitorController(CompetitorLogic logic, UserManager<AppUser> userManager)
        {
            this.logic = logic;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task AddCompetitor(CompetitorCreateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            logic.AddCompetitor(dto, "Anonymous"/*user.Id*/);
        }
        [HttpDelete("{id}")]
        public void DeleteCompetitor(string id)
        {
            logic.DeleteCompetitor(id);
        }
    }
}
