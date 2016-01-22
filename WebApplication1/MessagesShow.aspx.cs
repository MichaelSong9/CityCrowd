using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class MessagesShow : System.Web.UI.Page
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

            int otherId = Convert.ToInt32(Page.RouteData.Values["ProfileId"].ToString());

            Classes.Messages m = new Classes.Messages();
            Tuple<int, DataTable, DataTable, DataTable> result = m.showMessages(UserId, otherId);

            int status = result.Item1;
            DataTable dtUserName = result.Item2;
            DataTable dtOtherName = result.Item3;
            DataTable dtMessages = result.Item4;

            if (status == -1)
            {
                Response.Redirect("~/Messages");
            }

            HiddenFieldOwnerName.Value = dtUserName.Rows[0]["FirstName"].ToString() + " " + dtUserName.Rows[0]["LastName"].ToString();
            HiddenFieldOtherName.Value = dtOtherName.Rows[0]["FirstName"].ToString() + " " + dtOtherName.Rows[0]["LastName"].ToString();
            Page.Title = "Messages : " + dtOtherName.Rows[0]["FirstName"].ToString() + " " + dtOtherName.Rows[0]["LastName"].ToString();
            
            RepeaterMessages.DataSource = dtMessages;
            RepeaterMessages.DataBind();
        }

        protected void ImageButtonSend_Click(object sender, ImageClickEventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            int receiverId = Convert.ToInt32(Page.RouteData.Values["ProfileId"].ToString());
            string message = TextBoxMessage.Text;
            Classes.Messages m = new Classes.Messages();
            int status = m.addMessage(userId, receiverId, message);

            if (status == 0)
            {
                //unsuccessful
            }
            else if (status == 1)
            {
                //successfull
                Response.Redirect("~/Messages/" + Page.RouteData.Values["ProfileId"].ToString());
            }
        }
    }
}