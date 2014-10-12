using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Text;

namespace DSL_lib.Helper
{
    public class RazorHelper
    {
        public RawString HtmlRaw(string str)
        {
            return new RawString(str);
        }
    }
}
