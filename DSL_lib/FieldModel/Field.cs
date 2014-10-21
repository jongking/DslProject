using System.Collections.Generic;

namespace DSL_lib.FieldModel
{
    /// <summary>
    /// Field基类负责保存字段的上下文
    /// </summary>
    public class Field
    {
        private DslClassBase _dslmodel = null;

        private readonly Dictionary<int, string> _attribute = new Dictionary<int, string>();
        protected Dictionary<int, string> Attribute
        {
            get { return _attribute; }
        }

        private string _outPutStream;
        public string OutPutStream
        {
            set { _outPutStream = value; }
            get { return _outPutStream; }
        }

        public DslClassBase Dslmodel
        {
            get { return _dslmodel; }
            set { _dslmodel = value; }
        }

        protected string GetAttribute(int key)
        {
            return Attribute.ContainsKey(key) ? Attribute[key] : "";
        }


        public virtual string Write(string eventName)
        {
            return _outPutStream;
        }
    }
}