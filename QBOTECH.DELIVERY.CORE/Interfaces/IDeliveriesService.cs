using QBOTECH.DELIVERY.CORE.DTOs;

namespace QBOTECH.DELIVERY.CORE.Interfaces
{
    public interface IDeliveriesService
    {
        Task<DeliveriesCreateResponseDTO> CreateDelivery(DeliveriesCreateDTO deliveryDTO);
        void DeleteDelivery(int id);
        Task<IEnumerable<DeliveriesListDTO>> GetAllDeliveriesAsync();
        Task<DeliveriesListDTO> GetDeliveryById(int id);
        Task<DeliveriesListDTO> GetDeliveryByTrackingNumber(string trackingNumber);
        void UpdateDelivery(DeliveriesUpdateDTO deliveryDTO);
        Task UpdateDeliveryStatusAsync(DeliveryStatusUpdateDTO statusUpdateDTO);
        Task<IEnumerable<DeliveriesListDTO>> GetDeliveriesByUserIdAsync(int userId);
    }
}