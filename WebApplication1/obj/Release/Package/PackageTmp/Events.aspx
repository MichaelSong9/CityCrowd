<%@ Page Title="Events" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="WebApplication1.Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            events();
        });
    </script>
    <script src="../../JS/script.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Events</div>
        <div class="nMenuAdd"><div class="nMenuAddIcon">+</div></div>
    </div>

    <div class="nTabbedContent">
        <div class="nTabsHolder">
            <div class="nTabs nExploreSelected" ntabnumber="1">
                <b>
                    <asp:HyperLink ID="HyperLinkCreated" runat="server" NavigateUrl="~/Events/Mode/Created">CREATED</asp:HyperLink></b>
            </div>
            <div class="nTabs reviewButton" ntabnumber="2">
                <b>
                    <asp:HyperLink ID="HyperLinkAccepted" runat="server" NavigateUrl="~/Events/Mode/Accepted">ACCEPTED</asp:HyperLink></b>
            </div>
            <div class="nTabs" ntabnumber="3">
                <b>
                    <asp:HyperLink ID="HyperLinkRequested" runat="server" NavigateUrl="~/Events/Mode/Requested">REQUESTED</asp:HyperLink></b>
            </div>
            <div class="nTabs" ntabnumber="4">
                <b>BOOKMARK</b>
            </div>
        </div>
        <div class="nExploreContentHolder">
            <asp:Label ID="LabelTitle" runat="server"></asp:Label>
            <asp:Repeater ID="RepeaterEvents" runat="server">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEvent" runat="server" NavigateUrl='<%# "~/Events/"+ Eval("EventId") %>'><%# Eval("Name") %></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <asp:Label ID="LabelNoRecord" runat="server" Text="There is no event!" Visible="False"></asp:Label>
        </div>
    </div>
   
</asp:Content>
