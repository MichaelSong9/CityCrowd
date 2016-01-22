using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //reading cookies for login
            if (Request.Cookies["VC"] != null)
            {
                string VC = Request.Cookies["VC"].Values["VC"];
                Classes.LoginSession ls = new Classes.LoginSession();
                int UserId = ls.getUserId(VC);
                if (UserId != 0) //user logged before
                {
                    Response.Redirect("~/Explore");
                }
            }

            //check if register is allowed in the system
            Classes.SiteSettings ss = new Classes.SiteSettings();
            bool registerAllowed = Convert.ToBoolean(ss.getSettings("RegisterAllow"));
            if (!registerAllowed) // register not allowed
            {
                LabelError.Text = "Register is temporary disabled! Please try again later!";
                ButtonRegister.Enabled = false;
            }
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            string email = TextBoxEmail.Text;
            string password1 = TextBoxPassword1.Text;
            string password2 = TextBoxPassword2.Text;

            Classes.UserProfileSet ups = new Classes.UserProfileSet();
            Tuple<int, string, int> result = ups.register(email, password1, password2);

            if (result.Item1 == -1)
            {
                LabelError.Visible = true;
                LabelError.Text = result.Item2;
            }
            else if (result.Item1 == 1)
            {
                Classes.UserInfo ui = new Classes.UserInfo();
                Session["UserId"] = ui.getUserIdByEmail(email);

                int Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursShort"].ToString());
                string VerificationCode = Convert.ToString(Guid.NewGuid());

                // set login information
                Classes.LoginSession ls = new Classes.LoginSession();
                ls.setLoginSession(Convert.ToInt32(Session["UserId"]), VerificationCode, Hours);

                // create the cookies
                HttpCookie _userInfoCookies = new HttpCookie("VC");
                _userInfoCookies["VC"] = VerificationCode;
                _userInfoCookies.Expires = DateTime.Now.AddHours(Hours);
                Response.Cookies.Add(_userInfoCookies);

                // redirect the user
                Response.Redirect("~/Completion");
            }
        }
    }
}