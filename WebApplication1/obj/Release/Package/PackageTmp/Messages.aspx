<%@ Page Title="Messages" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="WebApplication1.Messages" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            messages();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Messages</div>
        <div class="nMenuAdd">+</div>
    </div>

    <asp:Repeater ID="RepeaterMessages" runat="server">
        <ItemTemplate>
            <div class="fullNotificationContainer">
                <div class="nMessagesPicture" style="background-image: url('Images/artistic.png')">
                </div>
                <div class="nMessagesInsideDiv">
                    <div class="MessageSender">
                        <asp:HyperLink ID="HyperLinkSubject" runat="server" NavigateUrl='<%#"~/Messages/"+ Eval("OtherId") %>'><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                    </div>
                    <br />
                    <div class="MessageBody">
                        <asp:Label ID="LabelBeriefBody" runat="server" Text='<%# Eval("Brief", "{0}...") %>'></asp:Label>
                    </div>

                    <div class="MessageListDate">
                        <div class="nSmallIcon nBlogClock nMessagesClock"></div>
                        <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("PassedDate") %>'></asp:Label>
                    </div>
                </div>
                <div class="nMessagesNumberContainer hidden">
                    <div class="nMessagesUnreadNumber">
                        <asp:Label ID="LabelNewCount" runat="server" CssClass="nMessagesNew" Text='<%# Eval("NewCount") %>'></asp:Label>
                    </div>
                </div>
                <div class="clear"></div>
                <hr class="nMessagesHr" />
                <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />

                <div class="invisible">
                    <asp:Image ID="ImagePhoto" runat="server" Height="60px" ImageUrl='<%# Eval("OtherId") %>' Width="60px" />
                    <asp:Label ID="LabelUnread" runat="server" CssClass="nMessagesNew" Text='<%# Eval("Unread") %>'></asp:Label>
                </div>
            </div>

        </ItemTemplate>
    </asp:Repeater>
    <asp:Label ID="LabelNoRecord" runat="server" Text="You have no messages!" Visible="False"></asp:Label>


    <div class="nExploreFooter">
        <div class="nFooterButton nProfileMessageButton">WRITE A MESSAGE</div>
    </div>
</asp:Content>
