using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITunes_API_dotnet_site.Models
{
    public class DetailJSON
    {
        public int resultCount { get; set; }
        public Application[] results { get; set; }
    }

    public class Application
    {
        public int? artistId { get; set; }
        public string artistName { get; set; }
        public string artistViewUrl { get; set; }
        public string artworkUrl100 { get; set; }
        public double? averageUserRating { get; set; }
        public double? averageUserRatingForCurrentVersion { get; set; }
        public string contentAdvisoryRating { get; set; }
        public DateTime currentVersionReleaseDate { get; set; }
        public string description { get; set; }
        public string formattedPrice { get; set; }
        public string primaryGenreName { get; set; }
        public DateTime releaseDate { get; set; }
        public string releaseNotes { get; set; }
        [NotMapped]
        public string[] screenshotUrls { get; set; }
        public string sellerName { get; set; }
        public string sellerUrl { get; set; }
        [NotMapped]
        public string[] supportedDevices { get; set; }
        public string trackCensoredName { get; set; }
        [Key]
        public int? trackId { get; set; }
        public string trackName { get; set; }
        public string trackViewUrl { get; set; }
        public int UserRatingCount { get; set; }
        public int UserRatingCountForCurrentVersion { get; set; }
        public string version { get; set; }
    }
}

