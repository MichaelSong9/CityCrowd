<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication1.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            profilePage();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Profile</div>
        <div class="nMenuAdd">+</div>
    </div>
    <div class="nContentCover nProfileCover">

        <div class="nEventCoverBox ">
            <div class="arrow-right">
                <div class="nVerified"></div>
            </div>
            <div class="nCountry">
                <div class="nCountryFlag">
                </div>
                <div class="nProfileBlackText strokedText">
                    <asp:Label ID="LabelCountry" runat="server"></asp:Label>
                </div>
                <div class="nProfileBlackText strokedText">
                    <asp:Label ID="LabelCity" runat="server"></asp:Label>
                </div>
            </div>

        </div>
        <div class="nEventCoverBox">
            <div class="nRoundPicture nProfilePicture nProfpagePicture">
                <canvas id="canvas2" width="115" height="115"></canvas>
            </div>
        </div>
        <div class="nEventCoverBox nFollowings">
            <div class="nProfileBlackText nProfileFollowersLabel strokedText">
                Followers
            </div>
            <div class="nProfileBlackText nProfileFollowersLabel strokedText">
                <asp:Label ID="LabelFollowers" runat="server" Text="# followers"></asp:Label>
            </div>
            <div class="nProfileBlackText nProfileFollowingsLabel strokedText">
                Following
            </div>
            <div class="nProfileBlackText nProfileFollowingsLabel strokedText">
                <asp:Label ID="LabelFollowing" runat="server" Text="# following"></asp:Label>
            </div>
        </div>
    </div>

    <div class="clear"></div>
    <div class="nProfileMiddle">
        <div class="profileName nEVOwnerName">
            <b>
                <asp:Label ID="LabelName" runat="server"></asp:Label>
            </b>
        </div>
        <div class="profileName"><b>@<asp:Label ID="LabelUsername" runat="server"></asp:Label></b></div>
        <div class="nDots nDotsProfile">
            <i class="fa fa-ellipsis-h nDotsLogo"></i>
            <div class="clear"></div>
            <div class="nDotsBox nDotBoxLinks">
                <div class="nDotsBoxInsideContainer nDotsBoxInsideContainerLinks">
                    <div class="nDotsBoxEachLogo nDotsBoxEachLogoBlock"></div>
                    <div class="nDotsBoxEach">Block user</div>
                    <div class="nDotsBoxEachLogo nDotsBoxEachLogoReport"></div>
                    <div class="nDotsBoxEach">Report user</div>
                </div>
                <div class="nDotsBoxInsideContainer nDotsBoxInsideContainerEdit invisible">
                    <div class="nDotsBoxEachLogo nDotsEditProfile"></div>
                    <div class="nDotsBoxEach">Edit profile</div>
                </div>
            </div>
        </div>
        <div class="nProfileUserDescription">
            <asp:Label ID="LabelAbout" runat="server"></asp:Label>
        </div>
        <div class="nProfileFollowButton">FOLLOW</div>
    </div>

    <div class="nTabbedContent">
        <div class="nTabsHolder">
            <div class="nTabs nTabs4 nExploreSelected" ntabnumber="1">
                <b>EVENTS</b>
            </div>
            <div class="nTabs nTabs4 reviewButton" ntabnumber="2">
                <b>REVIEW</b>
            </div>
            <div class="nTabs nTabs4" ntabnumber="3">
                <b>FOLLOWERS</b>
            </div>
            <div class="nTabs nTabs4" ntabnumber="4">
                <b>FOLLOWING</b>
            </div>
        </div>
        <div class="nExploreContentHolder">
            <!-- events-->
            <div class="nDescriptionContent nTabContents">
                <asp:Repeater ID="RepeaterEvents" runat="server">
                    <ItemTemplate>
                        <asp:HiddenField ID="HiddenFieldTypeId" runat="server" Value='<%# Eval("TypeId") %>' />
                        <asp:HyperLink ID="HyperLinkEvent" runat="server" NavigateUrl='<%# "~/Events/"+ Eval("EventId") %>'><%# Eval("Name") %></asp:HyperLink>
                        <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="nProfileErrorMessage"><asp:Label ID="LabelEventsNoRecord" runat="server" Visible="True"></asp:Label></div>
            </div>

            <!-- reviews-->
            <div class="nBoardContent nTabContents hidden">
                <div>
                    <div class="nGraphTitle">
                        <div class="nGraphTitleNumber">
                            <asp:Label ID="LabelRatePercent" runat="server" Text="0"></asp:Label>%  
                        </div>
                        <div class="nGraphTitleRating">
                            <asp:Label ID="LabelRateCount" runat="server" Text="0"></asp:Label>
                            rating
                        </div>
                    </div>
                    <div class="nBarGraphs">
                        <div>
                            <div class="nLeft">5</div>
                            <div class="nGraphBar" data-score="90">
                                <asp:Label ID="LabelRateFive" CssClass="invisible" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div>
                            <div class="nLeft">4</div>
                            <div class="nGraphBar" data-score="10">
                                <asp:Label ID="LabelRateFour" CssClass="invisible" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div>
                            <div class="nLeft">3</div>
                            <div class="nGraphBar" data-score="12">
                                <asp:Label ID="LabelRateThree" CssClass="invisible" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div>
                            <div class="nLeft">2</div>
                            <div class="nGraphBar" data-score="5">
                                <asp:Label ID="LabelRateTwo" CssClass="invisible" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div>
                            <div class="nLeft">1</div>
                            <div class="nGraphBar" data-score="20">
                                <asp:Label ID="LabelRateOne" CssClass="invisible" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div>
                    <asp:Repeater ID="RepeaterReviews" runat="server">
                        <ItemTemplate>
                            <div class="nMessagesPicture nEVMessagesPicture">
                                <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                                <div class="nSmallIcon nDotsBoxEachLogoBookmark nProfileMessagesHeart"></div>
                                <asp:Label ID="LabelRate" runat="server" CssClass="nProfileReviewHeartText" Text='<%# Eval("Rate") %>'></asp:Label>
                                
                            </div>
                            <div class="invisible">
                                <asp:HyperLink ID="HyperLinkReviewer" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("Username") %>' CssClass="nEVparticipantName"><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                            </div>
                            <div class="nProfileReviewBody">
                                <asp:Label ID="LabelComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                <!--"He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person""He is such a nice person"-->
                            </div>
                            <div class="clear"></div>
                            <div class="MessageListDate nProfileReviewDate">
                                <div class="nSmallIcon nBlogClock nMessagesClock"></div>

                                <asp:Label ID="LabelSubmitDate" runat="server" Text='<%# Eval("SubmitDate") %>'></asp:Label>
                            </div>
                            <div class="clear"></div>
                            <hr class="nMessagesHr nMessagesProfileHr">
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="nProfileErrorMessage"><asp:Label ID="LabelReviewsNoRecord" runat="server" Visible="True"></asp:Label></div>
            </div>

            <!-- followers-->
            <div class="nParticipantsContent nTabContents hidden">
                <asp:Repeater ID="RepeaterFollowers" runat="server">
                    <ItemTemplate>
                        <div class="nMessagesPicture nEVMessagesPicture">
                            <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                        </div>
                        <asp:HyperLink ID="HyperLinkParticipant" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("Username") %>' CssClass="nEVparticipantName nNoDecoration"><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="nProfileErrorMessage"><asp:Label ID="LabelFollowersNoRecord" runat="server" Visible="True"></asp:Label></div>
            </div>

            <!-- followings-->
            <div class="nMapContent nTabContents hidden">
                <asp:Repeater ID="RepeaterFollowing" runat="server">
                    <ItemTemplate>
                        <div class="nMessagesPicture nEVMessagesPicture">
                            <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                        </div>
                        <asp:HyperLink ID="HyperLinkParticipant" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("Username") %>' CssClass="nEVparticipantName nNoDecoration"><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="nProfileErrorMessage"><asp:Label ID="LabelFollowingNoRecord" runat="server" Visible="True"></asp:Label></div>
            </div>
        </div>
    </div>
    <div class="nExploreFooter">
        <div class="nFooterButton nProfileMessageButton">WRITE A MESSAGE</div>
    </div>

    <div class="invisible">
        <!-- profile rate -->
        <asp:Label ID="LabelRate" runat="server"></asp:Label>
    </div>
    <asp:HiddenField ID="HiddenFieldUserId" runat="server" />
    <asp:HiddenField ID="HiddenFieldProfilePhoto" runat="server" />


    <asp:HiddenField ID="HiddenFieldFlagId" runat="server" />
    <asp:HiddenField ID="HiddenFieldProfileVerified" runat="server" />

    <!-- 
        <asp:Button ID="ButtonFollow" runat="server" Text="Button" OnClick="ButtonFollow_Click" />
        <asp:Label ID="LabelMessage" runat="server" Visible="False"></asp:Label>
        <asp:HyperLink ID="HyperLinkEdit" runat="server" Visible="False" NavigateUrl="~/Settings">Edit Profile</asp:HyperLink>
        -->
</asp:Content>
