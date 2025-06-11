using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Enums;
using QBOTECH.DELIVERY.CORE.Interfaces;

namespace QBOTECH.DELIVERY.CORE.Services
{
    public class DeliveriesService : IDeliveriesService
    {
        private readonly IRepository<Deliveries> repository;
        private readonly IDeliveryRepository deliveryRepository;
        public DeliveriesService(IRepository<Deliveries> repository, IDeliveryRepository deliveryRepository)
        {
            this.repository = repository;
            this.deliveryRepository = deliveryRepository;
        }
        public async Task<IEnumerable<DeliveriesListDTO>> GetAllDeliveriesAsync()
        {
            var deliveries = await repository.GetAllAsync();
            // Convert deliveries to DeliveriesDTO
            var deliveriesDTO = deliveries.Select(d => new DeliveriesListDTO
            {
                Id = d.Id,
                UserId = d.UserId,
                OriginLat = d.OriginLat,
                OriginLng = d.OriginLng,
                DestinationLat = d.DestinationLat,
                DestinationLng = d.DestinationLng,
                RecipientName = d.RecipientName,
                RecipientEmail = d.RecipientEmail,
                RecipientPhone = d.RecipientPhone,
                PackageDetails = d.PackageDetails,
                Status = d.Status,
                TrackingNumber = d.TrackingNumber,
                OriginDescription = d.OriginDescription,
                DestinationDescription = d.DestinationDescription,
                CreatedAt = d.CreatedAt,
                EstimatedDeliveryDate = d.EstimatedDeliveryDate,
                EstimatedDeliveryTime = d.EstimatedDeliveryTime,
                EstimatedTimeFrom = d.EstimatedTimeFrom,
                EstimatedTimeTo = d.EstimatedTimeTo
            });

            return deliveriesDTO;
        }

        public async Task<DeliveriesListDTO> GetDeliveryById(int id)
        {
            var delivery = await repository.GetByIdAsync(id);
            if (delivery == null)
            {
                return null;
            }
            var deliveryDTO = new DeliveriesListDTO
            {
                Id = delivery.Id,
                UserId = delivery.UserId,
                OriginLat = delivery.OriginLat,
                OriginLng = delivery.OriginLng,
                DestinationLat = delivery.DestinationLat,
                DestinationLng = delivery.DestinationLng,
                RecipientName = delivery.RecipientName,
                RecipientEmail = delivery.RecipientEmail,
                RecipientPhone = delivery.RecipientPhone,
                PackageDetails = delivery.PackageDetails,
                Status = delivery.Status,
                TrackingNumber = delivery.TrackingNumber,
                OriginDescription = delivery.OriginDescription,
                DestinationDescription = delivery.DestinationDescription,
                CreatedAt = delivery.CreatedAt,
                EstimatedDeliveryDate = delivery.EstimatedDeliveryDate,
                EstimatedDeliveryTime = delivery.EstimatedDeliveryTime,
                EstimatedTimeFrom = delivery.EstimatedTimeFrom,
                EstimatedTimeTo = delivery.EstimatedTimeTo
            };
            return deliveryDTO;
        }
        //Create a new delivery
        public async Task<DeliveriesCreateResponseDTO> CreateDelivery(DeliveriesCreateDTO deliveryDTO)
        {
            var delivery = new Deliveries
            {
                UserId = deliveryDTO.UserId,
                OriginLat = deliveryDTO.OriginLat,
                OriginLng = deliveryDTO.OriginLng,
                DestinationLat = deliveryDTO.DestinationLat,
                DestinationLng = deliveryDTO.DestinationLng,
                RecipientName = deliveryDTO.RecipientName,
                RecipientEmail = deliveryDTO.RecipientEmail,
                RecipientPhone = deliveryDTO.RecipientPhone,
                PackageDetails = deliveryDTO.PackageDetails,
                TrackingNumber = GenerateTrackingNumber(),
                Status = "P",
                OriginDescription = deliveryDTO.OriginDescription,
                DestinationDescription = deliveryDTO.DestinationDescription,
                EstimatedDeliveryDate = deliveryDTO.EstimatedDeliveryDate,
                EstimatedDeliveryTime = deliveryDTO.EstimatedDeliveryTime,
                EstimatedTimeFrom = deliveryDTO.EstimatedTimeFrom,
                EstimatedTimeTo = deliveryDTO.EstimatedTimeTo,
            };
            await repository.AddAsync(delivery);
            await repository.SaveChangesAsync();
            return new DeliveriesCreateResponseDTO
            {
                Id = delivery.Id,
                UserId = delivery.UserId,
                OriginLat = delivery.OriginLat,
                OriginLng = delivery.OriginLng,
                DestinationLat = delivery.DestinationLat,
                DestinationLng = delivery.DestinationLng,
                RecipientName = delivery.RecipientName,
                RecipientEmail = delivery.RecipientEmail,
                RecipientPhone = delivery.RecipientPhone,
                PackageDetails = delivery.PackageDetails,
                CreatedAt = delivery.CreatedAt,
                Status = delivery.Status,
                TrackingNumber = delivery.TrackingNumber,
                OriginDescription = delivery.OriginDescription,
                DestinationDescription = delivery.DestinationDescription,
                EstimatedDeliveryDate = delivery.EstimatedDeliveryDate,
                EstimatedDeliveryTime = delivery.EstimatedDeliveryTime,
                EstimatedTimeFrom = delivery.EstimatedTimeFrom,
                EstimatedTimeTo = delivery.EstimatedTimeTo
            };
        }
        //Update a delivery
        public void UpdateDelivery(DeliveriesUpdateDTO deliveryDTO)
        {
            var delivery = repository.GetById(deliveryDTO.Id);
            if (delivery == null)
                throw new Exception("Delivery not found");

            delivery.UserId = deliveryDTO.UserId;
            delivery.OriginLat = deliveryDTO.OriginLat;
            delivery.OriginLng = deliveryDTO.OriginLng;
            delivery.DestinationLat = deliveryDTO.DestinationLat;
            delivery.DestinationLng = deliveryDTO.DestinationLng;
            delivery.RecipientName = deliveryDTO.RecipientName;
            delivery.RecipientEmail = deliveryDTO.RecipientEmail;
            delivery.RecipientPhone = deliveryDTO.RecipientPhone;
            delivery.PackageDetails = deliveryDTO.PackageDetails;

            repository.Update(delivery);
            repository.SaveChanges();
        }
        //Delete a delivery( Change status to deleted)
        public void DeleteDelivery(int id)
        {
            var delivery = repository.GetById(id);
            if (delivery == null)
                throw new Exception("Delivery not found");
            delivery.Status = "D";
            repository.Update(delivery);
            repository.SaveChanges();
        }
        //Get deliveries by tracking number
        public async Task<DeliveriesListDTO> GetDeliveryByTrackingNumber(string trackingNumber)
        {
            var delivery = await deliveryRepository.GetDeliveryByTrackingNumberAsync(trackingNumber);
            if (delivery == null)
            {
                return null;
            }
            var deliveryDTO = new DeliveriesListDTO
            {
                Id = delivery.Id,
                UserId = delivery.UserId,
                OriginLat = delivery.OriginLat,
                OriginLng = delivery.OriginLng,
                DestinationLat = delivery.DestinationLat,
                DestinationLng = delivery.DestinationLng,
                RecipientName = delivery.RecipientName,
                RecipientEmail = delivery.RecipientEmail,
                RecipientPhone = delivery.RecipientPhone,
                PackageDetails = delivery.PackageDetails,
                Status = delivery.Status,
                TrackingNumber = delivery.TrackingNumber,
                OriginDescription = delivery.OriginDescription,
                DestinationDescription = delivery.DestinationDescription,
                CreatedAt = delivery.CreatedAt,
                EstimatedDeliveryDate = delivery.EstimatedDeliveryDate,
                EstimatedDeliveryTime = delivery.EstimatedDeliveryTime,
                EstimatedTimeFrom = delivery.EstimatedTimeFrom,
                EstimatedTimeTo = delivery.EstimatedTimeTo
            };
            return deliveryDTO;
        }

        public async Task UpdateDeliveryStatusAsync(DeliveryStatusUpdateDTO statusUpdateDTO)
        {
            var delivery = await repository.GetByIdAsync(statusUpdateDTO.Id);
            if (delivery == null)
                throw new Exception("Delivery not found");
            // Validar y asignar el nuevo estado
            if (!Enum.TryParse<DeliveryStatus>(statusUpdateDTO.Status, out var newStatus))
                throw new Exception("Invalid status value");
            delivery.StatusEnum = newStatus;
            repository.Update(delivery);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateDeliveryStatusByTrackingAsync(DeliveryStatusUpdateByTrackingDTO statusUpdateDTO)
        {
            var delivery = await deliveryRepository.GetDeliveryByTrackingNumberAsync(statusUpdateDTO.TrackingNumber);
            if (delivery == null)
                throw new Exception("Delivery not found");
            if (!Enum.TryParse<DeliveryStatus>(statusUpdateDTO.Status, out var newStatus))
                throw new Exception("Invalid status value");
            delivery.StatusEnum = newStatus;
            repository.Update(delivery);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeliveriesListDTO>> GetDeliveriesByUserIdAsync(int userId)
        {
            var deliveries = await deliveryRepository.GetDeliveriesByUserIdAsync(userId);
            return deliveries.Select(d => new DeliveriesListDTO
            {
                Id = d.Id,
                UserId = d.UserId,
                OriginLat = d.OriginLat,
                OriginLng = d.OriginLng,
                DestinationLat = d.DestinationLat,
                DestinationLng = d.DestinationLng,
                RecipientName = d.RecipientName,
                RecipientEmail = d.RecipientEmail,
                RecipientPhone = d.RecipientPhone,
                PackageDetails = d.PackageDetails,
                Status = d.Status,
                TrackingNumber = d.TrackingNumber,
                OriginDescription = d.OriginDescription,
                DestinationDescription = d.DestinationDescription,
                CreatedAt = d.CreatedAt,
                EstimatedDeliveryDate = d.EstimatedDeliveryDate,
                EstimatedDeliveryTime = d.EstimatedDeliveryTime,
                EstimatedTimeFrom = d.EstimatedTimeFrom,
                EstimatedTimeTo = d.EstimatedTimeTo
            });
        }

        private string GenerateTrackingNumber()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            // Letras aleatorias
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var suffix = new string(Enumerable.Range(0, 3)
                .Select(_ => chars[random.Next(chars.Length)]).ToArray());

            return $"DLV-{timestamp}-{suffix}";
        }

    }
}
