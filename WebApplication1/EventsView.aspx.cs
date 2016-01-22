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
    public partial class EventsView : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                
                // get info
                Classes.Events ev = new Classes.Events();
                DataTable dt = ev.eventInfo(Convert.ToInt32(Page.RouteData.Values["EventId"].ToString()), UserId);

                if (dt.Rows.Count == 0)// event doesn't exist
                {
                    Response.Redirect("~/Error/EventNotFound");
                }
                else
                {
                    //count available spots
                    int participantsAvailable = Convert.ToInt32(dt.Rows[0]["Participants"].ToString()) - Convert.ToInt32(dt.Rows[0]["ParticipantsAccepted"].ToString());

                    HiddenFieldOwnerId.Value = dt.Rows[0]["OwnerId"].ToString();
                    LabelName.Text = dt.Rows[0]["Name"].ToString();
                    Page.Title = dt.Rows[0]["Name"].ToString();
                    HiddenFieldDate.Value = dt.Rows[0]["Date"].ToString();
                    HiddenFieldDuration.Value = dt.Rows[0]["Duration"].ToString();
                    LabelParticipants.Text = dt.Rows[0]["Participants"].ToString();
                    LabelParticipantsAvailable.Text = participantsAvailable.ToString();
                    LabelLanguages.Text = dt.Rows[0]["Languages"].ToString(); //enhance
                    LabelDescriptions.Text = dt.Rows[0]["Descriptions"].ToString();
                    HiddenFieldUsername.Value = dt.Rows[0]["Username"].ToString();
                    HiddenFieldTypeId.Value = dt.Rows[0]["TypeId"].ToString();
                    HiddenFieldCoverId.Value = dt.Rows[0]["CoverId"].ToString();
                    int OwnerId = Convert.ToInt32(dt.Rows[0]["OwnerId"].ToString());

                    Classes.Locations l = new Classes.Locations();
                    DataTable dtLocation = l.getLocationInfoByCityId(Convert.ToInt32(dt.Rows[0]["LocationId"].ToString()));
                    if (dtLocation.Rows.Count == 0)
                    {
                        LabelLocation.Text = "Not Available!";
                    }
                    else
                    {
                        LabelLocation.Text = dtLocation.Rows[0]["CityName"].ToString() + " - " + dtLocation.Rows[0]["StateName"].ToString() + " - " + dtLocation.Rows[0]["CountryName"].ToString();
                    }

                    //Event photo url
                    //if (Convert.ToBoolean(dt.Rows[0]["EventHasPhoto"].ToString()))
                    //{
                    //    HiddenFieldEventPhotoUrl.Value = "Files/Events/" + Page.RouteData.Values["EventId"] + ".jpg";
                    //}
                    //else
                    //{
                    //    HiddenFieldEventPhotoUrl.Value = "Images/Moods/" + dt.Rows[0]["MoodId"].ToString() + "-big.png";
                    //}
                    //Owner photo url
                    if (Convert.ToBoolean(dt.Rows[0]["OwnerHasPhoto"].ToString()))
                    {
                        HiddenFieldOwnerPhotoUrl.Value = "Files/ProfilesPhotos/" + dt.Rows[0]["OwnerId"].ToString() + "-220.jpg";
                    }
                    else
                    {
                        HiddenFieldOwnerPhotoUrl.Value = "Images/ProfilesPhotos/nophoto220.png";
                    }
                    //owner rate
                    int RateCount = Convert.ToInt32(dt.Rows[0]["RateCount"].ToString());
                    int RateScore = Convert.ToInt32(dt.Rows[0]["RateScore"].ToString());
                    int RateSufficient = Convert.ToInt32(ConfigurationManager.AppSettings["RateSufficient"].ToString());

                    if (RateCount >= RateSufficient)
                    {
                        int RatePercent = (20 * RateScore / RateCount);
                        HiddenFieldOwnerRateScore.Value = RatePercent.ToString();
                        HiddenFieldOwnerRateCount.Value = RateCount.ToString();
                    }
                    else
                    {
                        HiddenFieldOwnerRateScore.Value = "0";
                        HiddenFieldOwnerRateCount.Value = "0";
                    }

                    switch (dt.Rows[0]["Status"].ToString())
                    {
                        case "1":
                            LabelStatus.Text = "Available";
                            break;
                        case "2":
                            LabelStatus.Text = "Full";
                            break;
                        case "3":
                            LabelStatus.Text = "Passed";
                            break;
                    }

                    //check to see if the user logged in or is a guest
                    if (UserId.ToString() == dt.Rows[0]["OwnerId"].ToString())
                    {
                        HyperLinkModify.Visible = true;
                        HyperLinkModify.NavigateUrl = "~/Events/Modify/" + Page.RouteData.Values["EventId"].ToString();
                    }

                    //bookmark
                    int bookmarkStatus = ev.eventBookmark(UserId, Convert.ToInt64(Page.RouteData.Values["EventId"]));

                    if (bookmarkStatus == 1)
                    {
                        ButtonBookmark.Text = "Remove Bookmark";
                    }
                    else if (bookmarkStatus == 2)
                    {
                        ButtonBookmark.Text = "Add Bookmark";
                    }
                }


                ////////////////// participants list
                DataTable dtParticipants = ev.eventParticipants(Convert.ToInt32(Page.RouteData.Values["EventId"]));

                RepeaterParticipants.DataSource = dtParticipants;
                RepeaterParticipants.DataBind();

                if (RepeaterParticipants.Items.Count == 0)
                {
                    LabelNoRecord.Visible = true;
                }

                /////////////////////////////////////////////board messages
                getBoardMessages(Convert.ToInt64(Page.RouteData.Values["EventId"]), Convert.ToInt32(dt.Rows[0]["OwnerId"].ToString()));
            }
        }

        protected void ButtonActionYes_Click(object sender, EventArgs e)
        {
            Classes.Requests r = new Classes.Requests();
            int status = r.requestSend(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Page.RouteData.Values["EventId"].ToString()), "test");

            switch (status)
            {
                case 1:
                    LabelMessage.Text = "You cannot send a request to your event.";
                    break;
                case 2:
                    LabelMessage.Text = "You already send a request to this event.";
                    break;
                case 3:
                    LabelMessage.Text = "Your request has been sent.";
                    break;
            }
        }

        protected void ButtonActionNo_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonBookmark_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Classes.Events ev = new Classes.Events();
                int status = ev.eventBookmark(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Page.RouteData.Values["EventId"]));

                if (status == 1)
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "You have successfully added this event to your bookmark list";
                    ButtonBookmark.Text = "Remove Bookmark";
                }
                else if (status == 2)
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "You have successfully removed this event from your bookmark list";
                    ButtonBookmark.Text = "Add Bookmark";
                }
            }
            else
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "You have to login first.";
            }
        }

        protected void getBoardMessages(Int64 eventId, int ownerId)
        {
            Classes.Events e = new Classes.Events();
            DataTable dt = e.eventBoardMessages(eventId, ownerId);

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("~/Error/EventNotFound");
            }
            else
            {
                RepeaterBoardMessages.DataSource = dt;
                RepeaterBoardMessages.DataBind();
            }
        }
    }
}