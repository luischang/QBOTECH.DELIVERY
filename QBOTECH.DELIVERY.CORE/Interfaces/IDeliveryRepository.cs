using QBOTECH.DELIVERY.CORE.Entities;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IDeliveryRepository : IRepository<Deliveries>
    {
        Task<IEnumerable<Deliveries>> GetDeliveriesByUserIdAsync(int userId);
        Task<Deliveries> GetDeliveryByTrackingNumberAsync(string trackingNumber);

    }
}