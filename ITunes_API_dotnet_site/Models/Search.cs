using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITunes_API_dotnet_site.Models
{
    public class SearchRoot
    {
        public int resultCount { get; set; }
        public Result[] results { get; set; }
        public bool Empty
        {
            get
            {
                return (resultCount == 0);
            }
        }
    }

    public class Result
    {
        public bool isGameCenterEnabled { get; set; }
        public string artworkUrl60 { get; set; }
        public string artworkUrl100 { get; set; }
        public string artistViewUrl { get; set; }
        public List<string> screenshotUrls { get; set; }
        public List<string> ipadScreenshotUrls { get; set; }
        public List<object> appletvScreenshotUrls { get; set; }
        public string artworkUrl512 { get; set; }
        public List<string> supportedDevices { get; set; }
        public List<string> advisories { get; set; }
        public string kind { get; set; }
        public List<string> features { get; set; }
        public string contentAdvisoryRating { get; set; }
        public string fileSizeBytes { get; set; }
        public double averageUserRating { get; set; }
        public string trackCensoredName { get; set; }
        public string trackViewUrl { get; set; }
        public List<string> languageCodesISO2A { get; set; }
        public double averageUserRatingForCurrentVersion { get; set; }
        public int userRatingCountForCurrentVersion { get; set; }
        public string trackContentRating { get; set; }
        public string releaseNotes { get; set; }
        public int trackId { get; set; }
        public string trackName { get; set; }
        public string formattedPrice { get; set; }
        public string primaryGenreName { get; set; }
        public DateTime releaseDate { get; set; }
        public int primaryGenreId { get; set; }
        public DateTime currentVersionReleaseDate { get; set; }
        public bool isVppDeviceBasedLicensingEnabled { get; set; }
        public List<string> genreIds { get; set; }
        public string sellerName { get; set; }
        public string minimumOsVersion { get; set; }
        public string currency { get; set; }
        public string version { get; set; }
        public string wrapperType { get; set; }
        public int artistId { get; set; }
        public string artistName { get; set; }
        public List<string> genres { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string bundleId { get; set; }
        public int userRatingCount { get; set; }
    }
}