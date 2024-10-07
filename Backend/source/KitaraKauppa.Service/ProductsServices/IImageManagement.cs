using KitaraKauppa.Core.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.ProductsServices
{
    public interface IImageManagement
    {
        Task<UploadedImage> UploadImage(IFormFile file); 
    }
}
