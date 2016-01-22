<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admins.ascx.cs" Inherits="WebApplication1.AdminPages.Admins" %>
<asp:GridView ID="GridViewAdmins" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="UserId" DataSourceID="SqlDataSourceAdmins" OnSelectedIndexChanged="GridViewAdmins_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="UserId" HeaderText="User Id" SortExpression="UserId"></asp:BoundField>
        <asp:TemplateField HeaderText="Permissions">
            <ItemTemplate>
                <asp:Image ID="ImagePermAdmins" runat="server" Height="24px" ImageUrl='<%# Eval("PermAdmins", "~/Images/Admin/Premissions/Admins{0}.png") %>' ToolTip="Admins" Width="24px" />
                <asp:Image ID="ImagePermBlog" runat="server" Height="24px" ImageUrl='<%# Eval("PermBlog", "~/Images/Admin/Premissions/Blog{0}.png") %>' ToolTip="Blog" Width="24px" />
                <asp:Image ID="ImagePermComments" runat="server" Height="24px" ImageUrl='<%# Eval("PermComments", "~/Images/Admin/Premissions/Comments{0}.png") %>' ToolTip="Comments" Width="24px" />
                <asp:Image ID="ImagPermContent" runat="server" Height="24px" ImageUrl='<%# Eval("PermContent", "~/Images/Admin/Premissions/Content{0}.png") %>' ToolTip="Content" Width="24px" />
                <asp:Image ID="ImagePermEvents" runat="server" Height="24px" ImageUrl='<%# Eval("PermEvents", "~/Images/Admin/Premissions/Events{0}.png") %>' ToolTip="Events" Width="24px" />
                <asp:Image ID="ImagePermLocations" runat="server" Height="24px" ImageUrl='<%# Eval("PermLocations", "~/Images/Admin/Premissions/Locations{0}.png") %>' ToolTip="Locations" Width="24px" />
                <asp:Image ID="ImagePermNewsletter" runat="server" Height="24px" ImageUrl='<%# Eval("PermNewsletter", "~/Images/Admin/Premissions/Newsletter{0}.png") %>' ToolTip="Newsletter" Width="24px" />
                <asp:Image ID="ImagePermSettings" runat="server" Height="24px" ImageUrl='<%# Eval("PermSettings", "~/Images/Admin/Premissions/Settings{0}.png") %>' ToolTip="Settings" Width="24px" />
                <asp:Image ID="ImagePermStats" runat="server" Height="24px" ImageUrl='<%# Eval("PermStats", "~/Images/Admin/Premissions/Stats{0}.png") %>' ToolTip="Stats" Width="24px" />
                <asp:Image ID="ImagePermUsers" runat="server" Height="24px" ImageUrl='<%# Eval("PermUsers", "~/Images/Admin/Premissions/Users{0}.png") %>' ToolTip="Users" Width="24px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ImageField DataImageUrlField="Status" DataImageUrlFormatString="~/Images/Admin/Premissions/status{0}.png" HeaderText="Status" SortExpression="Status">
        </asp:ImageField>
        <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceAdmins" runat="server" ConnectionString="<%$ ConnectionStrings:AppConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="sp_admins" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
<br />
<asp:Panel ID="PanelEdit" runat="server" Visible="False">
    <table>
        <tr>
            <td>
                <asp:Label ID="LabelUserId0" runat="server" CssClass="FormLabel" Text="User Id:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelEditUserId" runat="server" CssClass="FormLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelPremissions0" runat="server" AssociatedControlID="CheckBoxListPremissions" CssClass="FormLabel" Text="Permissions:"></asp:Label>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxListEditPremissions" runat="server" CssClass="FormLabel">
                    <asp:ListItem Value="Admins">Admins</asp:ListItem>
                    <asp:ListItem Value="Blog">Blog</asp:ListItem>
                    <asp:ListItem Value="Comments">Comments</asp:ListItem>
                    <asp:ListItem Value="Content">Content</asp:ListItem>
                    <asp:ListItem Value="Events">Events</asp:ListItem>
                    <asp:ListItem Value="Locations">Locations</asp:ListItem>
                    <asp:ListItem Value="Newsletter">Newsletter</asp:ListItem>
                    <asp:ListItem Value="Settings">Settings</asp:ListItem>
                    <asp:ListItem Value="Statistics">Statistics</asp:ListItem>
                    <asp:ListItem Value="Support">Support</asp:ListItem>
                    <asp:ListItem Value="Users">Users</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="ButtonEdit" runat="server" OnClick="ButtonEdit_Click" ValidationGroup="Add" Text="Edit" />
            </td>
        </tr>
    </table>
    <asp:Label ID="LabelEditMessage" runat="server"></asp:Label>
</asp:Panel>
<asp:Label ID="Label1" runat="server" Text="Add an Admin"></asp:Label>
<br />
<br />
<table>
    <tr>
        <td>
            <asp:Label ID="LabelUserId" runat="server" AssociatedControlID="TextBoxUserId" CssClass="FormLabel" Text="User Id:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxUserId" runat="server" ValidationGroup="Regeant" Width="100px"></asp:TextBox>
            &nbsp;
            </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelPremissions" runat="server" AssociatedControlID="CheckBoxListPremissions" CssClass="FormLabel" Text="Permissions:"></asp:Label>
        </td>
        <td>
            <asp:CheckBoxList ID="CheckBoxListPremissions" runat="server" CssClass="FormLabel">
                <asp:ListItem Value="Admins">Admins</asp:ListItem>
                <asp:ListItem Value="Blog">Blog</asp:ListItem>
                <asp:ListItem Value="Comments">Comments</asp:ListItem>
                <asp:ListItem Value="Content">Content</asp:ListItem>
                <asp:ListItem Value="Events">Events</asp:ListItem>
                <asp:ListItem Value="Locations">Locations</asp:ListItem>
                <asp:ListItem Value="Newsletter">Newsletter</asp:ListItem>
                <asp:ListItem Value="Settings">Settings</asp:ListItem>
                <asp:ListItem Value="Statistics">Statistics</asp:ListItem>
                <asp:ListItem Value="Support">Support</asp:ListItem>
                <asp:ListItem Value="Users">Users</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Add" />
        </td>
    </tr>
</table>
<asp:Label ID="LabelAddMessage" runat="server"></asp:Label>