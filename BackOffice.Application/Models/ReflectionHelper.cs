using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Models
{
    public static class ReflectionHelper
    {
        public static bool IsAssignableFromBaseTypeGeneric(this object obj, Type type)
        {
            return obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(type);
        }
    }
}
