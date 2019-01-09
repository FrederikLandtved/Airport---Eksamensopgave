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
		private readonly HttpClient _httpClient;
		private Uri BaseEndPoint { get; set; }


		public FlightsController()
		{
			BaseEndPoint = new Uri("http://localhost:63898/api/flights/");
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

			var response = await _httpClient.GetAsync(BaseEndPoint + $"/{id}", HttpCompletionOption.ResponseHeadersRead);

			var data = await response.Content.ReadAsStringAsync();
			var flight = JsonConvert.DeserializeObject<Flight>(data);

			if (flight == null)
			{
				return NotFound();
			}
			return View(flight);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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
					var response = await _httpClient.PutAsJsonAsync<Flight>(BaseEndPoint + $"/{id}", flight);
					response.EnsureSuccessStatusCode();
				}
				catch (HttpRequestException)
				{
					if (!await FlightExists(flight.FlightId))
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


		private async Task<bool> FlightExists(int id)
		{
			var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			var context = JsonConvert.DeserializeObject<List<Flight>>(data);
			return context.Any(e => e.FlightId == id);
		}

	}
}