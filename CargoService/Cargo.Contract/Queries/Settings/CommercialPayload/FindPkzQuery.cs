using Cargo.Contract.DTOs.Settings;
using System;

namespace Cargo.Contract.Queries.Settings
{
    public class FindPkzQuery : IQuery<PkzDto>
    {
        public string IataCode { get; set; }
        public string OnboardNumber { get; set; }       //0
        public string AircraftType { get; set; }        //1
        public DateTime? DateStart { get; set; }        //2
        public DateTime? DateEnd { get; set; }          //2
        public string FlightNumber { get; set; }        //3
        public string Origin { get; set; }              //4
        public string Destination { get; set; }         //4
        public string AccseptedShr { get; set; }
    }
}
