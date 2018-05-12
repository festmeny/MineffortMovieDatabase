using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineffort_Movie_DB.Models
{
    public class MovieNowPlaying
    {

        public List<MovieBasic> results { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public Dates dates { get; set; }
        public int total_pages { get; set; }

        public class Dates
        {
            public string maximum { get; set; }
            public string minimum { get; set; }
        }


    }
}
