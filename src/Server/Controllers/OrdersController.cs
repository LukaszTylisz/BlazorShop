using AutoMapper;
using BlazorShop.Application.Commands.ChangeOrderStatus;
using BlazorShop.Application.Commands.CreateOrder;
using BlazorShop.Application.Exceptions;
using BlazorShop.Application.Queries.GetOrderById;
using BlazorShop.Application.Queries.GetOrders;
using BlazorShop.Application.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.DTOs;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlazorShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class OrdersController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public OrdersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var ordersDto = await _mediator.Send(new GetOrdersQuery());
        return Ok(ordersDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
    {
        var orderDto = await _mediator.Send(new GetOrderByIdQuery(id));
        if (orderDto is null)
            return NotFound();

        return Ok(orderDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
    {
        var orderItemsCmd = new List<CreateOrderItemCommand>();

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            var productDto = await _mediator.Send(new GetProductByIdQuery(orderItemDto.ProductId));
            orderItemsCmd.Add(new CreateOrderItemCommand(orderItemDto.ProductId, orderItemDto.Quantity, productDto.Price));
        }

        var orderCmd = new CreateOrderCommand(Guid.NewGuid(), orderDto.CustomerId.Value, orderDto.City, orderDto.Street, orderItemsCmd);
        await _mediator.Send(orderCmd);

        return CreatedAtAction(nameof(GetOrderById), ControllerNameConstants.Orders, new { id = orderCmd.Id }, orderCmd);
    }

    [HttpPut("{id}/Status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeOrderStatus(Guid id, [FromBody][EnumDataType(typeof(OrderStatus))] OrderStatus orderStatus)
    {
        try
        {
            await _mediator.Send(new ChangeOrderStatusCommand(id, orderStatus));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}