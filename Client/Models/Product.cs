using System;

namespace Client.Models;

public record Product
{
    // id är serial i tabellen, därför int
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public required string ItemNumber { get; set; }
    public required string Name { get; set; }
    public required Supplier Supplier { get; set; }
    public int Price { get; set; }
}
