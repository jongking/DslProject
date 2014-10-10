using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.FieldModel
{
    public abstract class Field
    {
        protected string Container = "";
        /// <summary>
        /// 将字段的内容显示到页面中
        /// </summary>
        /// <returns></returns>
        public string W()
        {
            return Container;
        }
    }

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
