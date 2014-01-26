using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;

namespace LTEControls
{
    public class LabelForGuidValuesDesigner : ControlDesigner
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

            if (String.IsNullOrEmpty(((LTEControls.LabelForGuidValues)(this.Component)).Text) ||
                ((LTEControls.LabelForGuidValues)(this.Component)).Text.Trim() == "")
            {
                if (((LTEControls.LabelForGuidValues)(this.Component)).ShowBrowseButton)
                {
                    ((LTEControls.LabelForGuidValues)(this.Component)).BrowseButton.RenderControl(
                        new HtmlTextWriter(tw));

                    tw.Flush();
                }

                tw.Close();

                if (((LTEControls.LabelForGuidValues)(this.Component)).Guid == null)
                    return HttpUtility.HtmlEncode(String.Format("[{0}]", 
                        ((LTEControls.LabelForGuidValues)(this.Component)).ID)) + sb.ToString();

                return HttpUtility.HtmlEncode(String.Format("[{0}]", 
                    ((LTEControls.LabelForGuidValues)(this.Component)).Guid.Value.ToString())) + 
                    sb.ToString();
            }

            ((LTEControls.LabelForGuidValues)(this.Component)).RenderControl(new HtmlTextWriter(tw));

            tw.Flush();
            tw.Close();

            return sb.ToString();
        }
        protected override string GetEmptyDesignTimeHtml()
        {
            return HttpUtility.HtmlEncode(String.Format("[{0}]", 
                ((LTEControls.LabelForGuidValues)(this.Component)).ID));
        }
    }
}
