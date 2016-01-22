using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class Invite : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Invite");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Invite");
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string email = TextBoxEmail.Text;
            string name = TextBoxName.Text;
            string message = TextBoxMessage.Text;
            int userId = Convert.ToInt32(Session["UserId"]);
            int type = 1;

            Classes.Invite i = new Classes.Invite();
            int status = i.inviteSend(userId, email, name, message, type);

            if (status == 1)
            {
                LabelStatus.Visible = true;
                LabelStatus.Text = "You have successfully sent your invitation.";
            }
            else
            {
                LabelStatus.Visible = true;
                LabelStatus.Text = "Something went wrong!";
            }
        }
    }
}