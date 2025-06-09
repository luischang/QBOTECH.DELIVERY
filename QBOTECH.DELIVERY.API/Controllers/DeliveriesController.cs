using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.DTOs;
using QBOTECH.DELIVERY.CORE.Interfaces;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveriesService _deliveriesService;
        public DeliveriesController(IDeliveriesService deliveriesService)
        {
            _deliveriesService = deliveriesService;
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
    }
}
