using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL_lib.FieldModel;

namespace DSL_lib
{

    public class DslClassBase
    {
        // 记录资源名(Resource)
        private readonly IList<Field> _fields = new List<Field>();
        private string _pageLayout = "_layout";
        private Dictionary<string, string> _pageMap = new Dictionary<string, string>();
        private string _pageTitle = "还没有题目";
        private string _resourceName;

        public string ResourceName
        {
            get { return _resourceName; }
        }

        // 记录字段信息(用于取代view和control)

        public IList<Field> Fields
        {
            get { return _fields; }
        }

        // 记录动作(Action)到页面(模板)的映射
        protected Dictionary<string, string> PageMap
        {
            set { _pageMap = value; }
        }

        public string RenderFields(string eventname)
        {
            var sb = new StringBuilder();
            foreach (Field field in Fields)
            {
                sb.Append(field.Write(eventname));
            }
            return sb.ToString();
        }

        public string GetPageMap(string action)
        {
            return _pageMap.ContainsKey(action) ? _pageMap[action] : "error";
        }

        public bool HasPageMap(string action)
        {
            return _pageMap.ContainsKey(action);
        }

        public string GetTitle(string action = "")
        {
            return action + _pageTitle;
        }

        public string GetLayout()
        {
            return _pageLayout;
        }

        #region 连贯接口

        public DslClassBase SetResourceName(string resname)
        {
            _resourceName = resname;
            return this;
        }

        public DslClassBase SetTitle(string title)
        {
            _pageTitle = title;
            return this;
        }

        public DslClassBase SetLayout(string layout)
        {
            _pageLayout = layout;
            return this;
        }

        public DslClassBase AddPageMap(string action, string page)
        {
            _pageMap[action] = page;
            return this;
        }

        public DslClassBase AddField(Field field)
        {
            _fields.Add(field);
            field.Dslmodel = this;
            return this;
        }

        #endregion

        public IList<string> GetAllPageMapKey()
        {
            return _pageMap.Keys.ToList();
        }
    }
}
