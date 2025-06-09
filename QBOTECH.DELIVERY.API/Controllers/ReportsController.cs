using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.CORE.DTOs;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly IDeliveriesService _deliveriesService;
        public ReportsController(IReportsService reportsService, IDeliveriesService deliveriesService)
        {
            _reportsService = reportsService;
            _deliveriesService = deliveriesService;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetDeliveriesByStatus([FromQuery] int? userId = null)
        {
            var result = await _reportsService.GetDeliveriesByStatusAsync(userId);
            int total = 0;
            foreach (var r in result) total += r.Count;
            return Ok(new { totalDeliveries = total, result });
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetDeliveriesByDate([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null, [FromQuery] int? userId = null)
        {
            var result = await _reportsService.GetDeliveriesByDateAsync(from, to, userId);
            int total = 0;
            foreach (var r in result) total += r.Count;
            return Ok(new { totalDeliveries = total, result });
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDeliveriesByUser()
        {
            var result = await _reportsService.GetDeliveriesByUserAsync();
            int total = 0;
            foreach (var r in result) total += r.Count;
            return Ok(new { totalDeliveries = total, result });
        }
    }
}
