using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            //RouteTable.Routes.Ignore("{folder}/{*pathInfo}", new { folder = "Images" });
            /////////// User Pages

            routes.MapPageRoute(
               "ExplorePage",
               "Explore",
               "~/Explore.aspx"
            );
            routes.MapPageRoute(
               "LogoutPage",
               "Logout",
               "~/Logout.aspx"
            );
            routes.MapPageRoute(
               "RegisterPage",
               "Register",
               "~/Register.aspx"
            );
            routes.MapPageRoute(
               "NotificationsPage",
               "Notifications",
               "~/Notifications.aspx"
            );
            routes.MapPageRoute(
               "SearchPage",
               "Search",
               "~/Search.aspx"
            );
            routes.MapPageRoute(
               "CalendarPage",
               "Calendar",
               "~/Calendar.aspx"
            );
            routes.MapPageRoute(
               "IntroductionPage",
               "Introduction",
               "~/Introduction.aspx"
            );
            routes.MapPageRoute(
               "CompletionPage",
               "Completion",
               "~/Completion.aspx"
            );
            routes.MapPageRoute(
               "InvitePage",
               "Invite",
               "~/Invite.aspx"
            );
            routes.MapPageRoute(
               "VerifyCodePage",
               "Verify/{Mode}/{Code}",
               "~/Verify.aspx"
            );
            routes.MapPageRoute(
               "ContentPage",
               "Content/{Page}",
               "~/Content.aspx"
            );
            routes.MapPageRoute(
               "HelpPage",
               "Help",
               "~/Help.aspx"
            );
            routes.MapPageRoute(
               "ReviewPage",
               "Review/{ReviewRequestId}",
               "~/Review.aspx"
            );
            routes.MapPageRoute(
               "FeedPage",
               "Feed/{Mode}",
               "~/Feed.aspx"
            );

            /////////// Requests

            routes.MapPageRoute(
               "RequestsPage",
               "Requests",
               "~/Requests.aspx"
            );
            routes.MapPageRoute(
               "RequestsPageActions",
               "Requests/{RequestId}/{ActionCode}",
               "~/Requests.aspx"
            );

            /////////// Settings

            routes.MapPageRoute(
               "SettingsPage",
               "Settings",
               "~/Settings.aspx"
            );
            routes.MapPageRoute(
               "SettingsTabs",
               "Settings/{Section}",
               "~/Settings.aspx"
            );

            /////////// Messages

            routes.MapPageRoute(
               "MessagesPage",
               "Messages",
               "~/Messages.aspx"
            );
            routes.MapPageRoute(
               "MessagesShowPage",
               "Messages/{ProfileId}",
               "~/MessagesShow.aspx"
            );
            routes.MapPageRoute(
               "NewMessagePage",
               "Messages/New",
               "~/MessagesShow.aspx"
            );

            /////////// Events

            routes.MapPageRoute(
               "Events",
               "Events",
               "~/Events.aspx"
            );
            routes.MapPageRoute(
               "EventsMode",
               "Events/Mode/{Mode}",
               "~/Events.aspx"
            ); 
            routes.MapPageRoute(
               "EventsAdd",
               "Events/Add",
               "~/EventsAdd.aspx"
            );
            routes.MapPageRoute(
               "EventsModify",
               "Events/Modify/{EventId}",
               "~/EventsModify.aspx"
            );
            routes.MapPageRoute(
               "EventsView",
               "Events/{EventId}",
               "~/EventsView.aspx"
            );
            
            /////////// Login
            
            routes.MapPageRoute(
               "LoginPage",
               "Login",
               "~/Login.aspx"
            );
            routes.MapPageRoute(
               "LoginPageWithPath",
               "Login/{Page}/{ItemId}",
               "~/Login.aspx"
            );
            routes.MapPageRoute(
               "LoginRedirect",
               "Login/{Page}",
               "~/Login.aspx"
            );

            /////////// Other pages

            routes.MapPageRoute(
               "Error",
               "Error",
               "~/Error.aspx"
            ); 
            routes.MapPageRoute(
               "ErrorCode",
               "Error/{Code}",
               "~/Error.aspx"
            );
            routes.MapPageRoute(
               "ForgotPasswordPage",
               "ForgotPassword/{Mode}",
               "~/ForgotPassword.aspx"
            );
            routes.MapPageRoute(
               "ForgotPasswordRecover",
               "ForgotPassword/{Mode}/{VCode}",
               "~/ForgotPassword.aspx"
            );

            /////////// Profile Pages

            routes.MapPageRoute(
               "Profile",
               "Profile/{Id}",
               "~/Profile.aspx"
            );

            /////////// Blog
            routes.MapPageRoute(
               "Blog",
               "Blog",
               "~/Blog.aspx"
            );
            routes.MapPageRoute(
                "BlogEntries",
                "Blog/{EntryId}/{EntryTitle}",
                "~/BlogEntries.aspx"
            );

            /////////// Admin pages
            routes.MapPageRoute(
               "Admin",
               "Admin",
               "~/Admin.aspx"
            );
            routes.MapPageRoute(
                "AdminPages",
                "Admin/{Page}",
                "~/Admin.aspx"
            );
        }
    }
}