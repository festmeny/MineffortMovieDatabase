using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineffort_Movie_DB.Models
{
    public class PersonCredits
    {
        public List<Crew> crew { get; set; }
        public List<Cast> cast { get; set; }
        public int id { get; set; }

        public class Cast
        {
            public string character { get; set; }
            public string credit_id { get; set; }
            public string poster_path { get; set; }
            public int id { get; set; }
            public bool video { get; set; }
            public int vote_count { get; set; }
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public List<int> genre_ids { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public float popularity { get; set; }
            public string title { get; set; }
            public float vote_average { get; set; }
            public string overview { get; set; }
            public string release_date { get; set; }
        }

        public class Crew
        {

            public string credit_id { get; set; }
            public string department { get; set; }
            public int gender { get; set; }
            public int id { get; set; }
            public string job { get; set; }
            public string name { get; set; }
            public object profile_path { get; set; }

        }

    }
}
