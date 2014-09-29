<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script RunAt="server">

    // private static string uploadDir;

    void Application_Start(object sender, EventArgs e)
    {
        //路由设定
        var defaults = new RouteValueDictionary { { "main", "Index" }, { "action", "" }, { "id", ""} };
        
        RouteTable.Routes.MapPageRoute("default", "{main}/{action}/{id}", "~/WWW/index.aspx", false, defaults);
    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。
        //string sessionId = Session.SessionID;
        //DirectoryInfo dir = new DirectoryInfo(uploadDir);
        //if (dir.Exists)
        //{
        //    Debug.WriteLine(dir.FullName);
        //    FileInfo[] fs = dir.GetFiles();
        //    foreach (FileInfo f in fs)
        //    {
        //        if (f.Name.StartsWith(sessionId + "_") && f.Exists)
        //        {
        //            f.Delete();
        //        }
        //    }
        //}
    }

    public void Application_BeginRequest(object sender, EventArgs e)
    {
        //屏蔽此代码。进程与IIS的程序池的生命周期一致 放弃session修改时屏蔽以下代码
        //HttpApplication httpApplication = (HttpApplication)sender;
        //HttpContext context = httpApplication.Context;
        //Thread.SetData(ThreadVariable.FileLDSS, context.Server.MapPath("~/Web/upload/Files") + "\\");
    }


    #region 清除线程数据(编写:叶英权)
    //public void Application_EndRequest(object sender, EventArgs e)
    //{
        
    //    HttpApplication httpApplication = (HttpApplication)sender;
    //    HttpContext context = httpApplication.Context;
    //    string filePath = context.Request.FilePath;
    //    string fileExtension = VirtualPathUtility.GetExtension(filePath);
    //    if (fileExtension.Equals(".aspx", StringComparison.CurrentCultureIgnoreCase)
    //        || fileExtension.Equals(".ashx", StringComparison.CurrentCultureIgnoreCase))
    //    {
    //        DbHelperSQL.closeConnection();
    //        Debug.WriteLine("_EndRequest");
    //    }
    //}

    //public void Application_PreSendRequestContent(object sender, EventArgs e)
    //{
       
    //    HttpApplication httpApplication = (HttpApplication)sender;
    //    HttpContext context = httpApplication.Context;
    //    string filePath = context.Request.FilePath;
    //    string fileExtension = VirtualPathUtility.GetExtension(filePath);
    //    if (fileExtension.Equals(".aspx", StringComparison.CurrentCultureIgnoreCase)
    //        || fileExtension.Equals(".ashx", StringComparison.CurrentCultureIgnoreCase))
    //    {
    //        DbHelperSQL.clearThreadData();
    //        Debug.WriteLine("_PreSendRequestContent");
    //    }
    //}
    #endregion
      
</script>

