using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorType = "unknown";

            try
            {
                if (Page.RouteData.Values["Code"].ToString() != "") // redirect user to a page with item id
                {
                    errorType = Page.RouteData.Values["Code"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            switch (errorType)
            {
                case "unknown":
                    {
                        Image1.ImageUrl = "";
                        LabelMessage.Text = "Oops!<br/>Unfortunately,<br/>an error occured!";
                        HyperLink1.Visible = true;
                        break;
                    }
                case "404":
                    {
                        Image1.ImageUrl = "";
                        LabelMessage.Text = "Page not found!";
                        HyperLink1.Visible = false;
                        break;
                    }
            }
        }
    }
}