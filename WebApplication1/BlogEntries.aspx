<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageUser.Master" CodeBehind="BlogEntries.aspx.cs" Inherits="WebApplication1.BlogEntries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Blog</title>
    <script src="../../JS/script.js"></script>
    <script>
        $(document).ready(function () {
            blog();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Blog</div>
        <div class="nMenuAdd">+</div>
    </div>
    <!-- header end -->
    <div class="nBlogMainContainer">
        <div class="nBlogTitle">
            <asp:Label ID="LabelTitle" runat="server"></asp:Label>
        </div>
        <div class="nBlogTime">
            <div class="nBlogClock"></div>
            <asp:Label ID="LabelDate" runat="server"></asp:Label>
        </div>
        <div class="clear"></div>
        <div class="nBlogBody">
            <asp:Literal ID="LiteralBody" runat="server"></asp:Literal>
        </div>
        <hr />
        <div class="hidden">
        </div>
    </div>
    <!--
    
        <div>
            <asp:Label ID="Label1" runat="server" Text="Blog"></asp:Label>
            <asp:Image ID="Image4" runat="server" Height="22px"
                ImageUrl="~/Images/Icons/right22.png" Width="22px" />
            <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="~/Blog">Back to blog&#39;s home</asp:HyperLink>
            <br />
            
            <br />
            <asp:Label ID="LabelCat0" runat="server" Text="Submit Date:"></asp:Label>
            
            <br />
            
            <asp:Panel ID="PanelNext" runat="server" Visible="False">
                <asp:Image ID="Image12" runat="server" Height="22px"
                    ImageUrl="~/images/icons/right22.png" Width="22px" />
                <asp:HyperLink ID="HyperLinkNext" runat="server">[HyperLinkPervious]</asp:HyperLink>
            </asp:Panel>
            <asp:Panel ID="PanelPervious" runat="server" Visible="False">
                <asp:HyperLink ID="HyperLinkPervious" runat="server">[HyperLinkPervious]</asp:HyperLink>
                <asp:Image ID="Image11" runat="server" Height="22px"
                    ImageUrl="~/images/icons/left22.png" Width="22px" />
            </asp:Panel>
        </div>
    </form>
    -->
    <div class="nExploreFooter">
        <div class="nFooterButton nProfileMessageButton">BACK TO BLOG</div>
    </div>
</asp:Content>


