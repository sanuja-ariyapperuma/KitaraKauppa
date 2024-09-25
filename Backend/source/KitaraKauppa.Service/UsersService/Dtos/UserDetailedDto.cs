﻿namespace KitaraKauppa.Service.UsersService.Dtos
{
    public class UserDetailedDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public Guid UserRoleId { get; set; }
        public string UserRoleName { get; set; } = null!;
        public bool IsUserActive { get; set; }
        public string[] ContactNumbers { get; set; } = [];
        public List<AddressDto> Addresses { get; set; } = [];
    }
}
