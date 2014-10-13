using System.Collections.Generic;

namespace DSL_lib.FieldModel
{
    /// <summary>
    /// Field基类负责保存字段的上下文
    /// </summary>
    public class Field
    {
//        protected IList<IPlug> Plugs = new List<IPlug>();

        private readonly Dictionary<int, string> _attribute = new Dictionary<int, string>();
        protected Dictionary<int, string> Attribute
        {
            get { return _attribute; }
        }

        private string _outPutStream;
        public string OutPutStream
        {
            set { _outPutStream = value; }
        }

        protected string GetAttribute(int key)
        {
            return Attribute.ContainsKey(key) ? Attribute[key] : "";
        }

        protected Field SetAttr(int key, string value)
        {
            Attribute[key] = value;
            return this;
        }

        protected Field AddAttr(int key, string value)
        {
            Attribute[key] = GetAttribute(key) + " " + value;
            return this;
        }

        public virtual string Write()
        {
            return _outPutStream;
        }
    }
}