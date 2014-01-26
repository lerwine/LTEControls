using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Security.Permissions;

[assembly: TagPrefix("LTEControls", "lte")]

namespace LTEControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, 
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:LabelForGuidValues runat=\"server\"></{0}:LabelForGuidValues>")]
    [DefaultProperty("Text")]
    [Designer(typeof(LabelForGuidValuesDesigner))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultEvent("Click")]
    [ControlValueProperty("Guid")]
    public class LabelForGuidValues : WebControl, INamingContainer
    {
        #region Behavior Properties

        [Browsable(true)]
        [Bindable(false)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Localizable(true)]
        [Themeable(true)]
        [DisplayName("Show Browse Button")]
        [Description("If set to false, then the browse button will not be displayed")]
        public bool ShowBrowseButton
        {
            get
            {
                if (this.ViewState["ShowBrowseButton"] == null)
                    return true;

                return ((bool)(this.ViewState["ShowBrowseButton"]));
            }
            set
            {
                this.ViewState["ShowBrowseButton"] = value;
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Command Name")]
        [Description("String value to pass as command name when browse button is clicked")]
        public string CommandName
        {
            get
            {
                if (this.ViewState["CommandName"] == null)
                    return "";

                return ((string)(this.ViewState["CommandName"]));
            }
            set
            {
                this.ViewState["CommandName"] = value;
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Command Argument")]
        [Description("String value to pass as command argument when browse button is clicked")]
        public string CommandArgument
        {
            get
            {
                if (this.ViewState["CommandArgument"] == null)
                    return "";

                return ((string)(this.ViewState["CommandArgument"]));
            }
            set
            {
                this.ViewState["CommandArgument"] = value;
            }
        }

        #endregion

        #region Appearance Properties

        Style _browseButtonStyle;

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Themeable(false)]
        [Description("Text to display")]
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

        [Browsable(true)]
        [Bindable(false)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue("...")]
        [Localizable(true)]
        [Themeable(true)]
        [DisplayName("Browse Button Text")]
        [Description("Text to display for browse button")]
        public string BrowseButtonText
        {
            get
            {
                if (this.ViewState["BrowseButtonText"] == null)
                    return "...";

                return ((string)(this.ViewState["BrowseButtonText"]));
            }
            set
            {
                this.ViewState["BrowseButtonText"] = value;
            }
        }

        [Browsable(true)]
        [Bindable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Styles")]
        [Localizable(true)]
        [Themeable(true)]
        [NotifyParentProperty(true)]
        [DisplayName("Browse Button Style")]
        [Description("Style to apply to browse button")]
        public Style BrowseButtonStyle
        {
            get
            {
                if (this._browseButtonStyle == null)
                    this._browseButtonStyle = new Style(this.ViewState);

                return this._browseButtonStyle;
            }
        }

        #endregion

        #region Miscellaneous Properties

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [Category("Misc")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("GUID")]
        [Description("GUID value to associate with control")]
        public Guid? Guid
        {
            get
            {
                return ((Guid?)(this.ViewState["Guid"]));
            }
            set
            {
                this.ViewState["Guid"] = value;
            }
        }

        #endregion

        #region Controls
        
        protected Button _browseButton;

        internal Button BrowseButton
        {
            get
            {
                Control c;

                if (this._browseButton != null)
                    return this._browseButton;

                if ((c = this.FindControl("BrowseButton")) == null)
                {
                    this._browseButton = new Button();
                    this._browseButton.ID = "BrowseButton";
                    this._browseButton.CommandName = "Browse";
                    this._browseButton.Text = "...";
                    this._browseButton.Visible = true;
                    this._browseButton.Enabled = true;
                    this._browseButton.Click += new EventHandler(BrowseButton_Click);
                    this._browseButton.Command += new CommandEventHandler(BrowseButton_Command);
                }
                else
                    this._browseButton = (Button)c;

                return this._browseButton;
            }
        }

        #endregion

        #region Constructors

        public LabelForGuidValues(HtmlTextWriterTag tag)
            : base(tag)
        {
            this.InitControl();
        }
        public LabelForGuidValues(string tag)
            : base(tag)
        {
            this.InitControl();
        }

        public LabelForGuidValues()
            : base(HtmlTextWriterTag.Span)
        {
            this.InitControl();
        }

        protected void InitControl()
        {
            this._browseButton = null;
            this._browseButtonStyle = null;
        }

        #endregion

        #region Child Control Event Handlers

        protected void BrowseButton_Command(object sender, CommandEventArgs e)
        {
            CommandEventHandler temp;

            temp = this.Command;

            if (temp != null)
                temp(this, new CommandEventArgs(this.CommandName, this.CommandArgument));
        }

        protected void BrowseButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = this.Click;
            if (temp != null)
                temp(this, e);
        }

        #endregion

        #region Event Handlers

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler Click;

        [Browsable(true)]
        [Category("Action")]
        public event CommandEventHandler Command;

        #endregion

        #region Overrides

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(this.BrowseButton);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(HttpUtility.HtmlEncode(this.Text));

            if (this.ShowBrowseButton)
            {
                this.BrowseButton.Visible = true;
                this.BrowseButton.RenderControl(writer);
            }
            else
                this.BrowseButton.Visible = false;
        }

        #endregion
    }
}
