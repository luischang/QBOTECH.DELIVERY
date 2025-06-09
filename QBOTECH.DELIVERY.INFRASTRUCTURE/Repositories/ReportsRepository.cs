using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly QbotechDeliveryContext _context;
        public ReportsRepository(QbotechDeliveryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeliveriesByStatusReportDTO>> GetDeliveriesByStatusAsync(int? userId = null)
        {
            var query = _context.Deliveries.AsQueryable();
            if (userId.HasValue)
                query = query.Where(d => d.UserId == userId.Value);
            return await query
                .GroupBy(d => d.Status)
                .Select(g => new DeliveriesByStatusReportDTO
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DeliveriesByDateReportDTO>> GetDeliveriesByDateAsync(DateTime? from = null, DateTime? to = null, int? userId = null)
        {
            var query = _context.Deliveries.AsQueryable();
            if (from.HasValue)
                query = query.Where(d => d.CreatedAt >= from.Value);
            if (to.HasValue)
                query = query.Where(d => d.CreatedAt <= to.Value);
            if (userId.HasValue)
                query = query.Where(d => d.UserId == userId.Value);
            return await query
                .GroupBy(d => d.CreatedAt.Date)
                .Select(g => new DeliveriesByDateReportDTO
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<DeliveriesByUserReportDTO>> GetDeliveriesByUserAsync()
        {
            return await _context.Deliveries
                .GroupBy(d => new { d.UserId, d.User.FullName })
                .Select(g => new DeliveriesByUserReportDTO
                {
                    UserId = g.Key.UserId,
                    UserName = g.Key.FullName,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();
        }
    }
}
