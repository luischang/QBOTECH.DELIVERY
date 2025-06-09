using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<IEnumerable<DeliveryStatusHistoryDTO>> GetDeliveryStatusHistoryAsync(int deliveryId)
        {
            return await _context.DeliveryStatusHistories
                .Where(h => h.DeliveryId == deliveryId)
                .OrderBy(h => h.ChangedAt)
                .Select(h => new DeliveryStatusHistoryDTO
                {
                    Id = h.Id,
                    DeliveryId = h.DeliveryId,
                    PreviousStatus = h.PreviousStatus,
                    NewStatus = h.NewStatus,
                    ChangedAt = h.ChangedAt,
                    ChangedBy = h.ChangedBy,
                    Comment = h.Comment
                })
                .ToListAsync();
        }

        public async Task<DeliveriesOnTimeReportDTO> GetDeliveriesOnTimeReportAsync()
        {
            var delivered = await _context.Deliveries
                .Where(d => d.Status == "F" && d.EstimatedDeliveryDate != null && d.EstimatedDeliveryTime != null)
                .ToListAsync();
            int totalDelivered = delivered.Count;
            int deliveredOnTime = delivered.Count(d =>
                d.EstimatedDeliveryDate.HasValue &&
                d.EstimatedDeliveryTime.HasValue &&
                d.CreatedAt.Date <= d.EstimatedDeliveryDate.Value.ToDateTime(TimeOnly.MinValue).Date &&
                d.CreatedAt.TimeOfDay <= d.EstimatedDeliveryTime.Value.ToTimeSpan()
            );
            double percentage = totalDelivered > 0 ? (double)deliveredOnTime / totalDelivered * 100 : 0;
            return new DeliveriesOnTimeReportDTO
            {
                TotalDelivered = totalDelivered,
                DeliveredOnTime = deliveredOnTime,
                PercentageOnTime = percentage
            };
        }
    }
}
