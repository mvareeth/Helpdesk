using System;

namespace Helpdesk.Diagnostics
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
