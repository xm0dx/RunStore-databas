using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Models;

public record class SalesOrder
{
    public int OrderId { get; set; }
    public required string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public required Customer Customer { get; set; }
    public required List<OrderItem> Items { get; set; }
    public int OrderSum { get; set; }
}
