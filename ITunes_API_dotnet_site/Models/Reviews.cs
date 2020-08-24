using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ITunes_API_dotnet_site.Models
{
    public class Review
    {
        [Key]
        public int reviewId { get; set; }
        [Range(0,5)]
        public int rating { get; set; }
        public string username { get; set; }
        public string title { get; set; }
        public string review { get; set; }
        [NotMapped]
        public int formId { get; set; }
        [JsonIgnore]
        public Application app { get; set; }
    }
}