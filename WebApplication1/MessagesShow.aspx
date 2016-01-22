<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="MessagesShow.aspx.cs" Inherits="WebApplication1.MessagesShow" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            messagesShow();
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
                <div class="eachMessage">
                    <div class="nMessagesPicture nMessagesShowPicture">
                        <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                    </div>
                    <div class="nMSWhiteDiv1"></div>
                    <div class="nMSWhiteDiv2"></div>
                    <asp:HiddenField ID="HiddenFieldMessageId" runat="server" Value='<%# Eval("MessageId") %>' />
                    <div>
                        
                    </div>
                    <div class="nMSMessage">
                        <span><%# Eval("Message") %></span>
                    </div>
                    
                    <div>
                        <!--<asp:Label ID="LabelUnread" runat="server" Text='<%# Eval("Unread") %>' CssClass="EachMessageUnread ViewClass"></asp:Label> -->
                    </div>
                    <div class="invisible">
                        <asp:Label ID="LabelSender" runat="server" Text='<%# Eval("Sender") %>' CssClass="EachMessageSender"></asp:Label>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="MessageListDate nMSDate">
                    <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("PassedDate") %>' CssClass="EachMessageDate ViewClass"></asp:Label>
                </div>
                <div class="clear"></div>
            </ItemTemplate>
        </asp:Repeater>
            
            <div style="height:6.5em; width:100%"></div>
            <div class="nExploreFooter nMSFooter">
                <div class="nMessageSendArea">
                    <asp:Panel ID="PanelMessage" runat="server">
                        <div class="nMSTextArea">
                            <asp:TextBox ID="TextBoxMessage" runat="server" CssClass="TextBoxMultiR2L" TextMode="MultiLine" data-autogrow="false" autofocus></asp:TextBox>
                        </div>
                        <div class="nMSSendButton">
                            <b>SEND</b>
                        </div>
                        <div class="invisible">
                            <asp:ImageButton ID="ImageButtonSend" runat="server" ImageUrl="~/images/send-button.png" OnClick="ImageButtonSend_Click" ValidationGroup="Send" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="nFooterButton nProfileMessageButton">BACK TO MESSAGES</div>
            </div>
    <!--
        <asp:HiddenField ID="HiddenFieldOwnerName" runat="server" />
        <asp:HiddenField ID="HiddenFieldOtherName" runat="server" />    
    -->
</asp:Content>
