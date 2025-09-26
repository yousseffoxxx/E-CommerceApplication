namespace Presentation.Controllers
{
    // BaseUrl/api/Payment
    public class PaymentsController(IServiceManager _serviceManager) : ApiController
    {
        // POST BaseUrl/api/Payment/basketId
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);

            return Ok(result);
        }

        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await _serviceManager.PaymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]);

            return new EmptyResult();
        }
        
    }
}

