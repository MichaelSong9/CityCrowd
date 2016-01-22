<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="WebApplication1.Content" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td style="vertical-align:top; width:150px;">
            <asp:Image ID="ImageAbout" runat="server" Height="24" 
                ImageUrl="~/images/icons/information24.png" Width="24" />
            <asp:HyperLink ID="HyperLinkAbout" runat="server" 
                NavigateUrl="~/Content/About">About</asp:HyperLink>
            <br />
            <asp:Image ID="ImageContact" runat="server" Height="24" 
                ImageUrl="~/images/icons/conversation24.png" Width="24" />
            <asp:HyperLink ID="HyperLinkContact" runat="server"
                NavigateUrl="~/Content/Contact">Contact</asp:HyperLink>
            <br />
            <asp:Image ID="ImageRules" runat="server" Height="24" 
                ImageUrl="~/images/icons/rules24.png" Width="24" />
            <asp:HyperLink ID="HyperLinkRules" runat="server"
                NavigateUrl="~/Content/Rules">Terms of use</asp:HyperLink>
            <br />
            <asp:Image ID="ImagePrivacy" runat="server" Height="24" 
                ImageUrl="~/images/icons/panel24.png" Width="24" />
            <asp:HyperLink ID="HyperLinkPrivacy" runat="server"
                NavigateUrl="~/Content/Privacy">Privacy policy</asp:HyperLink>
            <br />
        </td>
        <td>
            <asp:Image ID="ImageIcon" runat="server" Height="48px" Width="48px" />
            <asp:Label ID="LabelTitle" runat="server" CssClass="LabelBig"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="PanelAbout" runat="server" Visible="False" Width="100%">

            </asp:Panel>
            <asp:Panel ID="PanelContact" runat="server" Visible="False" Width="100%">
                <div>
                    
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelPrivacy" runat="server" Visible="False" Width="100%">
                <div>
                   
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelRules" runat="server" Visible="False" Width="100%">
                <div>
                    
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Content>
