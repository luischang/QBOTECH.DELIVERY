using System;
using System.Collections.Generic;

namespace QBOTECH.DELIVERY.CORE.DTOs
{
    // Entregas agrupadas por estado
    public class DeliveriesByStatusReportDTO
    {
        public string Status { get; set; } = null!;
        public int Count { get; set; }
    }

    // Entregas agrupadas por fecha (día o mes)
    public class DeliveriesByDateReportDTO
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    // Entregas agrupadas por repartidor
    public class DeliveriesByUserReportDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int Count { get; set; }
    }

    // Entregas agrupadas por ubicación (origen/destino)
    public class DeliveriesByLocationReportDTO
    {
        public string Location { get; set; } = null!;
        public int Count { get; set; }
    }

    // Historia de estados de entrega
    public class DeliveryStatusHistoryDTO
    {
        public int Id { get; set; }
        public int DeliveryId { get; set; }
        public string? PreviousStatus { get; set; }
        public string NewStatus { get; set; } = null!;
        public DateTime ChangedAt { get; set; }
        public int? ChangedBy { get; set; }
        public string? Comment { get; set; }
    }

    // Reporte de entregas a tiempo
    public class DeliveriesOnTimeReportDTO
    {
        public int TotalDelivered { get; set; }
        public int DeliveredOnTime { get; set; }
        public double PercentageOnTime { get; set; }
    }
}
