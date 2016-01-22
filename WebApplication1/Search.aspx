<%@ Page Title="Search" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebApplication1.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
    <script>
        $(document).ready(function () {
            messages();
            search();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Search</div>
        <div class="nMenuAdd">+</div>
    </div>

    <div class="nTabbedContent">
        <div class="nTabsHolder">
            <div class="nTabs nExploreSelected nTabs3" ntabnumber="1">
                <b>TAG</b>
            </div>
            <div class="nTabs reviewButton nTabs3" ntabnumber="2">
                <b>FILTER</b>
            </div>
            <div class="nTabs nTabs3" ntabnumber="3">
                <b>PEOPLE</b>
            </div>
        </div>
        <div class="nExploreContentHolder">
            <!-- tag -->
            <div class="nDescriptionContent nTabContents">
                <div class="nSmallIcon nMarkerIcon"></div>
                <div class="nSearchLocation">Finland Helsinki </div>
                <div class="nRight nSearchLocationLink"><a href="#">change location</a></div>
                <div class="clear"></div>
                <div class="nLeft nSearchSearchBox">
                    <asp:TextBox ID="TextBoxTag" runat="server" data-mini="true"></asp:TextBox></div>
                <div class="nRight nSearchButton nSearchTagButton"></div>
            </div>
            <!-- filter -->
            <div class="nBoardContent nTabContents hidden">
                <div class="nSmallIcon nMarkerIcon"></div>
                <div class="nSearchLocation">Finland Helsinki </div>
                <div class="nRight nSearchLocationLink"><a href="#">change location</a></div>
                <div class="clear"></div>
                <div class="nSearchLogos">
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityTheater" data="Theater"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityBook" data="Book"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityDance" data="Entertainment"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityRestaurant" data="Restaurant"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityNature" data="Nature"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityShopping" data="Shopping"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivitySport" data="Sport"></div>
                    </div>
                    <div class="nSearchLogoContainer">
                        <div class="nSearchLogo nActivityMore" data="More"></div>
                    </div>
                </div>
                <div class="nMiddle nSearchButton nSearchFilterButton"></div>
                <div class="clear"></div>
            </div>
            <!-- people -->
            <div class="nParticipantsContent nTabContents hidden">
                <div class="nLeft nSearchSearchBox">
                    <asp:TextBox ID="TextBoxUsername" runat="server" data-mini="true"></asp:TextBox>
                </div>
                <div class="nRight nSearchButton nSearchTagButton"></div>
            </div>
            <div class="nSearchResults">
                <asp:Label ID="LabelMessage" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Panel ID="PanelResult" runat="server" Visible="False">
                    <asp:Repeater ID="RepeaterResult" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="LabelImage0" runat="server" Text='<%# Eval("EventId") %>'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("RemainedDate") %>'></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("ParticipantsRemained") %>'></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("MoodId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="PanelUsername" runat="server" Visible="False">
                    <asp:Repeater ID="RepeaterUsername" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="LabelImage" runat="server" Text='<%# Eval("ProfilePicUrl") %>'></asp:Label>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Profile/"+ Eval("Username") %>'><%# Eval("Username") %></asp:HyperLink>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
            <div class="hidden">
                <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" />
            </div>
        </div>
    </div>



    <!--
    <asp:Label ID="LabelTag" runat="server" Text="Tag:"></asp:Label>
    <br />
    
    <br />
    <asp:DropDownList ID="DropDownListMood" runat="server">
        <asp:ListItem Value="0">Select Mood</asp:ListItem>
        <asp:ListItem>Geek/Nerd</asp:ListItem>
        <asp:ListItem>Naughty</asp:ListItem>
        <asp:ListItem>Fun</asp:ListItem>
        <asp:ListItem>Romantic</asp:ListItem>
        <asp:ListItem>Artistic</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:DropDownList ID="DropDownListDate" runat="server">
        <asp:ListItem Value="0">Select When</asp:ListItem>
        <asp:ListItem Value="day">In a day</asp:ListItem>
        <asp:ListItem Value="week">In a week</asp:ListItem>
        <asp:ListItem Value="month">In a month</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Label ID="LabelTag0" runat="server" Text="Location:"></asp:Label>
    <br />
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
    <br />
    <br />
    <asp:Label ID="LabelUsername" runat="server" Text="Username:"></asp:Label>
    <br />
    
    <br />
    <br />
    
    <asp:HiddenField ID="HiddenFieldSearchType" runat="server" Value="2" />
    <asp:HiddenField ID="HiddenFieldLocationId" runat="server" Value="1" />
    <asp:HiddenField ID="HiddenFieldTypeId" runat="server" Value="1" />
    <br />
    
    
    <br />
    
    -->
</asp:Content>
