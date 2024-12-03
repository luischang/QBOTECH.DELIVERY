using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QBOTECH.DELIVERY.CORE.Entities;
using QBOTECH.DELIVERY.CORE.Interfaces;

namespace QBOTECH.DELIVERY.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly IRepository<SubscriptionPlans> _subscriptionPlanRepository;

        public SubscriptionPlanController(IRepository<SubscriptionPlans> subscriptionPlanRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        // GET: api/SubscriptionPlan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionPlans>>> GetSubscriptionPlans()
        {
            var plans = await _subscriptionPlanRepository.GetAllAsync();
            return Ok(plans);
        }

        // GET: api/SubscriptionPlan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionPlans>> GetSubscriptionPlan(int id)
        {
            var plan = await _subscriptionPlanRepository.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(plan);
        }

        // POST: api/SubscriptionPlan
        [HttpPost]
        public async Task<ActionResult<SubscriptionPlans>> CreateSubscriptionPlan(SubscriptionPlans plan)
        {
            await _subscriptionPlanRepository.AddAsync(plan);
            await _subscriptionPlanRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSubscriptionPlan), new { id = plan.Id }, plan);
        }

        // PUT: api/SubscriptionPlan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscriptionPlan(int id, SubscriptionPlans plan)
        {
            if (id != plan.Id)
            {
                return BadRequest();
            }

            var existingPlan = await _subscriptionPlanRepository.GetByIdAsync(id);
            if (existingPlan == null)
            {
                return NotFound();
            }

            existingPlan.Name = plan.Name;
            existingPlan.Price = plan.Price;
            existingPlan.DeliveryLimit = plan.DeliveryLimit;

            _subscriptionPlanRepository.Update(existingPlan);
            await _subscriptionPlanRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/SubscriptionPlan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriptionPlan(int id)
        {
            var plan = await _subscriptionPlanRepository.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            _subscriptionPlanRepository.Delete(plan);
            await _subscriptionPlanRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
