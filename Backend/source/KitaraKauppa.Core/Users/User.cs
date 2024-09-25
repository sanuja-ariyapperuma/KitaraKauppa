using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Core.Users;

public partial class User : BaseEntity
{

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string LastName { get; set; } = String.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    [Required]
    public Guid UserRoleId { get; set; }
    [Required]
    public DateTime? LastLogin { get; set; }
    public bool? IsUserActive { get; set; } = true;

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual UserRole UserRole { get; set; } = null!;

    public virtual ICollection<UserContactNumber> UserContactNumbers { get; set; } = new List<UserContactNumber>();

    public virtual UserCredential UserCredential { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
