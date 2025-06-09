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
}
