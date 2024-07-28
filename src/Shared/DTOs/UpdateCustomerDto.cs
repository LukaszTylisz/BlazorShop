using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class UpdateCustomerDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;
}