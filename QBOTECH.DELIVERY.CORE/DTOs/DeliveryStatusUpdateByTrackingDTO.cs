namespace QBOTECH.DELIVERY.CORE.DTOs
{
    public class DeliveryStatusUpdateByTrackingDTO
    {
        public string TrackingNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}