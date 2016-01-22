using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace WebApplication1
{
    public partial class MasterPageUser : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check to see if the user logged in or is a guest
            bool userLogin = false;
            int UserId = 0;

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0)
                {

                }
                else
                {
                    userLogin = true;
                    Session["UserId"] = UserId.ToString();
                    HyperLinkProfile.NavigateUrl = "~/Profile/" + UserId.ToString();
                }
            }

            DataTable dt;
            Classes.UserInfo ui = new Classes.UserInfo();
            dt = ui.masterPageInfo(UserId);

            if (dt.Rows.Count == 0)// Profile doesn't exist OR user didn't logged in
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                LabelFullName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                HyperLinkProfile.NavigateUrl = "~/Profile/" + dt.Rows[0]["Username"].ToString();
                HiddenFieldMessages.Value = dt.Rows[0]["MessagesCount"].ToString();
                HiddenFieldRequests.Value = dt.Rows[0]["RequestsCount"].ToString();
                HiddenFieldNotifications.Value = dt.Rows[0]["NotificationsCount"].ToString();
                HiddenFieldUsername.Value = dt.Rows[0]["Username"].ToString();

                string photoUrl = "Images/NoPhoto.png";
                if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                {
                    photoUrl = "Files/" + ConfigurationManager.AppSettings["folderName"].ToString() + "/ProfilesPhotos/" + UserId.ToString() + ".jpg";
                }
                HiddenFieldPhotoUrl.Value = photoUrl;
            }
        }
    }
}