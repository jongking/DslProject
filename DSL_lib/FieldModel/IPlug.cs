using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.FieldModel
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlug<FieldClass>
    {
        void InitPlug(FieldClass field);

        void Handle(string eventName, FieldClass field);
    }
}
