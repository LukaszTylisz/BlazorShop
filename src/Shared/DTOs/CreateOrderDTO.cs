using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class CreateOrderDto
{
    [Display(Name = "Customer")]
    [Required]
    public Guid? CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string City { get; set; } = default!;

    [Required]
    [StringLength(100)]
    public string Street { get; set; } = default!;

    public List<CreateOrderItemDto> OrderItems { get; set; } = default!;
}

public abstract class CreateOrderItemDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}