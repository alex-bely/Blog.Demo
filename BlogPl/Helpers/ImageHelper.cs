using BlogPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Helpers
{
    public static class ImageHelper
    {
        
        public static MvcHtmlString ViewAvatar(this HtmlHelper htmlHelper, string src, string alt)
        {
            var imageTag = new TagBuilder("image");
            imageTag.MergeAttribute("src", src);
            imageTag.MergeAttribute("alt", alt);
            //imageTag.MergeAttribute("width", width.ToString());
            //imageTag.MergeAttribute("height", height.ToString());
            imageTag.AddCssClass("img-wrapper img-thumbnail");

            return MvcHtmlString.Create(imageTag.ToString(TagRenderMode.SelfClosing));
        }


    }
}