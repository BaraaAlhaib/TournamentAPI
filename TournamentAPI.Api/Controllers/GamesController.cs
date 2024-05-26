using AutoMapper;
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
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<GameDto>> GetGames()
        {
            // Antag att du har en metod som hämtar spel från din datakälla
            var games = GetGamesFromDataSource();
            var gameDtos = _mapper.Map<IEnumerable<GameDto>>(games);
            return Ok(gameDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null)
                return NotFound();

            var gameDto = _mapper.Map<GameDto>(game);
            return Ok(gameDto);
        }

        // PATCH: api/games/{id}
        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameDto>> PatchGame(int gameId, JsonPatchDocument<GameDto> patchDocument)
        {
            var game = await _gameRepository.GetAsync(gameId);
            if (game == null)
                return NotFound();

            var gameDto = _mapper.Map<GameDto>(game);

            patchDocument.ApplyTo(gameDto, ModelState);

            if (!TryValidateModel(gameDto))
                return BadRequest(ModelState);

            _mapper.Map(gameDto, game);
            _gameRepository.Update(game);

            return Ok(_mapper.Map<GameDto>(game));
        }

        private IEnumerable<Game> GetGamesFromDataSource()
        {
            // Mock data för exempel
            return new List<Game>
            {
                new Game { Title = "Final Match", Time = DateTime.Now }
            };
        }
    }
}
