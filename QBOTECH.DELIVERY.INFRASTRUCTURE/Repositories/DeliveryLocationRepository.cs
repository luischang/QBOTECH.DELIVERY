using Microsoft.EntityFrameworkCore;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories
{
    public class DeliveryLocationRepository : IDeliveryLocationRepository
    {
        private readonly QbotechDeliveryContext _context;
        public DeliveryLocationRepository(QbotechDeliveryContext context)
        {
            _context = context;
        }
        public async Task AddAsync(DeliveryLocation location)
        {
            await _context.DeliveryLocations.AddAsync(location);
            await _context.SaveChangesAsync();
        }
        public async Task<DeliveryLocation?> GetLastLocationAsync(int deliveryId)
        {
            return await _context.DeliveryLocations
                .Where(l => l.DeliveryId == deliveryId)
                .OrderByDescending(l => l.CreatedAt)
                .FirstOrDefaultAsync();
        }
        public async Task<List<DeliveryLocation>> GetLocationsByDeliveryAsync(int deliveryId)
        {
            return await _context.DeliveryLocations
                .Where(l => l.DeliveryId == deliveryId)
                .OrderBy(l => l.CreatedAt)
                .ToListAsync();
        }
        public void Update(DeliveryLocation location)
        {
            _context.DeliveryLocations.Update(location);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
