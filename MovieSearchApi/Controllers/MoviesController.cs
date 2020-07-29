using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MovieSearchApi.Models;

namespace MovieSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext context;

        public MoviesController(MovieContext context)
        {
            this.context = context;
        }

        //Get list of all movies from database
        [HttpGet]
        [Route("GetAllMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await context.Movies.ToListAsync();
        }

        //This action Loads latest movie list from external API
        [HttpPost]
        [Route("RefreshMovieList")]
        public async Task<ActionResult> LoadMovieList()
        {

            //Remove Old Movie List
            context.Movies.RemoveRange(await context.Movies.ToListAsync());
            context.SaveChanges();


            //Make API call to fetch latest movies from data service
            HttpClient client = new HttpClient();
            var httpResponse = await client.GetAsync("https://data.sfgov.org/resources/yitu-d5am.json");
            if(!httpResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }

            // Deserialize Movie object
            var content = await httpResponse.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);

            //Save data to DB
            foreach(Movie movie in movieList)
            {
                context.Movies.Add(movie);
            }

            context.SaveChanges();

            // Return latest movie list
            return Ok("List Updated");


        }

        [HttpPost]
        [Route("SearchMovie")]
        public async Task<ActionResult<List<Movie>>> SearchMovie(SearchFilter filter)
        {
            //Get all movies from DB
            var movieList = await context.Movies.ToListAsync();

            //Waterfall search
            var filteredByTitle = GetMoviesFilteredByTitle(movieList, filter.title);
            var filteredByReleaseYear = GetMoviesFilteredByTitle(filteredByTitle, filter.title);
            var filteredByLocation = GetMoviesFilteredByTitle(filteredByReleaseYear, filter.title);
            var filteredByProductionCompany = GetMoviesFilteredByTitle(filteredByLocation, filter.title);
            var filteredByDirector = GetMoviesFilteredByTitle(filteredByProductionCompany, filter.title);
            var filteredByWriter = GetMoviesFilteredByTitle(filteredByDirector, filter.title);
            var filteredByActor = GetMoviesFilteredByTitle(filteredByWriter, filter.title);

            //Return filtered result
            return filteredByActor;
        }

        #region Private Methods


        private List<Movie> GetMoviesFilteredByTitle(List<Movie> movieList, string searchParam)
        {
            if(string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.title.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        private List<Movie> GetMoviesFilteredByReleaseYear(List<Movie> movieList, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.release_year.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        private List<Movie> GetMoviesFilteredByLocations(List<Movie> movieList, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.locations.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        private List<Movie> GetMoviesFilteredByProductionCompany(List<Movie> movieList, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.production_company.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        private List<Movie> GetMoviesFilteredByDirector(List<Movie> movieList, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.director.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        private List<Movie> GetMoviesFilteredByWriter(List<Movie> movieList, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.writer.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        private List<Movie> GetMoviesFilteredByActor(List<Movie> movieList, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam) || movieList == null || movieList.Count == 0)
            {
                return movieList;
            }
            else
            {
                return movieList.Where(n => n.actor_1.ToUpper().Contains(searchParam.ToUpper()) || n.actor_2.ToUpper().Contains(searchParam.ToUpper()) || n.actor_3.ToUpper().Contains(searchParam.ToUpper())).ToList();
            }
        }

        #endregion


    }
}
