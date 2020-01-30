using Microsoft.EntityFrameworkCore;
using System;

namespace Helpdesk.Data
{
    /// <summary>
    /// Implementatio of this interface is used for mapping table with entities and insert intial records
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// table map method maps the entities with table
        /// </summary>
        /// <param name="builder"></param>
        void TableMap(ModelBuilder builder);
    }
}
