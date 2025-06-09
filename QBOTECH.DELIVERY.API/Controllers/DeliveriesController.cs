using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.CORE.Services;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveriesService _deliveriesService;
        private readonly DeliveryLocationService _locationService;
        public DeliveriesController(IDeliveriesService deliveriesService, DeliveryLocationService locationService)
        {
            _deliveriesService = deliveriesService;
            _locationService = locationService;
        }

        // GET: api/v1/deliveries
        [HttpGet]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _deliveriesService.GetAllDeliveriesAsync();
            return Ok(deliveries);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDeliveryById(int id)
        {
            var delivery = await _deliveriesService.GetDeliveryById(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        [HttpPost]
        public async Task<IActionResult> AddDelivery([FromBody] DeliveriesCreateDTO deliveriesCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newDelivery = await _deliveriesService.CreateDelivery(deliveriesCreateDTO);
                return CreatedAtAction(nameof(GetDeliveryById), new { id = newDelivery.Id }, newDelivery);
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating delivery");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDelivery(int id, [FromBody] DeliveriesUpdateDTO deliveriesUpdateDTO)
        {
            if (id != deliveriesUpdateDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                _deliveriesService.UpdateDelivery(deliveriesUpdateDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating delivery");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDelivery(int id)
        {
            try
            {
                _deliveriesService.DeleteDelivery(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //Handle exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting delivery");
            }
        }

        [HttpGet]
        [Route("tracking/{trackingNumber}")]
        public async Task<IActionResult> GetDeliveryByTrackingNumber(string trackingNumber)
        {
            var delivery = await _deliveriesService.GetDeliveryByTrackingNumber(trackingNumber);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateDeliveryStatus(int id, [FromBody] DeliveryStatusUpdateDTO statusUpdateDTO)
        {
            if (id != statusUpdateDTO.Id)
                return BadRequest();
            try
            {
                await _deliveriesService.UpdateDeliveryStatusAsync(statusUpdateDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating delivery status");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDeliveriesByUserId(int userId)
        {
            var deliveries = await _deliveriesService.GetDeliveriesByUserIdAsync(userId);
            return Ok(deliveries);
        }

        [HttpPost("{id}/location")]
        public async Task<IActionResult> PostLocation(int id, [FromBody] DeliveryLocationDTO dto)
        {
            if (id != dto.DeliveryId)
                return BadRequest();
            await _locationService.AddLocationAsync(dto);
            return Ok();
        }

        [HttpGet("{id}/location")]
        public async Task<IActionResult> GetLastLocation(int id)
        {
            var location = await _locationService.GetLastLocationAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpGet("{id}/locations")]
        public async Task<IActionResult> GetLocations(int id)
        {
            var locations = await _locationService.GetLocationsByDeliveryAsync(id);
            return Ok(locations);
        }

        [HttpPost("tracking/{trackingNumber}/location")]
        public async Task<IActionResult> PostLocationByTracking(string trackingNumber, [FromBody] DeliveryLocationByTrackingDTO dto)
        {
            if (!string.Equals(trackingNumber, dto.TrackingNumber, StringComparison.OrdinalIgnoreCase))
                return BadRequest();
            await _locationService.AddLocationByTrackingAsync(dto);
            return Ok();
        }

        [HttpGet("tracking/{trackingNumber}/location")]
        public async Task<IActionResult> GetLastLocationByTracking(string trackingNumber)
        {
            var location = await _locationService.GetLastLocationByTrackingAsync(trackingNumber);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpGet("tracking/{trackingNumber}/locations")]
        public async Task<IActionResult> GetLocationsByTracking(string trackingNumber)
        {
            var locations = await _locationService.GetLocationsByTrackingAsync(trackingNumber);
            return Ok(locations);
        }
    }
}
