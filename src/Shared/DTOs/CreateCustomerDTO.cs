using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class CreateCustomerDto
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;
}