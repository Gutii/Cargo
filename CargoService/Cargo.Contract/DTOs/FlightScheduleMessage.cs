using IDeal.Common.Messaging.Shedule;
using System;

namespace Cargo.Contract.DTOs;

public class FlightScheduleMessage : FlightSheduleChanged
{
    public ulong Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Number { get; set; }
    public DateTime FlightDate { get; set; }
    public DateTime StOrigin { get; set; }
    public DateTime StDestination { get; set; }
    public DateTime StOriginLocal { get; set; }
    public DateTime StDestinationLocal { get; set; }
    public int State { get; set; }
    public int SaleState { get; set; }
    public string AircraftRegistration { get; set; }
    public string AircraftType { get; set; }
    public string SHR { get; set; }
    public double? PayloadWeight { get; set; }
    public double? PayloadVolume { get; set; }
    public bool? CanBooking { get; set; }
    public int CustomerId { get; }
}