using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Core.Users;

public partial class UserRole : BaseEntity
{
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string UserRoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
