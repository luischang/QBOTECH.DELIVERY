using QBOTECH.DELIVERY.CORE.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IDeliveryLocationRepository
    {
        Task AddAsync(DeliveryLocation location);
        Task<DeliveryLocation?> GetLastLocationAsync(int deliveryId);
        Task<List<DeliveryLocation>> GetLocationsByDeliveryAsync(int deliveryId);
    }
}
