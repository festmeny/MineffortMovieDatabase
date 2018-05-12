using Mineffort_Movie_DB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mineffort_Movie_DB.Services
{
    class MovieService
    {
        private readonly Uri serverUrl = new Uri("https://api.themoviedb.org");
        private readonly string apiKey = "44faa5742f191efe4bccc168fa9f7b96";

        public async Task<MoviePopular> GetPopularMovies()
        {
            return await GetAsync<MoviePopular>(new Uri(serverUrl, "3/movie/popular?api_key=" + apiKey));
        }

        public async Task<MovieTopRated> GetTopRatedMovies()
        {
            return await GetAsync<MovieTopRated>(new Uri(serverUrl, "3/movie/top_rated?api_key=" + apiKey));
        }

        public async Task<MovieNowPlaying> GetNowPlayingMovies()
        {
            return await GetAsync<MovieNowPlaying>(new Uri(serverUrl, "3/movie/now_playing?api_key=" + apiKey + "&region=hu"));
        }

        public async Task<MovieUpcoming> GetUpcomingMovies()
        {
            return await GetAsync<MovieUpcoming>(new Uri(serverUrl, "3/movie/upcoming?api_key=" + apiKey + "&region=hu"));
        }

        public async Task<SearchMovie> SearchMovies(string query)
        {
            return await GetAsync<SearchMovie>(new Uri(serverUrl, "3/search/movie?api_key=" + apiKey + "&query=" + query));
        }

        public async Task<MovieExtended> GetMovieDetails(int id)
        {
            return await GetAsync<MovieExtended>(new Uri(serverUrl, "3/movie/" + id + "?api_key=" + apiKey));
        }

        public async Task<MovieSimilar> GetSimilarMovies(int id)
        {
            return await GetAsync<MovieSimilar>(new Uri(serverUrl, "3/movie/" + id + "/similar?api_key=" + apiKey));
        }

        public async Task<MovieCredits> GetMovieCredits(int id)
        {
            return await GetAsync<MovieCredits>(new Uri(serverUrl, "3/movie/" + id + "/credits?api_key=" + apiKey));
        }

        public async Task<PersonExtended> GetPersonExtended(int id)
        {
            return await GetAsync<PersonExtended>(new Uri(serverUrl, "3/person/" + id + "?api_key=" + apiKey));
        }

        public async Task<PersonCredits> GetPersonCredits(int id)
        {
            return await GetAsync<PersonCredits>(new Uri(serverUrl, "3/person/" + id + "/movie_credits?api_key=" + apiKey));
        }

        public async Task<Genres> GetGenres()
        {
            return await GetAsync<Genres>(new Uri(serverUrl, "3/genre/movie/list/?api_key=" + apiKey));
        }

        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                T result = JsonConvert.DeserializeObject<T>(json, settings);
                return result;
            }
        }
    }
}
