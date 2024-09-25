using KitaraKauppa.Core.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Shared
{
    public static class Helper
    {
        public static string GetImageUrl(Image image) 
        {
            if (image == null)
            {
                return "";
            }

            var fileId = image.Id;
            var fileExtention = image.Extention;
            var baseUrl = "https://kitarakauppa.azurewebsites.net/ProductImages";

            return $"{baseUrl}/{fileId}.{fileExtention}";
        }
    }
}
