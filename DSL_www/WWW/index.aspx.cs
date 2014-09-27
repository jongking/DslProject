//using DSL_lib.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using DSL_lib.FieldModel;
using RazorEngine;
using DSL_lib.Helper;
public partial class WWW_index : Page
{
    protected string test;

    protected string RouteMain
    {
        get { return RouteData.Values["main"].ToString(); }
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
        var indexobj = FactoryHelper.Create(RouteMain) as Index;
        string strPath = Server.MapPath("./");
        string template = File.ReadAllText(strPath + "WWW/View/Default/index.cshtml");
//        test = Razor.Parse(template, new {Name = indexobj.Test.InputName});
        test = Razor.Parse(template, new {Name = "xiaoxie"});
    }
}

public class FactoryHelper
{
    public static Object Create(string classname)
    {
        return Activator.CreateInstance(null, classname).Unwrap();
    }
}

public class Index : DslClassBase
{
    private readonly Password _pw;
    private readonly Test _test;

    public Index()
    {
        _pw = new Password("密码");
        _test = new Test("姓名");
        Pagelist.Add("show");
    }

    public Password Pw
    {
        get { return _pw; }
    }

    public Test Test
    {
        get { return _test; }
    }
}

public class Member : DslClassBase
{
    private readonly Password _pw;
    private readonly Test _test;

    public Member()
    {
        _pw = new Password("密码");
        _test = new Test("姓名");
    }

    public Password Pw
    {
        get { return _pw; }
    }

    public Test Test
    {
        get { return _test; }
    }
}


public abstract class DslClassBase
{
    protected List<string> Pagelist = new List<string>();
    protected string SourcePath = "./View/Default/";
    private Hashtable _pageMap = new Hashtable();
    protected Hashtable PageMap
    {
        set { _pageMap = value; }
    }
    protected string GetPageMap(string action)
    {
        if (_pageMap.ContainsKey(action))
        {
            return _pageMap[action].ToString();
        }
        return action;
    }

    protected DslClassBase()
    {
    }

    protected bool HasPageType(string pageType)
    {
        return Pagelist.Any(type => type == pageType);
    }


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