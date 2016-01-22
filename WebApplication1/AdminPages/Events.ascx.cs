﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.AdminPages
{
    public partial class Events : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check permissions
            Classes.AdminPermissions ap = new Classes.AdminPermissions();
            if (!ap.getAdminPremissions(Convert.ToInt32(Session["UserId"]), "Events"))
            {
                Response.Redirect("~/Error/404");
            }
        }
    }
}