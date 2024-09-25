using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.InMemory
{
    public interface IInMemoryDB
    {
        public void AddDeniedToken(string token);
        public List<string> GetDeniedTokens();
    }
}
