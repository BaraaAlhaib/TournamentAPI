using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Dto;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentsController(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TournamentDto>> GetTournaments()
        {
            // Antag att du har en metod som hämtar turneringar från din datakälla
            var tournaments = GetTournamentsFromDataSource();
            var tournamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return Ok(tournamentDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            var tournament = await _tournamentRepository.GetAsync(id);
            if (tournament == null)
                return NotFound();

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            return Ok(tournamentDto);
        }

        // GET: api/tournaments/{id}
        [HttpGet("{title}/by-title")]
        public async Task<ActionResult<TournamentDto>> GetTournamentByTitle(string title)
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            if (tournaments == null)
                return NotFound();

            var tournamentByTitle = tournaments.Where(x=> x.Title == title).FirstOrDefault();
            if (tournamentByTitle == null) 
                return NotFound();

            return Ok(tournamentByTitle);
        }

        [HttpPatch("{tournamentId}")]
        public async Task<ActionResult<TournamentDto>> PatchTournament(int tournamentId, 
            JsonPatchDocument<TournamentDto> patchDocument)
        {
            var tournament = await _tournamentRepository.GetAsync(tournamentId);
            if (tournament == null)
                return NotFound();

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);

            patchDocument.ApplyTo(tournamentDto, ModelState);

            if (!TryValidateModel(tournamentDto))
                return BadRequest(ModelState);

            _mapper.Map(tournamentDto, tournament);
            _tournamentRepository.Update(tournament);

            return Ok(_mapper.Map<TournamentDto>(tournament));
        }


        private IEnumerable<Tournament> GetTournamentsFromDataSource()
        {
            // Mock data för exempel
            return new List<Tournament>
            {
                new Tournament { Title = "Spring Championship", StartDate = DateTime.Now }
            };
        }
    }
}
