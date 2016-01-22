<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebApplication1.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            errorPage();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Error</div>
        <div class="nMenuAdd">+</div>
    </div>
    <!-- header end -->
    <div class="nMiddleWindow">
        <div class="nErrorImage"></div>
        <div class="nErrorMessage">
            <asp:Image ID="Image1" runat="server" />
            <b>
                <asp:Label ID="LabelMessage" runat="server" Text="Label"></asp:Label></b>
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server">Click here to report it.</asp:HyperLink>
        </div>
    </div>
    <div class="nExploreFooter">
        <div class="nFooterButton nProfileMessageButton">GO TO EXPLORE</div>
    </div>
</asp:Content>
