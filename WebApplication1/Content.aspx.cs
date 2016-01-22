using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Page.RouteData.Values["Page"].ToString())
            {
                case "About":
                    {
                        LabelTitle.Text = "About";
                        Page.Title = "About";
                        ImageIcon.ImageUrl = "~/images/icons/information48.png";
                        PanelAbout.Visible = true;
                        break;
                    }
                case "Contact":
                    {
                        LabelTitle.Text = "Contact";
                        Page.Title = "Contact";
                        ImageIcon.ImageUrl = "~/images/icons/conversation48.png";
                        PanelContact.Visible = true;
                        break;
                    }
                case "Rules":
                    {
                        LabelTitle.Text = "Terms of Use";
                        Page.Title = "Terms of Use";
                        ImageIcon.ImageUrl = "~/images/icons/rules48.png";
                        PanelRules.Visible = true;
                        break;
                    }
                case "Privacy":
                    {
                        LabelTitle.Text = "Privacy Policy";
                        Page.Title = "Privacy Policy";
                        ImageIcon.ImageUrl = "~/images/icons/panel48.png";
                        PanelPrivacy.Visible = true;
                        break;
                    }
                default:
                    {
                        Response.Redirect("~/Error/404");
                        break;
                    }
            }
        }
    }
}