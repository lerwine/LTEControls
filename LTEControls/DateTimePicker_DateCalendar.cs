using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.ComponentModel;

namespace LTEControls
{
    public partial class DateTimePicker
    {
        #region Controls

        protected Calendar _nullDateCalendar, _notNullDateCalendar;

        internal Calendar NullDateCalendar
        {
            get
            {
                Control c;

                if (this._nullDateCalendar != null)
                    return this._nullDateCalendar;

                if ((c = this.FindControl("NullDateCalendar")) == null)
                {
                    this._nullDateCalendar = new Calendar();
                    this._nullDateCalendar.ID = "NullDateCalendar";
                    this._nullDateCalendar.SkinID = "NullDatePickerCalendar";
                    this._nullDateCalendar.EnableTheming = true;
                    this._nullDateCalendar.EnableViewState = true;
                    this._nullDateCalendar.SelectionChanged += new EventHandler(DateCalendar_SelectionChanged);
                    this._nullDateCalendar.VisibleMonthChanged += new MonthChangedEventHandler(DateCalendar_VisibleMonthChanged);
                }
                else
                    this._nullDateCalendar = (Calendar)c;

                return this._nullDateCalendar;
            }
        }

        internal Calendar NotNullDateCalendar
        {
            get
            {
                Control c;

                if (this._notNullDateCalendar != null)
                    return this._notNullDateCalendar;

                if ((c = this.FindControl("NotNullDateCalendar")) == null)
                {
                    this._notNullDateCalendar = new Calendar();
                    this._notNullDateCalendar.ID = "NotNullDateCalendar";
                    this._notNullDateCalendar.SkinID = "NotNullDatePickerCalendar";
                    this._notNullDateCalendar.EnableTheming = true;
                    this._notNullDateCalendar.EnableViewState = true;
                    this._notNullDateCalendar.SelectionChanged += new EventHandler(DateCalendar_SelectionChanged);
                    this._notNullDateCalendar.VisibleMonthChanged += new MonthChangedEventHandler(DateCalendar_VisibleMonthChanged);
                }
                else
                    this._notNullDateCalendar = (Calendar)c;

                return this._notNullDateCalendar;
            }
        }

        #endregion

        #region Initialize

        private void InitializeDateCalendar()
        {
            this._nullDateCalendar = null;
        }

        private void AddDateCalendarControls(ControlCollection controls)
        {
            if (!controls.Contains(this.NullDateCalendar))
                controls.Add(this.NullDateCalendar);
            if (!controls.Contains(this.NotNullDateCalendar))
                controls.Add(this.NotNullDateCalendar);
        }

        #endregion

        #region Render

        private void CreateCalendarControl(ControlCollection controlCollection)
        {
            controlCollection.Add(this.NullDateCalendar);
            controlCollection.Add(this.NotNullDateCalendar);
        }

        private void SetCalendarDynamicStyle()
        {
            if (!this.ShowCalendar)
            {
                this.NullDateCalendar.Visible = false;
                this.NotNullDateCalendar.Visible = false;
                return;
            }

            if (this.SelectedDate == null)
            {
                this.NullDateCalendar.Visible = true;
                this.NotNullDateCalendar.Visible = false;
                this.NullDateCalendar.VisibleDate = this.VisibleDate;
                this.NullDateCalendar.SelectedDate = this.DateValue;
                return;
            }

            this.NotNullDateCalendar.Visible = true;
            this.NullDateCalendar.Visible = false;
            this.NotNullDateCalendar.VisibleDate = this.VisibleDate;
            this.NotNullDateCalendar.SelectedDate = this.SelectedDate.Value;
        }

        #endregion

        #region Event Handlers

        void DateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            this.SelectedDate = (DateTime?)(this.NullDateCalendar.SelectedDate);

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        void DateCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            this.VisibleDate = this.NullDateCalendar.VisibleDate;
        }

        #endregion
    }
}