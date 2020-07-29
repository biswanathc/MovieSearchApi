using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieSearchApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieSearchApi.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Movies.db");
        }
    }
}
