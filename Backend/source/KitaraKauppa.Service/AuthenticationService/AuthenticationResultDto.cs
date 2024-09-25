using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.AuthenticationService
{
    public class AuthenticationResultDto
    {
        public bool IsAuthenticated { get; set; }
        public string FullName { get; set; } = String.Empty;
        public bool IsAdmin { get; set; } = false;
        public string Token { get; set; } = null!;
    }
}
