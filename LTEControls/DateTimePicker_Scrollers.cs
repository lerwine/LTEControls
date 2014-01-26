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
        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral," +
            " PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [UrlProperty]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Localizable(true)]
        [Themeable(false)]
        [DisplayName("Not Null Up/Down Image URL")]
        [Description("URL of image to use for up/down scroller when selected date is not null")]
        public string UpDownImageUrl
        {
            get
            {
                if (this.ViewState["NotNullUpDownImageUrl"] == null)
                    return "";
                return (string)(this.ViewState["NotNullUpDownImageUrl"]);
            }
            set
            {
                this.ViewState["NotNullUpDownImageUrl"] = value;
            }
        }

        [Bindable(false)]
        [Browsable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(false)]
        [DisplayName("Up Hot Spot")]
        [Description("Rectangle defining the area of the scroller image for scrolling up")]
        public System.Drawing.Rectangle UpHotSpot
        {
            get
            {
                if (this.ViewState["UpHotSpot"] == null)
                    this.ViewState["UpHotSpot"] = new System.Drawing.Rectangle();

                return ((System.Drawing.Rectangle)(this.ViewState["UpHotSpot"]));
            }
        }

        [Bindable(false)]
        [Browsable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [NotifyParentProperty(true)]
        [Themeable(false)]
        [DisplayName("Down Hot Spot")]
        [Description("Rectangle defining the area of the scroller image for scrolling down")]
        public System.Drawing.Rectangle DownHotSpot
        {
            get
            {
                if (this.ViewState["DownHotSpot"] == null)
                    this.ViewState["DownHotSpot"] = new System.Drawing.Rectangle();

                return ((System.Drawing.Rectangle)(this.ViewState["DownHotSpot"]));
            }
        }

        [Bindable(true)]
        [DefaultValue(-1)]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [Category("Layout")]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Themeable(false)]
        [DisplayName("Up/Down Image Height")]
        [Description("Height of images used for up/down scroller")]
        public int UpDownImageHeight
        {
            get
            {
                if (this.ViewState["UpDownImageHeight"] == null)
                    return -1;
                return (int)(this.ViewState["UpDownImageHeight"]);
            }
            set
            {
                this.ViewState["UpDownImageHeight"] = value;
            }
        }

        [Bindable(true)]
        [DefaultValue(-1)]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [Category("Layout")]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Themeable(false)]
        [DisplayName("Up/Down Image Width")]
        [Description("Width of images used for up/down scroller")]
        public int UpDownImageWidth
        {
            get
            {
                if (this.ViewState["UpDownImageWidth"] == null)
                    return -1;
                return (int)(this.ViewState["UpDownImageWidth"]);
            }
            set
            {
                this.ViewState["UpDownImageWidth"] = value;
            }
        }

        [Bindable(true)]
        [DefaultValue(5)]
        [Browsable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [Category("Behavior")]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Themeable(true)]
        [DisplayName("Minute/Second Increment")]
        [Description("When the scroller's up/down is clicked, the minutes or seconds are changed by" +
            " this amount")]
        public int MinuteSecondIncrement
        {
            get
            {
                if (this.ViewState["MinuteSecondIncrement"] == null)
                    return 5;
                return (int)(this.ViewState["MinuteSecondIncrement"]);
            }
            set
            {
                this.ViewState["MinuteSecondIncrement"] = (value < 1) ? -1 : value;
            }
        }

        #endregion

        #region Controls

        protected TextBox _notNullHourScrollerTextBox, _nullHourScrollerTextBox, _notNullMinuteScrollerTextBox, 
            _nullMinuteScrollerTextBox, _notNullSecondScrollerTextBox, _nullSecondScrollerTextBox;
        protected ImageMap _hourUpDownImageMap, _minuteUpDownImageMap, _secondUpDownImageMap;
        protected DropDownList _incrementDropDownList;
        protected Label _hourMinuteSeparatorLabel, _minuteSecondSeparatorLabel;

        internal TextBox NotNullHourScrollerTextBox
        {
            get
            {
                Control c;

                if (this._notNullHourScrollerTextBox != null)
                    return this._notNullHourScrollerTextBox;

                if ((c = this.FindControl("NotNullHourScrollerTextBox")) == null)
                {
                    this._notNullHourScrollerTextBox = new TextBox();
                    this._notNullHourScrollerTextBox.ID = "NotNullHourScrollerTextBox";
                    this._notNullHourScrollerTextBox.SkinID = "NotNullDateTimeTextBox";
                    this._notNullHourScrollerTextBox.TextChanged += new EventHandler(HourScrollerTextBox_TextChanged);
                    this._notNullHourScrollerTextBox.AutoPostBack = true;
                    this._notNullHourScrollerTextBox.Width = new Unit("24px");
                }
                else
                    this._notNullHourScrollerTextBox = (TextBox)c;

                return this._notNullHourScrollerTextBox;
            }
        }

        internal TextBox NullHourScrollerTextBox
        {
            get
            {
                Control c;

                if (this._nullHourScrollerTextBox != null)
                    return this._nullHourScrollerTextBox;

                if ((c = this.FindControl("NullHourScrollerTextBox")) == null)
                {
                    this._nullHourScrollerTextBox = new TextBox();
                    this._nullHourScrollerTextBox.ID = "NullHourScrollerTextBox";
                    this._notNullHourScrollerTextBox.SkinID = "NullDateTimeTextBox";
                    this._nullHourScrollerTextBox.TextChanged += new EventHandler(HourScrollerTextBox_TextChanged);
                    this._nullHourScrollerTextBox.AutoPostBack = true;
                    this._nullHourScrollerTextBox.Width = new Unit("24px");
                }
                else
                    this._nullHourScrollerTextBox = (TextBox)c;

                return this._nullHourScrollerTextBox;
            }
        }

        internal TextBox NotNullMinuteScrollerTextBox
        {
            get
            {
                Control c;

                if (this._notNullMinuteScrollerTextBox != null)
                    return this._notNullMinuteScrollerTextBox;

                if ((c = this.FindControl("NotNullMinuteScrollerTextBox")) == null)
                {
                    this._notNullMinuteScrollerTextBox = new TextBox();
                    this._notNullMinuteScrollerTextBox.ID = "NotNullMinuteScrollerTextBox";
                    this._notNullHourScrollerTextBox.SkinID = "NotNullDateTimeTextBox";
                    this._notNullMinuteScrollerTextBox.TextChanged += new EventHandler(MinuteScrollerTextBox_TextChanged);
                    this._notNullMinuteScrollerTextBox.AutoPostBack = true;
                    this._notNullMinuteScrollerTextBox.Width = new Unit("24px");
                }
                else
                    this._notNullMinuteScrollerTextBox = (TextBox)c;

                return this._notNullMinuteScrollerTextBox;
            }
        }

        internal TextBox NullMinuteScrollerTextBox
        {
            get
            {
                Control c;

                if (this._notNullMinuteScrollerTextBox != null)
                    return this._notNullMinuteScrollerTextBox;

                if ((c = this.FindControl("NullMinuteScrollerTextBox")) == null)
                {
                    this._notNullMinuteScrollerTextBox = new TextBox();
                    this._notNullMinuteScrollerTextBox.ID = "NullMinuteScrollerTextBox";
                    this._notNullHourScrollerTextBox.SkinID = "NullDateTimeTextBox";
                    this._notNullMinuteScrollerTextBox.TextChanged += new EventHandler(MinuteScrollerTextBox_TextChanged);
                    this._notNullMinuteScrollerTextBox.AutoPostBack = true;
                    this._notNullMinuteScrollerTextBox.Width = new Unit("24px");
                }
                else
                    this._notNullMinuteScrollerTextBox = (TextBox)c;

                return this._notNullMinuteScrollerTextBox;
            }
        }

        internal TextBox NotNullSecondScrollerTextBox
        {
            get
            {
                Control c;

                if (this._notNullSecondScrollerTextBox != null)
                    return this._notNullSecondScrollerTextBox;

                if ((c = this.FindControl("NotNullSecondScrollerTextBox")) == null)
                {
                    this._notNullSecondScrollerTextBox = new TextBox();
                    this._notNullSecondScrollerTextBox.ID = "NotNullSecondScrollerTextBox";
                    this._notNullHourScrollerTextBox.SkinID = "NotNullDateTimeTextBox";
                    this._notNullSecondScrollerTextBox.TextChanged += new EventHandler(SecondScrollerTextBox_TextChanged);
                    this._notNullSecondScrollerTextBox.AutoPostBack = true;
                    this._notNullSecondScrollerTextBox.Width = new Unit("24px");
                }
                else
                    this._notNullSecondScrollerTextBox = (TextBox)c;

                return this._notNullSecondScrollerTextBox;
            }
        }

        internal TextBox NullSecondScrollerTextBox
        {
            get
            {
                Control c;

                if (this._nullSecondScrollerTextBox != null)
                    return this._nullSecondScrollerTextBox;

                if ((c = this.FindControl("NullSecondScrollerTextBox")) == null)
                {
                    this._nullSecondScrollerTextBox = new TextBox();
                    this._nullSecondScrollerTextBox.ID = "NullSecondScrollerTextBox";
                    this._notNullHourScrollerTextBox.SkinID = "NullDateTimeTextBox";
                    this._nullSecondScrollerTextBox.TextChanged += new EventHandler(SecondScrollerTextBox_TextChanged);
                    this._nullSecondScrollerTextBox.AutoPostBack = true;
                    this._nullSecondScrollerTextBox.Width = new Unit("24px");
                }
                else
                    this._notNullSecondScrollerTextBox = (TextBox)c;

                return this._notNullSecondScrollerTextBox;
            }
        }

        internal ImageMap HourUpDownImageMap
        {
            get
            {
                Control c;
                RectangleHotSpot upHotSpot, downHotSpot;

                if (this._hourUpDownImageMap != null)
                    return this._hourUpDownImageMap;

                if ((c = this.FindControl("HourUpDownImageMap")) == null)
                {
                    this._hourUpDownImageMap = new ImageMap();
                    this._hourUpDownImageMap.ID = "HourUpDownImageMap";
                    this._hourUpDownImageMap.HotSpotMode = HotSpotMode.PostBack;
                    upHotSpot = new RectangleHotSpot();
                    upHotSpot.PostBackValue = "Up";
                    downHotSpot = new RectangleHotSpot();
                    downHotSpot.PostBackValue = "Down";
                    this._hourUpDownImageMap.HotSpots.Add(upHotSpot);
                    this._hourUpDownImageMap.HotSpots.Add(downHotSpot);
                    this._hourUpDownImageMap.Click += new ImageMapEventHandler(HourUpDownImageMap_Click);
                }
                else
                    this._hourUpDownImageMap = (ImageMap)c;

                return this._hourUpDownImageMap;
            }
        }

        internal ImageMap MinuteUpDownImageMap
        {
            get
            {
                Control c;
                RectangleHotSpot upHotSpot, downHotSpot;

                if (this._minuteUpDownImageMap != null)
                    return this._minuteUpDownImageMap;

                if ((c = this.FindControl("MinuteUpDownImageMap")) == null)
                {
                    this._minuteUpDownImageMap = new ImageMap();
                    this._minuteUpDownImageMap.ID = "MinuteUpDownImageMap";
                    this._minuteUpDownImageMap.HotSpotMode = HotSpotMode.PostBack;
                    upHotSpot = new RectangleHotSpot();
                    upHotSpot.PostBackValue = "Up";
                    downHotSpot = new RectangleHotSpot();
                    downHotSpot.PostBackValue = "Down";
                    this._minuteUpDownImageMap.HotSpots.Add(upHotSpot);
                    this._minuteUpDownImageMap.HotSpots.Add(downHotSpot);
                    this._minuteUpDownImageMap.Click += new ImageMapEventHandler(MinuteUpDownImageMap_Click);
                }
                else
                    this._minuteUpDownImageMap = (ImageMap)c;

                return this._minuteUpDownImageMap;
            }
        }

        internal ImageMap SecondUpDownImageMap
        {
            get
            {
                Control c;
                RectangleHotSpot upHotSpot, downHotSpot;

                if (this._secondUpDownImageMap != null)
                    return this._secondUpDownImageMap;

                if ((c = this.FindControl("SecondUpDownImageMap")) == null)
                {
                    this._secondUpDownImageMap = new ImageMap();
                    this._secondUpDownImageMap.ID = "SecondUpDownImageMap";
                    this._secondUpDownImageMap.HotSpotMode = HotSpotMode.PostBack;
                    upHotSpot = new RectangleHotSpot();
                    upHotSpot.PostBackValue = "Up";
                    downHotSpot = new RectangleHotSpot();
                    downHotSpot.PostBackValue = "Down";
                    this._secondUpDownImageMap.HotSpots.Add(upHotSpot);
                    this._secondUpDownImageMap.HotSpots.Add(downHotSpot);
                    this._secondUpDownImageMap.Click += new ImageMapEventHandler(SecondUpDownImageMap_Click);
                }
                else
                    this._secondUpDownImageMap = (ImageMap)c;

                return this._secondUpDownImageMap;
            }
        }

        internal Label HourMinuteSeparatorLabel
        {
            get
            {
                Control c;

                if (this._hourMinuteSeparatorLabel != null)
                    return this._hourMinuteSeparatorLabel;

                if ((c = this.FindControl("HourMinuteSeparatorLabel")) == null)
                {
                    this._hourMinuteSeparatorLabel = new Label();
                    this._hourMinuteSeparatorLabel.ID = "HourMinuteSeparatorLabel";
                    this._hourMinuteSeparatorLabel.Font.Bold = true;
                    this._hourMinuteSeparatorLabel.Text = ":";
                }
                else
                    this._hourMinuteSeparatorLabel = (Label)c;

                return this._hourMinuteSeparatorLabel;
            }
        }

        internal Label MinuteSecondSeparatorLabel
        {
            get
            {
                Control c;

                if (this._minuteSecondSeparatorLabel != null)
                    return this._minuteSecondSeparatorLabel;

                if ((c = this.FindControl("MinuteSecondSeparatorLabel")) == null)
                {
                    this._minuteSecondSeparatorLabel = new Label();
                    this._minuteSecondSeparatorLabel.ID = "MinuteSecondSeparatorLabel";
                    this._minuteSecondSeparatorLabel.Font.Bold = true;
                    this._minuteSecondSeparatorLabel.Text = ":";
                }
                else
                    this._minuteSecondSeparatorLabel = (Label)c;

                return this._minuteSecondSeparatorLabel;
            }
        }

        internal DropDownList IncrementDropDownList
        {
            get
            {
                Control c;

                if (this._incrementDropDownList != null)
                    return this._incrementDropDownList;

                if ((c = this.FindControl("IncrementDropDownList")) == null)
                {
                    this._incrementDropDownList = new DropDownList();
                    this._incrementDropDownList.ID = "IncrementDropDownList";
                    this._incrementDropDownList.Items.Add(new ListItem("1", "1"));
                    this._incrementDropDownList.Items.Add(new ListItem("2", "2"));
                    this._incrementDropDownList.Items.Add(new ListItem("5", "5"));
                    this._incrementDropDownList.Items.Add(new ListItem("10", "10"));
                    this._incrementDropDownList.Items.Add(new ListItem("15", "15"));
                    this._incrementDropDownList.SelectedIndexChanged += new EventHandler(IncrementDropDownList_SelectedIndexChanged);
                    this._incrementDropDownList.AutoPostBack = true;
                }
                else
                    this._incrementDropDownList = (DropDownList)c;

                return this._incrementDropDownList;
            }
        }

        #endregion

        #region Initialize

        private void InitializeScrollers()
        {
            this._notNullHourScrollerTextBox = null;
            this._nullHourScrollerTextBox = null;
            this._notNullMinuteScrollerTextBox = null;
            this._nullMinuteScrollerTextBox = null;
            this._notNullSecondScrollerTextBox = null;
            this._nullSecondScrollerTextBox = null;
            this._hourUpDownImageMap = null;
            this._minuteUpDownImageMap = null;
            this._secondUpDownImageMap = null;
            this._incrementDropDownList = null;
        }

        private void AddScrollerControls()
        {
            if (!this.Controls.Contains(this.NotNullHourScrollerTextBox))
                this.Controls.Add(this.NotNullHourScrollerTextBox);
            if (!this.Controls.Contains(this.NullHourScrollerTextBox))
                this.Controls.Add(this.NullHourScrollerTextBox);
            if (!this.Controls.Contains(this.HourUpDownImageMap))
                this.Controls.Add(this.HourUpDownImageMap);
            if (!this.Controls.Contains(this.NotNullMinuteScrollerTextBox))
                this.Controls.Add(this.NotNullMinuteScrollerTextBox);
            if (!this.Controls.Contains(this.NullMinuteScrollerTextBox))
                this.Controls.Add(this.NullMinuteScrollerTextBox);
            if (!this.Controls.Contains(this.MinuteUpDownImageMap))
                this.Controls.Add(this.MinuteUpDownImageMap);
            if (!this.Controls.Contains(this.NotNullSecondScrollerTextBox))
                this.Controls.Add(this.NotNullSecondScrollerTextBox);
            if (!this.Controls.Contains(this.NullSecondScrollerTextBox))
                this.Controls.Add(this.NullSecondScrollerTextBox);
            if (!this.Controls.Contains(this.SecondUpDownImageMap))
                this.Controls.Add(this.SecondUpDownImageMap);
            if (!this.Controls.Contains(this.IncrementDropDownList))
                this.Controls.Add(this.IncrementDropDownList);
        }

        #endregion

        #region Render

        private void CreateTimePickerCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

            this.CreateScrollerTable(td.Controls);

            htmlTableCellCollection.Add(td);
        }

        private void CreateScrollerTable(ControlCollection controlCollection)
        {
            System.Web.UI.HtmlControls.HtmlTable table;
            System.Web.UI.HtmlControls.HtmlTableRow tr;

            table = new System.Web.UI.HtmlControls.HtmlTable();
            table.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            table.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            table.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            table.Style.Add(HtmlTextWriterStyle.BorderCollapse, "collapse");
            tr = new System.Web.UI.HtmlControls.HtmlTableRow();
            this.CreateHourScrollerCells(tr.Cells);
            this.CreateMinuteScrollerCells(tr.Cells);
            this.CreateSecondScrollerCells(tr.Cells);
            this.CreateIncrementDropDownCell(tr.Cells);
            table.Rows.Add(tr);
            controlCollection.Add(table);
        }

        private void CreateHourScrollerCells(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.NotNullHourScrollerTextBox);
            td.Controls.Add(this.NullHourScrollerTextBox);
            htmlTableCellCollection.Add(td);

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.HourUpDownImageMap);
            htmlTableCellCollection.Add(td);

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.HourMinuteSeparatorLabel);
            htmlTableCellCollection.Add(td);
        }

        private void CreateMinuteScrollerCells(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.NotNullMinuteScrollerTextBox);
            td.Controls.Add(this.NullMinuteScrollerTextBox);
            htmlTableCellCollection.Add(td);

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.MinuteUpDownImageMap);
            htmlTableCellCollection.Add(td);

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.MinuteSecondSeparatorLabel);
            htmlTableCellCollection.Add(td);
        }

        private void CreateSecondScrollerCells(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.NotNullSecondScrollerTextBox);
            td.Controls.Add(this.NullSecondScrollerTextBox);
            htmlTableCellCollection.Add(td);

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.SecondUpDownImageMap);
            htmlTableCellCollection.Add(td);
        }

        private void CreateIncrementDropDownCell(System.Web.UI.HtmlControls.HtmlTableCellCollection htmlTableCellCollection)
        {
            System.Web.UI.HtmlControls.HtmlTableCell td;

            td = new System.Web.UI.HtmlControls.HtmlTableCell();
            td.Style.Add(HtmlTextWriterStyle.Margin, "0px");
            td.Style.Add(HtmlTextWriterStyle.Padding, "0px");
            td.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            td.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            td.Controls.Add(this.IncrementDropDownList);
            htmlTableCellCollection.Add(td);
        }

        private void SetScrollerDynamicStyle()
        {
            if (!this.ShowCalendar)
            {
                this.NotNullHourScrollerTextBox.Visible = false;
                this.NullHourScrollerTextBox.Visible = false;
                this.HourUpDownImageMap.Visible = false;
                this.HourMinuteSeparatorLabel.Visible = false;
                this.NotNullMinuteScrollerTextBox.Visible = false;
                this.NullMinuteScrollerTextBox.Visible = false;
                this.MinuteUpDownImageMap.Visible = false;
                this.MinuteSecondSeparatorLabel.Visible = false;
                this.NotNullSecondScrollerTextBox.Visible = false;
                this.NullSecondScrollerTextBox.Visible = false;
                this.SecondUpDownImageMap.Visible = false;
                this.IncrementDropDownList.Visible = false;
                return;
            }

            if (this.SelectedDate == null)
            {
                this.NullHourScrollerTextBox.Visible = true;
                this.NotNullHourScrollerTextBox.Visible = false;
                this.NullHourScrollerTextBox.Text = this.DateValue.ToString("HH");
                this.NullMinuteScrollerTextBox.Visible = true;
                this.NotNullMinuteScrollerTextBox.Visible = false;
                this.NullMinuteScrollerTextBox.Text = this.DateValue.ToString("mm");
                this.NullSecondScrollerTextBox.Visible = true;
                this.NotNullSecondScrollerTextBox.Visible = false;
                this.NullSecondScrollerTextBox.Text = this.DateValue.ToString("ss");
            }
            else
            {
                this.NullHourScrollerTextBox.Visible = false;
                this.NotNullHourScrollerTextBox.Visible = true;
                this.NullHourScrollerTextBox.Text = this.DateValue.ToString("HH");
                this.NullMinuteScrollerTextBox.Visible = false;
                this.NotNullMinuteScrollerTextBox.Visible = true;
                this.NullMinuteScrollerTextBox.Text = this.DateValue.ToString("mm");
                this.NullSecondScrollerTextBox.Visible = false;
                this.NotNullSecondScrollerTextBox.Visible = true;
                this.NullSecondScrollerTextBox.Text = this.DateValue.ToString("ss");
            }

            this.HourUpDownImageMap.Visible = true;
            this.MinuteUpDownImageMap.Visible = true;
            this.SecondUpDownImageMap.Visible = true;

            this.HourUpDownImageMap.ImageUrl = this.UpDownImageUrl;
            this.MinuteUpDownImageMap.ImageUrl = this.UpDownImageUrl;
            this.SecondUpDownImageMap.ImageUrl = this.UpDownImageUrl;

            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[0])).Top = this.UpHotSpot.Top;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[0])).Bottom = this.UpHotSpot.Bottom;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[0])).Left = this.UpHotSpot.Left;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[0])).Right = this.UpHotSpot.Right;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[1])).Top = this.DownHotSpot.Top;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[1])).Bottom = this.DownHotSpot.Bottom;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[1])).Left = this.DownHotSpot.Left;
            ((RectangleHotSpot)(this.HourUpDownImageMap.HotSpots[1])).Right = this.DownHotSpot.Right;
            if (this.UpDownImageHeight > 0)
                this.HourUpDownImageMap.Height = this.UpDownImageHeight;
            if (this.UpDownImageWidth > 0)
                this.HourUpDownImageMap.Width = this.UpDownImageWidth;

            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[0])).Top = this.UpHotSpot.Top;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[0])).Bottom = this.UpHotSpot.Bottom;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[0])).Left = this.UpHotSpot.Left;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[0])).Right = this.UpHotSpot.Right;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[1])).Top = this.DownHotSpot.Top;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[1])).Bottom = this.DownHotSpot.Bottom;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[1])).Left = this.DownHotSpot.Left;
            ((RectangleHotSpot)(this.MinuteUpDownImageMap.HotSpots[1])).Right = this.DownHotSpot.Right;
            if (this.UpDownImageHeight > 0)
                this.MinuteUpDownImageMap.Height = this.UpDownImageHeight;
            if (this.UpDownImageWidth > 0)
                this.MinuteUpDownImageMap.Width = this.UpDownImageWidth;

            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[0])).Top = this.UpHotSpot.Top;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[0])).Bottom = this.UpHotSpot.Bottom;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[0])).Left = this.UpHotSpot.Left;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[0])).Right = this.UpHotSpot.Right;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[1])).Top = this.DownHotSpot.Top;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[1])).Bottom = this.DownHotSpot.Bottom;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[1])).Left = this.DownHotSpot.Left;
            ((RectangleHotSpot)(this.SecondUpDownImageMap.HotSpots[1])).Right = this.DownHotSpot.Right;
            if (this.UpDownImageHeight > 0)
                this.SecondUpDownImageMap.Height = this.UpDownImageHeight;
            if (this.UpDownImageWidth > 0)
                this.SecondUpDownImageMap.Width = this.UpDownImageWidth;

            this.HourMinuteSeparatorLabel.Visible = true;
            this.MinuteSecondSeparatorLabel.Visible = true;

            this.IncrementDropDownList.Visible = true;
            foreach (ListItem item in this.IncrementDropDownList.Items)
            {
                item.Selected = (item.Value == this.MinuteSecondIncrement.ToString());
            }
        }

        #endregion

        #region Event Handlers

        void HourScrollerTextBox_TextChanged(object sender, EventArgs e)
        {
            int hourValue;

            try
            {
                hourValue = Convert.ToInt32(this.NotNullHourScrollerTextBox.Text);
            }
            catch
            {
                hourValue = 0;
            }
            if (hourValue < 0)
                hourValue = 0;
            else
            {
                if (hourValue == 24)
                    hourValue = 0;
                else
                {
                    if (hourValue > 24)
                        hourValue = 23;
                }
            }
            this.DateValue = Convert.ToDateTime(this.DateValue.ToString("MM/dd/yyyy '00':mm:ss"));
            this.DateValue.AddHours(hourValue);
            this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void MinuteScrollerTextBox_TextChanged(object sender, EventArgs e)
        {
            int minuteValue;

            try
            {
                minuteValue = Convert.ToInt32(this.NotNullMinuteScrollerTextBox.Text);
            }
            catch
            {
                minuteValue = 0;
            }
            if (minuteValue < 0)
                minuteValue = 0;
            else
            {
                if (minuteValue == 60)
                    minuteValue = 0;
                else
                {
                    if (minuteValue > 60)
                        minuteValue = 59;
                }
            }
            this.DateValue = Convert.ToDateTime(this.DateValue.ToString("MM/dd/yyyy HH:'00':ss"));
            this.DateValue.AddMinutes(minuteValue);
            this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void SecondScrollerTextBox_TextChanged(object sender, EventArgs e)
        {
            int secondValue;

            try
            {
                secondValue = Convert.ToInt32(this.NotNullSecondScrollerTextBox.Text);
            }
            catch
            {
                secondValue = 0;
            }
            if (secondValue < 0)
                secondValue = 0;
            else
            {
                if (secondValue == 60)
                    secondValue = 0;
                else
                {
                    if (secondValue > 60)
                        secondValue = 59;
                }
            }
            this.DateValue = Convert.ToDateTime(this.DateValue.ToString("MM/dd/yyyy HH:mm:'00'"));
            this.DateValue.AddSeconds(secondValue);
            this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void HourUpDownImageMap_Click(object sender, ImageMapEventArgs e)
        {
            if (e.PostBackValue == "Up")
                this.DateValue.AddHours(1);
            else
                this.DateValue.AddHours(-1);

            this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void MinuteUpDownImageMap_Click(object sender, ImageMapEventArgs e)
        {
            if (e.PostBackValue == "Up")
                this.DateValue.AddMinutes(this.MinuteSecondIncrement);
            else
                this.DateValue.AddMinutes(this.MinuteSecondIncrement * -1);

            this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void SecondUpDownImageMap_Click(object sender, ImageMapEventArgs e)
        {
            if (e.PostBackValue == "Up")
                this.DateValue.AddSeconds(this.MinuteSecondIncrement);
            else
                this.DateValue.AddSeconds(this.MinuteSecondIncrement * -1);

            this.SelectedDate = (DateTime?)(this.DateValue);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void IncrementDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MinuteSecondIncrement = Convert.ToInt32(this.IncrementDropDownList.SelectedValue);
        }

        #endregion
    }
}