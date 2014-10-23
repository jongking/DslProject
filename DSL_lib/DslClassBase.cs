using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DSL_lib.FieldModel;
using DSL_lib.Helper;

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

        public string Handle(string eventname)
        {
            EventContext eventContext = null;
            try
            {
                eventContext = new EventContext(false);
                foreach (Field field in Fields)
                {
                    field.Handle(eventname.ToLower(), eventContext);
                }
                eventContext.EndSqlConection(true);
            }
            catch (Exception ex)
            {
                if (eventContext != null) eventContext.EndSqlConection(false);
                return ex.Message;
            }
            return eventContext.Output.ToString();    
        }

        public string HandleWithDb(string eventname)
        {
            EventContext eventContext = null;
            try
            {
                eventContext = new EventContext(true);
                foreach (Field field in Fields)
                {
                    field.Handle(eventname.ToLower(), eventContext);
                }
                eventContext.EndSqlConection(true);
            }
            catch (Exception ex)
            {
                if (eventContext != null) eventContext.EndSqlConection(false);
                return ex.Message;
            }
            return eventContext.Output.ToString();
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

        public IList<string> GetAllPageMapKey()
        {
            return _pageMap.Keys.ToList();
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
    }

    /// <summary>
    ///     事件处理上下文
    /// </summary>
    public class EventContext
    {
        public StringBuilder Output;

        public SqlConnection SqlCn;
        public SqlTransaction SqlTc;

        public EventContext(bool withdb)
        {
            Output = new StringBuilder();

            if (!withdb) return;
            SqlCn = DbHelper.GetConnection();
            SqlTc = DbHelper.StartTransaction(SqlCn);
        }

        public EventContext(SqlConnection sqlCn)
        {
            Output = new StringBuilder();
            SqlCn = sqlCn;
            SqlTc = DbHelper.StartTransaction(SqlCn);
        }

        public EventContext(SqlConnection sqlCn, SqlTransaction sqlTc)
        {
            Output = new StringBuilder();
            SqlCn = sqlCn;
            SqlTc = sqlTc;
        }

        public void EndSqlConection(bool isok)
        {
            if (isok)
            {
                DbHelper.CommitTransaction(SqlTc);
            }
            else
            {
                DbHelper.RollBackTransaction(SqlTc);
            }
            DbHelper.CloseConnection(SqlCn);
        }
    }
}