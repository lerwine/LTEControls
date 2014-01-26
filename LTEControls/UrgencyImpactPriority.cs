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
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:UrgencyImpactPriority runat=\"server\"></{0}:UrgencyImpactPriority>")]
    [DefaultProperty("Text")]
    [Designer(typeof(UrgencyImpactPriorityDesigner))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultEvent("SelectionChanged")]
    [ControlValueProperty("Priority")]
    public class UrgencyImpactPriority : WebControl, INamingContainer
    {
        #region Properties

        protected Style _priorityLabelStyle, _headingLabelStyle, _radioButtonListStyle;

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Themeable(false)]
        [ReadOnly(true)]
        [DisplayName("Urgency Byte")]
        [Description("Byte value representation of urgency (commonly used for data binding)")]
        public byte UrgencyByte
        {
            get
            {
                if (this.ViewState["Urgency"] == null)
                    this.ViewState["Urgency"] = 2;

                return Convert.ToByte((System.Int16)(this.ViewState["Urgency"]));
            }
            set
            {
                int v;

                if ((v = (System.Int32)(Convert.ToInt16(value))) < 1 || v > 3)
                    throw new ArgumentOutOfRangeException("Urgency", "Urgency cannot be less than 1 or greater than 3.");

                this.ViewState["Urgency"] = v;
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Themeable(false)]
        [ReadOnly(true)]
        [DisplayName("Impact Byte")]
        [Description("Byte value representation of impact (commonly used for data binding)")]
        public byte ImpactByte
        {
            get
            {
                return Convert.ToByte((System.Int16)(this.Urgency));
            }
            set
            {
                int v;

                if ((v = (System.Int32)(Convert.ToInt16(value))) < 1 || v > 3)
                    throw new ArgumentOutOfRangeException("Impact", "Impact cannot be less than 1 or greater than 3.");

                this.Urgency = v;
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Themeable(false)]
        [Description("Urgency Value")]
        public int Urgency
        {
            get
            {
                if (this.ViewState["Urgency"] == null)
                    return 2;

                return ((int)(this.ViewState["Urgency"]));
            }
            set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentOutOfRangeException("Urgency", "Urgency cannot be less than 1 or greater than 3.");

                this.ViewState["Urgency"] = value;
            }
        }

        [Browsable(true)]
        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Themeable(false)]
        [Description("Impact Value")]
        public int Impact
        {
            get
            {
                if (this.ViewState["Impact"] == null)
                    return 2;

                return ((int)(this.ViewState["Impact"]));
            }
            set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentOutOfRangeException("Impact", "Impact cannot be less than 1 or greater than 3.");

                this.ViewState["Impact"] = value;
            }
        }

        public int Priority
        {
            get
            {
                return this.Urgency * this.Impact;
            }
        }

        [Bindable(false)]
        [Browsable(true)]
        [Category("Styles")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(true)]
        [DisplayName("Priority Label Style")]
        [Description("Style to apply to the priority label")]
        public Style PriorityLabelStyle
        {
            get
            {
                return this._priorityLabelStyle;
            }
        }

        [Bindable(false)]
        [Browsable(true)]
        [Category("Styles")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(true)]
        [DisplayName("Heading Label Style")]
        [Description("Style to apply to the radio button list titles")]
        public Style HeadingLabelStyle
        {
            get
            {
                return this._headingLabelStyle;
            }
        }

        [Bindable(false)]
        [Browsable(true)]
        [Category("Styles")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(true)]
        [DisplayName("RadioButtonList Style")]
        [Description("Style to apply to the RadioButtonList")]
        public Style RadioButtonListStyle
        {
            get
            {
                return this._radioButtonListStyle;
            }
        }

        #endregion

        #region Controls

        protected RadioButtonList _urgencyRadioButtonList, _impactRadioButtonList;
        Label _priorityLabel, _urgencyHeadingLabel, _impactHeadingLabel;
        protected System.Web.UI.HtmlControls.HtmlTable _topLevelTable;

        System.Web.UI.HtmlControls.HtmlTable TopLevelTable
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

        internal Label PriorityLabel
        {
            get
            {
                Control c;

                if (this._priorityLabel != null)
                    return this._priorityLabel;

                if ((c = this.FindControl("PriorityLabel")) == null)
                {
                    this._priorityLabel = new Label();
                    this._priorityLabel.ID = "PriorityLabel";
                }
                else
                    this._priorityLabel = (Label)c;

                return this._priorityLabel;
            }
        }

        internal Label UrgencyHeadingLabel
        {
            get
            {
                Control c;

                if (this._urgencyHeadingLabel != null)
                    return this._urgencyHeadingLabel;

                if ((c = this.FindControl("UrgencyHeadingLabel")) == null)
                {
                    this._urgencyHeadingLabel = new Label();
                    this._urgencyHeadingLabel.Text = "Urgency";
                    this._urgencyHeadingLabel.ID = "UrgencyHeadingLabel";
                }
                else
                    this._urgencyHeadingLabel = (Label)c;

                return this._urgencyHeadingLabel;
            }
        }

        internal Label ImpactHeadingLabel
        {
            get
            {
                Control c;

                if (this._impactHeadingLabel != null)
                    return this._impactHeadingLabel;

                if ((c = this.FindControl("ImpactHeadingLabel")) == null)
                {
                    this._impactHeadingLabel = new Label();
                    this._impactHeadingLabel.Text = "Impact";
                    this._impactHeadingLabel.ID = "ImpactHeadingLabel";
                }
                else
                    this._impactHeadingLabel = (Label)c;

                return this._impactHeadingLabel;
            }
        }

        internal RadioButtonList UrgencyRadioButtonList
        {
            get
            {
                Control c;

                if (this._urgencyRadioButtonList != null)
                    return this._urgencyRadioButtonList;

                if ((c = this.FindControl("UrgencyRadioButtonList")) == null)
                {
                    this._urgencyRadioButtonList = new RadioButtonList();
                    this._urgencyRadioButtonList.ID = "UrgencyRadioButtonList";
                    this._urgencyRadioButtonList.EnableViewState = false;
                    this._urgencyRadioButtonList.AutoPostBack = true;
                    this._urgencyRadioButtonList.RepeatDirection = RepeatDirection.Vertical;
                    this._urgencyRadioButtonList.RepeatLayout = RepeatLayout.Table;
                    this._urgencyRadioButtonList.Items.Add(new ListItem("High", "1"));
                    this._urgencyRadioButtonList.Items.Add(new ListItem("Medium", "2"));
                    this._urgencyRadioButtonList.Items.Add(new ListItem("Low", "3"));
                    this._urgencyRadioButtonList.SelectedIndexChanged += new EventHandler(UrgencyRadioButtonList_SelectedIndexChanged);
                }
                else
                    this._urgencyRadioButtonList = (RadioButtonList)c;

                return this._urgencyRadioButtonList;
            }
        }

        internal RadioButtonList ImpactRadioButtonList
        {
            get
            {
                Control c;

                if (this._impactRadioButtonList != null)
                    return this._impactRadioButtonList;

                if ((c = this.FindControl("ImpactRadioButtonList")) == null)
                {
                    this._impactRadioButtonList = new RadioButtonList();
                    this._impactRadioButtonList.ID = "ImpactRadioButtonList";
                    this._impactRadioButtonList.EnableViewState = false;
                    this._impactRadioButtonList.AutoPostBack = true;
                    this._impactRadioButtonList.RepeatDirection = RepeatDirection.Vertical;
                    this._impactRadioButtonList.RepeatLayout = RepeatLayout.Table;
                    this._impactRadioButtonList.Items.Add(new ListItem("High", "1"));
                    this._impactRadioButtonList.Items.Add(new ListItem("Medium", "2"));
                    this._impactRadioButtonList.Items.Add(new ListItem("Low", "3"));
                    this._impactRadioButtonList.SelectedIndexChanged += new EventHandler(ImpactRadioButtonList_SelectedIndexChanged);
                }
                else
                    this._impactRadioButtonList = (RadioButtonList)c;

                return this._impactRadioButtonList;
            }
        }

        #endregion

        public UrgencyImpactPriority()
        {
            this._priorityLabelStyle = null;
            this._headingLabelStyle = null;
            this._radioButtonListStyle = null;

            this._urgencyRadioButtonList = null;
            this._impactRadioButtonList = null;
            this._priorityLabel = null;
            this._urgencyHeadingLabel = null;
            this._impactHeadingLabel = null;
            this._topLevelTable = null;

        }

        protected override void CreateChildControls()
        {
            System.Web.UI.HtmlControls.HtmlTableRow row;
            System.Web.UI.HtmlControls.HtmlTableCell cell;

            row = new System.Web.UI.HtmlControls.HtmlTableRow();

            cell = new System.Web.UI.HtmlControls.HtmlTableCell();
            cell.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            cell.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            cell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            cell.Controls.Add(this.PriorityLabel);

            row.Cells.Add(cell);

            cell = new System.Web.UI.HtmlControls.HtmlTableCell();
            cell.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            cell.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            cell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            cell.Controls.Add(this.UrgencyHeadingLabel);
            row.Cells.Add(cell);

            cell = new System.Web.UI.HtmlControls.HtmlTableCell();
            cell.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            cell.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            cell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            cell.Controls.Add(this.ImpactHeadingLabel);
            row.Cells.Add(cell);
            this.TopLevelTable.Rows.Add(row);

            row = new System.Web.UI.HtmlControls.HtmlTableRow();

            cell = new System.Web.UI.HtmlControls.HtmlTableCell();
            cell.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            cell.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            cell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            cell.InnerText = " ";
            row.Cells.Add(cell);

            cell = new System.Web.UI.HtmlControls.HtmlTableCell();
            cell.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            cell.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            cell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            cell.Controls.Add(this.UrgencyRadioButtonList);
            row.Cells.Add(cell);

            cell = new System.Web.UI.HtmlControls.HtmlTableCell();
            cell.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            cell.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            cell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            cell.Controls.Add(this.ImpactRadioButtonList);
            row.Cells.Add(cell);
            this.TopLevelTable.Rows.Add(row);

            this.Controls.Add(this.TopLevelTable);
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            this.PriorityLabel.ApplyStyle(this.PriorityLabelStyle);
            this.PriorityLabel.Text = this.Priority.ToString();
            this.UrgencyRadioButtonList.ApplyStyle(this.RadioButtonListStyle);
            this.UrgencyRadioButtonList.SelectedValue = this.Urgency.ToString();
            this.UrgencyHeadingLabel.ApplyStyle(this.HeadingLabelStyle);
            this.ImpactRadioButtonList.ApplyStyle(this.RadioButtonListStyle);
            this.ImpactRadioButtonList.SelectedValue = this.Impact.ToString();
            this.ImpactHeadingLabel.ApplyStyle(this.HeadingLabelStyle);

            this.TopLevelTable.RenderControl(writer);
        }

        protected void UrgencyRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Urgency = Convert.ToInt32(this.UrgencyRadioButtonList.SelectedValue);

            EventHandler temp = this.SelectionChanged;
            if (temp != null)
                temp(this, e);
        }

        void ImpactRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Impact = Convert.ToInt32(this.ImpactRadioButtonList.SelectedValue);

            EventHandler temp = this.SelectionChanged;
            if (temp != null)
                temp(this, e);
        }

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler SelectionChanged;
    }
}
