<%@ Page Title="Review" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="WebApplication1.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LabelEventName" runat="server"></asp:Label>
    <br />
    <asp:HiddenField ID="HiddenFieldUserPhotoUrl" runat="server" />
    <asp:HiddenField ID="HiddenFieldTypeId" runat="server" />
    <asp:HiddenField ID="HiddenFieldCoverId" runat="server" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Hi!"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Recently you went to an event with"></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLinkUser" runat="server">[HyperLinkUser]</asp:HyperLink>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Can you please take your time and review your experience with him/her."></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Any comment to write?"></asp:Label>
    <br />
    <asp:TextBox ID="TextBoxComment" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Submit" />
    <asp:HiddenField ID="HiddenFieldRate" runat="server" Value="5" />
    <asp:Button ID="ButtonRefuse" runat="server" OnClick="ButtonRefuse_Click" Text="No Thanks" />
</asp:Content>
