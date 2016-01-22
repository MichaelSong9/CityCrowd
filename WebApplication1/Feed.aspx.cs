using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Feed : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Feed");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Feed");
            }

            try
            {
                string mode = Page.RouteData.Values["Mode"].ToString().ToLower();

                switch (mode)
                {
                    case "city":
                        Page.Title = "Feed - City";
                        feedCity();
                        break;
                    case "map":
                        Page.Title = "Feed - Map";
                        feedMap();
                        break;
                    case "following":
                        Page.Title = "Feed - Following";
                        feedFollowing();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void feedCity()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            Classes.UserInfo ui = new Classes.UserInfo();
            int locationId = ui.locationIdByUserId(userId);

            Classes.Feed f = new Classes.Feed();
            DataTable dt = f.eventsCity(locationId);

            if (dt.Rows.Count > 0)
            {
                RepeaterCity.DataSource = dt;
                RepeaterCity.DataBind();
            }
            else
            {
                //LabelNoRecord.Visible = true;
            }
        }

        protected void feedMap()
        {

        }

        protected void feedFollowing()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            Classes.Feed f = new Classes.Feed();
            DataTable dt = f.eventsFollowing(userId);

            if (dt.Rows.Count > 0)
            {
                RepeaterCity.DataSource = dt;
                RepeaterCity.DataBind();
            }
            else
            {
                //LabelNoRecord.Visible = true;
            }
        }
    }
}