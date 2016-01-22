using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Events : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Events");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Events");
            }


            DataTable dt;
            string mode = "created";

            try
            {
                mode = Page.RouteData.Values["Mode"].ToString().ToLower();
                switch (mode)
                {
                    case "created":
                    case "accepted":
                    case "requested":
                    case "bookmark":
                        
                        break;
                    default:
                        mode = "created";
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            Classes.Events ev = new Classes.Events();
            dt = ev.eventslist(Convert.ToInt32(Session["UserId"]), mode);

            if (dt.Rows.Count > 0)
            {
                RepeaterEvents.DataSource = dt;
                RepeaterEvents.DataBind();
            }
            else
            {
                LabelNoRecord.Visible = true;
            }
        }
    }
}