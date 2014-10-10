using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.FieldModel
{
    public class Field
    {
        protected string Container = "";
        protected IList<IPlug> Plugs = null; 

        /// <summary>
        /// 将字段的内容显示到页面中
        /// </summary>
        /// <returns></returns>
        public string W()
        {
            return Container;
        }

        public Field()
        {
        }

        public void Init()
        {
            RenderContainer();
        }

        private void RenderContainer()
        {
            foreach (var plug in Plugs)
            {
                Container = plug.RenderFiled(Container);
            }
        }

        public void AddPlug(IPlug plug)
        {
            Plugs.Add(plug);
        }
    }
}
