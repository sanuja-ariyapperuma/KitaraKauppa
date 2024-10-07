using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Core.Products
{
    public class UploadedImage
    {
        public Guid Name { get; set; }
        public string Extension { get; set; } = string.Empty;
    }
}
