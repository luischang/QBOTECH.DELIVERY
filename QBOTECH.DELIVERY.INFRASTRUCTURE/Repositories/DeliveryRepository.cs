using Microsoft.EntityFrameworkCore;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories
{
    public class DeliveryRepository : Repository<Deliveries>, IDeliveryRepository
    {
        public DeliveryRepository(QbotechDeliveryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Deliveries>> GetDeliveriesByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        //Get delivery by tracking number
        public async Task<Deliveries> GetDeliveryByTrackingNumberAsync(string trackingNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.TrackingNumber == trackingNumber);
        }
    }
}
