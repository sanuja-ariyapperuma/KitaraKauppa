using KitaraKauppa.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace KitaraKauppa.Infrastrcture.Configuration
{
    public class AzureBlobStorageSettings : IAzureBlobStorageSettings
    {
        private readonly IConfiguration _configuration;

        public AzureBlobStorageSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string IAzureBlobStorageSettings.BaseUrl
        {
            get => _configuration["AzureBlobStorage:BaseUrl"];
        }

        string IAzureBlobStorageSettings.Container
        {
            get => _configuration["AzureBlobStorage:Container"];
        }
    }
}
