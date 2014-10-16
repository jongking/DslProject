using System;
using System.Text;
using DSL_lib.Helper;

namespace DSL_lib.FieldModel
{
    public class BasePlug : IPlug<WebField, DslEvent>
    {
        public virtual void InitPlug(WebField field)
        {
        }

        public virtual void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.NoThisEvent:
                    NoThisEventHandle(field);
                    break;
            }
        }

        protected void NoThisEventHandle(WebField field)
        {
            field.OutPutStream = "<script>location.href='./error'</script>";
        }
    }

    public class MainPagePlug : BasePlug
    {
        public override void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.GetMainMenu:
                    GetMainMenuHandle(field);
                    break;
                default:
                    base.Handle(eventName, field);
                    break;
            }
        }

//        <ul class='nav navbar-nav'>
//        <li class='active'><a href='#'>Link</a></li>
//        <li><a href='#'>Link</a></li>
//        <li class='dropdown'>
//          <a href='#' class='dropdown-toggle' data-toggle='dropdown'>Dropdown <span class='caret'></span></a>
//          <ul class='dropdown-menu' role='menu'>
//            <li class='active'><a href='#'>Action</a></li>
//            <li><a href='#'>Another action</a></li>
//            <li><a href='#'>Something else here</a></li>
//            <li class='divider'></li>
//            <li><a href='#'>Separated link</a></li>
//            <li class='divider'></li>
//            <li><a href='#'>One more separated link</a></li>
//          </ul>
//        </li>
//      </ul>
        private void GetMainMenuHandle(WebField field)
        {
            var sb = new StringBuilder();
            sb.Append("<ul class='nav navbar-nav'>");
            foreach (var model in CacheHelper.HashCache.Values)
            {
                var dcb = (DslClassBase) model;
                var title = dcb.GetTitle();
                var pagelist = dcb.GetAllPageMapKey();
                sb.AppendFormat("<li class='dropdown'>" +
                                "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>{0} <span class='caret'></span></a>" +
                                "<ul class='dropdown-menu' role='menu'>", title);
                foreach (var page in pagelist)
                {
                    sb.AppendFormat("<li><a href='#'>{0}</a></li>", page);                    
                }
                sb.Append("</ul></li>");
            }
            field.OutPutStream = sb.ToString();
        }
    }

    public class InputTextPlug : BasePlug
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

        public override void Handle(DslEvent eventName, WebField field)
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