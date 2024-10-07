using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.Products
{
    public interface IImageStorageRepository
    {
        Task<bool> UploadImageToStorage(string imageName, IFormFile file);
    }
}
