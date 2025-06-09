using System;

namespace QBOTECH.DELIVERY.CORE.DTOs
{
    public class DeliveryLocationByTrackingDTO
    {
        public string TrackingNumber { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
