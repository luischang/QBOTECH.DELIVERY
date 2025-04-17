using QBOTECH.DELIVERY.CORE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

    public string Status { get; set; }

    [NotMapped]
    public DeliveryStatus StatusEnum
    {
        get => Enum.Parse<DeliveryStatus>(Status);
        set => Status = value.ToString();
    }
    public string TrackingNumber { get; set; } = null!;
    public string OriginDescription { get; set; } = null!;
    public string DestinationDescription { get; set; } = null!;
    public DateOnly? EstimatedDeliveryDate { get; set; }
    public TimeOnly? EstimatedDeliveryTime { get; set; }
    public TimeOnly? EstimatedTimeFrom { get; set; }
    public TimeOnly? EstimatedTimeTo { get; set; }


}
