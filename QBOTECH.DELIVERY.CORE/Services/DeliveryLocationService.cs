using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;
using System.Threading.Tasks;

namespace QBOTECH.DELIVERY.CORE.Services
{
    public class DeliveryLocationService
    {
        private readonly IDeliveryLocationRepository _locationRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        public DeliveryLocationService(IDeliveryLocationRepository locationRepository, IDeliveryRepository deliveryRepository)
        {
            _locationRepository = locationRepository;
            _deliveryRepository = deliveryRepository;
        }
        public async Task AddLocationAsync(DeliveryLocationDTO dto)
        {
            var entity = new DeliveryLocation
            {
                DeliveryId = dto.DeliveryId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                CreatedAt = dto.CreatedAt
            };
            await _locationRepository.AddAsync(entity);
        }
        public async Task AddLocationByTrackingAsync(DeliveryLocationByTrackingDTO dto)
        {
            var delivery = await _deliveryRepository.GetDeliveryByTrackingNumberAsync(dto.TrackingNumber);
            if (delivery == null) throw new Exception("Delivery not found");
            var entity = new DeliveryLocation
            {
                DeliveryId = delivery.Id,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                CreatedAt = dto.CreatedAt ?? DateTime.UtcNow
            };
            await _locationRepository.AddAsync(entity);
        }
        public async Task<DeliveryLocationDTO?> GetLastLocationAsync(int deliveryId)
        {
            var entity = await _locationRepository.GetLastLocationAsync(deliveryId);
            if (entity == null) return null;
            return new DeliveryLocationDTO
            {
                DeliveryId = entity.DeliveryId,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreatedAt = entity.CreatedAt
            };
        }
        public async Task<DeliveryLocationDTO?> GetLastLocationByTrackingAsync(string trackingNumber)
        {
            var delivery = await _deliveryRepository.GetDeliveryByTrackingNumberAsync(trackingNumber);
            if (delivery == null) return null;
            var entity = await _locationRepository.GetLastLocationAsync(delivery.Id);
            if (entity == null) return null;
            return new DeliveryLocationDTO
            {
                DeliveryId = entity.DeliveryId,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreatedAt = entity.CreatedAt
            };
        }
        public async Task<List<DeliveryLocationDTO>> GetLocationsByDeliveryAsync(int deliveryId)
        {
            var list = await _locationRepository.GetLocationsByDeliveryAsync(deliveryId);
            return list.Select(entity => new DeliveryLocationDTO
            {
                DeliveryId = entity.DeliveryId,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreatedAt = entity.CreatedAt
            }).ToList();
        }
        public async Task<List<DeliveryLocationDTO>> GetLocationsByTrackingAsync(string trackingNumber)
        {
            var delivery = await _deliveryRepository.GetDeliveryByTrackingNumberAsync(trackingNumber);
            if (delivery == null) return new List<DeliveryLocationDTO>();
            var list = await _locationRepository.GetLocationsByDeliveryAsync(delivery.Id);
            return list.Select(entity => new DeliveryLocationDTO
            {
                DeliveryId = entity.DeliveryId,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreatedAt = entity.CreatedAt
            }).ToList();
        }
    }
}
