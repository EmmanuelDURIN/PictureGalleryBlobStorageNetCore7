using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.StaticFiles;
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
        private string? GetImageTypeFromExtension(string fileName)
        {
            FileExtensionContentTypeProvider provider = new();
            provider.TryGetContentType(fileName, out string? contentType);
            return contentType;
        }
    }
}
