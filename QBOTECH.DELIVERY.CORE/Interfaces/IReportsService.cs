using QBOTECH.DELIVERY.CORE.DTOs;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IReportsService
    {
        Task<IEnumerable<DeliveriesByStatusReportDTO>> GetDeliveriesByStatusAsync(int? userId = null);
        Task<IEnumerable<DeliveriesByDateReportDTO>> GetDeliveriesByDateAsync(DateTime? from = null, DateTime? to = null, int? userId = null);
        Task<IEnumerable<DeliveriesByUserReportDTO>> GetDeliveriesByUserAsync();
    }
}
