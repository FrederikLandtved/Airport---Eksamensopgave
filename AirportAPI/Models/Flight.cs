using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirportAPI.Models
{
	public class Flight
	{
		[Key]
		public int FlightId { get; set; }
		public string AircraftType { get; set; }
		public string FromLocation { get; set; }
		public string ToLocation { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }


	}
}
