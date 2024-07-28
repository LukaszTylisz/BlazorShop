using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class UpdateProductDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    [Range(1, 1000)]
    public decimal Price { get; set; }
}