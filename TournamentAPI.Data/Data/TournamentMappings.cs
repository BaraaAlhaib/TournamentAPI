using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Data.Dto;

namespace TournamentAPI.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
        }
    }
}
