using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                //check if login is allowed in the system
                Classes.SiteSettings ss = new Classes.SiteSettings();
                bool loginAllowed = Convert.ToBoolean(ss.getSettings("LoginAllow"));
                if (!loginAllowed) // not allowed
                {
                    LabelError.Text = "Login is not allowed! Please try again later!";
                    ButtonLogin.Enabled = false;
                }
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (TextBoxUsername.Text.Length == 0 || TextBoxPassword.Text.Length == 0) //blank information
            {
                LabelError.Visible = true;
                LabelError.Text = "Please enter the username and password!";
            }
            else
            {
                string username = TextBoxUsername.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(username);
                int mode = 0;
                bool isValid = false;

                if (match.Success) //user entered email
                {
                    mode = 1;
                    isValid = true;
                }
                else //user entered phone number
                {
                    Regex strPattern = new Regex("[0-9]*[_]*");

                    if (!strPattern.IsMatch(username))
                    {
                        mode = 2;
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    Classes.LoginSession ls = new Classes.LoginSession();
                    int userId = ls.login(mode, username, TextBoxPassword.Text);

                    if (userId == 0) // user information was not valid
                    {
                        LabelError.Visible = true;
                        LabelError.Text = "You username and/or password is not valid!";
                    }
                    else if (userId == -1)
                    {
                        LabelError.Visible = true;
                        LabelError.Text = "Access to user's panel is not allowed!";
                    }
                    else // user information was valid
                    {
                        Session["UserId"] = userId.ToString();

                        int Hours;
                        string VerificationCode = Convert.ToString(Guid.NewGuid());

                        if (CheckBoxRemember.Checked) // user want the system to remember him/her
                        {
                            Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursLong"].ToString());
                        }
                        else
                        {
                            Hours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginHoursShort"].ToString());
                        }

                        // set login information
                        ls.setLoginSession(Convert.ToInt32(Session["UserId"]), VerificationCode, Hours);

                        // create the cookies
                        HttpCookie _userInfoCookies = new HttpCookie("VC");
                        _userInfoCookies["VC"] = VerificationCode;
                        _userInfoCookies.Expires = DateTime.Now.AddHours(Hours);
                        Response.Cookies.Add(_userInfoCookies);

                        // redirect user
                        try
                        {
                            if (Page.RouteData.Values["ItemId"].ToString() != "") // redirect user to a page with item id
                            {
                                Response.Redirect("~/" + Page.RouteData.Values["Page"].ToString() + "/" + Page.RouteData.Values["ItemId"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {

                        }
                        try
                        {
                            if (Page.RouteData.Values["Page"].ToString() != "") // redirect user to a page
                            {
                                Response.Redirect("~/" + Page.RouteData.Values["Page"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {

                        }
                        // redirect the user to his/her panel
                        Response.Redirect("~/Explore");
                    }
                }
                else
                {
                    LabelError.Visible = true;
                    LabelError.Text = "Wrong information!";
                }
            }
        }

    }
}