using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AirportAPI.Data;
using AirportAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AirportWEB.Controllers
{
    public class FlightsController : Controller
    {
		private readonly AirportDbContext _context;
		private readonly HttpClient _httpClient;
		private Uri BaseEndPoint { get; set; }


		public FlightsController(AirportDbContext context)
		{
			BaseEndPoint = new Uri("http://localhost:63898/api/flights/");
			_context = context;
			_httpClient = new HttpClient();
		}

		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return View(JsonConvert.DeserializeObject<List<Flight>>(data));
		}

		[HttpGet]
		public async Task<IActionResult> GetSpecificFlight(string fromLocation, string toLocation)
		{
			var response = await _httpClient.GetAsync(BaseEndPoint + "fromLocation=" + fromLocation + "&toLocation=" + toLocation, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return View(JsonConvert.DeserializeObject<List<Flight>>(data));
		}


		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var flight = await _context.Flights.SingleOrDefaultAsync(m => m.FlightId == id);
			if (flight == null)
			{
				return NotFound();
			}
			return View(flight);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Flight flight)
		{
			if (id != flight.FlightId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(flight);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!FlightExists(flight.FlightId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(flight);
		}


		private bool FlightExists(int id)
		{
			return _context.Flights.Any(e => e.FlightId == id);
		}

	}
}