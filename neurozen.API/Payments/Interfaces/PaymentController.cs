using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using neurozen.API.Payments.Application.Internal.QueryService;
using neurozen.API.Payments.Domain.Models.Queries;
using neurozen.API.Payments.Domain.Repositories;
using neurozen.API.Payments.Domain.Services;
using neurozen.API.Payments.Interfaces.REST.Resources;
using neurozen.API.Payments.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace neurozen.API.Payments.Interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Payments")]
    public class PaymentController(
        IPaymentQueryService paymentQueryService) : ControllerBase
    {
        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "Get all payments (purchases) of a user")]
        [SwaggerResponse(200, "Payment found", typeof(IEnumerable<PaymentResource>))]
        [SwaggerResponse(400, "No payments found for given UserId")]
        public async Task<ActionResult<IEnumerable<PaymentResource>>> GetAllPaymentsForUserId(Guid userId)
        {
            var getAllPaymentsByUserIdQuery = new GetAllPaymentsForUserIdQuery(userId);
            var result = await paymentQueryService.Handle(getAllPaymentsByUserIdQuery);
            if(result == null || !result.Any()) return NotFound();
            var payments = result.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(payments);
        }
    }
}