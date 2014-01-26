using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.ComponentModel;

namespace LTEControls
{
    public partial class DateTimePicker : WebControl, INamingContainer
    {
        #region Properties

        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Themeable(true)]
        [DisplayName("No Date Text")]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DefaultValue("No Date")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Text to display next to the checkbox which indicates that there is no date selection")]
        public string NoDateText
        {
            get
            {
                if (this.ViewState["NoDateText"] == null)
                    return "No Date";

                return ((string)(this.ViewState["NoDateText"]));
            }
            set
            {
                this.ViewState["NoDateText"] = value;
            }
        }

        #endregion

        #region Controls

        protected Button _nowButton, _todayButton;
        protected CheckBox _nullDateCheckbox;

        internal Button NowButton
        {
            get
            {
                Control c;

                if (this._nowButton != null)
                    return this._nowButton;

                if ((c = this.FindControl("NowButton")) == null)
                {
                    this._nowButton = new Button();
                    this._nowButton.ID = "NowButton";
                    this._nowButton.Text = "Now";
                    this._nowButton.CommandName = "Now";
                    this._nowButton.Command += new CommandEventHandler(NowButton_Command);
                }
                else
                    this._nowButton = (Button)c;

                return this._nowButton;
            }
        }

        internal Button TodayButton
        {
            get
            {
                Control c;

                if (this._todayButton != null)
                    return this._todayButton;

                if ((c = this.FindControl("TodayButton")) == null)
                {
                    this._todayButton = new Button();
                    this._todayButton.ID = "TodayButton";
                    this._todayButton.Text = "Today";
                    this._todayButton.CommandName = "Today";
                    this._todayButton.Command += new CommandEventHandler(TodayButton_Command);
                }
                else
                    this._todayButton = (Button)c;

                return this._todayButton;
            }
        }

        internal CheckBox NullDateCheckbox
        {
            get
            {
                Control c;

                if (this._nullDateCheckbox != null)
                    return this._nullDateCheckbox;

                if ((c = this.FindControl("NullDateCheckbox")) == null)
                {
                    this._nullDateCheckbox = new CheckBox();
                    this._nullDateCheckbox.ID = "NullDateCheckbox";
                    this._nullDateCheckbox.CheckedChanged += new EventHandler(NullDateCheckbox_CheckedChanged);
                    this._nullDateCheckbox.AutoPostBack = true;
                }
                else
                    this._nullDateCheckbox = (CheckBox)c;

                return this._nullDateCheckbox;
            }
        }

        #endregion

        #region Initialize

        private void InitializeButtonsAndCheckbox()
        {
            this._nowButton = null;
            this._todayButton = null;
            this._nullDateCheckbox = null;
        }

        private void AddNullDateCheckbox(ControlCollection controls)
        {
            if (!controls.Contains(this.NullDateCheckbox))
                controls.Add(this.NullDateCheckbox);
        }

        private void AddNowButton(ControlCollection controls)
        {
            if (!controls.Contains(this.NowButton))
                controls.Add(this.NowButton);
        }

        private void AddTodayButton(ControlCollection controls)
        {
            if (!controls.Contains(this.TodayButton))
                controls.Add(this.TodayButton);
        }

        #endregion

        #region Render

        private void CreateCheckBoxCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.NullDateCheckbox);
            htmlTableCellCollection.Add(td);
        }

        private void CreateTodayCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.TodayButton);
            htmlTableCellCollection.Add(td);
        }

        private void CreateNowCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.NowButton);
            htmlTableCellCollection.Add(td);
        }

        private void SetButtonsAndCheckboxDynamicStyle()
        {
            this.NullDateCheckbox.Checked = (this.SelectedDate == null);
            this.NullDateCheckbox.Text = this.NoDateText;
        }

        #endregion

        #region Event Handlers

        void NullDateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NullDateCheckbox.Checked)
                this.SelectedDate = null;
            else
                this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void NowButton_Command(object sender, CommandEventArgs e)
        {
            this.SelectedDate = (DateTime?)(DateTime.Now);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void TodayButton_Command(object sender, CommandEventArgs e)
        {
            this.SelectedDate = (DateTime?)(DateTime.Now.Date);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        #endregion
    }
}