using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            switch (HiddenFieldSearchType.Value)
            {
                case "1":
                    {
                        Classes.Search s = new Classes.Search();
                        DataTable dt = s.searchUsername(TextBoxUsername.Text);

                        if (dt.Rows.Count == 0)
                        {
                            PanelUsername.Visible = false;
                            PanelResult.Visible = false;
                            LabelMessage.Visible = true;
                            LabelMessage.Text = "No record found!";
                        }
                        else
                        {
                            RepeaterUsername.DataSource = dt;
                            RepeaterUsername.DataBind();

                            LabelMessage.Visible = false; 
                            PanelUsername.Visible = true;
                            PanelResult.Visible = false;
                        }
                        break;
                    }
                case "2":
                    {
                        Classes.Search s = new Classes.Search();
                        DataTable dt = s.searchHashtag(TextBoxTag.Text, Convert.ToInt32(HiddenFieldLocationId.Value));

                        if (dt.Rows.Count == 0)
                        {
                            PanelResult.Visible = false;
                            LabelMessage.Visible = true;
                            LabelMessage.Text = "No record found!";
                        }
                        else
                        {
                            RepeaterResult.DataSource = dt;
                            RepeaterResult.DataBind();

                            LabelMessage.Visible = false;
                            PanelUsername.Visible = false;
                            PanelResult.Visible = true;
                        }
                        break;
                    }
                case "3":
                    {
                        Classes.Search s = new Classes.Search();
                        DataTable dt = s.searchType(Convert.ToInt32(HiddenFieldTypeId.Value), Convert.ToInt32(HiddenFieldLocationId.Value));

                        if (dt.Rows.Count == 0)
                        {
                            PanelResult.Visible = false;
                            LabelMessage.Visible = true;
                            LabelMessage.Text = "No record found!";
                        }
                        else
                        {
                            RepeaterResult.DataSource = dt;
                            RepeaterResult.DataBind();

                            LabelMessage.Visible = false;
                            PanelUsername.Visible = false;
                            PanelResult.Visible = true;
                        }
                        break;
                    }
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