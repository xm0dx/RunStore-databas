using System;

namespace Client.Models;

public class Supplier
{
    public int Id { get; set; }
    public required string Name { get; set; }

    // combobox visar namnet
    public override string ToString()
    {
        return Name;
    }
}
