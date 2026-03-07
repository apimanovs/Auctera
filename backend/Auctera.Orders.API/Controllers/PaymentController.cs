using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Orders.Application.Commands;
using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Application.Service;
using Auctera.Orders.Infrastructure.Options;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stripe;
using Stripe.Checkout;

namespace Auctera.Orders.API.Controllers;

[ApiController]
[Route("api/payments/stripe")]
public sealed class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly StripeOptions _stripeOptions;
    private readonly IOrderRepository _orderRepository;
    private readonly PaymentService _paymentService;

    public PaymentController(IMediator mediator, StripeOptions stripeOptions,
        IOrderRepository orderRepository, PaymentService paymentService)
    {
        _mediator = mediator;
        _stripeOptions = stripeOptions;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    [HttpPost]
    [Route("orders/{id:guid}/pay")]
    public async Task<ActionResult> CreatePaymentSession(Guid id , CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.First(c => c.Type == "sub").Value);

        var command = new CreateOrderPaymentSessionCommand(id, userId);

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("webhook")]
    public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken)
    {
        string json;

        using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
        {
            json = await reader.ReadToEndAsync(cancellationToken);
        }

        var signatureHeader = Request.Headers["Stripe-Signature"];

        Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                signatureHeader,
                _stripeOptions.WebhookSecret);
        }
        catch (StripeException)
        {
            return BadRequest("Invalid Stripe signature.");
        }
        catch (Exception)
        {
            return BadRequest("Invalid webhook payload.");
        }

        switch (stripeEvent.Type)
        {
            case "checkout.session.completed":
            case "checkout.session.async_payment_succeeded":
                {
                    var session = stripeEvent.Data.Object as Session;

                    if (session is null)
                    {
                        return BadRequest("Session object not found.");
                    }

                    await _paymentService.HandleSuccessfulCheckoutSession(session, cancellationToken);
                    break;
                }

            case "checkout.session.async_payment_failed":
                {
                    var session = stripeEvent.Data.Object as Session;

                    if (session is null)
                    {
                        return BadRequest("Session object not found.");
                    }

                    await _paymentService.HandleFailedCheckoutSession(session, cancellationToken);
                    break;
                }

            case "checkout.session.expired":
                {
                    var session = stripeEvent.Data.Object as Session;

                    if (session is null)
                    {
                        return BadRequest("Session object not found.");
                    }

                    await _paymentService.HandleExpiredCheckoutSession(session, cancellationToken);
                    break;
                }
        }

        return Ok();
    }
}
