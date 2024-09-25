﻿using KitaraKauppa.Service.Repositories.InMemory;

namespace KitaraKauppa.Infrastrcture.Database
{
    public class InMemoryDB : IInMemoryDB
    {
        private readonly List<string> _deniedTokens = new List<string>();
        public void AddDeniedToken(string token)
        {
            _deniedTokens.Add(token);
        }

        public List<string> GetDeniedTokens()
        {
            return _deniedTokens.ToList();
        }
    }
}
