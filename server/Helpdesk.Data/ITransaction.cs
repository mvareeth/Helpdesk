using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
