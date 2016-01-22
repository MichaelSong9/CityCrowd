using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class EventsAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            int UserId = 0;

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0) //if user not logged in redirect to login
                {
                    Response.Redirect("~/Login/Events/Add");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Events/Add");
            }

            if (!IsPostBack)
            {
                Classes.Locations l = new Classes.Locations();
                DataTable dtCountries = l.countriesList();

                List<System.Web.UI.WebControls.ListItem> countries = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
                for (int i = 0; i < dtCountries.Rows.Count; i++)
                {
                    DropDownListCountry.Items.Add(new ListItem(dtCountries.Rows[i]["CountryName"].ToString(), dtCountries.Rows[i]["CountryId"].ToString()));
                }

                Classes.UserInfo ui = new Classes.UserInfo();
                int locationId = ui.locationIdByUserId(Convert.ToInt32(Session["UserId"]));

                if (locationId == 0)
                {
                    DropDownListCountry.SelectedValue = "0";
                }
                else
                {
                    int cityId = locationId;

                    DataTable dtLocation = l.locationInfoOnlyId(locationId);

                    locationCity(Convert.ToInt32(dtLocation.Rows[0]["StateId"].ToString()));
                    locationState(Convert.ToInt32(dtLocation.Rows[0]["CountryId"].ToString()));
                    DropDownListCountry.SelectedValue = dtLocation.Rows[0]["CountryId"].ToString();
                    DropDownListState.SelectedValue = dtLocation.Rows[0]["StateId"].ToString();
                    DropDownListCity.SelectedValue = locationId.ToString();
                }
            }

        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            bool check = true;
            LiteralMessage.Text = "";

            if (TextBoxName.Text.Length == 0) // check name
            {
                check = false;
                LiteralMessage.Text = "Enter name<br/>";
            }
            if(TextBoxDate.Text.Length == 0) // check date
            {
                check = false;
                LiteralMessage.Text += "Enter date<br/>";
            }
            if (TextBoxParticipants.Text.Length == 0) // check participants +++only number
            {
                check = false;
                LiteralMessage.Text += "Enter participants number<br/>";
            }
            if (TextBoxLanguages.Text.Length == 0) // check language
            {
                check = false;
                LiteralMessage.Text += "Enter language<br/>";
            }
            // check duration
            double num;
            string candidate = TextBoxDuration.Text;
            if (!double.TryParse(candidate, out num) && TextBoxDuration.Text.Length != 0)
            {
                check = false;
                LiteralMessage.Text += "Duration is not valid, enter number<br/>";
            }
            // check pricefrom
            candidate = TextBoxPriceFrom.Text;
            if (!double.TryParse(candidate, out num) && TextBoxPriceFrom.Text.Length != 0)
            {
                check = false;
                LiteralMessage.Text += "Price from is not valid, enter number<br/>";
            }
            // check priceto
            candidate = TextBoxPriceTo.Text;
            if (!double.TryParse(candidate, out num) && TextBoxPriceTo.Text.Length != 0)
            {
                check = false;
                LiteralMessage.Text += "Price from is not valid, enter number<br/>";
            }
            // participants
            candidate = TextBoxParticipants.Text;
            if (!double.TryParse(candidate, out num) && TextBoxParticipants.Text.Length != 0)
            {
                check = false;
                LiteralMessage.Text += "Participants is not valid, enter number<br/>";
            }

            if(!check) // there is something wrong with the values that user entered
            {
                LiteralMessage.Visible = true;
            }
            else
            { // user entered valid values
                LiteralMessage.Visible = false;

                DateTime date = Convert.ToDateTime(HiddenFieldDate.Value);

                Classes.Events ev = new Classes.Events();
                int status = ev.eventAdd(Convert.ToInt32(Session["UserId"]),
                    TextBoxName.Text,
                    HiddenFieldDate.Value,
                    Convert.ToInt32(HiddenFieldParticipants.Value),
                    Convert.ToInt32(DropDownListCity.SelectedValue),
                    Convert.ToInt16(DropDownListPrivacy.SelectedValue),
                    Convert.ToInt16(HiddenFieldTypeId.Value),
                    Convert.ToInt16(HiddenFieldCoverId.Value),
                    HiddenFieldLanguages.Value,
                    TextBoxDescriptions.Text,
                    false,
                    false);

                if (status == 1)
                {
                     //show success message
                    //PanelInfo.Visible = false;
                    LiteralMessage.Text = "You have successfully added an event.";
                    LiteralMessage.Visible = true;
                }
                else if (status == -1)
                {
                    // +show error message
                }
            }
        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationState(Convert.ToInt32(DropDownListCountry.SelectedValue));
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationCity(Convert.ToInt32(DropDownListState.SelectedValue));
        }

        void locationCity(int stateId)
        {
            DataTable dt;
            Classes.Locations l = new Classes.Locations();
            dt = l.citiesList(stateId);

            List<System.Web.UI.WebControls.ListItem> states = new List<System.Web.UI.WebControls.ListItem>();
            DropDownListCity.Items.Clear();
            DropDownListCity.Items.Add(new ListItem("Select City", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListCity.Items.Add(new ListItem(dt.Rows[i]["CityName"].ToString(), dt.Rows[i]["CityId"].ToString()));
            }
        }

        void locationState(int countryId)
        {
            DataTable dt;
            Classes.Locations l = new Classes.Locations();
            dt = l.statesList(countryId);

            List<System.Web.UI.WebControls.ListItem> states = new List<System.Web.UI.WebControls.ListItem>();
            DropDownListState.Items.Clear();
            DropDownListState.Items.Add(new ListItem("Select State", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListState.Items.Add(new ListItem(dt.Rows[i]["StateName"].ToString(), dt.Rows[i]["StateId"].ToString()));
            }
        }
    }
}