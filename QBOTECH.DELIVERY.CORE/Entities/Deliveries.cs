using System;
using System.Collections.Generic;

namespace QBOTECH.DELIVERY.CORE.Entities;

public partial class Deliveries
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string RecipientName { get; set; } = null!;

    public string? RecipientEmail { get; set; }

    public string? RecipientPhone { get; set; }

    public string? PackageDetails { get; set; }

    public decimal OriginLat { get; set; }

    public decimal OriginLng { get; set; }

    public decimal DestinationLat { get; set; }

    public decimal DestinationLng { get; set; }

    public DateTime CreatedAt { get; set; }
}
