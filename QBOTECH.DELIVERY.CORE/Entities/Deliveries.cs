using System;
using System.Collections.Generic;

namespace QBOTECH.DELIVERY.CORE.Entities;

public partial class Deliveries
{
    public int Id { get; set; }

    public string PickupAddress { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public string? Status { get; set; }

    public int UserId { get; set; }
}
