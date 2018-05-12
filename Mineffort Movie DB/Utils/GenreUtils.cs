using Mineffort_Movie_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineffort_Movie_DB.Utils
{
    public class GenreUtils
    {
        private static Dictionary<int, string> genres;

        public static void init (List<Genres.GenreBase> genreList)
        {
            genres = new Dictionary<int, string>();

            foreach (var item in genreList)
            {
                genres.Add(item.id, item.name);
            }
        }

        public static string getGenre(int id)
        {
            return genres[id];
        }
    }
}
