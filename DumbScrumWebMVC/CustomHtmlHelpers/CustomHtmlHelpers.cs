using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DumbScrumWebMVC.CustomHtmlHelpers {
    public static class CustomHtmlHelpers {
        public static IHtmlString ProfilePicture(this HtmlHelper helper, string base64, string alt) {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", base64);
            tb.Attributes.Add("alt", alt);
            tb.AddCssClass("rounded-circle");
            tb.AddCssClass("m-1");
            tb.Attributes.Add("style", "height:35px;width:35px;");
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString ErrorAlert(this HtmlHelper helper, string errorMessage) {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<div class='alert alert-danger alert-dismissible fade show' role='alert'><strong>{0}</strong>              <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>", errorMessage);

            return new MvcHtmlString(sb.ToString());
        }
    }
}