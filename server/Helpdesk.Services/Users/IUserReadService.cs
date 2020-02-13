using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.Services.Users
{
	public interface IUserReadService
	{
		Task<IActionResult> GetTechnicians();
	}
}
