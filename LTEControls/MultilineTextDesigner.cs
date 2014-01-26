using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;

namespace LTEControls
{
    class MultilineTextDesigner : ControlDesigner
    {
        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }
        public override string GetDesignTimeHtml()
        {
            TextWriter tw;
            StringBuilder sb;

            sb = new StringBuilder();
            tw = new StringWriter(sb);

            if (String.IsNullOrEmpty(((LTEControls.MultilineText)(this.Component)).Text))
                return HttpUtility.HtmlEncode(String.Format("[{0}]", ((LTEControls.MultilineText)(this.Component)).ID));

            ((LTEControls.MultilineText)(this.Component)).RenderControl(new HtmlTextWriter(tw));

            tw.Flush();
            tw.Close();

            return sb.ToString();
        }
        protected override string GetEmptyDesignTimeHtml()
        {
            return HttpUtility.HtmlEncode(String.Format("[{0}]", ((LTEControls.MultilineText)(this.Component)).ID));
        }
    }
}
