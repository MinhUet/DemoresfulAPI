using System;
using System.Collections.Generic;
using System.Text;

namespace Nhom9.Docker_Kubernet.Demo.Common.Utilities
{
    public class Utility
    {
        public static string GetEntityName<T>()
        {
            return Activator.CreateInstance<T>().GetType().Name;
        }
    }
}
