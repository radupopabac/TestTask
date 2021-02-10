using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Identity.API.Di
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetTypesForPath(this Assembly assembly, string path)
        {
            return (from t in assembly.GetTypes()
                    where t.IsClass &&
                          t.IsNotAbstractNorNested() &&
                          t.IsPathValid(path) &&
                          !t.IsGenericType
                    select t).ToList();
        }
    }
}
