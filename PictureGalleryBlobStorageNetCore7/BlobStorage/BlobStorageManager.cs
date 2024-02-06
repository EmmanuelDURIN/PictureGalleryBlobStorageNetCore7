using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.StaticFiles;
using PictureGalleryBlobStorageNetCore.Models;
using PictureGalleryBlobStorageNetCore.ViewModels;
using System.Net.Mime;

namespace PictureGalleryBlobStorageNetCore.BlobStorage
{
    public class BlobStorageManager
    {
        public static string connectionString => "DefaultEndpointsProtocol=https;AccountName=edpicturestorage;AccountKey=aTGZWk/mmp6P/sD2eCi6iLS2s0Z5bTbi8VyUCJvVvsQelCQS06D/XKHXSgbJ+8jVQAClyfKnP+08+AStOyHSMw==;EndpointSuffix=core.windows.net";
        private const string containerName = "imagesupload";
        private static BlobContainerClient CreateBlobContainerClient()
            => new BlobContainerClient(connectionString, containerName);

        public async Task<IEnumerable<Picture>> GetPicturesAsync()
        {
            var pictures = new List<Picture>();
            // TODO : retrouver la liste des images (Uri) dans le container
            BlobContainerClient blobContainerClient = CreateBlobContainerClient();
            AsyncPageable<BlobItem> blobItems = blobContainerClient.GetBlobsAsync(BlobTraits.Metadata);
            string containerUrl = blobContainerClient.Uri.ToString();
            await foreach (BlobItem blobItem in blobItems)
            {
                Picture picture = new Picture
                {
                    Title = blobItem.Name,
                    ImageUrl = containerUrl + "/" + blobItem.Name,
                    Date = GetDate(blobItem)

                };
                pictures.Add(picture);
            }
            return pictures;
        }
        private DateTime GetDate(BlobItem blobItem)
        {
            // TODO : retrouver la date dans les métadonnées du blob
            if (blobItem.Metadata.TryGetValue("Date", out string date))
                return DateTime.Parse(date);
            return DateTime.Now;
        }
        public async Task SavePictureAsync(PictureUploadViewModel picture)
        {
            // TODO : sauvegarder l'écriture en blob storage
            BlobContainerClient blobContainerClient = CreateBlobContainerClient();
            IFormFile? imageFromFile = picture.Image;
            ArgumentNullException.ThrowIfNull(imageFromFile);
            string fileName = imageFromFile.FileName;
            var blobName = Path.GetFileNameWithoutExtension(fileName)
                                + new Random().Next().ToString()
                                + Path.GetExtension(fileName) ?? "";
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            var blobUploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = GetImageTypeFromExtension(fileName) },
                Metadata = new Dictionary<string, string> { { "Date", picture.Date.ToString() } }
            };
            await blobClient.UploadAsync(imageFromFile.OpenReadStream(), blobUploadOptions);
        }
        private string GetImageTypeFromExtension(string fileName)
        {
            FileExtensionContentTypeProvider provider = new();
            if (provider.TryGetContentType(fileName, out string? contentType) && contentType != null)
                return contentType;
            return MediaTypeNames.Application.Octet;
        }
    }
}
