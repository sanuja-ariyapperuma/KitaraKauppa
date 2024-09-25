﻿using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Repositories.Users
{
    public interface IUserRoleRepository
    {
        public Task<UserRole?> GetUserRoleByName(string roleName);
    }
}
