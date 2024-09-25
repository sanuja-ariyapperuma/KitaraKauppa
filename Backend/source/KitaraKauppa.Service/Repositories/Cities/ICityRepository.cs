using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.Cities
{
    public interface ICityRepository
    {
        public Task<City?> GetCity(Guid cityId);

        public Task<bool> IsCityExists(Guid cityId);
    }
}
