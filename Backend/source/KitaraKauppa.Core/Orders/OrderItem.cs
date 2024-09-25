using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Core.Shared;

namespace KitaraKauppa.Core.Orders;

public partial class OrderItem : BaseEntity
{

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public Guid ColorId { get; set; }
    public Orientation Orientation { get; set; }

    public int Units { get; set; }

    public decimal Price { get; set; }

    public Color Color { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
