using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Explore : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Explore");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Explore");
            }

            if (!IsPostBack)
            {
                Classes.Explore ex = new Classes.Explore();
                Tuple<int, DataTable> result = ex.startRecommending(UserId);

                int status = result.Item1;
                DataTable dt = result.Item2;

                if (status == 1 && dt.Rows.Count != 0)
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "EventId: " + dt.Rows[0]["EventId"].ToString() + " EventName:" + dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "Unfortunatly there is no event to show now, try later!";
                }
            }
        }
    }
}