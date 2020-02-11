using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Repository.Security
{
	public interface ISecurityReadRepository
	{
		public int? GetAccountId(string userName, string password);
	}
}
