using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace ITunes_API_dotnet_site.Models
{
    public class OuterJSON
    {
        public Feed feed { get; set; }
    }

    public class Feed
    {
        public Author author { get; set; }
        public string copyright { get; set; }
        public string country { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string updated { get; set; }
        public TopApp[] results { get; set; }
    }

    public class TopApp
    {
        [Key]
        public int id { get; set; }
        public int artistId { get; set; }
        public string artistName { get; set; }
        public string artistUrl { get; set; }
        public string artworkUrl100 { get; set; }
        public string copyright { get; set; }
        [NotMapped]
        public List<Genre> genres { get; set; }
        [JsonIgnore]
        public List<TopAppGenres> appGenres { get; set; }
        public string kind { get; set; }
        public string name { get; set; }
        public DateTime releaseDate { get; set; }
        public string url { get; set; }
        [JsonIgnore]
        public bool paid { get; set; }
    }

    public class Genre
    {
        [Key]
        public int genreId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<TopAppGenres> appGenres { get; set; }
    }

    public class TopAppGenres
    {
        [Key]
        public int id { get; set; }
        public Genre Genre { get; set; }
        public TopApp TopApp { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string uri { get; set; }
    }
}