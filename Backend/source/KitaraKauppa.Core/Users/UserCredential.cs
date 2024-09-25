using KitaraKauppa.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Core.Users;

public partial class UserCredential : BaseEntity
{
    public Guid UserId { get; set; }

    [Required]
    [MinLength(5)]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public User User { get; set; } = null!;
}
