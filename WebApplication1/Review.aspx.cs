using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check to see if the user logged in or is a guest
            int UserId = 0;

            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                UserId = ls.getUserId(VC);
                if (UserId == 0)
                {
                    Response.Redirect("~/Login");
                }
            }

            Classes.Reviews r = new Classes.Reviews();
            DataTable dt = r.reviewRequestInfo(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Page.RouteData.Values["ReviewRequestId"]));

            if (dt.Rows.Count == 0) //doesn't exist
            {
                Response.Redirect("~/Error/NotFound");
            }
            else //review request exists
            {
                LabelEventName.Text = dt.Rows[0]["EventName"].ToString();
                HiddenFieldTypeId.Value = dt.Rows[0]["TypeId"].ToString();
                HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();
                HyperLinkUser.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                HyperLinkUser.NavigateUrl = "~/Profile/" + dt.Rows[0]["UserId"].ToString();

                // Show profile's photo
                if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString()))
                {
                    HiddenFieldUserPhotoUrl.Value = "Files/ProfilesPhotos/" + Page.RouteData.Values["Id"] + "-220.jpg";
                }
                else
                {
                    HiddenFieldUserPhotoUrl.Value = "Images/nophoto.png";
                }
            }
        }

        protected void ButtonRefuse_Click(object sender, EventArgs e)
        {
            Classes.Reviews r = new Classes.Reviews();
            r.reviewRefuse(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Page.RouteData.Values["ReviewRequestId"]));

            Response.Redirect("~/Explore");
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            Classes.Reviews r = new Classes.Reviews();
            int status = r.reviewAdd(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Page.RouteData.Values["ReviewRequestId"]), TextBoxComment.Text, Convert.ToInt16(HiddenFieldRate.Value));

            if (status == -1)
            {
                //not found
                Response.Redirect("~/Error/NotFound");
            }
            else
            {
                Response.Redirect("~/Explore");
            }
        }
    }
}