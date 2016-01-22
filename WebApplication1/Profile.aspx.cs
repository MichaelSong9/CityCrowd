using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Profile : System.Web.UI.Page
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
                }
            }

            Classes.UserInfo ui = new Classes.UserInfo();
            Tuple<int, DataTable> result = ui.profileInfo(UserId, Page.RouteData.Values["Id"].ToString());

            int status = result.Item1;
            DataTable dt = result.Item2;
            

            if (status == -1)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfile");
            }
            else
            {
                // Profile is redistrected
                if (status != 1)
                {
                    Response.Redirect("~/Error/RedistrictedProfile");
                }
                else
                {
                    
                    HiddenFieldProfilePhoto.Value = dt.Rows[0]["ProfilePicUrl"].ToString();

                    // Show action buttons
                    if (Convert.ToBoolean(dt.Rows[0]["isOwner"].ToString())) //user is the profile owner
                    {
                        HyperLinkEdit.Visible = true;
                        ButtonFollow.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["isFollower"].ToString()))
                        {
                            ButtonFollow.Text = "Unfollow";
                        }
                        else
                        {
                            ButtonFollow.Text = "Follow";
                        }
                    }
                }
 
                // Profile info
                LabelName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                LabelUsername.Text = dt.Rows[0]["Username"].ToString();
                LabelFollowers.Text = dt.Rows[0]["FollowersCount"].ToString();
                LabelFollowing.Text = dt.Rows[0]["FollowingCount"].ToString();
                LabelAbout.Text = dt.Rows[0]["About"].ToString();
                HiddenFieldUserId.Value = dt.Rows[0]["UserId"].ToString();
                HiddenFieldProfileVerified.Value = dt.Rows[0]["ProfileVerified"].ToString();
                LabelRate.Text = dt.Rows[0]["RatePercent"].ToString();
                LabelRatePercent.Text = dt.Rows[0]["RatePercent"].ToString();
                LabelRateCount.Text = dt.Rows[0]["RateCount"].ToString();
                LabelCountry.Text = dt.Rows[0]["CountryName"].ToString();
                LabelCity.Text = dt.Rows[0]["CityName"].ToString();
                HiddenFieldFlagId.Value = dt.Rows[0]["CountryId"].ToString();
            }

            getEvents();
            getReviews();
            getFollowers();
            getFollowing();
        }

        protected void getEvents()
        {
            Classes.Events ev = new Classes.Events();
            DataTable dt = ev.eventslist(Convert.ToInt32(Session["UserId"]), "created");

            if (dt.Rows.Count > 0)
            {
                RepeaterEvents.DataSource = dt;
                RepeaterEvents.DataBind();
            }
            else
            {
                LabelEventsNoRecord.Visible = true;
                LabelEventsNoRecord.Text = "There is no event!";
            }

        }

        protected void getReviews()
        {
            Classes.UserInfo ui = new Classes.UserInfo();
            Tuple<int[], DataTable> result = ui.reviews(Convert.ToInt32(Page.RouteData.Values["Id"].ToString()));

            int[] rateCount = result.Item1;
            DataTable dt = result.Item2;

            if (dt.Rows.Count != 0)
            {
                LabelRateOne.Text = rateCount[0].ToString();
                LabelRateTwo.Text = rateCount[1].ToString();
                LabelRateThree.Text = rateCount[2].ToString();
                LabelRateFour.Text = rateCount[3].ToString();
                LabelRateFive.Text = rateCount[4].ToString();

                RepeaterReviews.DataSource = dt;
                RepeaterReviews.DataBind();
            }
            else
            {
                LabelReviewsNoRecord.Visible = true;
                LabelReviewsNoRecord.Text = "There is no review!";
            }
        }

        protected void getFollowers()
        {
            Classes.UserInfo ui = new Classes.UserInfo();
            DataTable dt = ui.followersList(Convert.ToInt32(Session["UserId"]));

            if (dt.Rows.Count != 0)
            {
                RepeaterFollowers.DataSource = dt;
                RepeaterFollowers.DataBind();
            }
            else
            {
                LabelFollowersNoRecord.Visible = true;
                LabelFollowersNoRecord.Text = "There is no follower!";
            }
        }

        protected void getFollowing()
        {
            Classes.UserInfo ui = new Classes.UserInfo();
            DataTable dt = ui.followingList(Convert.ToInt32(Session["UserId"]));

            if (dt.Rows.Count != 0)
            {
                RepeaterFollowing.DataSource = dt;
                RepeaterFollowing.DataBind();
            }
            else
            {
                LabelFollowingNoRecord.Visible = true;
                LabelFollowingNoRecord.Text = "There is no following!";
            }
        }

        protected void ButtonFollow_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            int profileId = Convert.ToInt32(HiddenFieldUserId.Value);

            if (userId == 0)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "You need to login first!";
            }
            else
            {
                // is visitor follower
                Classes.UserActions ua = new Classes.UserActions();
                int followStatus = ua.followAction(userId, profileId);


                    if (followStatus == 1)
                    {
                        LabelMessage.Visible = true;
                        LabelMessage.Text = "You have successfully followed this user.";
                        ButtonFollow.Text = "Unfollow";
                    }
                    else if (followStatus == 2)
                    {

                        LabelMessage.Visible = true;
                        LabelMessage.Text = "You have successfully unfollowed this user.";
                        ButtonFollow.Text = "Follow";
                    }
                    else if (followStatus == 0)
                    {

                        LabelMessage.Visible = true;
                        LabelMessage.Text = "Something strange happened! Please try again later!";
                    }
            }
        }
    }
}