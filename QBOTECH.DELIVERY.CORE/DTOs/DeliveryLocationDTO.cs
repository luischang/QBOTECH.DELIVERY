using System;

namespace QBOTECH.DELIVERY.CORE.DTOs
{
    public class DeliveryLocationDTO
    {
        public int DeliveryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
