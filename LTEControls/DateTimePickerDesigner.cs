using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;

namespace LTEControls
{
    public class DateTimePickerDesigner : ControlDesigner
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

            ((LTEControls.DateTimePicker)(this.Component)).RenderControl(new HtmlTextWriter(tw));

            tw.Flush();
            tw.Close();

            return sb.ToString();
        }

        protected override string GetEmptyDesignTimeHtml()
        {
            TextWriter tw;
            StringBuilder sb;

            sb = new StringBuilder();
            tw = new StringWriter(sb);

            DateTimePicker picker = new DateTimePicker();

            picker.RenderControl(new HtmlTextWriter(tw));

            tw.Flush();
            tw.Close();

            return sb.ToString();
         }
    }
}
