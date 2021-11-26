using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace demo.Database
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DirectorName { get; set; }
        public string Actor { get; set; }
        public string Actress { get; set; }
        public Int16? TotalCast { get; set; }
    }
}
