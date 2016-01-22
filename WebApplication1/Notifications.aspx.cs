using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Notifications : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Notifications");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Notifications");
            }

            //all read
            Classes.Notifications n = new Classes.Notifications();
            n.allRead(UserId);

            //get botifications
            DataTable dt = n.notifications(UserId);
            
            if (dt.Rows.Count == 0)
            {
                //LabelNoRecord.Visible = true;
            }
            else
            {
                RepeaterNotifications.DataSource = dt;
                RepeaterNotifications.DataBind();
            }
        }

        //protected void GridViewNotifications_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "Click":
        //            {
        //                Response.Redirect(e.CommandArgument.ToString());
        //                break;
        //            }
        //        default:
        //            {
        //                break;
        //            }
        //    }
        //}
    }
}