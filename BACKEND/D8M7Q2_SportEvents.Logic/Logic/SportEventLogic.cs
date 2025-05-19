using D8M7Q2_SportEvents.Data;
using D8M7Q2_SportEvents.Entities;
using D8M7Q2_SportEvents.Entities.Dto.SportEvent;
using D8M7Q2_SportEvents.Logic.Helpers;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace D8M7Q2_SportEvents.Logic.Logic
{
    public class SportEventLogic
    {
        Repository<SportEvent> repo;
        DtoProvider dtoProvider;

        public SportEventLogic(Repository<SportEvent> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public void AddSportEvent(SportEventCreateUpdateDto dto)
        {
            SportEvent s = dtoProvider.Mapper.Map<SportEvent>(dto);
            if (repo.GetAll().FirstOrDefault(x => x.Title == s.Title) == null)
            {
                repo.Create(s);
            }
            else
            {
                throw new ArgumentException("Már van ilyen esemény!");
            }
        }
        public IEnumerable<SportEventShortViewDto> GetAllSportEvents()
        {
            return repo.GetAll().Select(x =>
                dtoProvider.Mapper.Map<SportEventShortViewDto>(x)
            );
        }
        public void DeleteSportEvent(string id)
        {
            repo.DeleteById(id);
        }
        public void UpdateSportEvent(string id, SportEventCreateUpdateDto dto)
        {
            var old = repo.FindById(id);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }
        public SportEventViewDto GetSportEvent(string id)
        {
            var model = repo.FindById(id);
            if (model == null)
            {
                throw new InvalidOperationException("SportEvent not found.");
            }
            return dtoProvider.Mapper.Map<SportEventViewDto>(model);
        }

        public IEnumerable<SportEventStatisticsDto> GetSportEventStatistics()
        {
            return repo.GetAll()
                .Select(e => new SportEventStatisticsDto
                {
                    SportEventId = e.Id,
                    Title = e.Title,
                    CompetitorCount = e.Competitors != null ? e.Competitors.Count : 0
                })
                .ToList();
        }
    }
}
