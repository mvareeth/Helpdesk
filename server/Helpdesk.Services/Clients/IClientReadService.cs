using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.Services.Clients
{
	public interface IClientReadService
	{
		Task<IActionResult> GetClientDetail(int clientId);
	}
}
