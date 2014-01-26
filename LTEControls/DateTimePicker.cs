using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.ComponentModel;

[assembly: TagPrefix("LTEControls", "lte")]

namespace LTEControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, 
        Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:DateTimePicker runat=\"server\"></{0}:DateTimePicker>")]
    [DefaultProperty("SelectedDate")]
    [Designer(typeof(DateTimePickerDesigner))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultEvent("SelectionChanged")]
    [ControlValueProperty("SelectedDate")]
    public partial class DateTimePicker : WebControl, INamingContainer
    {
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("Action")]
        public event EventHandler<EventArgs> SelectionChanged;

        protected System.Web.UI.HtmlControls.HtmlTable _topLevelTable;

        internal System.Web.UI.HtmlControls.HtmlTable TopLevelTable
        {
            get
            {
                Control c;

                if (this._topLevelTable != null)
                    return this._topLevelTable;

                if ((c = this.FindControl("TopLevelTable")) == null)
                {
                    this._topLevelTable = new System.Web.UI.HtmlControls.HtmlTable();
                    this._topLevelTable.ID = "TopLevelTable";
                    this._topLevelTable.Style.Add(HtmlTextWriterStyle.Margin, "0px");
                    this._topLevelTable.Style.Add(HtmlTextWriterStyle.Padding, "0px");
                    this._topLevelTable.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
                    this._topLevelTable.Style.Add(HtmlTextWriterStyle.BorderCollapse, "collapse");
                }
                else
                    this._topLevelTable = (System.Web.UI.HtmlControls.HtmlTable)c;

                return this._topLevelTable;
            }
        }

        #region properties

        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Themeable(false)]
        [DisplayName("Date Value")]
        [Description("This will return a date value, regardless of whether or not a date has been" +
           " selected. If no date has been selected, then it will return the last selected or" +
           " current date")]
        public DateTime DateValue
        {
            get
            {
                if (this.ViewState["DateValue"] == null)
                    return DateTime.Now;

                return ((DateTime)(this.ViewState["DateValue"]));
            }
            set
            {
                this.ViewState["DateValue"] = value;

                if (this.ViewState["SelectedDate"] != null)
                    this.ViewState["SelectedDate"] = (DateTime?)value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(false)]
        [DisplayName("Visible Date")]
        [Description("This is used to determine the currently displayed month of the calendar")]
        public DateTime VisibleDate
        {
            get
            {
                if (this.ViewState["VisibleDate"] == null)
                    return DateTime.Now;

                return ((DateTime)(this.ViewState["VisibleDate"]));
            }
            set
            {
                this.ViewState["VisibleDate"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(false)]
        [DisplayName("Selected Date")]
        [Description("Returns currently selected date or null if no date has been selected")]
        public DateTime? SelectedDate
        {
            get
            {
                return ((DateTime?)(this.ViewState["SelectedDate"]));
            }
            set
            {
                this.ViewState["SelectedDate"] = value;

                if (value != null)
                    this.ViewState["DateValue"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(false)]
        [ReadOnly(true)]
        [DisplayName("Is Date Null?")]
        [Description("Get or set a value to indicate if no date is selected")]
        public bool IsDateNull
        {
            get
            {
                return (this.ViewState["SelectedDate"] == null);
            }
            set
            {
                if (value == (this.ViewState["SelectedDate"] == null))
                    return;

                if (value)
                    this.ViewState["SelectedDate"] = null;
                else 
                    this.ViewState["SelectedDate"] = (DateTime?)(this.DateValue);
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(true)]
        [DefaultValue(false)]
        [DisplayName("Show Calendar")]
        [Description("Get or set a value to determine whether calendar is shown" +
            " (versus the date textbox)")]
        public bool ShowCalendar
        {
            get
            {
                if (this.ViewState["ShowCalendar"] == null)
                    return false;

                return ((bool)(this.ViewState["ShowCalendar"]));
            }
            set
            {
                this.ViewState["ShowCalendar"] = value;
            }
        }

        #endregion

        #region Constructors

        public DateTimePicker()
            : base(HtmlTextWriterTag.Div)
        {
            this.InitializeControl();
        }

        public DateTimePicker(HtmlTextWriterTag tag)
            : base(tag)
        {
            this.InitializeControl();
        }

        public DateTimePicker(string tag)
            : base(tag)
        {
            this.InitializeControl();
        }

        private void InitializeControl()
        {
            this.InitializeDateCalendar();
            this.InitializeDateTextBox();
            this.InitializeScrollers();
            this.InitializeButtonsAndCheckbox();
            this.InitializeModeButtons();
        }

        #endregion

        #region Overrides

        protected override void CreateChildControls()
        {
            this.CreateTopRow(this.TopLevelTable.Rows);
            this.CreateBottomRow(this.TopLevelTable.Rows);

            this.Controls.Add(this.TopLevelTable);
        }

        private void CreateTopRow(System.Web.UI.HtmlControls.HtmlTableRowCollection htmlTableRowCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableRow row;

            row = new System.Web.UI.HtmlControls.HtmlTableRow();

            this.CreateDateCell(row.Cells);
            this.CreateModeChangeCell(row.Cells);

            htmlTableRowCollection.Add(row);
        }

        private void CreateDateCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.ColSpan = 4;
            this.CreateCalendarControl(td.Controls);
            this.CreateTextboxControl(td.Controls);

            htmlTableCellCollection.Add(td);
        }

        private void CreateBottomRow(System.Web.UI.HtmlControls.HtmlTableRowCollection htmlTableRowCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableRow row;

            row = new System.Web.UI.HtmlControls.HtmlTableRow();

            this.CreateCheckBoxCell(row.Cells);
            this.CreateTimePickerCell(row.Cells);
            this.CreateTodayCell(row.Cells);
            this.CreateNowCell(row.Cells);

            htmlTableRowCollection.Add(row);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.SetCalendarDynamicStyle();
            this.SetTextBoxDynamicStyle();
            this.SetModeChangeDynamicStyle();
            this.SetScrollerDynamicStyle();
            this.SetButtonsAndCheckboxDynamicStyle();

            this.TopLevelTable.RenderControl(writer);
        }

        #endregion
    }
}
