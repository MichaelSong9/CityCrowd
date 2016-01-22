using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Requests : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Requests");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Requests");
            }

            // Actions
            Int64 requestId = 0;
            int actionCode = 0;
            try
            {
                requestId = Convert.ToInt64(Page.RouteData.Values["RequestId"].ToString());
                actionCode = Convert.ToInt32(Page.RouteData.Values["ActionCode"].ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            if (requestId != 0)
                {
                    // accept
                    
                
                    if (actionCode == 1)
                    {
                        Classes.Requests r = new Classes.Requests();
                        int status = r.requestAccept(Convert.ToInt32(Session["UserId"]), requestId);
                        
                        if (status == 1)
                        {
                            //successful
                        }
                        else if (status == -1)
                        {
                            //unsuccessful
                        }
                    }
                    // reject
                    else if (actionCode == 2)
                    {
                        Classes.Requests r = new Classes.Requests();
                        r.requestReject(requestId);
                    }

                    Response.Redirect("~/Requests");
                }

            //all read
            Classes.Requests re = new Classes.Requests();
            re.allRead(UserId);

            DataTable dt = re.requestsList(UserId);

            if (dt.Rows.Count == 0)
            {
                LabelNoRecord.Visible = true;
            }
            else
            {
                LabelNoRecord.Visible = false;
                RepeaterRequests.DataSource = dt;
                RepeaterRequests.DataBind();
            }
        }
    }
}