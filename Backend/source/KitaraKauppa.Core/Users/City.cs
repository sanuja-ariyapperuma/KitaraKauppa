using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Core.Users;

public partial class City : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string CityName { get; set; } = String.Empty;

}
