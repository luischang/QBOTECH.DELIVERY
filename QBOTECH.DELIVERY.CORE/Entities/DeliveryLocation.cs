using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QBOTECH.DELIVERY.CORE.Entities
{
    public class DeliveryLocation
    {
        public int Id { get; set; }
        public int DeliveryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Deliveries Delivery { get; set; } = null!;
    }
}
