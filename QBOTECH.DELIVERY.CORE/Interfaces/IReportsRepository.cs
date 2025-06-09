using QBOTECH.DELIVERY.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IReportsRepository
    {
        Task<IEnumerable<DeliveriesByStatusReportDTO>> GetDeliveriesByStatusAsync(int? userId = null);
        Task<IEnumerable<DeliveriesByDateReportDTO>> GetDeliveriesByDateAsync(DateTime? from = null, DateTime? to = null, int? userId = null);
        Task<IEnumerable<DeliveriesByUserReportDTO>> GetDeliveriesByUserAsync();
        Task<IEnumerable<DeliveryStatusHistoryDTO>> GetDeliveryStatusHistoryAsync(int deliveryId);
        Task<DeliveriesOnTimeReportDTO> GetDeliveriesOnTimeReportAsync();
    }
}
