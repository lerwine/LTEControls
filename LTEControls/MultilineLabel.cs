using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.ComponentModel;

namespace LTEControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:MultilineText runat=\"server\"></{0}:MultilineText>")]
    [DefaultProperty("Text")]
    public class MultilineText : WebControl, INamingContainer
    {
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text = "";

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(HttpUtility.HtmlEncode(this.Text).Replace("\r\n", "\n").Replace("\n", "&nbsp;<br/>"));
        }

        protected override void LoadViewState(object savedState)
        {
            this.Text = (string)(((object[])savedState)[0]);
            base.LoadViewState(((object[])savedState)[1]);
        }

        protected override object SaveViewState()
        {
            return new object[] { this.Text, base.SaveViewState() };
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Span;
            }
        }

        protected override string TagName
        {
            get
            {
                return HtmlTextWriterTag.P.ToString();
            }
        }
    }
}
