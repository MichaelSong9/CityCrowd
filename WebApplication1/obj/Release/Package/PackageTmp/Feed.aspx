<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Feed.aspx.cs" Inherits="WebApplication1.Feed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="RepeaterCity" runat="server">
        <ItemTemplate>
            <asp:HiddenField ID="HiddenFieldEventId" runat="server" Value='<%# Eval("EventId") %>' />
            <asp:HiddenField ID="HiddenFieldName" runat="server" Value='<%# Eval("Name") %>' />
            <asp:HiddenField ID="HiddenFieldDate" runat="server" Value='<%# Eval("Date") %>' />
            <asp:HiddenField ID="HiddenFieldTypeId" runat="server" Value='<%# Eval("TypeId") %>' />
            <asp:HiddenField ID="HiddenFieldCoverId" runat="server" Value='<%# Eval("CoverId") %>' />
            <asp:HiddenField ID="HiddenFieldUserId" runat="server" Value='<%# Eval("UserId") %>' />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="RepeaterFollowing" runat="server">
        <ItemTemplate>
            <asp:HiddenField ID="HiddenFieldEventId" runat="server" Value='<%# Eval("EventId") %>' />
            <asp:HiddenField ID="HiddenFieldName" runat="server" Value='<%# Eval("Name") %>' />
            <asp:HiddenField ID="HiddenFieldDate" runat="server" Value='<%# Eval("Date") %>' />
            <asp:HiddenField ID="HiddenFieldTypeId" runat="server" Value='<%# Eval("TypeId") %>' />
            <asp:HiddenField ID="HiddenFieldCoverId" runat="server" Value='<%# Eval("CoverId") %>' />
            <asp:HiddenField ID="HiddenFieldUserId" runat="server" Value='<%# Eval("UserId") %>' />
            <asp:HiddenField ID="HiddenFieldUserFullName" runat="server" Value='<%# Eval("UserFullName") %>' />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="RepeaterMap" runat="server">
        <ItemTemplate>

        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
