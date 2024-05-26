using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournament { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Eventuella specifika modellkonfigurationer
        }
    }
}
