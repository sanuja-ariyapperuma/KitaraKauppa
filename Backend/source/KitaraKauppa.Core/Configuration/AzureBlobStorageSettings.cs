using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Core.Configuration
{
    public interface IAzureBlobStorageSettings
    {
        string BaseUrl { get; }
        string Container { get; }
    }
}
