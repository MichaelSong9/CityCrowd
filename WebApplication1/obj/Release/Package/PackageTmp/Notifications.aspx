<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="WebApplication1.Notifications" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            notifications();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Notifications</div>
        <div class="nMenuAdd">+</div>
    </div>

    <asp:Repeater ID="RepeaterNotifications" runat="server">
        <ItemTemplate>
            <div class="fullNotificationContainer">
                <div class="nMessagesPicture nNotificationPicture" style="background-image: url('Images/artistic.png')">
                </div>
                <div class="nMessagesInsideDiv">
                    <div class="MessageBody">
                        <asp:Label ID="LabelText" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                    </div>

                    <div class="MessageListDate">
                        <div class="nSmallIcon nBlogClock nMessagesClock"></div>
                        <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("SubmitDate") %>'></asp:Label>
                    </div>
                </div>
                <div class="clear"></div>
                <hr class="nMessagesHr nNotificationHr" />
                <div class="invisible">
                    <asp:Label ID="LabelImage" runat="server" Text='<%# "Images/Notifications/"+ Eval("NotificationType") +".jpg" %>'></asp:Label>
                    <asp:Label ID="LabelUnread" CssClass="nNotificationUnread" runat="server" Text='<%# Eval("Unread") %>'></asp:Label>
                    <asp:Button ID="ButtonDelete" runat="server" Text="" CommandName="Delete" CommandArgument='<%#Eval("NotificationId") %>' />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
