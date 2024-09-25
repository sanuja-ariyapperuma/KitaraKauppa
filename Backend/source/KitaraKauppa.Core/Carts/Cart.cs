using KitaraKauppa.Core.Products;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;

namespace KitaraKauppa.Core.Carts;

public partial class Cart : BaseEntity
{

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public int NumberOfUnits { get; set; } 
}
