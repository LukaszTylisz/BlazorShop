using AutoMapper;
using BlazorShop.Application.Commands.CreateCustomer;
using BlazorShop.Application.Commands.DeleteCustomer;
using BlazorShop.Application.Commands.UpdateCustomer;
using BlazorShop.Application.Exceptions;
using BlazorShop.Application.Queries.GetCustomerById;
using BlazorShop.Application.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.DTOs;

namespace BlazorShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CustomersController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CustomersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        var customersDto = await _mediator.Send(new GetCustomersQuery());
        return Ok(customersDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(Guid id)
    {
        var customerDto = await _mediator.Send(new GetCustomerByIdQuery(id));

        return customerDto == null
            ? (ActionResult<CustomerDto>)NotFound()
            : (ActionResult<CustomerDto>)Ok(customerDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDto customerDto)
    {
        var customerCmd = new CreateCustomerCommand(Guid.NewGuid(), customerDto.Name);
        await _mediator.Send(customerCmd);

        return CreatedAtAction(nameof(GetCustomerById), ControllerNameConstants.Customers, new { id = customerCmd.Id }, customerCmd);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerDto customerDto)
    {
        if (id != customerDto.Id)
        {
            return BadRequest();
        }

        var customerCmd = new UpdateCustomerCommand(customerDto.Id, customerDto.Name);

        try
        {
            await _mediator.Send(customerCmd);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteCustomerCommand(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
