using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.ComponentModel;
using System.Drawing.Design;

namespace LTEControls
{
    public partial class DateTimePicker : WebControl, INamingContainer
    {
        #region Properties

        [DefaultValue("")]
        [Bindable(true)]
        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [UrlProperty]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Calendar Mode Image URL")]
        [Description("URL of image used for the image button which switches control to calendar mode")]
        public string CalendarModeImageUrl
        {
            get
            {
                if (this.ViewState["CalendarModeImageUrl"] == null)
                    return "";

                return ((string)(this.ViewState["CalendarModeImageUrl"]));
            }
            set
            {
                this.ViewState["CalendarModeImageUrl"] = value;
            }
        }

        [DefaultValue("")]
        [Bindable(true)]
        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [UrlProperty]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Text Mode Image URL")]
        [Description("URL of image used for the image button which switches control to textbox mode")]
        public string TextModeImageUrl
        {
            get
            {
                if (this.ViewState["TextModeImageUrl"] == null)
                    return "";

                return ((string)(this.ViewState["TextModeImageUrl"]));
            }
            set
            {
                this.ViewState["TextModeImageUrl"] = value;
            }
        }

        [Bindable(true)]
        [DefaultValue(-1)]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Layout")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Mode Image Height")]
        [Description("Height of image used for the image button which switches control mode")]
        public int ModeImageHeight
        {
            get
            {
                if (this.ViewState["ModeImageHeight"] == null)
                    return -1;

                return ((int)(this.ViewState["ModeImageHeight"]));
            }
            set
            {
                this.ViewState["ModeImageHeight"] = value;
            }
        }

        [Bindable(true)]
        [DefaultValue(-1)]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Layout")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Mode Image Width")]
        [Description("Width of image used for the image button which switches control mode")]
        public int ModeImageWidth
        {
            get
            {
                if (this.ViewState["ModeImageWidth"] == null)
                    return -1;

                return ((int)(this.ViewState["ModeImageWidth"]));
            }
            set
            {
                this.ViewState["ModeImageWidth"] = value;
            }
        }

        #endregion

        #region Controls

        protected ImageButton _calendarModeImageButton, _textModeImageButton;

        internal ImageButton CalendarModeImageButton
        {
            get
            {
                Control c;

                if (this._calendarModeImageButton != null)
                    return this._calendarModeImageButton;

                if ((c = this.FindControl("CalendarModeImageButton")) == null)
                {
                    this._calendarModeImageButton = new ImageButton();
                    this._calendarModeImageButton.ID = "CalendarModeImageButton";
                    this._calendarModeImageButton.CommandName = "Calendar";
                    this._calendarModeImageButton.Command += new CommandEventHandler(CalendarModeImageButton_Command);
                }
                else
                    this._calendarModeImageButton = (ImageButton)c;

                return this._calendarModeImageButton;
            }
        }

        internal ImageButton TextModeImageButton
        {
            get
            {
                Control c;

                if (this._textModeImageButton != null)
                    return this._textModeImageButton;

                if ((c = this.FindControl("TextModeImageButton")) == null)
                {
                    this._textModeImageButton = new ImageButton();
                    this._textModeImageButton.ID = "TextModeImageButton";
                    this._textModeImageButton.CommandName = "Text";
                    this._textModeImageButton.Command += new CommandEventHandler(TextModeImageButton_Command);
                }
                else
                    this._textModeImageButton = (ImageButton)c;

                return this._textModeImageButton;
            }
        }

        #endregion

        #region Initialize

        protected void InitializeModeButtons()
        {
            this._calendarModeImageButton = null;
            this._textModeImageButton = null;
        }

        private void AddModeButtonControls(ControlCollection controls)
        {
            if (!controls.Contains(this.CalendarModeImageButton))
                controls.Add(this.CalendarModeImageButton);
            if (!this.Controls.Contains(this.TextModeImageButton))
                controls.Add(this.TextModeImageButton);
        }

        #endregion

        #region Render

        private void CreateModeChangeCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

            td.Controls.Add(this.TextModeImageButton);
            td.Controls.Add(this.CalendarModeImageButton);

            htmlTableCellCollection.Add(td);
        }

        private void SetModeChangeDynamicStyle()
        {
            if (this.ShowCalendar)
            {
                this.TextModeImageButton.ImageUrl = this.TextModeImageUrl;
                if (this.ModeImageHeight > 0)
                    this.TextModeImageButton.Height = this.ModeImageHeight;
                if (this.ModeImageWidth > 0)
                    this.TextModeImageButton.Width = this.ModeImageWidth;
                this.TextModeImageButton.Visible = true;
                this.CalendarModeImageButton.Visible = false;
                return;
            }

            this.TextModeImageButton.Visible = false;
            this.CalendarModeImageButton.Visible = true;
            this.CalendarModeImageButton.ImageUrl = this.CalendarModeImageUrl;
            if (this.ModeImageHeight > 0)
                this.CalendarModeImageButton.Height = new Unit(this.ModeImageHeight);
            if (this.ModeImageWidth > 0)
                this.CalendarModeImageButton.Width = new Unit(this.ModeImageWidth);
        }

        #endregion

        #region Event Handlers

        void TextModeImageButton_Command(object sender, CommandEventArgs e)
        {
            this.ShowCalendar = false;
        }

        void CalendarModeImageButton_Command(object sender, CommandEventArgs e)
        {
            this.ShowCalendar = true;
        }

        #endregion
    }
}