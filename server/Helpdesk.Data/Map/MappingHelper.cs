using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Helpdesk.Data
{
    public static class MappingsHelper
    {
        /// <summary>
        /// get the list of mapping classes based on the IMap interface implementation.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IMap> GetMainMappings()
        {
            var assemblyTypes = typeof(ClientMap).GetTypeInfo().Assembly.DefinedTypes;
            var mappings = assemblyTypes
                // ReSharper disable once AssignNullToNotNullAttribute
                .Where(t => t.Namespace != null && t.Namespace.Contains(typeof(ClientMap).Namespace))
                .Where(t => typeof(IMap).GetTypeInfo().IsAssignableFrom(t));
            mappings = mappings.Where(x => !x.IsAbstract);
            return mappings.Select(m => (IMap)Activator.CreateInstance(m.AsType())).ToArray();
        }
    }
}
