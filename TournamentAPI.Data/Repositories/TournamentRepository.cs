using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Core.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentContext _context;

        public TournamentRepository(TournamentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Tournament.ToListAsync();
        }

        public async Task<Tournament?> GetAsync(int id)
        {
            return await _context.Tournament.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournament.AnyAsync(t => t.Id == id);
        }

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
        }

        public void Update(Tournament tournament)
        {
            _context.Tournament.Update(tournament);
        }

        public void Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
        }
    }
}
