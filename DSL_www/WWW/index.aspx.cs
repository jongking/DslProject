using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using DSL_lib;
using DSL_lib.FieldModel;
using DSL_lib.Helper;
using RazorEngine;

public partial class WWW_index : Page
{
    protected string test;

    protected string RouteResource
    {
        get { return RouteData.Values["resource"].ToString(); }
    }

    protected string RouteAction
    {
        get { return RouteData.Values["action"].ToString(); }
    }

    protected string RouteId
    {
        get { return RouteData.Values["id"].ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.ToLower() == "post")
        {
            DoPost();
        }
        else
        {
            DoGet();
        }
    }

    private void DoGet()
    {
        string routeResource = RouteResource;
        string routeAction = RouteAction;
        string routeId = RouteId;
        string strPath = Server.MapPath("/");

        var helper = new RazorHelper();

        DslClassBase mainObj;
        var layoutObj = FactoryHelper.Create("_null");
        try
        {
            //创建客户需要的资源(Resource)
            mainObj = FactoryHelper.Create(routeResource);
            if (mainObj.GetLayout() != "")
            {
                layoutObj = FactoryHelper.Create(mainObj.GetLayout());
            }

            //判断资源(Resource)有没有要求的动作(Action)
            if (!mainObj.HasPageMap(routeAction))
            {
                throw new DslException();
            }
            if (!layoutObj.HasPageMap("default"))
            {
                throw new DslException();
            }
        }
        catch (DslException dslExceptionex)
        {
            mainObj = FactoryHelper.Create("error");
            if (mainObj.GetLayout() != "")
            {
                layoutObj = FactoryHelper.Create(mainObj.GetLayout());
            }
            routeAction = "default";
        }

        #region 编译_Layout.cshtml模版

        string layout =
            File.ReadAllText(string.Format("{0}WWW/View/Default/{1}.cshtml", strPath, layoutObj.GetPageMap("default")));
        Razor.GetTemplate(layout, new {M = mainObj, L = layoutObj, Help = helper}, layoutObj.ResourceName);

        #endregion

        string template =
            File.ReadAllText(string.Format("{0}WWW/View/Default/{1}.cshtml", strPath, mainObj.GetPageMap(routeAction)));
        //        test = Razor.Parse(template, new {Name = mainObj.Test.InputName});
        test = Razor.Parse(template, new {M = mainObj, L = layoutObj, Help = helper});
    }

    private void DoPost()
    {
        string routeResource = RouteResource;
        string routeAction = RouteAction;
        string routeId = RouteId;
        Request;
        var mainObj = FactoryHelper.Create(routeResource);
        var result = mainObj.Handle(string.Format("DoPost/{0}/{1}", routeAction, routeId));
        Response.Clear();
        Response.Write(result);
        Response.End();
    }
}

public static class FactoryHelper
{
    /// <summary>
    /// 组装的语义模型
    /// </summary>
    private static void InitAll()
    {
        if (!CheckCache("_null"))
        {
            DslClassBase model = new DslClassBase()
                .SetResourceName("_null")
                .SetTitle("空白公共页")
                .AddPageMap("default", "_null");
            CacheModel(model);
        }
        if (!CheckCache("error"))
        {
            DslClassBase model = new DslClassBase()
                .SetResourceName("error")
                .SetTitle("错误页")
                .AddPageMap("default", "error");
            CacheModel(model);
        }
        if (!CheckCache("_layout"))
        {
            DslClassBase model = new DslClassBase()
                .SetResourceName("_layout")
                .SetTitle("公共页")
                .AddPageMap("default", "_Layout");
            CacheModel(model);
        }
        if (!CheckCache("index"))
        {
            DslClassBase model = new DslClassBase()
                .SetResourceName("index")
                .SetTitle("主页")
                .AddPageMap("default", "default")
                .AddField
                (
                    new WebField()
                        .AddPlugs(new MainPagePlug())
                );
            CacheModel(model);
        }
        if (!CheckCache("user"))
        {
            DslClassBase model = new DslClassBase()
                .SetResourceName("user")
                .SetTitle("用户")
                .AddPageMap("new", "new")
                .AddPageMap("modify", "modify")
                .AddPageMap("delete", "delete")
                .AddPageMap("search", "search")
                .AddField
                (
                    new WebField("MemberName")
                        .AddPlugs(new InputTextPlug("用户名"))
                        .AddPlugs(new DbPlug("User"))
                )
                .AddField
                (
                    new WebField("Email")
                        .AddPlugs(new InputEmailPlug("邮箱"))
                        .AddPlugs(new DbPlug("User"))
                )
                .AddField
                (
                    new WebField("Pwd")
                        .AddPlugs(new InputPasswordPlug("密码"))
                        .AddPlugs(new DbPlug("User"))
                )
                .AddField
                (
                    new WebField()
                        .AddPlugs(new SubmitButtonPlug("提交"))
                );
            CacheModel(model);
        }
    }

    private static bool CheckCache(string modelname)
    {
        return DslCacheHelper.HasCache(modelname);
    }

    private static void CacheModel(DslClassBase model)
    {
        DslCacheHelper.SetCache(model.ResourceName, model);
    }

    public static DslClassBase Create(string modelname)
    {
        var obj = DslCacheHelper.GetCache(modelname);
        if (obj != null) return obj;
        InitAll();
        obj = DslCacheHelper.GetCache(modelname);
        if (obj != null) return obj;
        throw new DslException();
    }
}

//
//public class Index : DslClassBase
//{
//    private readonly Password _pw;
//    private readonly Test _test;
//
//    public Index()
//    {
//        _pw = new Password("密码");
//        _test = new Test("姓名");
//    }
//
//    public Password Pw
//    {
//        get { return _pw; }
//    }
//
//    public Test Test
//    {
//        get { return _test; }
//    }
//}

