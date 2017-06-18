using MVcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVcPL.Helpers
{
    public static class PageHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pageInfo.TotalPages > 1)
            {
                for (int i = 1; i <= pageInfo.TotalPages; i++)
                {
                    TagBuilder tag2 = new TagBuilder("li");
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();

                    if (i == pageInfo.PageNumber)
                    {
                        tag2.AddCssClass("active");
                    }
                    tag2.InnerHtml = tag.ToString();
                    result.Append(tag2.ToString());
                }
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}