using AutoMapper;
using D8M7Q2_SportEvents.Data;
using D8M7Q2_SportEvents.Entities;
using D8M7Q2_SportEvents.Entities.Dto.Competitor;
using D8M7Q2_SportEvents.Entities.Dto.SportEvent;
using D8M7Q2_SportEvents.Entities.Dto.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8M7Q2_SportEvents.Logic.Helpers
{
    public class DtoProvider
    {
        UserManager<AppUser> userManager;
        public Mapper Mapper { get; }

        public DtoProvider(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SportEvent, SportEventShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    if (src.Competitors.Count == 0)
                    {
                        dest.CompetitorCount = 0;
                        
                    }
                    else
                    {
                        dest.CompetitorCount = src.Competitors.Count;
                    }
                    
                });
                cfg.CreateMap<AppUser, UserViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.IsAdmin = userManager.IsInRoleAsync(src, "Admin").Result;
                });

                cfg.CreateMap<SportEvent, SportEventViewDto>();
                cfg.CreateMap<SportEventCreateUpdateDto, SportEvent>();
                cfg.CreateMap<CompetitorCreateDto, Competitor>();
                cfg.CreateMap<Competitor, CompetitorViewDto>()
                .AfterMap((src, dest) =>
                {
                    //var user = userManager.Users.First(u => u.Id == src.UserId);
                    //if (user != null)
                    //{
                    //    dest.UserFullName = user.LastName + " " + user.FirstName;
                    //}
                    //else
                    //{
                    //    dest.UserFullName = "Unknown User";
                    //}
                    ////dest.UserFullName = user.LastName! + " " + user.FirstName;
                    dest.UserFullName = "Unknown User";
                });
            });

            Mapper = new Mapper(config);
        }
    }
}
