using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL_lib.FieldModel
{
    public class InputTextPlug : IPlug<HtmlField>
    {
        public InputTextPlug()
        {
            SelfInit();
        }

        public InputTextPlug(string id = "", string classname = "", string type = "", string other = "")
        {
            SelfInit().SetAttr("type", type).AddAttr("class", classname).SetAttr("id", id).SetAttr("other", other);
        }

        private InputTextPlug SelfInit()
        {
            SetAttr("tag", "input")
                .SetAttr("selfclose", "true");
            return this;
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }

        public void InitPlug(HtmlField field)
        {
            throw new NotImplementedException();
        }

        public void Handle(string eventName, HtmlField field)
        {
            throw new NotImplementedException();
        }
    }

    public class LabelPlug : IPlug<HtmlField>
    {
        public LabelPlug()
        {
            SelfInit();
        }

        public LabelPlug(string label, string other = "")
        {
            SelfInit().SetAttr("innerhtml", label).SetAttr("other", other);
        }

        private LabelPlug SelfInit()
        {
            SetAttr("tag", "label");
            return this;
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }

        public void InitPlug(HtmlField field)
        {
            throw new NotImplementedException();
        }

        public void Handle(string eventName, HtmlField field)
        {
            throw new NotImplementedException();
        }
    }

    public class DivPlug : IPlug<HtmlField>
    {
        public DivPlug()
        {
            SelfInit();
        }

        public DivPlug(string className = "")
        {
            SelfInit().AddAttr("class", className);
        }

        private DivPlug SelfInit()
        {
            SetAttr("tag", "div");
            return this;
        }

        public override void InitPlug(Field field)
        {
            field.AddAttr("innerhtml", Html());
        }

        public void InitPlug(HtmlField field)
        {
            throw new NotImplementedException();
        }

        public void Handle(string eventName, HtmlField field)
        {
            throw new NotImplementedException();
        }
    }
}
