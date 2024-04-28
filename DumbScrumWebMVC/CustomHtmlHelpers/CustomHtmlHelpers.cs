using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DumbScrumWebMVC.CustomHtmlHelpers {
    public static class CustomHtmlHelpers {
        public static IHtmlString Image(this HtmlHelper helper, string src, string alt) {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("alt", alt);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString ErrorAlert(this HtmlHelper helper, string errorMessage) {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<div class='alert alert-danger alert-dismissible fade show' role='alert'><strong>{0}</strong>              <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>", errorMessage);

            return new MvcHtmlString(sb.ToString());
        }
    }
}