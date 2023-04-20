using System.ComponentModel.DataAnnotations;

namespace PictureGalleryBlobStorageNetCore.ViewModels
{
    public class PictureUploadViewModel
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public IFormFile? Image { set; get; }
    }
}