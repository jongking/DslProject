using System;

namespace DSL_lib.FieldModel
{
    public class InputTextPlug : IPlug<WebField, DslEvent>
    {
//        public InputTextPlug()
//        {
//            SelfInit();
//        }
//
//        public InputTextPlug(string id = "", string classname = "", string type = "", string other = "")
//        {
//            SelfInit().SetAttr("type", type).AddAttr("class", classname).SetAttr("id", id).SetAttr("other", other);
//        }

        public void InitPlug(WebField field)
        {
        }

        public void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.Default:
                    DefaultHandle(field);
                    break;
                case DslEvent.NoThisEvent:
                    NoThisEventHandle(field);
                    break;
            }
        }

        private void NoThisEventHandle(WebField field)
        {
            field.OutPutStream = "<script>location.href='./error'</script>";
        }

        private void DefaultHandle(WebField field)
        {
            string result = "";
            result += "<div class='form-group'>";
            result += "<label for='exampleInputEmail1'>Email address</label>";
            result += "<input type='email' class='form-control' id='exampleInputEmail1' placeholder='Enter email'>";
            result += "</div>";
            field.OutPutStream = result;
        }

//
//        private InputTextPlug SelfInit()
//        {
//            SetAttr("tag", "input")
//                .SetAttr("selfclose", "true");
//            return this;
//        }
//
//        public override void InitPlug(Field field)
//        {
//            field.AddAttr("innerhtml", Html());
//        }
    }

//    public class LabelPlug : IPlug<WebField>
//    {
//        public LabelPlug()
//        {
//            SelfInit();
//        }
//
//        public LabelPlug(string label, string other = "")
//        {
//            SelfInit().SetAttr("innerhtml", label).SetAttr("other", other);
//        }
//
//        private LabelPlug SelfInit()
//        {
//            SetAttr("tag", "label");
//            return this;
//        }
//
//        public override void InitPlug(Field field)
//        {
//            field.AddAttr("innerhtml", Html());
//        }
//
//        public void InitPlug(WebField field)
//        {
//            throw new NotImplementedException();
//        }
//
//        public void Handle(string eventName, WebField field)
//        {
//            throw new NotImplementedException();
//        }
//    }
//
//    public class DivPlug : IPlug<WebField>
//    {
//        public DivPlug()
//        {
//            SelfInit();
//        }
//
//        public DivPlug(string className = "")
//        {
//            SelfInit().AddAttr("class", className);
//        }
//
//        private DivPlug SelfInit()
//        {
//            SetAttr("tag", "div");
//            return this;
//        }
//
//        public override void InitPlug(Field field)
//        {
//            field.AddAttr("innerhtml", Html());
//        }
//
//        public void InitPlug(WebField field)
//        {
//            throw new NotImplementedException();
//        }
//
//        public void Handle(string eventName, WebField field)
//        {
//            throw new NotImplementedException();
//        }
//    }
}