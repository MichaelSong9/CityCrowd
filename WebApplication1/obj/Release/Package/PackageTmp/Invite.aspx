<%@ Page Title="Invite" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Invite.aspx.cs" Inherits="WebApplication1.Invite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LabelEmail" runat="server" AssociatedControlID="TextBoxEmail" Text="Email:"></asp:Label>
    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelName" runat="server" AssociatedControlID="TextBoxName" Text="Name:"></asp:Label>
    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelMessage" runat="server" AssociatedControlID="TextBoxMessage" Text="Message:"></asp:Label>
    <asp:TextBox ID="TextBoxMessage" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Send" />
    <br />
    <br />
    <asp:Label ID="LabelStatus" runat="server" Visible="False"></asp:Label>
</asp:Content>
