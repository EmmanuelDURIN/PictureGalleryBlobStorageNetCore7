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
        public static string connectionString => "DefaultEndpointsProtocol=https;AccountName=edpicturestorage;AccountKey=aqx0d98NGD9pjBQbzHkH7tzjGygQgWljba9WK/sheOgxVMF3uRym5UJRQnDLIt+o+OF5z47WXmHj+AStkvJgOQ==;EndpointSuffix=core.windows.net";
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
            return DateTime.Parse(blobItem.Metadata["Date"]);
        }
        public async Task SavePictureAsync(PictureUploadViewModel picture)
        {
            // TODO : sauvegarder l'écriture en blob storage
            BlobContainerClient blobContainerClient = CreateBlobContainerClient();
            IFormFile? imageFromFile = picture.Image;
            ArgumentNullException.ThrowIfNull(imageFromFile);
            var blobName = Path.GetFileNameWithoutExtension(imageFromFile.FileName)
                                + new Random().Next().ToString()
                                + Path.GetExtension(imageFromFile.FileName) ?? "";
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(imageFromFile.OpenReadStream(), true);
            await UpdateContentTypeAync(blobClient, imageFromFile.FileName);

            await blobClient.SetMetadataAsync(new Dictionary<string, string> { { "Date", picture.Date.ToString() } });

        }
        private async Task UpdateContentTypeAync(BlobClient blobClient, string fileName)
        {
            FileExtensionContentTypeProvider provider = new();
            if (provider.TryGetContentType(fileName, out string? contentType) && contentType != null )
            {
                BlobHttpHeaders httpHeaders = new() { ContentType = contentType };
                await blobClient.SetHttpHeadersAsync(httpHeaders);
            }
        }
     }
}
