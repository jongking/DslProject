using System.Collections.Generic;
using System.Text;

namespace DSL_lib.FieldModel
{
    public enum ContextCell
    {
        FieldName
    }

    public class WebField : Field
    {
        protected IList<IPlug<WebField>> Plugs = new List<IPlug<WebField>>();

        /// <summary>
        ///     添加插件
        /// </summary>
        /// <param name="plug"></param>
        /// <returns></returns>
        public WebField AddPlugs(IPlug<WebField> plug)
        {
            Plugs.Add(plug);
            return this;
        }

        public override void Handle(string eventName, EventContext eventContext)
        {
            foreach (var plug in Plugs)
            {
                plug.Handle(eventName, this, eventContext);
            }
        }

        public WebField(string FieldName = "")
        {
            //设置上下文
            SetAttr(ContextCell.FieldName, FieldName);
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

        public string GetAttribute(ContextCell key)
        {
            return GetAttribute((int) key);
        }

        protected Field SetAttr(ContextCell key, string value)
        {
            return SetAttr((int) key, value);
        }

        protected Field AddAttr(ContextCell key, string value)
        {
            return AddAttr((int) key, value);
        }

    }
}