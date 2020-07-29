using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSearchApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string title { get; set; }

        public string release_year { get; set; }

        public string locations { get; set; }

        public string production_company { get; set; }

        public string director { get; set; }

        public string writer { get; set; }

        public string actor_1 { get; set; }

        public string actor_2 { get; set; }

        public string actor_3 { get; set; }
    }
}
