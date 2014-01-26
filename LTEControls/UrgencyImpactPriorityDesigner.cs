using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;

namespace LTEControls
{
    public class UrgencyImpactPriorityDesigner : ControlDesigner
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

            sb = new StringBuilder("");
            tw = new StringWriter(sb);

            ((LTEControls.UrgencyImpactPriority)(this.Component)).RenderControl(new HtmlTextWriter(tw));

            tw.Flush();
            tw.Close();

            return sb.ToString();
        }
        protected override string GetEmptyDesignTimeHtml()
        {
            LTEControls.UrgencyImpactPriority control;
            TextWriter tw;
            StringBuilder sb;

            sb = new StringBuilder("");
            tw = new StringWriter(sb);

            control = new UrgencyImpactPriority();

            control.RenderControl(new HtmlTextWriter(tw));

            tw.Flush();
            tw.Close();

            return sb.ToString();
        }
    }
}
