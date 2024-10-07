using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KitaraKauppa.Service.Repositories.Products;

namespace KitaraKauppa.Infrastrcture.Repositories.Products
{
    public class ImageStorageRepository : IImageStorageRepository
    {
        private readonly IConfiguration _configuration;

        public ImageStorageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> UploadImageToStorage(string imageName, IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_configuration["AzureBlobStorage:ConnectionString"]);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_configuration["AzureBlobStorage:Container"]);

            var blobClient = blobContainerClient.GetBlobClient(imageName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return true;
        }
    }
}
