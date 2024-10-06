using KitaraKauppa.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.AuthenticationService
{
    public interface IAuthManagement
    {
        Task<KKResult<AuthenticationResultDto>> Authenticate(string username, string password);
        public KKResult<string> Logout(string token);
    }
}
