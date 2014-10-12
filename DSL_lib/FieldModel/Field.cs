using System.Collections.Generic;

namespace DSL_lib.FieldModel
{
    public class Field : IPlug
    {
        protected IList<IPlug> Plugs = new List<IPlug>();
        private readonly Dictionary<string, string> _attribute = new Dictionary<string, string>();

        public Dictionary<string, string> Attribute
        {
            get { return _attribute; }
        }

        /// <summary>
        /// 将字段的内容转化为Html语法
        /// </summary>
        /// <returns></returns>
        public string Html()
        {
            string result = "";
            if (GetAttribute("selfclose") != "true")
            {
                result = string.Format("<{0} class='{1}' {2} {3} style='{4}' {5} {6} {7} >{8}</{0}>",
                    A("tag"), A("class"), A("id", "id"), A("type", "type"), A("style"), A("data"), A("event"),
                    A("other"), A("innerhtml"));
            }
            else
            {
                result = string.Format("<{0} class='{1}' {2} {3} style='{4}' {5} {6} {7} />",
                    A("tag"), A("class"), A("id", "id"), A("type", "type"), A("style"), A("data"), A("event"),
                    A("other"));
            }
            return result;
        }


        public string GetAttribute(string key)
        {
            return Attribute.ContainsKey(key) ? Attribute[key] : "";
        }

        public Field InitPlugs()
        {
            foreach (var plug in Plugs)
            {
                plug.InitPlug(this);
            }
            return this;
        }

        public Field AddPlug(IPlug plug)
        {
            Plugs.Add(plug);
            return this;
        }

        private string A(string key)
        {
            return GetAttribute(key);
        }

        private string A(string key, string attrName)
        {
            string attr = GetAttribute(key);
            return attr.Length > 0 ? string.Format("{0}='{1}'", attrName, attr) : "";
        }

        public Field SetAttr(string key, string value)
        {
            Attribute[key] = value;
            return this;
        }

        public Field AddAttr(string key, string value)
        {
            Attribute[key] = GetAttribute(key) + " " + value;
            return this;
        }

        public virtual void InitPlug(Field field)
        {
        }

        public Field()
        {
            
        }
    }
}