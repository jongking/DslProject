using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSL_lib.Helper;

namespace DSL_lib.FieldModel
{
    public class BasePlug : IPlug<WebField>
    {
        public virtual void InitPlug(WebField field)
        {
        }

        public virtual void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                default:
                    NoThisEventHandle(field, eventName, Out);
                    break;
            }
        }

        protected void NoThisEventHandle(WebField field, string eventName, StringBuilder Out)
        {
            Out.AppendFormat("<script>console.log('{1}: 没有处理 {0}')</script>", eventName, field.Dslmodel.ResourceName);
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

        public override void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                case "getmainmenu":
                    GetMainMenuHandle(field, Out);
                    break;
                default:
                    base.Handle(eventName, field, Out);
                    break;
            }
        }

        public MainPagePlug AddMenuList(string from, string to)
        {
            _transactionlist.Add(from, to);
            return this;
        }

        private Dictionary<string, string> _transactionlist = new Dictionary<string, string>();

        private void GetMainMenuHandle(WebField field, StringBuilder Out)
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
            Out.Append(sb);
        }

        private bool HasPage(IEnumerable<string> pagelist)
        {
            return pagelist.Any(page => _transactionlist.ContainsKey(page));
        }
    }

    public class InputEmailPlug : BasePlug
    {
        private string _textName = "";
        private string _placeholder;

        public InputEmailPlug(string textName, string placeholder = "")
        {
            _textName = textName;
            this._placeholder = placeholder;
        }

        public override void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                case "newcontext":
                    DefaultHandle(field, Out);
                    break;
                default:
                    base.Handle(eventName, field, Out);
                    break;
            }
        }

        private void DefaultHandle(WebField field, StringBuilder Out)
        {
            Out.AppendFormat("<div class='form-group'><label for='{1}'>{0}</label><input type='email' class='form-control' id='{1}' placeholder='{2}'></div>"
                , _textName, field.GetAttribute(ContextCell.FieldName), _placeholder);
        }
    }

    public class InputTextPlug : BasePlug
    {
        private string _textName = "";
        private string _placeholder;

        public InputTextPlug(string textName, string placeholder = "")
        {
            _textName = textName;
            this._placeholder = placeholder;
        }

        public override void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                case "newcontext":
                    DefaultHandle(field, Out);
                    break;
                default:
                    base.Handle(eventName, field, Out);
                    break;
            }
        }

        private void DefaultHandle(WebField field, StringBuilder Out)
        {
            Out.AppendFormat(
                "<div class='form-group'><label for='{1}'>{0}</label><input type='text' class='form-control' id='{1}' placeholder='{2}'></div>",
                _textName, field.GetAttribute(ContextCell.FieldName), _placeholder);
        }
    }

    public class InputPasswordPlug : BasePlug
    {
        private string _textName = "";
        private string _placeholder;

        public InputPasswordPlug(string textName, string placeholder = "")
        {
            _textName = textName;
            this._placeholder = placeholder;
        }

        public override void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                case "newcontext":
                    DefaultHandle(field, Out);
                    break;
                default:
                    base.Handle(eventName, field, Out);
                    break;
            }
        }

        private void DefaultHandle(WebField field, StringBuilder Out)
        {
            Out.AppendFormat(
                "<div class='form-group'><label for='{1}'>{0}</label><input type='password' class='form-control' id='{1}' placeholder='{2}'></div>",
                _textName, field.GetAttribute(ContextCell.FieldName), _placeholder);
        }
    }

    public class SubmitButtonPlug : BasePlug
    {
        private string _textName = "";

        public SubmitButtonPlug(string textName)
        {
            _textName = textName;
        }

        public override void Handle(string eventName, WebField field, StringBuilder Out)
        {
            switch (eventName)
            {
                case "newcontext":
                    DefaultHandle(field, Out);
                    break;
                default:
                    base.Handle(eventName, field, Out);
                    break;
            }
        }

        private void DefaultHandle(WebField field, StringBuilder Out)
        {
            Out.AppendFormat("<button type='submit' class='btn btn-primary' id='{1}'>{0}</button>", _textName, field.GetAttribute(ContextCell.FieldName));
        }
    }
}