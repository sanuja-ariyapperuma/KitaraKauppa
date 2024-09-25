using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Core.Users;

public partial class UserAddress : BaseEntity
{
    public Guid UserId { get; set; }
    [Required]
    [MaxLength(50)]
    public string AddressLine1 { get; set; } = String.Empty;
    [MaxLength(50)]
    public string AddressLine2 { get; set; } = String.Empty;
    [Required]
    public Guid CityId { get; set; }

    public virtual City City { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
