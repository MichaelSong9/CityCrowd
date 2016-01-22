using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Messages : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Messages");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Messages");
            }

            //all read
            Classes.Messages m = new Classes.Messages();
            m.allRead(UserId);

            //get message lists
            DataTable dt = m.messageLists(UserId);
            if (dt.Rows.Count == 0)
            {
                LabelNoRecord.Visible = true;
            }
            else
            {
                RepeaterMessages.DataSource = dt;
                RepeaterMessages.DataBind();
            }
        }
    }
}