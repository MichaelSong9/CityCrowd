using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Completion : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Completion");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Completion");
            }

            //check if user entered these information before
            Classes.UserInfo ui = new Classes.UserInfo();

            if (ui.getUserStatus(UserId) != 0) //if user not logged in redirect to login
            {
                //Response.Redirect("~/Explore");
            }

            if (!IsPostBack)
            {
                DataTable dtCountries;
                Classes.Locations l = new Classes.Locations();
                dtCountries = l.countriesList();

                List<System.Web.UI.WebControls.ListItem> countries = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
                for (int i = 0; i < dtCountries.Rows.Count; i++)
                {
                    DropDownListCountry.Items.Add(new ListItem(dtCountries.Rows[i]["CountryName"].ToString(), dtCountries.Rows[i]["CountryId"].ToString()));
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            //validate

            //save
            int status;
            Classes.UserProfileSet ups = new Classes.UserProfileSet();
            status = ups.completion(
                Convert.ToInt32(Session["UserId"]),
                TextBoxUsername.Text,
                TextBoxFirstName.Text,
                TextBoxLastName.Text,
                Convert.ToInt16(DropDownListGender.SelectedValue),
                Convert.ToInt32(DropDownListCity.SelectedValue),
                HiddenFieldDOB.Value);

            if (status == 1)
            {
                Response.Redirect("~/Introduction");
            }
    }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
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

    }
}