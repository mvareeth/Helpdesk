using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data
{
    public class DbTransaction : ITransaction
    {
        private readonly IDbContextTransaction efTransaction;

        public DbTransaction(IDbContextTransaction efTransaction)
        {
            this.efTransaction = efTransaction;
        }

        public void Commit()
        {
            efTransaction.Commit();
        }

        public void Rollback()
        {
            efTransaction.Rollback();
        }

        public void Dispose()
        {
            efTransaction.Dispose();
        }
    }
}
