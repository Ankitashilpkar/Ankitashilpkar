using System;
using System.Collections.Generic;

namespace ProjectLearn.Models;

public partial class FlightCheck
{
    public int Id { get; set; }

    public string? FromPlace { get; set; }

    public string? ToPlace { get; set; }
}
