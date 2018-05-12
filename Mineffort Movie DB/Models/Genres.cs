using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineffort_Movie_DB.Models
{
    public class Genres
    {

        public List<GenreBase> genres { get; set; }

        public class GenreBase
        {
            public int id { get; set; }
            public string name { get; set; }
        }

    }
}
