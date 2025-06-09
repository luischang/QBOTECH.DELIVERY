using System;

namespace QBOTECH.DELIVERY.CORE.Entities
{
    public class DeliveryStatusHistory
    {
        public int Id { get; set; }
        public int DeliveryId { get; set; }
        public string? PreviousStatus { get; set; }
        public string NewStatus { get; set; } = null!;
        public DateTime ChangedAt { get; set; }
        public int? ChangedBy { get; set; }
        public string? Comment { get; set; }

        // Navigation property
        public virtual Deliveries Delivery { get; set; } = null!;
    }
}
