using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using DSL_lib.FieldModel;
using DSL_lib.Helper;

namespace DSL_lib
{
    public class DslClassBase
    {
        // 记录资源名(Resource)
        private readonly IList<Field> _fields = new List<Field>();
        protected IList<IPlug<Field>> begin_Plugs = new List<IPlug<Field>>();
        protected IList<IPlug<Field>> end_Plugs = new List<IPlug<Field>>();
        private Dictionary<string, string> _pageMap = new Dictionary<string, string>();

        private string _pageLayout = "_layout";
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
                    HandleBegin(eventname, field, eventContext);
                    field.Handle(eventname.ToLower(), eventContext);
                    HandleEnd(eventname, field, eventContext);
                }
                eventContext.Clear(true);
            }
            catch (Exception ex)
            {
                if (eventContext != null) eventContext.Clear(false);
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
                    HandleBegin(eventname, field, eventContext);
                    field.Handle(eventname.ToLower(), eventContext);
                    HandleEnd(eventname, field, eventContext);
                }
                eventContext.Clear(true);
            }
            catch (Exception ex)
            {
                if (eventContext != null) eventContext.Clear(false);
                return ex.Message;
            }
            return eventContext.Output.ToString();
        }

        public string HandleWithDbAndRequest(string eventname)
        {
            EventContext eventContext = null;
            try
            {
                eventContext = new EventContext(true);

                foreach (Field field in Fields)
                {
                    field.Handle(eventname.ToLower(), eventContext);
                }
                eventContext.Clear(true);
            }
            catch (Exception ex)
            {
                if (eventContext != null) eventContext.Clear(false);
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

        public DslClassBase AddBeginPlug(IPlug<Field> plug)
        {
            begin_Plugs.Add(plug);
            return this;
        }

        public DslClassBase AddEndPlug(IPlug<Field> plug)
        {
            end_Plugs.Add(plug);
            return this;
        }
        #endregion

        private void HandleEnd(string eventname, Field field, EventContext eventContext)
        {
            if (end_Plugs.Count > 0)
            {
                foreach (var plug in end_Plugs)
                {
                    plug.Handle(eventname.ToLower(), field, eventContext);
                }
            }
        }

        private void HandleBegin(string eventname, Field field, EventContext eventContext)
        {
            if (begin_Plugs.Count > 0)
            {
                foreach (var plug in begin_Plugs)
                {
                    plug.Handle(eventname.ToLower(), field, eventContext);
                }
            }
        }
    }

    /// <summary>
    ///     事件处理上下文
    /// </summary>
    public class EventContext
    {
        public StringBuilder Output;

        public HttpRequest Request;

        public SqlConnection SqlCn;
        public SqlTransaction SqlTc;


        public EventContext(bool withdb, HttpRequest request)
        {
            Output = new StringBuilder();
            Request = request;

            if (!withdb) return;
            SqlCn = DbHelper.GetConnection();
            SqlTc = DbHelper.StartTransaction(SqlCn);
        }

        public EventContext(SqlConnection sqlCn, HttpRequest request)
        {
            Output = new StringBuilder();
            Request = request;

            SqlCn = sqlCn;
            SqlTc = DbHelper.StartTransaction(SqlCn);
        }

        public EventContext(SqlConnection sqlCn, SqlTransaction sqlTc, HttpRequest request)
        {
            Output = new StringBuilder();
            Request = request;

            SqlCn = sqlCn;
            SqlTc = sqlTc;
        }

        public void Clear(bool isok)
        {
            Output.Clear();
            Request = null;

            if (SqlCn != null)
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
}