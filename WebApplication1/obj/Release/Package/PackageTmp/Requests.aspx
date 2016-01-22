<%@ Page Title="Requests" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="WebApplication1.Requests" EnableEventValidation="false" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            requestsPage();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Requests</div>
        <div class="nMenuAdd">+</div>
    </div>

    <div class="nTabbedContent">
        <div class="nTabsHolder">
            <div class="nTabs nTabs2 nExploreSelected" ntabnumber="1">
                <b>REQUESTS</b>
            </div>
            <div class="nTabs nTabs2 reviewButton" ntabnumber="2">
                <b>INVITATIONS</b>
            </div>
        </div>
        <div class="nExploreContentHolder nRequestsContentHolder">
            <!-- requests-->
            <div class="nDescriptionContent nTabContents">
                <asp:Repeater ID="RepeaterRequests" runat="server">
                    <%-- 
                        Requests/{RequestId}/{ActionCode}
                        action code 1 for accept
                        action code 2 for reject
                    --%>
                    <ItemTemplate>
                        <div class="nEachRequest">
                            
                            <div class="nContentTitle">
                                <asp:HyperLink ID="HyperLinkEvent" runat="server" NavigateUrl='<%# "~/Events/"+ Eval("EventId") %>'><%# Eval("EventName") %></asp:HyperLink>
                            </div>
                            <div class="clear"></div>
                            <div class="nTimeAndLocation">
                                <div class="nSmallIcon nBlogClock nMessagesClock"></div>
                                <asp:Label ID="LabelRemainedTime" runat="server" Text='<%# Eval("RemainedTime") %>' CssClass="nRequestRemainedTime requestsUserInfo" />
                            </div>
                            <div class="nTimeAndLocation">
                                <div class="nSmallIcon nRequestsPerson nMessagesClock"></div>
                                <span>Needs <asp:Label ID="LabelParticipantsRemained" runat="server" Text='<%# Eval("ParticipantsRemained") %>' /> participants</span>
                            </div>
                            <br />
                            <div class="nContentCover nProfileCover">
                                <div class="arrow-right nRequestArrowRight">
                                    <div class="nVerified"></div>
                                </div>
                                <div class="nRequestUserInfo nRight strokedText">
                                    @ name &nbsp|&nbsp
                                    <asp:HyperLink ID="HyperLinkName" runat="server" NavigateUrl='<%# "~/Profile/"+ Eval("Username") %>'><%# Eval("FullName") %></asp:HyperLink>
                                </div>
                                <div class="nRoundPicture nProfilePicture  nRequestPicture">
                                    <canvas class="nRequestCanvas" width="115" height="115"></canvas>
                                    <div class="nCountryFlag nRequestCountryFlag">
                                    </div>
                                    <div class="strokedText nLeft nRequestsLocation">
                                        <h5>
                                            <asp:Label ID="LabelCity" runat="server" Text='<%# Eval("City") %>' CssClass="LabelCity requestsUserInfo" /></h5>
                                    </div>

                                </div>
                                <div class="nRequestMessage">
                                    <asp:Label ID="LabelMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                    <span>sdfadfasdfadfasdfadfasdfadfasdfa  </span>
                                    <div class="nRequestMessageDecoration"></div>

                                </div>
                                <div class="nRequestFooter">
                                    <div class="nFooterButton nRequestButton "><i class="fa fa-check"></i>&nbsp ACCEPT</div>
                                    <div class="nFooterButton nNotButton nRequestFooterSecondButton"><i class="fa fa-close"></i>&nbsp REJECT</div>
                                </div>
                            </div>
                            <div class="clear"></div>

                            <div class="invisible">
                                <asp:Label ID="LabelProfilePicUrl" runat="server" Text='<%# Eval("ProfilePicUrl") %>'></asp:Label>
                                <asp:Label ID="LabelRate" runat="server" Text='<%# Eval("Rate") %>' CssClass="LabelRequestRate requestsUserInfo"></asp:Label>
                                <asp:Label ID="LabelRequestId" runat="server" Text='<%# Eval("RequestId") %>' CssClass="LabelRequestId requestsUserInfo" />
                                <asp:Label ID="LabelSenderId" runat="server" CssClass="requestSenderId" Text='<%# Eval("SenderId") %>' />
                                
                                <asp:Label ID="LabelGender" runat="server" Text='<%# Eval("Gender") %>' CssClass="LabelGender requestsUserInfo" />
                                <asp:Label ID="LabelAge" runat="server" Text='<%# Eval("Age") %>' CssClass="LabelAge requestsUserInfo" />
                                
                            </div>
                        </div>
                        <hr class="nRequestsHr" />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="LabelNoRecord" runat="server" Text="You have no requests!" Visible="False"></asp:Label>
            </div>
            <!-- invitations -->
            <div class="nBoardContent nTabContents hidden">
                second
            </div>

        </div>
    </div>


</asp:Content>
