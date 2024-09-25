using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.AuthenticationService
{
    public interface IJwtManagement
    {
        string GenerateToken(User user);
        void InvalidateToken(string token);
        bool ValidateToken(string token);
    }
}
