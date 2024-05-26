using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Data.Dto
{
    public class TournamentDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate
        {
            get { return StartDate.AddMonths(3); }
        }

        public TournamentDto(string title, DateTime startDate)
        {
            Title = title;
            StartDate = startDate;
        }
    }
}
