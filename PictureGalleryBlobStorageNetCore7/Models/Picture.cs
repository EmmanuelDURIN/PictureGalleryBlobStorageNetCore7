using System.ComponentModel.DataAnnotations;

namespace PictureGalleryBlobStorageNetCore.Models
{
    public class Picture
    {
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