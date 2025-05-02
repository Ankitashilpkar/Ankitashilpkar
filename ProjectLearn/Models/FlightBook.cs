using System;
using System.Collections.Generic;

namespace ProjectLearn.Models;

public partial class FlightBook
{
    public int FlightId { get; set; }

    public string? FlightName { get; set; }

    public string? FlightNumber { get; set; }

    public string? FromPlace { get; set; }

    public string? ToPlace { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public string? FlightSeat { get; set; }

    public decimal? Price { get; set; }

    public string? Class { get; set; }

    public string? SeatType { get; set; }

    public string? FreeBaggage { get; set; }

    public string? FreMeal { get; set; }

    public string? PassengerName { get; set; }

    public int? PassengerId { get; set; }

    public string? PassengerAadhaar { get; set; }

    public string? PassengerMobile { get; set; }

    public int? PassengerAge { get; set; }

    public bool? IsMedicalFit { get; set; }
}
