using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PictureGalleryBlobStorageNetCore.Models
{
    public class Picture
    {
        // Pour la sauvegarde Cosmos DB
        [JsonPropertyName("id")]
        public String? Id { get; set; }
        [Required]
        public String? Title { get; set; }
        [Required]
        public String? ImageUrl { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }
        public String? BrowserFile { get; set; }
    }
}