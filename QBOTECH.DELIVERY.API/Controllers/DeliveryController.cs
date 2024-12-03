using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        // GET: api/Delivery
        [HttpGet]
        public async Task<IActionResult> GetDeliveries()
        {
            var deliveries = await _deliveryRepository.GetAllAsync();
            return Ok(deliveries);
        }

        // GET: api/Delivery/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryById(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
            {
                return NotFound(new { Message = "Delivery not found" });
            }

            return Ok(delivery);
        }

        // POST: api/Delivery
        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromBody] Deliveries delivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _deliveryRepository.AddAsync(delivery);
            await _deliveryRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeliveryById), new { id = delivery.Id }, delivery);
        }

        // PUT: api/Delivery/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDelivery(int id, [FromBody] Deliveries delivery)
        {
            if (id != delivery.Id)
            {
                return BadRequest(new { Message = "Delivery ID mismatch" });
            }

            var existingDelivery = await _deliveryRepository.GetByIdAsync(id);
            if (existingDelivery == null)
            {
                return NotFound(new { Message = "Delivery not found" });
            }

            existingDelivery.PickupAddress = delivery.PickupAddress;
            existingDelivery.DeliveryAddress = delivery.DeliveryAddress;
            existingDelivery.Status = delivery.Status;

            _deliveryRepository.Update(existingDelivery);
            await _deliveryRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Delivery/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
            {
                return NotFound(new { Message = "Delivery not found" });
            }

            _deliveryRepository.Delete(delivery);
            await _deliveryRepository.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Delivery/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateDeliveryStatus(int id, [FromBody] string status)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
            {
                return NotFound(new { Message = "Delivery not found" });
            }

            delivery.Status = status;
            _deliveryRepository.Update(delivery);
            await _deliveryRepository.SaveChangesAsync();

            return Ok(delivery);
        }

        // GET: api/Delivery/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDeliveriesByUserId(int userId)
        {
            var deliveries = await _deliveryRepository.GetDeliveriesByUserIdAsync(userId);
            return Ok(deliveries);
        }
    }

}