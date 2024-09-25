using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Core.Users;

public partial class UserContactNumber : BaseEntity
{
    public Guid UserId { get; set; }

    [StringLength(10,MinimumLength = 10)]
    public string ContactNumber { get; set; } = String.Empty;

    public virtual User User { get; set; } = null!;
}
