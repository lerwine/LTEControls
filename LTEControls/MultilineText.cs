using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Security.Permissions;

[assembly:TagPrefix("LTEControls", "lte")]

namespace LTEControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:MultilineText runat=\"server\"></{0}:MultilineText>")]
    [DefaultProperty("Text")]
    [Designer(typeof(MultilineTextDesigner))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [ControlValueProperty("Text")]
    public class MultilineText : WebControl, INamingContainer
    {
        public MultilineText(HtmlTextWriterTag tag)
            : base(tag)
        {
        }
        public MultilineText(string tag)
            : base(tag)
        {
        }

        public MultilineText() : base(HtmlTextWriterTag.Span)
        {
        }

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("Text to display. Newlines are converted to HTML line breaks.")]
        public string Text
        {
            get
            {
                if (this.ViewState["Text"] == null)
                    return "";

                return ((string)(this.ViewState["Text"]));
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(HttpUtility.HtmlEncode(this.Text).Replace("\r\n", "\n").Replace("\n", "&nbsp;<br/>"));
        }
    }
}
