using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PictureGalleryBlobStorageNetCore.Models;
using PictureGalleryBlobStorageNetCore.ViewModels;

namespace PictureGalleryBlobStorageNetCore.BlobStorage
{
    public class BlobStorageManager
    {
        public static string connectionString => "xxxx";
        private const string containerName = "imagesupload";
        private static BlobContainerClient CreateBlobContainerClient()
            => new BlobContainerClient(connectionString, containerName);

        public async Task<IEnumerable<Picture>> GetPicturesAsync()
        {
            var pictures = new List<Picture>();
            // TODO : retrouver la liste des images (Uri) dans le container
            return pictures;
        }
        private  DateTime GetDate(BlobItem blobItem)
        {
            // TODO : retrouver la date dans les métadonnées du blob
            return DateTime.Now;
        }
        public async Task SavePictureAsync(PictureUploadViewModel picture)
        {
            // TODO : sauvegarder l'écriture en blob storage
        }
        private string? GetImageTypeFromExtension(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            switch (ext)
            {
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".tiff":
                    return "image/tiff";
                case ".gif":
                    return "image/gif";
            }
            return null;
        }
    }
}
