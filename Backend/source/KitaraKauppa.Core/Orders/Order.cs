
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;

namespace KitaraKauppa.Core.Orders;

public partial class Order : BaseEntity
{

    public Guid UserId { get; set; }

    public Guid? AddressId { get; set; }

    public decimal? TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public User User { get; set; } = null!;

    public UserAddress Address { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
