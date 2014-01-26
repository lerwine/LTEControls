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
        #region Controls

        protected TextBox _notNulldateTextBox, _nulldateTextBox;

        internal TextBox NotNullDateTextBox
        {
            get
            {
                Control c;

                if (this._notNulldateTextBox != null)
                    return this._notNulldateTextBox;

                if ((c = this.FindControl("NotNullDateTextBox")) == null)
                {
                    this._notNulldateTextBox = new TextBox();
                    this._notNulldateTextBox.ID = "NotNullDateTextBox";
                    this._notNulldateTextBox.SkinID = "NotNullDateTimeTextBox";
                    this._notNulldateTextBox.TextChanged += new EventHandler(DateTextBox_TextChanged);
                    this._notNulldateTextBox.AutoPostBack = true;
                }
                else
                    this._notNulldateTextBox = (TextBox)c;

                return this._notNulldateTextBox;
            }
        }

        internal TextBox NullDateTextBox
        {
            get
            {
                Control c;

                if (this._nulldateTextBox != null)
                    return this._nulldateTextBox;

                if ((c = this.FindControl("NullDateTextBox")) == null)
                {
                    this._nulldateTextBox = new TextBox();
                    this._nulldateTextBox.ID = "NullDateTextBox";
                    this._nulldateTextBox.SkinID = "NullDateTimeTextBox";
                    this._nulldateTextBox.TextChanged += new EventHandler(DateTextBox_TextChanged);
                    this._nulldateTextBox.AutoPostBack = true;
                }
                else
                    this._nulldateTextBox = (TextBox)c;

                return this._nulldateTextBox;
            }
        }

        #endregion

        #region Initialize

        private void InitializeDateTextBox()
        {
            this._notNulldateTextBox = null;
            this._nulldateTextBox = null;
        }

        private void AddDateTextBoxControls(ControlCollection controls)
        {
            if (!controls.Contains(this.NotNullDateTextBox))
                controls.Add(this.NotNullDateTextBox);
            if (!controls.Contains(this.NullDateTextBox))
                controls.Add(this.NullDateTextBox);
        }

        #endregion

        #region Render

        private void CreateTextboxControl(ControlCollection controlCollection)
        {
            controlCollection.Add(this.NotNullDateTextBox);
        }

        private void SetTextBoxDynamicStyle()
        {
            if (this.ShowCalendar)
            {
                this.NotNullDateTextBox.Visible = false;
                this.NullDateTextBox.Visible = false;
                return;
            }

            if (this.SelectedDate == null)
            {
                this.NullDateTextBox.Text = this.DateValue.ToString("MM/dd/yyyy HH:mm:ss");
                this.NullDateTextBox.Visible = true;
                this.NotNullDateTextBox.Visible = false;
            }
            else
            {
                this.NotNullDateTextBox.Text = this.DateValue.ToString("MM/dd/yyyy HH:mm:ss");
                this.NotNullDateTextBox.Visible = true;
                this.NullDateTextBox.Visible = false;
            }
        }

        #endregion

        #region Event Handlers

        void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            bool changeSuccessful = false;

            try
            {
                this.SelectedDate = (DateTime?)(Convert.ToDateTime(this.NotNullDateTextBox.Text));
                changeSuccessful = true;
            }
            catch
            {
                changeSuccessful = false;
            }

            if (!changeSuccessful)
                return;

            EventHandler<EventArgs> temp = this.SelectionChanged;
            if (temp != null)
                temp(this, new EventArgs());
        }

        #endregion
    }
}