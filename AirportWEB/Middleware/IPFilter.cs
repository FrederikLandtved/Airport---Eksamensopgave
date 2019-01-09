using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportWEB.Middleware
{
	public class IPFilter
	{
		private readonly RequestDelegate _next;
		private readonly string[] _whiteListedIPs = { "127.0.0.1" };

		public IPFilter(RequestDelegate next)
		{
			_next = next;
		}

		public async Task IpBan(HttpContext context)
		{
			if(_whiteListedIPs.Any(i => i == context.Connection.RemoteIpAddress.ToString()))
			{
				await _next.Invoke(context);

			}
			else
			{
				context.Response.StatusCode = 401;
			}
		}

	}
}
