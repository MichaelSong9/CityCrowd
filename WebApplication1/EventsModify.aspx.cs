using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class EventsModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check to see if the user logged in or is a guest
            int UserId = 0;

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0)
                {
                    Response.Redirect("~/Login");
                }
            }

            if (!IsPostBack)
            {
                // get info
                Classes.Events ev = new Classes.Events();
                DataTable dt = ev.eventModifyInfo(Convert.ToInt64(Page.RouteData.Values["EventId"].ToString()));

                if (dt.Rows.Count == 0)// event doesn't exist
                {
                    Response.Redirect("~/Error/EventNotFound");
                }
                else
                {
                    //check if user is the owner
                    if (UserId.ToString() != dt.Rows[0]["OwnerId"].ToString())
                    {
                        Response.Redirect("~/Error/UserNotEventOwner");
                    }

                    //count available spots
                    int participantsAvailable = Convert.ToInt32(dt.Rows[0]["Participants"].ToString()) - Convert.ToInt32(dt.Rows[0]["ParticipantsAccepted"].ToString());
                    //get time
                    string timeHour = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).Hour.ToString();
                    string timeMinute = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).Minute.ToString();
                    DropDownListTimeHour.SelectedValue = timeHour;
                    DropDownListTimeMinute.SelectedValue = timeMinute;

                    TextBoxName.Text = dt.Rows[0]["Name"].ToString();
                    DropDownListType.SelectedValue = dt.Rows[0]["TypeId"].ToString();
                    TextBoxDescriptions.Text = dt.Rows[0]["Descriptions"].ToString();
                    DropDownListPrivacy.SelectedValue = dt.Rows[0]["Privacy"].ToString(); //show if price selected
                    HiddenFieldParticipants.Value = dt.Rows[0]["Participants"].ToString();
                    HiddenFieldParticipantsAccepted.Value = dt.Rows[0]["ParticipantsAccepted"].ToString();
                    HiddenFieldDate.Value = dt.Rows[0]["Date"].ToString();
                    HiddenFieldLanguages.Value = dt.Rows[0]["Languages"].ToString();

                    /////////////////// event photo
                    Classes.Locations l = new Classes.Locations();
                    DataTable dtLocation = l.locationInfoOnlyId(Convert.ToInt32(dt.Rows[0]["LocationId"].ToString()));
                    int countryId = Convert.ToInt32(dtLocation.Rows[0]["CountryId"].ToString());
                    int stateId = Convert.ToInt32(dtLocation.Rows[0]["StateId"].ToString());
                    int cityId = Convert.ToInt32(dt.Rows[0]["LocationId"].ToString());

                    locationCountry();
                    DropDownListCountry.SelectedValue = dtLocation.Rows[0]["CountryId"].ToString();
                    locationState(countryId);
                    DropDownListState.SelectedValue = dtLocation.Rows[0]["StateId"].ToString();
                    locationCity(stateId);
                    DropDownListCity.SelectedValue = dt.Rows[0]["LocationId"].ToString();
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            Classes.Events ev = new Classes.Events();
            Tuple<int, string> result = ev.eventModify(
                Convert.ToInt64(Page.RouteData.Values["EventId"].ToString()),
                TextBoxName.Text,
                TextBoxDate.Text,
                TextBoxParticipants.Text,
                Convert.ToInt32(DropDownListCity.SelectedValue),
                Convert.ToInt16(DropDownListPrivacy.SelectedValue),
                Convert.ToInt16(DropDownListType.SelectedValue),
                1,
                HiddenFieldLanguages.Value,
                TextBoxDescriptions.Text,
                false
                );

            int status = result.Item1;
            string message = result.Item2;

            if (status == -1)
            {
                LiteralMessage.Visible = true;
                LiteralMessage.Text = message;
            }
            else
            {
                // show success message
                PanelInfo.Visible = false;
                LiteralMessage.Text = "You have successfully modified this event.";
                LiteralMessage.Visible = true;
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            Classes.Events ev = new Classes.Events();
            int status = ev.eventDelete(Convert.ToInt32(Page.RouteData.Values["EventId"].ToString()));

            if (status == 1)
            {
                // show success message
                LiteralMessage.Text = "You have siccessfully deleted this event.";
                LiteralMessage.Visible = true;
                PanelInfo.Visible = false;
            }
        }

        protected void LinkButtonAdvance_Click(object sender, EventArgs e)
        {
            if (HiddenFieldAdvance.Value == "0")
            {
                PanelAdvance.Visible = true;
                HiddenFieldAdvance.Value = "1";
                LinkButtonAdvance.Text = "Hide Advance";
            }
            else
            {
                PanelAdvance.Visible = false;
                HiddenFieldAdvance.Value = "0";
                LinkButtonAdvance.Text = "Show Advance";
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
            dt = l.citiesList(Convert.ToInt32(DropDownListState.SelectedValue));

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
            dt = l.statesList(Convert.ToInt32(DropDownListCountry.SelectedValue));

            List<System.Web.UI.WebControls.ListItem> states = new List<System.Web.UI.WebControls.ListItem>();
            DropDownListState.Items.Clear();
            DropDownListState.Items.Add(new ListItem("Select State", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListState.Items.Add(new ListItem(dt.Rows[i]["StateName"].ToString(), dt.Rows[i]["StateId"].ToString()));
            }
        }

        void locationCountry()
        {
            DataTable dt;
            Classes.Locations l = new Classes.Locations();
            dt = l.countriesList();

            List<System.Web.UI.WebControls.ListItem> states = new List<System.Web.UI.WebControls.ListItem>();
            DropDownListState.Items.Clear();
            DropDownListState.Items.Add(new ListItem("Select Country", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownListState.Items.Add(new ListItem(dt.Rows[i]["CountryName"].ToString(), dt.Rows[i]["CountryId"].ToString()));
            }
        }
    }
}