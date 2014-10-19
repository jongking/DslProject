using System;
using System.Collections.Generic;
using System.Linq;
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
        public MainPagePlug()
        {
            this.AddMenuList("new", "新增")
                .AddMenuList("modify", "修改")
                .AddMenuList("delete", "删除")
                .AddMenuList("search", "查询");
        }

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

        public MainPagePlug AddMenuList(string from, string to)
        {
            _transactionlist.Add(from, to);
            return this;
        }

        private Dictionary<string, string> _transactionlist = new Dictionary<string, string>();

        private void GetMainMenuHandle(WebField field)
        {
            var sb = new StringBuilder();
            sb.Append("<ul class='nav navbar-nav'>");
            foreach (var model in CacheHelper.HashCache.Values)
            {
                var dcb = (DslClassBase) model;
                var title = dcb.GetTitle();
                var pagelist = dcb.GetAllPageMapKey();
                if (!HasPage(pagelist))
                {
                    continue;
                }
                sb.AppendFormat("<li class='dropdown'>" +
                                "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>{0} <span class='caret'></span></a>" +
                                "<ul class='dropdown-menu' role='menu'>", title);
                foreach (var page in pagelist)
                {
                    if (_transactionlist.ContainsKey(page))
                    {
                        sb.AppendFormat("<li><a href='/{1}/{2}'>{0}</a></li>", _transactionlist[page], dcb.ResourceName, page);  
                    }
                }
                sb.Append("</ul></li>");
            }
            sb.Append("</ul>");
            field.OutPutStream = sb.ToString();
        }

        private bool HasPage(IEnumerable<string> pagelist)
        {
            return pagelist.Any(page => _transactionlist.ContainsKey(page));
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