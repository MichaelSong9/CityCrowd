<%@ Page Title="Introduction" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Introduction.aspx.cs" Inherits="WebApplication1.Introduction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader invisible">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Profile</div>
        <div class="nMenuAdd">+</div>
    </div>
    <div class="nIntroProgress">
        <div class="nOrangeEllipse"></div>
        <div class="nYellowEllipse"></div>
        <div class="nYellowEllipse"></div>
        <div class="nYellowEllipse"></div>
        <div class="nYellowEllipse"></div>
    </div>
    <div class="nIntroTitle">
        <h1>ADDING EVENT</h1>
    </div>
    <br />
    <div class="nIntroImage"></div>
    <br />
    <div class="nIntroDesc">description</div>

    <div class="nExploreFooter">
        <div class="nFooterButton nProfileMessageButton">CONTINUE</div>
    </div>
    <!-- introduction link-->
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Home.aspx">Skip</asp:HyperLink>
</asp:Content>
