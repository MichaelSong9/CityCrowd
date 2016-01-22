<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="EventsView.aspx.cs" Inherits="WebApplication1.EventsView" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            eventsView();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">View Event</div>
        <div class="nMenuAdd">+</div>
    </div>

    <div class="nContentCover">
        <div class="nContentCoverTransparent">
            <div class="nContentTitle">
                <b>
                    <asp:Label ID="LabelName" runat="server"></asp:Label></b>
            </div>
            <!-- <div class="nDots"><i class="fa fa-ellipsis-h"></i>&nbsp</div> -->
            <div class="nDots nDotsEvent">
                <i class="fa fa-ellipsis-h nDotsLogo"></i>
                <div class="clear"></div>
                <div class="nDotsBox nDotBoxLinks">
                    <div class="nDotsBoxEachLogo nDotsBoxEachLogoShare"></div>
                    <div class="nDotsBoxEach">Share</div>
                    <div class="nDotsBoxEachLogo nDotsBoxEachLogoBookmark"></div>
                    <div class="nDotsBoxEach nEVbookmarkButton">Bookmark</div>
                    <div class="nDotsBoxEachLogo nDotsBoxEachLogoReport"></div>
                    <div class="nDotsBoxEach">Report user</div>
                </div>
                <div class="nDotsBox nDotsBoxShare hide">
                    <a class="nEVShareButtonFB" target="_blank" href="#">
                        <img class="nEVshareImg" src="../Images/Icons/fb.png" alt="facebook" width="32" height="32" /></a>
                    <a class="nEVShareButtonTwitter" target="_blank" href="#">
                        <img class="nEVshareImg" src="../Images/Icons/twitter.png" alt="facebook" width="32" height="32" /></a>
                </div>
            </div>
            <div class="clear"></div>
            <div class="nTimeAndLocation">
                <div class="nExploreTime"><i class="fa fa-clock-o"></i>&nbsp <span class="nEVdate">Time unknown</span> </div>
                <div class="nExploreLocation">
                    <i class="fa fa-map-marker"></i>&nbsp
                    <asp:Label ID="LabelLocation" runat="server"></asp:Label>
                </div>
            </div>
            <div class="clear"></div>
            <div class="nEventCoverBox">
                <div class="nRoundPicture nActivityPicture">
                </div>
            </div>
            <div class="nEventCoverBox">
                <div class="nRoundPicture nProfilePicture">
                    <canvas id="canvas2" width="115" height="115"></canvas>
                </div>
            </div>
            <div class="nEventCoverBox">
                <div class="nRoundPicture nParticipantNumberPicture">
                    <div class="nParticipantNumberPictureTop">
                        <asp:Label ID="LabelParticipantsAvailable" runat="server"></asp:Label>
                        <canvas id="EVcanvas" width="115" height="115"></canvas>
                    </div>
                    <div class="nParticipantNumberPictureBot">
                        <b>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </b>
                    </div>
                </div>
                <!-- old graphs
                <div class="nRoundPicture nNewGraph">
                    <div class="nNewGraphTop"></div>
                    <div class="nNewGraphBottom"></div>
                    <div class="nNewGraphText">
                        <b>
                            1</b> participant needed (<span class="nParticipantAvailable"></span>/3)
                    </div>
                </div>
                    -->
            </div>
        </div>
    </div>


    <div class="nTabbedContent">
        <div class="nTabsHolder">
            <div class="nTabs nTabs3 nExploreSelected" ntabnumber="1">
                <b>DESCRIPTION</b>
            </div>
            <div class="nTabs nTabs3" ntabnumber="2">
                <b>BOARD</b>
            </div>
            <div class="nTabs nTabs3" ntabnumber="3">
                <b>PARTICIPANTS</b>
            </div>
            <!--<div class="nTabs nTabs4" ntabnumber="4">
                <b>MAP</b>
            </div>
            -->
        </div>
        <div class="nExploreContentHolder">
            <div class="nDescriptionContent nTabContents">
                <asp:Label ID="LabelDescriptions" runat="server"></asp:Label>
            </div>
            <div class="nBoardContent nTabContents hidden">
                <asp:Panel ID="PanelBoardMessageAdd" runat="server">

                </asp:Panel>
                <asp:Repeater ID="RepeaterBoardMessages" runat="server">
                    <ItemTemplate>
                        <div class="MessageSender">
                            <asp:HiddenField ID="HiddenFieldSenderName" runat="server" Value='<%# Eval("SenderName") %>' />

                        </div>
                        <div class="eachMessage">
                            <div class="nMessagesPicture nMessagesShowPicture">
                                <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                            </div>
                            <div class="nMSWhiteDiv1"></div>
                            <div class="nMSWhiteDiv2"></div>
                            <asp:HiddenField ID="HiddenFieldMessageId" runat="server" Value='<%# Eval("MessageId") %>' />

                            <div class="nMSMessage">
                                <span><%# Eval("Message") %></span>
                            </div>

                            <div>
                            </div>
                            <div class="invisible">
                                each message sender lable
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="MessageListDate nMSDate">
                            <!-- <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("PassedDate") %>' CssClass="EachMessageDate ViewClass"></asp:Label> -->
                        </div>
                        <div class="clear"></div>

                        <asp:HiddenField ID="HiddenFieldIsOwner" runat="server" Value='<%# Eval("IsOwner") %>' />
                        <asp:HiddenField ID="HiddenFieldSenderId" runat="server" Value='<%# Eval("SenderId") %>' />

                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="nParticipantsContent nTabContents hidden">
                <asp:Label ID="LabelNoRecord" runat="server" Text="No participants yet!" Visible="False"></asp:Label>
                <asp:Repeater ID="RepeaterParticipants" runat="server">
                    <ItemTemplate>
                        <div class="nMessagesPicture nEVMessagesPicture">
                            <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                        </div>
                        <asp:HyperLink ID="HyperLinkParticipant" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("UserId") %>' CssClass="nEVparticipantName nNoDecoration"><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="nParticipantMap nTabContents hidden">Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map Map</div>
        </div>
    </div>
    <div class="hiddenButtons">
        <asp:Button ID="ButtonActionYes" runat="server" Text="Join" OnClick="ButtonActionYes_Click" />
        <asp:Button ID="ButtonActionNo" runat="server" Text="Not Interested" OnClick="ButtonActionNo_Click" />
    </div>
    <div class="nExploreFooter">
        <div class="nFooterButton nNotButton"><i class="fa fa-close"></i>&nbsp NOT INTERESTED</div>
        <div class="nFooterButton nRequestButton"><i class="fa fa-check"></i>&nbsp SEND REQUEST</div>
    </div>
    <asp:HiddenField ID="HiddenFieldOwnerId" runat="server" />
    <asp:HiddenField ID="HiddenFieldDate" runat="server" />
    <asp:HiddenField ID="HiddenFieldOwnerRateScore" runat="server" />
    <asp:HiddenField ID="HiddenFieldOwnerPhotoUrl" runat="server" />
    <asp:HiddenField ID="HiddenFieldUsername" runat="server" />
    <asp:HiddenField ID="HiddenFieldOwnerRateCount" runat="server" />
    <asp:HiddenField ID="HiddenFieldEventPhotoUrl" runat="server" />
    <asp:HiddenField ID="HiddenFieldTypeId" runat="server" />
    <asp:HiddenField ID="HiddenFieldCoverId" runat="server" />
    <asp:HiddenField ID="HiddenFieldDuration" runat="server" />

    <div class="invisible">
        <asp:Label ID="LabelParticipants" runat="server"></asp:Label>
        <asp:HyperLink ID="HyperLinkModify" runat="server" Visible="False">Modify event</asp:HyperLink>
        <asp:Label ID="LabelPriceFrom" runat="server"></asp:Label>
        <asp:Label ID="LabelPriceTo" runat="server"></asp:Label>
        <asp:Label ID="LabelCurrency" runat="server"></asp:Label>
        <asp:Label ID="LabelLanguages" runat="server"></asp:Label>
        <asp:Image ID="ImageMood" runat="server" />
        <asp:Label ID="LabelStatus" runat="server"></asp:Label>
        <asp:Button ID="ButtonBookmark" runat="server" OnClick="ButtonBookmark_Click" Text="Bookmark" />
        <asp:Label ID="LabelMessage" runat="server"></asp:Label>
    </div>
</asp:Content>
