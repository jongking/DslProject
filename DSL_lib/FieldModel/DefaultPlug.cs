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
                    NoThisEventHandle(field, eventName);
                    break;
            }
        }

        protected void NoThisEventHandle(WebField field, DslEvent eventName)
        {
            field.OutPutStream = string.Format("<script>console.log('{1}: 没有处理 {0}')</script>", eventName.ToString(), field.Dslmodel.ResourceName);
//            field.OutPutStream = "<script>location.href='./error'</script>";
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
            sb.Append("<ul id='menuDiv' class='nav navbar-nav'>");
            foreach (var dcb in DslCacheHelper.HashCache.Values)
            {
                var title = dcb.GetTitle();
                var pagelist = dcb.GetAllPageMapKey();
                if (!HasPage(pagelist))
                {
                    continue;
                }
                sb.AppendFormat("<li id='{1}-dropdown' class='dropdown'>" +
                                "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>{0} <span class='caret'></span></a>" +
                                "<ul class='dropdown-menu' role='menu'>", title, dcb.ResourceName);
                foreach (var page in pagelist)
                {
                    if (_transactionlist.ContainsKey(page))
                    {
                        sb.AppendFormat("<li id='{2}-{1}-dropdown' onclick=\"GOTO('/{1}/{2}')\"><a href='#'>{0}</a></li>", _transactionlist[page], dcb.ResourceName, page);  
                    }
                }
                sb.Append("</ul></li>");
            }
            sb.Append("</ul>");
            field.OutPutStream += sb.ToString();
        }

        private bool HasPage(IEnumerable<string> pagelist)
        {
            return pagelist.Any(page => _transactionlist.ContainsKey(page));
        }
    }

    public class InputEmailPlug : BasePlug
    {
        private string _textName = "";
        private string _inputId = "";
        private string _placeholder;

        public InputEmailPlug(string textName, string inputId = "InputEmail", string placeholder = "")
        {
            _textName = textName;
            _inputId = inputId;
            this._placeholder = placeholder;
        }

        public override void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.NewContext:
                    DefaultHandle(field);
                    break;
                default:
                    base.Handle(eventName, field);
                    break;
            }
        }

        private void DefaultHandle(WebField field)
        {
            string result = "";
            result += string.Format("<div class='form-group'><label for='{1}'>{0}</label><input type='email' class='form-control' id='{1}' placeholder='{2}'></div>", _textName, _inputId, _placeholder);
            field.OutPutStream += result;
        }
    }

    public class InputTextPlug : BasePlug
    {
        private string _textName = "";
        private string _inputId = "";
        private string _placeholder;

        public InputTextPlug(string textName, string inputId = "InputText", string placeholder = "")
        {
            _textName = textName;
            _inputId = inputId;
            this._placeholder = placeholder;
        }

        public override void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.NewContext:
                    DefaultHandle(field);
                    break;
                default:
                    base.Handle(eventName, field);
                    break;
            }
        }

        private void DefaultHandle(WebField field)
        {
            string result = "";
            result += string.Format("<div class='form-group'><label for='{1}'>{0}</label><input type='text' class='form-control' id='{1}' placeholder='{2}'></div>", _textName, _inputId, _placeholder);
            field.OutPutStream += result;
        }
    }

    public class InputPasswordPlug : BasePlug
    {
        private string _textName = "";
        private string _inputId = "";
        private string _placeholder;

        public InputPasswordPlug(string textName, string inputId = "InputPassword", string placeholder = "")
        {
            _textName = textName;
            _inputId = inputId;
            this._placeholder = placeholder;
        }

        public override void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.NewContext:
                    DefaultHandle(field);
                    break;
                default:
                    base.Handle(eventName, field);
                    break;
            }
        }

        private void DefaultHandle(WebField field)
        {
            string result = "";
            result += string.Format("<div class='form-group'><label for='{1}'>{0}</label><input type='password' class='form-control' id='{1}' placeholder='{2}'></div>", _textName, _inputId, _placeholder);
            field.OutPutStream += result;
        }
    }

    public class SubmitButtonPlug : BasePlug
    {
        private string _textName = "";
        private string _inputId = "";

        public SubmitButtonPlug(string textName, string inputId = "SubmitButton")
        {
            _textName = textName;
            _inputId = inputId;
        }

        public override void Handle(DslEvent eventName, WebField field)
        {
            switch (eventName)
            {
                case DslEvent.NewContext:
                    DefaultHandle(field);
                    break;
                default:
                    base.Handle(eventName, field);
                    break;
            }
        }

        private void DefaultHandle(WebField field)
        {
            string result = "";
            result += string.Format("<button type='submit' class='btn btn-primary' id='{1}'>{0}</button>", _textName, _inputId);
            field.OutPutStream += result;
        }
    }
//    <button type="button" class="btn btn-primary">Primary</button>
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