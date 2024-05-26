using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class SeedData
    {
        public static void Initialize(TournamentContext context)
        {
            if (!context.Tournament.Any()) // Ändra från Tournaments till Tournament här
            {
                // Skapa några turneringar med tillhörande matcher
                var tournaments = new[]
                {
                    new Tournament
                    {
                        Title = "Turnering 1",
                        StartDate = DateTime.Now.AddDays(7),
                        Games = new[]
                        {
                            new Game { Title = "Game 1" },
                            new Game { Title = "Game 2" },
                            new Game { Title = "Game 3" }
                        }
                    },
                    new Tournament
                    {
                        Title = "Turnering 2",
                        StartDate = DateTime.Now.AddDays(14),
                        Games = new[]
                        {
                            new Game { Title = "Game 4" },
                            new Game { Title = "Game 5" }
                        }
                    }

                };

                context.Tournament.AddRange(tournaments);
                context.SaveChanges();
            }
        }
    }
}
