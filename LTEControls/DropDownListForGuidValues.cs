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
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ValidationProperty("SelectedItem")]
    [SupportsEventValidation]
    [ToolboxData("<{0}:DropDownListForGuidValues runat=\"server\"></{0}:DropDownListForGuidValues>")]
    public class DropDownListForGuidValues : DropDownList
    {
        [Browsable(true)]
        [Bindable(false)]
        [DefaultValue("*")]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Themeable(false)]
        [DisplayName("Null GUID Value")]
        [Description("Value of item in list which will represent a null GUID value")]
        public string NullGuidValue
        {
            get
            {
                if (this.ViewState["NullGuidValue"] == null)
                    return "*";

                return ((string)(this.ViewState["NullGuidValue"]));
            }
            set
            {
                this.ViewState["NullGuidValue"] = value;
            }
        }

        [Browsable(true)]
        [Bindable(true, BindingDirection.TwoWay)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Themeable(false)]
        [ReadOnly(true)]
        [DisplayName("Selected GUID")]
        [Description("If the SelectedValue property matches NullGuidValue property, then null will be returned. Otherwise, this will convert SelectedValue to a GUID value.")]
        public object SelectedGuid
        {
            get
            {
                Guid? g;

                if (this.SelectedIndex < 0 || this.SelectedValue == this.NullGuidValue || this.SelectedValue.Length < 32)
                    return null;

                g = null;

                try
                {
                    g = (Guid?)(new Guid(this.SelectedValue));
                }
                catch
                {
                    g = null;
                }

                return g;
            }
            set
            {
                if (value == null || !(value is Guid))
                    this.SelectedValue = this.NullGuidValue;
                else
                    this.SelectedValue = ((Guid)value).ToString();
            }
        }
    }
}
