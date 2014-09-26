using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.FieldModel
{
    public class Test
    {
        private string _inputName;
        public string InputName
        {
            get { return _inputName; }
            set { _inputName = value; }
        }

        public Test(string inputName)
        {
            InputName = inputName;
        }
    }

    public class Password : Test
    {
        public Password(string inputName)
            : base(inputName)
        {
            InputName = inputName;
        }
    }
}
