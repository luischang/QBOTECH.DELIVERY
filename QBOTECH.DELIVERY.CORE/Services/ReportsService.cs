using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QBOTECH.DELIVERY.CORE.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;
        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public Task<IEnumerable<DeliveriesByStatusReportDTO>> GetDeliveriesByStatusAsync(int? userId = null)
            => _reportsRepository.GetDeliveriesByStatusAsync(userId);

        public Task<IEnumerable<DeliveriesByDateReportDTO>> GetDeliveriesByDateAsync(DateTime? from = null, DateTime? to = null, int? userId = null)
            => _reportsRepository.GetDeliveriesByDateAsync(from, to, userId);

        public Task<IEnumerable<DeliveriesByUserReportDTO>> GetDeliveriesByUserAsync()
            => _reportsRepository.GetDeliveriesByUserAsync();

        public Task<IEnumerable<DeliveryStatusHistoryDTO>> GetDeliveryStatusHistoryAsync(int deliveryId)
            => _reportsRepository.GetDeliveryStatusHistoryAsync(deliveryId);

        public Task<DeliveriesOnTimeReportDTO> GetDeliveriesOnTimeReportAsync()
            => _reportsRepository.GetDeliveriesOnTimeReportAsync();
    }
}
