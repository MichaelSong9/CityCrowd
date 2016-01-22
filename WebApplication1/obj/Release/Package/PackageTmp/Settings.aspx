<%@ Page Title="Settings" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="WebApplication1.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            settings();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Settings</div>
        <div class="nMenuAdd">+</div>
    </div>
    <!-- header end -->

    <asp:Panel ID="PanelMenu" runat="server" Visible="True">
        <div>
            <div class="nSettingsList nSettingsListInformation">
                <div class="nSettingsListText"><b>Information</b></div>
                <div class="nSettingsListArrow"></div>
                <div class="clear"></div>
                <hr class="nSettingsHr" />
            </div>
            <div class="nSettingsList nSettingsListPhoto">
                <div class="nSettingsListText"><b>Profile photo</b></div>
                <div class="nSettingsListArrow"></div>
                <div class="clear"></div>
                <hr class="nSettingsHr" />
            </div>
            <div class="nSettingsList nSettingsListLocation">
                <div class="nSettingsListText"><b>Location</b></div>
                <div class="nSettingsListArrow"></div>
                <div class="clear"></div>
                <hr class="nSettingsHr" />
            </div>
            <div class="nSettingsList nSettingsListPreferences">
                <div class="nSettingsListText"><b>Preferences</b></div>
                <div class="nSettingsListArrow"></div>
                <div class="clear"></div>
                <hr class="nSettingsHr" />
            </div>
            <div class="nSettingsList nSettingsListAccount">
                <div class="nSettingsListText"><b>Account</b></div>
                <div class="nSettingsListArrow"></div>
                <div class="clear"></div>
                <hr class="nSettingsHr" />
            </div>
            <div class="nSettingsList nSettingsListPrivacy">
                <div class="nSettingsListText"><b>Privacy</b></div>
                <div class="nSettingsListArrow"></div>
                <div class="clear"></div>
                <hr class="nSettingsHr" />
            </div>

        </div>
    </asp:Panel>


    <!-- edit information settings -->
    <asp:Panel ID="PanelInformation" runat="server" Visible="False">
        <script>
            $('.nPageTitle').html("Settings - Information");
        </script>

        <div class="nSIRow">
            <div class="nSIview ">
                <div class="nSIRowTitle">First Name</div>
                <div class="nSIRowViewArrow"></div>
                <div class="nSIRowValue">Farhad</div>
            </div>

            <div class="nSImodify invisible">
                <div class="nSIRowTitle">
                    <asp:TextBox ID="TextBoxFirstName" runat="server" data-mini="true"></asp:TextBox></div>
                <div class="nSIRowEnterArrow"></div>
            </div>
            <div class="clear"></div>
            <hr class="nSIEditHr" />
        </div>
        <div class="nSIRow">
            <div class="nSIview">
                <div class="nSIRowTitle">Last name</div>
                <div class="nSIRowViewArrow"></div>
                <div class="nSIRowValue">Eftekhari</div>
            </div>
            <div class="nSImodify invisible">
                <div class="nSIRowTitle">
                    <asp:TextBox ID="TextBoxLastName" runat="server" data-mini="true"></asp:TextBox></div>
                <div class="nSIRowEnterArrow"></div>
            </div>
            <div class="clear"></div>
            <hr class="nSettingsHr" />
        </div>
        <div class="nSIRow">
            <div class="nSIview ">
                <div class="nSIRowTitle">Username</div>
                <div class="nSIRowViewArrow"></div>
                <div class="nSIRowValue">Please enter</div>
            </div>
            <div class="nSImodify invisible ">
                <div class="nSIRowTitle">
                    <asp:TextBox ID="TextBoxUsername" runat="server" data-mini="true"></asp:TextBox></div>
                <div class="nSIRowEnterArrow"></div>
            </div>
            <div class="clear"></div>
            <hr class="nSettingsHr" />
        </div>
        <div class="nSIRow">
            <div class="nSIview ">
                <div class="nSIRowTitle">Gender</div>
                <div class="nSIRowViewArrow"></div>
                <div class="nSIRowValue">Male</div>
            </div>

            <div class="nSImodify invisible ">
                <div class="nSIRowTitle">
                    <asp:DropDownList ID="DropDownListGender" runat="server" data-mini="true" data-theme="b">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="2">Female</asp:ListItem>
                        <asp:ListItem Value="1">Male</asp:ListItem>
                        <asp:ListItem Value="3">Other</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="nSIRowEnterArrow"></div>
            </div>
            <div class="clear"></div>
            <hr class="nSettingsHr" />
        </div>
        <div class="nSIRow">
            <div class="nSIview ">
                <div class="nSIRowTitle">Birth Date</div>
                <div class="nSIRowViewArrow"></div>
                <div class="nSIRowValue">24 Nov. 1985</div>
            </div>

            <div class="nSImodify invisible ">
                <div class="nSIRowTitle">
                    <input type="date" name="date" id="date" value="1961-08-04" data-mini="true" />
                </div>
                <div class="nSIRowEnterArrow"></div>
            </div>
            <div class="clear"></div>
            <hr class="nSettingsHr" />
        </div>
        <div class="nSIRow">
            <div class="nSIview ">
                <div class="nSIRowTitle">About</div>
                <div class="nSIRowViewArrow"></div>
                <div class="nSIRowValue"></div>
                <div class="clear"></div>
                <div class="NSRowAboutValue">Hi, it is me, Farhad</div>
            </div>
            <div class="nSImodify invisible ">
                <div class="nSIRowTitle">About</div>
                <div class="nSIRowEnterArrow"></div>
                <div class="clear"></div>
                <div class="NSRowAboutValue">
                    <asp:TextBox ID="TextBoxAbout" runat="server"></asp:TextBox></div>
            </div>
            <div class="clear"></div>
            <hr class="nSettingsHr" />
        </div>

        <!--
            <asp:Label ID="LabelFirstName" runat="server" Text="First Name:" AssociatedControlID="TextBoxFirstName"></asp:Label>
            <asp:Label ID="LabelLastName" runat="server" AssociatedControlID="TextBoxLastName" Text="Last Name:"></asp:Label>
            <asp:Label ID="LabelUsername" runat="server" AssociatedControlID="TextBoxUsername" Text="Username:"></asp:Label>
            <asp:Label ID="LabelGender" runat="server" AssociatedControlID="DropDownListGender" Text="Gender:"></asp:Label>
            <asp:Label ID="LabelBirthdate" runat="server" Text="Birthdate:"></asp:Label>
            <asp:Label ID="LabelAbout" runat="server" AssociatedControlID="TextBoxAbout" Text="About:"></asp:Label>
            <asp:Button ID="ButtonInformation" runat="server" Text="Save Changes" OnClick="ButtonInformation_Click" Style="height: 26px" />
            <asp:Label ID="LabelInformationMessage" runat="server" Visible="False"></asp:Label>
            <asp:HiddenField ID="HiddenFieldDOB" runat="server" />
        -->
        <div class="nExploreFooter">
            <div class="nFooterButton nProfileMessageButton">&nbsp BACK TO SETTINGS</div>
        </div>
    </asp:Panel>





    <asp:Panel ID="PanelPhoto" runat="server" Visible="False">
        <script>
            $(document).ready(function () {
                $('.nPageTitle').html("Settings - Picture");
                settingsPhotos();
            });

        </script>
        <br />
        <div class="nRoundPicture nProfilePicture nProfpagePicture">
        </div>
        <div class="nSPButton nSPChooseFileButton">CHOOSE FILE</div>
        <div class="nSPButton nSPUploadButton">UPLOAD</div>
        <div class="nSPButton nSPRemoveButton">REMOVE</div>
        <br />
        <div class="nWrongLogin hidden">
            <div class="nWrongLoginIcon"></div>
            <b>
                <asp:Label ID="LabelPhotoMessage" runat="server" Visible="False"></asp:Label></b>
        </div>
        <div class="nExploreFooter">
            <div class="nFooterButton nProfileMessageButton">&nbsp BACK TO SETTINGS</div>
        </div>

        <div class="hidden">
            <asp:FileUpload ID="FileUploadPicture" runat="server" CssClass="nSPAspChooseButton" />
            <asp:Button ID="ButtonPhoto" runat="server" Text="Upload" OnClick="ButtonPhoto_Click" CssClass="nSPAspUploadButton" />
            <asp:LinkButton ID="LinkButtonPhotoRemove" runat="server" OnClick="LinkButtonPhotoRemove_Click" CssClass="nSPAspRemoveButton">Remove current photo</asp:LinkButton>
            <asp:HiddenField ID="HiddenFieldPhotoUrl" runat="server" />
        </div>
    </asp:Panel>


    <asp:Panel ID="PanelLocation" runat="server" Visible="False">
        <!--
        <div data-role="page" id="settings_location_edit">

            <div data-role="header">
                <div data-role="navbar">
                    <ul>
                        <li class="settingsTabs"><a href="Information" data-icon="grid">Information</a></li>
                        <li class="settingsTabs"><a href="Photo" data-icon="grid">Photo</a></li>
                        <li class="settingsTabs"><a href="Location" data-icon="grid" class="ui-btn-active ui-state-persist">Location</a></li>
                        <li class="settingsTabs"><a href="Preferences" data-icon="grid">Preferences</a></li>
                        <li class="settingsTabs"><a href="Account" data-icon="grid">Account</a></li>
                        <li class="settingsTabs"><a href="Privacy" data-icon="grid">Privacy</a></li>
                    </ul>
                </div>
            </div>

            <div data-role="main" class="ui-content">
                <div class="ui-corner-all ui-corner-all infoBox">


                    <div class="ui-body ui-body-a">
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <asp:Label ID="LabelLocation" runat="server" Text="Current Location:"></asp:Label>
                            </div>
                            <div class="ui-block-b rightText">
                                <ul id="autocomplete" data-role="listview" data-inset="true" data-filter="true" data-filter-placeholder="Find a city..." data-filter-theme="a">
                                </ul>
                                <asp:DropDownList ID="DropDownListCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select Country</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="DropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select State</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="DropDownListCity" runat="server">
                                        <asp:ListItem Value="0">Select City</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <asp:Label ID="LabelDistance" runat="server" Text="Distance Covered:"></asp:Label>
                            </div>
                            <div class="ui-block-b rightText">

                                <input type="range" name="slider-fill-mini" id="distanceSlider" value="40" min="0" max="100" data-mini="true" data-highlight="true" data-theme="a" data-track-theme="a" />
                            </div>
                        </div>
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <p>Detect my location automatically.</p>
                            </div>
                            <div class="ui-block-b rightText">
                                <select name="flip-2" id="locationDetectionFlip" data-role="slider" data-mini="true">
                                    <option value="off">Off</option>
                                    <option value="on">On</option>
                                </select>
                            </div>
                        </div>
                        <asp:Button ID="ButtonLocation" runat="server" OnClick="ButtonLocation_Click" Text="Save Changes" />
                        <asp:Label ID="LabelLocationMessage" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
                <!-- /content
            </div>
        </div>
        <asp:HiddenField ID="HiddenFieldDistance" runat="server" />
        <asp:HiddenField ID="HiddenFieldCurrentCity" runat="server" />
        <asp:HiddenField ID="HiddenFieldLocationDetect" runat="server" />
            -->
    </asp:Panel>



    <asp:Panel ID="PanelPreferences" runat="server" Visible="False">
        <!--
        <div data-role="page" id="settings_pref_edit">
            <div data-role="header">
                <div data-role="navbar">
                    <ul>
                        <li class="settingsTabs"><a href="Information" data-icon="grid">Information</a></li>
                        <li class="settingsTabs"><a href="Photo" data-icon="grid">Photo</a></li>
                        <li class="settingsTabs"><a href="Location" data-icon="grid">Location</a></li>
                        <li class="settingsTabs"><a href="Preferences" data-icon="grid" class="ui-btn-active ui-state-persist">Preferences</a></li>
                        <li class="settingsTabs"><a href="Account" data-icon="grid">Account</a></li>
                        <li class="settingsTabs"><a href="Privacy" data-icon="grid">Privacy</a></li>
                    </ul>
                </div>
            </div>

            <div data-role="main" class="ui-content">
                <div class="ui-corner-all ui-corner-all infoBox">


                    <div class="ui-body ui-body-a">
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <p>Notification emails</p>
                            </div>
                            <div class="ui-block-b rightText">
                                <select name="flip-2" id="emailNotificationFlip" data-role="slider" data-mini="true">
                                    <option value="off">Off</option>
                                    <option value="on">On</option>
                                </select>
                            </div>
                        </div>
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <p>Sound</p>
                            </div>
                            <div class="ui-block-b rightText">
                                <select name="flip-2" id="soundFlip" data-role="slider" data-mini="true">
                                    <option value="off">Off</option>
                                    <option value="on">On</option>
                                </select>
                            </div>
                        </div>
                        <asp:Button ID="ButtonPereferences" runat="server" Text="Save Changes" OnClick="ButtonPereferences_Click" />
                        <asp:Label ID="LabelPereferencesMessage" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
                <!-- /content
            </div>
        </div>
        <asp:HiddenField ID="HiddenFieldNotifications" runat="server" />
        <asp:HiddenField ID="HiddenFieldSound" runat="server" />
            -->
    </asp:Panel>

    <asp:Panel ID="PanelAccount" runat="server" Visible="False">
        <!--
        <div data-role="page" id="settings_account_edit">
            <div data-role="header">

                <div data-role="navbar">
                    <ul>
                        <li class="settingsTabs"><a href="Information" data-icon="grid">Information</a></li>
                        <li class="settingsTabs"><a href="Photo" data-icon="grid">Photo</a></li>
                        <li class="settingsTabs"><a href="Location" data-icon="grid">Location</a></li>
                        <li class="settingsTabs"><a href="Preferences" data-icon="grid">Preferences</a></li>
                        <li class="settingsTabs"><a href="Account" data-icon="grid" class="ui-btn-active ui-state-persist">Account</a></li>
                        <li class="settingsTabs"><a href="Privacy" data-icon="grid">Privacy</a></li>
                    </ul>
                </div>
            </div>

            <div data-role="main" class="ui-content">
                <div class="ui-corner-all ui-corner-all infoBox">
                    <div class="ui-bar ui-bar-a">
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <h3>
                                    <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label></h3>
                            </div>
                            <div class="ui-block-b rightText">
                            </div>
                        </div>
                    </div>

                    <div class="ui-body ui-body-a">
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <asp:Label ID="LabelPasswordOld" runat="server" AssociatedControlID="TextBoxPasswordOld" Text="Current Password:"></asp:Label>
                            </div>
                            <div class="ui-block-b rightText">
                                <asp:TextBox ID="TextBoxPasswordOld" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="ui-block-a">
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="TextBoxPasswordNew" Text="New Password:"></asp:Label>
                            </div>
                            <div class="ui-block-b rightText">
                                <asp:TextBox ID="TextBoxPasswordNew" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="ui-block-a">
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="TextBoxPasswordNew2" Text="Re-type New Password:"></asp:Label>
                            </div>
                            <div class="ui-block-b rightText">
                                <asp:TextBox ID="TextBoxPasswordNew2" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <asp:Button ID="ButtonPassword" runat="server" Text="Change Password" OnClick="ButtonPassword_Click" />
                            <asp:Label ID="LabelPasswordMessage" runat="server" Visible="False"></asp:Label>

                        </div>

                    </div>
                    <br />
                    <div class="ui-corner-all ui-corner-all infoBox">
                        <div class="ui-bar ui-bar-a">
                            <div class="ui-grid-a">
                                <div class="ui-block-a">
                                    <h3>Edit Account Details</h3>
                                </div>
                                <div class="ui-block-b rightText">
                                </div>
                            </div>
                        </div>

                        <div class="ui-body ui-body-a">
                            <div class="ui-grid-a">
                                <div class="ui-block-a">
                                    <asp:Label ID="LabelEmail" runat="server" Text="E-mail Address:"></asp:Label>
                                </div>
                                <div class="ui-block-b rightText">
                                    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Image ID="ImageEmailStatus" runat="server" />
                                    <br />
                                </div>
                            </div>
                            <div class="ui-grid-a">
                                <div class="ui-block-a">
                                    <asp:Label ID="LabelMobile" runat="server" Text="Phone Number:"></asp:Label>
                                </div>
                                <div class="ui-block-b rightText">
                                    <asp:TextBox ID="TextBoxMobile" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Image ID="ImageMobileStatus" runat="server" />
                                    <br />
                                </div>
                            </div>
                            <div class="ui-grid-a">
                                <div class="ui-block-a">
                                </div>
                                <div class="ui-block-b rightText">
                                    <asp:LinkButton ID="LinkButtonActivate" runat="server" OnClick="LinkButtonActivate_Click">De-activate Account</asp:LinkButton>
                                </div>
                            </div>

                            <asp:Label ID="LabelDeactivateMessage" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="ButtonAccount" runat="server" Text="Save Changes" OnClick="ButtonAccount_Click" />
                            <asp:Label ID="LabelAccountMessage" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
                <!-- /content
            </div>
        </div>
        <asp:HiddenField ID="HiddenFieldEmail" runat="server" />
        <asp:HiddenField ID="HiddenFieldMobile" runat="server" />
            -->
    </asp:Panel>
    <asp:Panel ID="PanelPrivacy" runat="server" Visible="False">
        <!--
        <div data-role="page" id="settings_privacy_edit">
            <div data-role="header">
                <div data-role="navbar">
                    <ul>
                        <li class="settingsTabs"><a href="Information" data-icon="grid">Information</a></li>
                        <li class="settingsTabs"><a href="Photo" data-icon="grid">Photo</a></li>
                        <li class="settingsTabs"><a href="Location" data-icon="grid">Location</a></li>
                        <li class="settingsTabs"><a href="Preferences" data-icon="grid">Preferences</a></li>
                        <li class="settingsTabs"><a href="Account" data-icon="grid">Account</a></li>
                        <li class="settingsTabs"><a href="Privacy" data-icon="grid" class="ui-btn-active ui-state-persist">Privacy</a></li>
                    </ul>
                </div>
            </div>

            <div data-role="main" class="ui-content">
                <div class="ui-corner-all ui-corner-all infoBox">

                    <div class="ui-body ui-body-a">
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                &nbsp;<asp:Label ID="LabelFriends" runat="server" AssociatedControlID="DropDownListFriends" Text="Who view your events?"></asp:Label>
                            </div>
                            <div class="ui-block-b rightText">
                                <asp:DropDownList ID="DropDownListFriends" runat="server">
                                    <asp:ListItem Value="False">Everyone</asp:ListItem>
                                    <asp:ListItem Value="True">Followers</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <asp:Button ID="ButtonPrivacy" runat="server" OnClick="ButtonPrivacy_Click" Text="Save The Changes" />
                        <asp:Label ID="LabelPrivacyMessage" runat="server" Visible="False"></asp:Label>

                    </div>
                </div>
                <!-- /content 
            </div>

        </div>

            -->
    </asp:Panel>
</asp:Content>
