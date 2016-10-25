using CinemaManagerWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace CinemaManagerWeb.Providers
{
    public class MoviesProvider
    {
        static HttpClient client = new HttpClient();

        public MoviesProvider()
        {
            client.BaseAddress = new Uri("http://www.omdbapi.com/?");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<MovieDTO>>  SearchMoviesByTitle(string title)
        {
            var response = await client.GetAsync($"/?s={title}&type=movie");

            if(response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await response.Content.ReadAsAsync<MovieSearchResult>();
                    return result.Search;
                }
                catch(Exception e)
                {
                    throw;
                }
            }
            throw new Exception(response.StatusCode.ToString());

        }

        public async Task<MovieDTO> GetMovieByImdbId(string ImdbId)
        {
            var response = await client.GetAsync($"/?i={ImdbId}&type=movie&r=json");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<MovieDTO>();
            }
            throw new Exception(response.StatusCode.ToString());
        }
    }

    internal class MovieSearchResult
    {
        public List<MovieDTO> Search { get; set; }
        public int TotalResults { get; set; }
        public string Response { get; set; }
    }


}