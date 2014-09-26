using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.Helper
{
    public class FactoryHelper
    {
        public static Object Create(string classname)
        {
            return Activator.CreateInstance("DSL_www", typeName: classname).Unwrap();
        }
    }
}
