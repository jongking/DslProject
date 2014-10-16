using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
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
        string routeResource = RouteResource;
        string routeAction = RouteAction;
        string routeId = RouteId;
        string strPath = Server.MapPath("/");

        var helper = new RazorHelper();

        DslClassBase mainObj;
        var layoutObj = FactoryHelper.Create("_null") as DslClassBase;
        try
        {
            //创建客户需要的资源(Resource)
            mainObj = FactoryHelper.Create(routeResource) as DslClassBase;
            if (mainObj.GetLayout() != "")
            {
                layoutObj = FactoryHelper.Create(mainObj.GetLayout()) as DslClassBase;
            }

            //判断资源(Resource)有没有要求的动作(Action)
            if (!mainObj.HasPageMap(routeAction))
            {
                throw new DslException();
            }
            if (!layoutObj.HasPageMap(routeAction))
            {
                throw new DslException();
            }
        }
        catch (DslException dslExceptionex)
        {
            mainObj = FactoryHelper.Create("error") as DslClassBase;
            if (mainObj.GetLayout() != "")
            {
                layoutObj = FactoryHelper.Create(mainObj.GetLayout()) as DslClassBase;
            }
            routeAction = "default";
        }

        #region 编译_Layout.cshtml模版

        string layout =
            File.ReadAllText(string.Format("{0}WWW/View/Default/{1}.cshtml", strPath, layoutObj.GetPageMap(routeAction)));
        Razor.GetTemplate(layout, new {M = mainObj, L = layoutObj, Help = helper}, layoutObj.ResourceName);

        #endregion

        string template =
            File.ReadAllText(string.Format("{0}WWW/View/Default/{1}.cshtml", strPath, mainObj.GetPageMap(routeAction)));
        //        test = Razor.Parse(template, new {Name = mainObj.Test.InputName});
        test = Razor.Parse(template, new {M = mainObj, L = layoutObj, Help = helper});
    }
}

public static class FactoryHelper
{
    /// <summary>
    ///     组装错误页的语义模型
    /// </summary>
    private static void InitAll()
    {
        if (!CheckCache("_null"))
        {
            DslClassBase model = new DslClassBase()
                .SetResourceName("_null")
                .SetTitle("")
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
                        .AddPlugs(new InputTextPlug())
//                    .AddPlug(new LabelPlug("Email address", "for=\"exampleInputEmail1\""))
//                    .AddPlug(new InputTextPlug("exampleInputEmail1", "form-control", "email", "placeholder=\"Enter email\""))
//                    .InitPlugs()
                );
            CacheModel(model);
        }
    }

    private static bool CheckCache(string modelname)
    {
        return CacheHelper.HasCache(modelname);
    }

    private static void CacheModel(DslClassBase model)
    {
        CacheHelper.SetCache(model.ResourceName, model);
    }

    public static Object Create(string modelname)
    {
        object obj = CacheHelper.GetCache(modelname);
        if (obj != null) return obj;
        InitAll();
        obj = CacheHelper.GetCache(modelname);
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
        return this;
    }

    #endregion

//
//    protected string GetHead()
//    {
//        var resultBuilder = new StringBuilder();
//        resultBuilder.Append(GetMetaSet() + "\n")
//            .Append(GetCssStyle());
//    }
//
//    protected string GetTitle()
//    {
//        return "还没有Title";
//    }
//
//    protected string GetDocumentType()
//    {
//        return "<!DOCTYPE html>";
//    }
//
//    protected string GetLanguage()
//    {
//        return "lang='zh-CN'";
//    }
//
//    protected string GetCssStyle()
//    {
//        return string.Format("<link rel=\"stylesheet\" href=\"{0}css/bootstrap.min.css\">" + //新 Bootstrap 核心 CSS 文件
//                             "<link rel=\"stylesheet\" href=\"{0}css/bootstrap-theme.min.css\">", SourcePath);
//            //可选的Bootstrap主题文件（一般不用引入）
//    }
//
//    protected string GetMetaSet()
//    {
//        return "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>" +
//               "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\">";
//    }
//
//    protected string GetJavaScript()
//    {
//        return string.Format("<script src=\"{0}/js/jquery.min.js\"></script>" +
//                             "<script src=\"{0}/js/bootstrap.min.js\"></script>", SourcePath);
//    }
}