using KitaraKauppa.Core.Products;
using KitaraKauppa.Service.Repositories.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices
{
    public class ImageManagement : IImageManagement
    {
        private readonly IImageStorageRepository _imageStorageRepository;

        public ImageManagement(IImageStorageRepository imageStorageRepository)
        {
            _imageStorageRepository = imageStorageRepository;
        }
        public async Task<UploadedImage> UploadImage(IFormFile file)
        {
            var image = new UploadedImage
            {
                Name = Guid.NewGuid(),
                Extension = file.ContentType.Substring(file.ContentType.LastIndexOf('/') + 1)
            };

            var fileName = image.Name.ToString() + Path.GetExtension(file.FileName);
            await _imageStorageRepository.UploadImageToStorage(fileName, file);

            return image;
        }
    }
}
