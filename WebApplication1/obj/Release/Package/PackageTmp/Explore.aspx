<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Explore.aspx.cs" Inherits="WebApplication1.Explore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            generalLook();
            profilePage();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- <ul class="nAll">
        <!-- side panel -->
        <!--<li class="nPanelLi" id="nMenuPanelLi">

            <div class="nPanelBox" id="nPanelBox1">
                <div class="nProfilePicture" id="nPanelProfilePicture">
                </div>
                <div class="nName">Natali</div>
                <div class="clear"></div>
            </div>
            <div class="nPanelBox">
                <div class="nPanelSubBox">
                    <div class="nEmailBut nPanelIcon"></div>
                </div>
                <div class="nPanelSubBox">
                    <div class="nNewEventBut nPanelIcon"></div>
                </div>
                <div class="nPanelSubBox">
                    <div class="nPersonBut nPanelIcon"></div>
                </div>
            </div>
            <div class="nPanelBox">
                <div class="nExploreBut nPanelIconInside"></div>
                <div class="nPanelName">EXPLORE</div>
            </div>


            <div class="nPanelBox">
                <div class="nFeedBut nPanelIconInside"></div>
                <div class="nPanelName">FEED</div>

            </div>
            <div class="nPanelBox">
                <div class="nEventsBut nPanelIconInside"></div>
                <div class="nPanelName">
                    EVENTS
                </div>
            </div>
            <div class="nPanelBox">
                <div class="nSearchBut nPanelIconInside"></div>
                <div class="nPanelName">SEARCH</div>
            </div>
            <div class="nPanelBox">
                <div class="nCalendarBut nPanelIconInside"></div>
                <div class="nPanelName">CALENDAR</div>
            </div>
            <div class="nPanelBox">
                <div class="nSettingsBut nPanelIconInside"></div>
                <div class="nPanelName">SETTINGS</div>
            </div>
            <div class="nPanelBox" id="logoutPanel">
                <div class="nLogoutBut nPanelIconInside"></div>
                <div class="nPanelName">LOGOUT</div>
            </div>

            
        </li><li class="nPanelLi" id="nMainPanelLi">
            -->
            <div class="nHeader">
                <div class="nMenuButton ">
                    <div class="nMenuButtonLogo">
                        </div>
                    </div>
                <div class="nPageTitle">Explore</div>
                <div class="nMenuAdd">+</div>
            </div>
            <div class="nContentCover">
                <div class="nContentCoverTransparent">
                    <div class="nContentTitle"><b>Photography in Helsinki</b></div>
                    <div class="nDots"><i class="fa fa-ellipsis-h"></i></div>
                    <div class="clear"></div>
                    <div class="nTimeAndLocation">
                        <div class="nExploreTime"><i class="fa fa-clock-o"></i>&nbsp 18 hours later </div>
                        <div class="nExploreLocation"><i class="fa fa-map-marker"></i>&nbsp Finland - Helsinki</div>
                    </div>
                    <div class="nEventCoverBox">
                        <div class="nRoundPicture nProfilePicture">
                            <canvas id="canvas2" width="115" height="115"></canvas>
                        </div>
                    </div>
                    <div class="nEventCoverBox">
                        <div class="nRoundPicture nActivityPicture">
                        </div>
                    </div>
                    <div class="nEventCoverBox">
                        <div class="nRoundPicture nNewGraph">
                            <div class="nNewGraphTop"></div>
                            <div class="nNewGraphBottom"></div>
                            <div class="nNewGraphText"><b>1</b> participant needed (1/3)</div>
                        </div>
                    </div>
                </div>
            </div>
               
            <div class="clear"></div>
            
                

            <div class="nTabbedContent">
                <div class="nTabsHolder">
                    <div class="nTabs nExploreSelected" ntabnumber="1">
                        <b>DESCRIPTION</b>
                    </div>
                    <div class="nTabs" ntabnumber="2">
                        <b>BOARD</b>
                    </div>
                    <div class="nTabs" ntabnumber="3">
                        <b>PARTICIPANTS</b>
                    </div>
                    <div class="nTabs" ntabnumber="4">
                        <b>MAP</b>
                    </div>
                </div>
                <div class="nExploreContentHolder">
                    <div class="nDescriptionContent nTabContents">Description Description Description Description Description Description Description Description </div>
                    <div class="nBoardContent nTabContents hidden">Board Board Board Board Board Board Board Board Board Board Board Board Board Board Board Board </div>
                    <div class="nParticipantsContent nTabContents hidden">
                        <asp:Label ID="LabelNoRecord" runat="server" Text="No participants yet!" Visible="False"></asp:Label>
                        <asp:Repeater ID="RepeaterParticipants" runat="server">
                            <ItemTemplate>
                                <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                                <asp:HyperLink ID="HyperLinkParticipant" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("UserId") %>'><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="nMapContent nTabContents hidden">Map content </div>
                </div>
            </div>
               <div class="nExploreFooter">
                    <div class="nFooterButton nNotButton"><i class="fa fa-close"></i>&nbsp NOT INTERESTED</div>
                    <div class="nFooterButton nRequestButton"><i class="fa fa-check"></i>&nbsp SEND REQUEST</div>
                </div>
        </li>
        
        <!--
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Events/Add">Add Event</asp:HyperLink>
    <br />
    <asp:Label ID="LabelMessage" runat="server" Visible="False"></asp:Label>
    -->

        <!-- needed info

            like events view

            -->

    </ul>
</asp:Content>