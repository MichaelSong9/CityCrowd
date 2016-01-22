using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using ImageManager;

namespace WebApplication1
{
    public partial class Settings : System.Web.UI.Page
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
                    Response.Redirect("~/Login/Settings");
                }
                else
                {
                    Session["UserId"] = UserId.ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login/Settings");
            }

            if (!IsPostBack)
            {
                // pre show a tab
                try
                {
                    switch (Page.RouteData.Values["Section"].ToString().ToLower())
                    {
                        case "information":
                            PanelInformation.Visible = true;
                            getDataInformation();
                            break;
                        case "photo":
                            PanelPhoto.Visible = true;
                            getDataPhoto();
                            break;
                        case "location":
                            PanelLocation.Visible = true;
                            getDataLocation();
                            break;
                        case "preferences":
                            PanelPreferences.Visible = true;
                            getDataPreferences();
                            break;
                        case "account":
                            PanelAccount.Visible = true;
                            getDataAccount();
                            break;
                        case "privacy":
                            PanelPrivacy.Visible = true;
                            getDataPrivacy();
                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        protected void hidePanels() // hide all the panels
        {
            PanelMenu.Visible = false;
            PanelInformation.Visible = false;
            PanelPhoto.Visible = false;
            PanelLocation.Visible = false;
            PanelPreferences.Visible = false;
            PanelAccount.Visible = false;
            PanelPrivacy.Visible = false;
        }

        protected void LinkButtonInfo_Click(object sender, EventArgs e)
        {
            getDataInformation();
        }

        protected void LinkButtonPhoto_Click(object sender, EventArgs e)
        {
            getDataPhoto();
        }

        protected void LinkButtonLocation_Click(object sender, EventArgs e)
        {
            getDataLocation();
        }

        protected void LinkButtonPreferences_Click(object sender, EventArgs e)
        {
            getDataPreferences();
        }

        protected void LinkButtonAccount_Click(object sender, EventArgs e)
        {
            getDataAccount();
        }

        protected void LinkButtonPrivacy_Click(object sender, EventArgs e)
        {
            getDataPrivacy();
        }

        protected void ButtonInformation_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsInformationEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = TextBoxUsername.Text;
            sqlCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = TextBoxFirstName.Text;
            sqlCmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = TextBoxLastName.Text;
            sqlCmd.Parameters.Add("@About", SqlDbType.NVarChar).Value = TextBoxAbout.Text;
            sqlCmd.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = Convert.ToInt32(DropDownListGender.SelectedValue);
            //DOB
            string dob = "";
            if (HiddenFieldDOB.Value != "0")
            {
                dob = HiddenFieldDOB.Value;
            }
            else
            {
                dob = "";
            }
            sqlCmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = Convert.ToDateTime(dob);

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }

            LabelInformationMessage.Visible = true;
            LabelInformationMessage.Text = "You have succesfully edited your information!";
        }

        protected void LinkButtonPhotoRemove_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsPhotoEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = 0;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }

            HiddenFieldPhotoUrl.Value = "Images/nophoto.png";
            LinkButtonPhotoRemove.Visible = false;
            LabelPhotoMessage.Visible = true;
            LabelPhotoMessage.Text = "You have succesfully removed your profile photo!";
        }

        protected void ButtonLocation_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsLocationEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@Distance", SqlDbType.Int).Value = Convert.ToInt32(HiddenFieldDistance.Value);
            sqlCmd.Parameters.Add("@LocationDetect", SqlDbType.Bit).Value = Convert.ToBoolean(HiddenFieldLocationDetect.Value);
            sqlCmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = Convert.ToInt32(DropDownListCity.SelectedValue);

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }

            LabelLocationMessage.Visible = true;
            LabelLocationMessage.Text = "You have succesfully changed your location settings!";
        }

        protected void LinkButtonActivate_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsDeactivate", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }

            LabelDeactivateMessage.Visible = true;
            LabelDeactivateMessage.Text = "You have succesfully deactivated your account!";
        }

        protected void ButtonPrivacy_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsPrivacyEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@FriendsOnly", SqlDbType.Bit).Value = Convert.ToBoolean(DropDownListFriends.SelectedValue);
            sqlCmd.Parameters.Add("@PublicProfile", SqlDbType.Bit).Value = 1;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }

            LabelPrivacyMessage.Visible = true;
            LabelPrivacyMessage.Text = "You have succesfully changed your privacy settings!";
        }

        protected void getDataInformation()
        {
            hidePanels();
            PanelInformation.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsInformationGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                TextBoxFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                TextBoxLastName.Text = dt.Rows[0]["LastName"].ToString();
                TextBoxUsername.Text = dt.Rows[0]["Username"].ToString();
                TextBoxAbout.Text = dt.Rows[0]["About"].ToString();
                //Gender
                if (dt.Rows[0]["Gender"].ToString() == "")
                {
                    DropDownListGender.SelectedValue = "0";
                }
                else
                {
                    DropDownListGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                }
                //Birthdate
                if (dt.Rows[0]["BirthDate"].ToString() != "")
                {
                    HiddenFieldDOB.Value = Convert.ToDateTime(dt.Rows[0]["BirthDate"].ToString()).ToString("yyyy-MM-dd");
                }
                else
                {
                    HiddenFieldDOB.Value = "0";
                }
            }
        }

        protected void getDataPhoto()
        {
            hidePanels();
            PanelPhoto.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsPhotoGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                if (Convert.ToBoolean(dt.Rows[0]["HasPhoto"].ToString())) // user has photo
                {
                    HiddenFieldPhotoUrl.Value = "Files/" + ConfigurationManager.AppSettings["folderName"].ToString() + "/ProfilesPhotos/" + Session["UserId"].ToString() + ".jpg";
                    LinkButtonPhotoRemove.Visible = true;
                }
                else // user doesnt have photo
                {
                    HiddenFieldPhotoUrl.Value = "Images/NoPhoto.png";
                    LinkButtonPhotoRemove.Visible = false;
                }
            }
        }

        protected void getDataLocation()
        {
            hidePanels();
            PanelLocation.Visible = true;

            DataTable dt = new DataTable();
            DataTable dtCountries = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsLocationGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
                dtCountries = ds.Tables[1];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                HiddenFieldDistance.Value = dt.Rows[0]["Distance"].ToString();
                HiddenFieldLocationDetect.Value = dt.Rows[0]["LocationDetect"].ToString();

                List<System.Web.UI.WebControls.ListItem> countries = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListCountry.Items.Add(new ListItem("Select Country", "0"));
                for (int i = 0; i < dtCountries.Rows.Count; i++)
                {
                    DropDownListCountry.Items.Add(new ListItem(dtCountries.Rows[i]["CountryName"].ToString(), dtCountries.Rows[i]["CountryId"].ToString()));
                }
                
                if (dt.Rows[0]["LocationId"].ToString() == "0")
                {
                    DropDownListCountry.SelectedValue = "0";
                }
                else
                {
                    int cityId = Convert.ToInt32(dt.Rows[0]["LocationId"].ToString());
                    DataTable dtLocation = new DataTable();
                    DataSet dsLocation = new DataSet();
                    SqlConnection sqlConnLocation = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlDataAdapter sdaLocation = new SqlDataAdapter("sp_locationInfoOnlyId", sqlConnLocation);
                    sdaLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sdaLocation.SelectCommand.Parameters.Add("@CityId", SqlDbType.Int).Value = cityId;

                    try
                    {
                        sdaLocation.Fill(dsLocation);
                        dtLocation = dsLocation.Tables[0];

                        locationCity(Convert.ToInt32(dtLocation.Rows[0]["StateId"].ToString()));
                        locationState(Convert.ToInt32(dtLocation.Rows[0]["CountryId"].ToString()));
                        DropDownListCountry.SelectedValue = dtLocation.Rows[0]["CountryId"].ToString();
                        DropDownListState.SelectedValue = dtLocation.Rows[0]["StateId"].ToString();
                        DropDownListCity.SelectedValue = dt.Rows[0]["LocationId"].ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConnLocation.Close();
                        sdaLocation.Dispose();
                        sqlConnLocation.Dispose();
                    }
                }

            }
        }

        protected void getDataPreferences()
        {
            hidePanels();
            PanelPreferences.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsPreferences", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                HiddenFieldNotifications.Value = dt.Rows[0]["Notifications"].ToString();
                HiddenFieldSound.Value = dt.Rows[0]["Sound"].ToString();
            }
        }

        protected void getDataAccount()
        {
            hidePanels();
            PanelAccount.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsAccountGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                TextBoxMobile.Text = dt.Rows[0]["Mobile"].ToString();
                HiddenFieldMobile.Value = dt.Rows[0]["Mobile"].ToString();
                ImageMobileStatus.ImageUrl = "~/Images/Verified" + dt.Rows[0]["MobileVerified"].ToString() + ".png";
                TextBoxEmail.Text = dt.Rows[0]["Email"].ToString();
                HiddenFieldEmail.Value = dt.Rows[0]["Email"].ToString();
                ImageEmailStatus.ImageUrl = "~/Images/Verified" + dt.Rows[0]["EmailVerified"].ToString() + ".png";
            }
        }

        protected void getDataPrivacy()
        {
            hidePanels();
            PanelPrivacy.Visible = true;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsPrivacyGet", sqlConn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

            try
            {
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }

            if (dt.Rows.Count == 0)// Profile doesn't exist
            {
                Response.Redirect("~/Error/NoProfileForSettings");
            }
            else
            {
                DropDownListFriends.SelectedValue = dt.Rows[0]["FriendsOnly"].ToString(); ;
            }
        }

        protected void ButtonAccount_Click(object sender, EventArgs e)
        {
            //email changed
            if (TextBoxEmail.Text != HiddenFieldEmail.Value)
            {
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_settingsAccountEmailEdit", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = TextBoxEmail.Text;
                HiddenFieldEmail.Value = TextBoxEmail.Text;

                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConn.Close();
                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                }

                LabelAccountMessage.Visible = true;
                LabelAccountMessage.Text = "You have succesfully changed your account settings!";
                ImageEmailStatus.ImageUrl = "~/Images/VerifiedFalse.png";
            }

            //mobile changed
            if (TextBoxMobile.Text != HiddenFieldMobile.Value)
            {
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                SqlCommand sqlCmd = new SqlCommand("sp_settingsAccountMobileEdit", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                sqlCmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = TextBoxMobile.Text;
                HiddenFieldMobile.Value = TextBoxMobile.Text;

                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConn.Close();
                    sqlCmd.Dispose();
                    sqlConn.Dispose();
                }

                LabelAccountMessage.Visible = true;
                LabelAccountMessage.Text = "You have succesfully changed your account settings!";
                ImageMobileStatus.ImageUrl = "~/Images/VerifiedFalse.png";
            }
        }

        protected void ButtonPassword_Click(object sender, EventArgs e)
        {
            if (TextBoxPasswordOld.Text == "")
            {
                LabelPasswordMessage.Text = "Please enter your current password";
                LabelPasswordMessage.Visible = true;
            }
            else
            {
                if (TextBoxPasswordOld.Text.Length < 4)
                {
                    LabelPasswordMessage.Text = "Current password is not currect";
                    LabelPasswordMessage.Visible = true;
                }
                else
                {
                    if (TextBoxPasswordNew.Text.Length < 4)
                    {
                        LabelPasswordMessage.Text = "New password must be atleast 4 characters";
                        LabelPasswordMessage.Visible = true;
                    }
                    else
                    {
                        if (TextBoxPasswordNew.Text != TextBoxPasswordNew2.Text)
                        {
                            LabelPasswordMessage.Text = "New password and re-type new password must be the same";
                            LabelPasswordMessage.Visible = true;
                        }
                        else
                        {
                            string currentPassword = "";
                            DataTable dt = new DataTable();
                            DataSet ds = new DataSet();
                            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                            SqlDataAdapter sda = new SqlDataAdapter("sp_settingsAccountPassword", sqlConn);
                            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                            sda.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);

                            try
                            {
                                sda.Fill(ds);
                                dt = ds.Tables[0];
                                currentPassword = dt.Rows[0]["Password"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            finally
                            {
                                sqlConn.Close();
                                sda.Dispose();
                                sqlConn.Dispose();
                            }

                            if (currentPassword != TextBoxPasswordOld.Text)
                            {
                                LabelPasswordMessage.Text = "Your current password is not correct" + currentPassword;
                                LabelPasswordMessage.Visible = true;
                            }
                            else
                            {
                                sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                                SqlCommand sqlCmd = new SqlCommand("sp_settingsAccountPasswordEdit", sqlConn);
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                                sqlCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = TextBoxPasswordNew.Text;

                                try
                                {
                                    sqlConn.Open();
                                    sqlCmd.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {

                                }
                                finally
                                {
                                    sqlConn.Close();
                                    sqlCmd.Dispose();
                                    sqlConn.Dispose();
                                }
                                LabelPasswordMessage.Text = "You have successfully changed your password";
                                LabelPasswordMessage.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void ButtonPereferences_Click(object sender, EventArgs e)
        {
            bool notifications = false;
            bool sound = false;
            if (HiddenFieldNotifications.Value == "True")
            {
                notifications = true;
            }
            if (HiddenFieldSound.Value == "True")
            {
                sound = true;
            }

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand("sp_settingsPereferencesEdit", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
            sqlCmd.Parameters.Add("@Notifications", SqlDbType.Bit).Value = notifications;
            sqlCmd.Parameters.Add("@Sound", SqlDbType.Bit).Value = sound;

            try
            {
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sqlCmd.Dispose();
                sqlConn.Dispose();
            }

            LabelPereferencesMessage.Visible = true;
            LabelPereferencesMessage.Text = "You have succesfully changed your pereferences settings!";
        }

        protected void ButtonPhoto_Click(object sender, EventArgs e)
        {
            if (FileUploadPicture.HasFile)
            {

                string filePath = "~/Files/Temp/" + FileUploadPicture.FileName;
                if (Path.GetExtension(filePath).ToLower() == ".jpg" || Path.GetExtension(filePath).ToLower() == ".jpeg")
                {
                    string fileName = Session["UserId"] + Path.GetExtension(filePath).ToLower();
                    string relativePath = @"~\Files\Temp\" + fileName;
                    FileUploadPicture.SaveAs(Server.MapPath(relativePath));

                    //use WebManager to get the file, and save it
                    IImageInfo img = WebManager.GetImageInfo(FileUploadPicture);
                    img.Path = Server.MapPath("~") + "\\Files\\Temp\\";
                    img.FileName = Session["UserId"] + ".jpg";

                    //now create resized versions, and save them
                    IImageInfo img160 = img.ResizeMe(160, 160);
                    img160.Save("~/Files/" + ConfigurationManager.AppSettings["folderName"].ToString() + "/ProfilesPhotos/");

                    LabelPhotoMessage.Visible = true;
                    LabelPhotoMessage.Text = "You have successfully uploaded your picture.";

                    HiddenFieldPhotoUrl.Value = "Files/" + ConfigurationManager.AppSettings["folderName"].ToString() + "/ProfilesPhotos/" + Session["UserId"].ToString() + ".jpg";

                    File.Delete(Server.MapPath("~") + "\\Files\\Temp\\" + fileName);

                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
                    SqlCommand sqlCmd = new SqlCommand("sp_settingsPhotoEdit", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Session["UserId"]);
                    sqlCmd.Parameters.Add("@HasPhoto", SqlDbType.Bit).Value = true;

                    try
                    {
                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConn.Close();
                        sqlCmd.Dispose();
                        sqlConn.Dispose();
                    }
                }
                else
                {
                    LabelPhotoMessage.Visible = true;
                    LabelPhotoMessage.Text = "Select a picture with JPG extenstion.";
                }
            }
            else
            {
                LabelPhotoMessage.Visible = true;
                LabelPhotoMessage.Text = "Select a picture file.";
            }

        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationState(Convert.ToInt32(DropDownListCountry.SelectedValue));
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            locationCity(Convert.ToInt32(DropDownListState.SelectedValue));
        }

        void locationCity(int stateId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationCities", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@StateId", SqlDbType.SmallInt).Value = stateId;
                sda.Fill(ds);
                dt = ds.Tables[0];

                List<System.Web.UI.WebControls.ListItem> states = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListCity.Items.Clear();
                DropDownListCity.Items.Add(new ListItem("Select City", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownListCity.Items.Add(new ListItem(dt.Rows[i]["CityName"].ToString(), dt.Rows[i]["CityId"].ToString()));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }
        }

        void locationState(int countryId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("sp_locationStates", sqlConn);

            try
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@CountryId", SqlDbType.SmallInt).Value = countryId;
                sda.Fill(ds);
                dt = ds.Tables[0];

                List<System.Web.UI.WebControls.ListItem> states = new List<System.Web.UI.WebControls.ListItem>();
                DropDownListState.Items.Clear();
                DropDownListState.Items.Add(new ListItem("Select State", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownListState.Items.Add(new ListItem(dt.Rows[i]["StateName"].ToString(), dt.Rows[i]["StateId"].ToString()));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConn.Close();
                sda.Dispose();
                sqlConn.Dispose();
            }
        }

        void locationCountry()
        {

        }
    }
}