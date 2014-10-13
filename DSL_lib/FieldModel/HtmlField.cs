﻿namespace DSL_lib.FieldModel
{
    public enum HtmlCell
    {
        Tag,
        Class,
        Id,
        Type,
        Style,
        Data,
        Event,
        Other,
        InnerHtml,
        自闭合,
    }

    public class HtmlField : Field
    {
        private string A(HtmlCell key)
        {
            return GetAttribute((int)key);
        }

        private string A(HtmlCell key, string attrName)
        {
            string attr = GetAttribute((int)key);
            return attr.Length > 0 ? string.Format("{0}='{1}'", attrName, attr) : "";
        }

        protected string GetAttribute(HtmlCell key)
        {
            return GetAttribute((int)key);
        }

        protected Field SetAttr(HtmlCell key, string value)
        {
            return SetAttr((int)key, value);
        }

        protected Field AddAttr(HtmlCell key, string value)
        {
            return AddAttr((int)key, value);
        }

        /// <summary>
        /// 将字段的内容转化为Html语法
        /// </summary>
        /// <returns></returns>
        public override string Write()
        {
            string result = "";
            if (GetAttribute(HtmlCell.自闭合) != "true")
            {
                result = string.Format("<{0} class='{1}' {2} {3} style='{4}' {5} {6} {7} >{8}</{0}>",
                    A(HtmlCell.Tag), A(HtmlCell.Class), A(HtmlCell.Id, "id"), A(HtmlCell.Type, "type"), A(HtmlCell.Style), A(HtmlCell.Data), A(HtmlCell.Event),
                    A(HtmlCell.Other), A(HtmlCell.InnerHtml));
            }
            else
            {
                result = string.Format("<{0} class='{1}' {2} {3} style='{4}' {5} {6} {7} />",
                    A(HtmlCell.Tag), A(HtmlCell.Class), A(HtmlCell.Id, "id"), A(HtmlCell.Type, "type"), A(HtmlCell.Style), A(HtmlCell.Data), A(HtmlCell.Event),
                    A(HtmlCell.Other));
            }
            return result;
        }
    }
}