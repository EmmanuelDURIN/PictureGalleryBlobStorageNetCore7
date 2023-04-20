using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PictureGalleryBlobStorageNetCore.Models
{
    public class Picture
    {
        // Pour la sauvegarde Cosmos DB
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }
        public string? BrowserFile { get; set; }
    }
}