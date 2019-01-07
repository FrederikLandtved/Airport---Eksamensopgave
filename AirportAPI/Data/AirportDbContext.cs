using AirportAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportAPI.Data
{
	public class AirportDbContext : DbContext
	{
		public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options)
		{
			Database.EnsureCreated();
			if (Flights.CountAsync().Result == 0)
			{
				var item = new Flight()
				{
					AircraftType = "Boeing 777 Jet",
					FromLocation = "København",
					ToLocation = "Amsterdam",
					DepartureTime = DateTime.Now,
					ArrivalTime = DateTime.Now
					
				};
				Flights.Add(item);
				SaveChanges();

				item = new Flight()
				{
					AircraftType = "Boeing 747 Jet",
					FromLocation = "København",
					ToLocation = "Rom",
					DepartureTime = DateTime.Now,
					ArrivalTime = DateTime.Now

				};
				Flights.Add(item);
				SaveChanges();

				item = new Flight()
				{
					AircraftType = "Boeing 777 Jet",
					FromLocation = "København",
					ToLocation = "Paris",
					DepartureTime = DateTime.Now,
					ArrivalTime = DateTime.Now

				};
				Flights.Add(item);
				SaveChanges();

				item = new Flight()
				{
					AircraftType = "Boeing 777 Jet",
					FromLocation = "København",
					ToLocation = "Berlin",
					DepartureTime = DateTime.Now,
					ArrivalTime = DateTime.Now

				};
				Flights.Add(item);
				SaveChanges();

				item = new Flight()
				{
					AircraftType = "Boeing 777 Jet",
					FromLocation = "København",
					ToLocation = "Madrid",
					DepartureTime = DateTime.Now,
					ArrivalTime = DateTime.Now
				};
				Flights.Add(item);
				SaveChanges();

			}
		}
		public DbSet<Flight> Flights { get; set; }
	}
}
