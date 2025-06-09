using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBOTECH.DELIVERY.CORE.Enums;

namespace QBOTECH.DELIVERY.CORE.DTOs
{
    public class DeliveriesDTO
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

    public class DeliveriesCreateDTO
    {
        public int UserId { get; set; }
        public string RecipientName { get; set; } = null!;
        public string? RecipientEmail { get; set; }
        public string? RecipientPhone { get; set; }
        public string? PackageDetails { get; set; }
        public decimal OriginLat { get; set; }
        public decimal OriginLng { get; set; }
        public decimal DestinationLat { get; set; }
        public decimal DestinationLng { get; set; }
        public string OriginDescription { get; set; } = null!;
        public string DestinationDescription { get; set; } = null!;
        public DateOnly? EstimatedDeliveryDate { get; set; }
        public TimeOnly? EstimatedDeliveryTime { get; set; }
        public TimeOnly? EstimatedTimeFrom { get; set; }
        public TimeOnly? EstimatedTimeTo { get; set; }
    }

    public class DeliveriesCreateResponseDTO
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
        public string Status { get; set; } = null!;
        public string TrackingNumber { get; set; } = null!;
        public string OriginDescription { get; set; } = null!;
        public string DestinationDescription { get; set; } = null!;
        public DateOnly? EstimatedDeliveryDate { get; set; }
        public TimeOnly? EstimatedDeliveryTime { get; set; }
        public TimeOnly? EstimatedTimeFrom { get; set; }
        public TimeOnly? EstimatedTimeTo { get; set; }
    }

    public class DeliveriesUpdateDTO
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
    }

    public class DeliveryStatusUpdateDTO
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
    }

    public class DeliveriesListDTO
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
        public string Status { get; set; } = null!;
        public DeliveryStatus StatusEnum => Enum.Parse<DeliveryStatus>(Status);
        public string TrackingNumber { get; set; } = null!;
        public string OriginDescription { get; set; } = null!;
        public string DestinationDescription { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateOnly? EstimatedDeliveryDate { get; set; }
        public TimeOnly? EstimatedDeliveryTime { get; set; }
        public TimeOnly? EstimatedTimeFrom { get; set; }
        public TimeOnly? EstimatedTimeTo { get; set; }
    }

}
