<%@ Page Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="WebApplication1.Blog" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Blog</title>
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
        <asp:Repeater ID="RepeaterBlog" runat="server">
            <ItemTemplate>
                <div class="nBlogTitle">
                    <asp:HyperLink ID="HyperLinkTitle" runat="server" NavigateUrl='<%# "~/Blog/"+ Eval("EntryId") + "/" + Eval("BrowserTitle") %>'
                        Text='<%# Eval("Title") %>'></asp:HyperLink>
                </div>
                <div class="nBlogTime">
                    <div class="nSmallIcon nBlogClock"></div>
                    <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("SubmitDate") %>'></asp:Label>
                </div>
                <div class="clear"></div>
                <div class="nBlogBody">
                    <asp:Literal ID="LiteralBody" runat="server" Text='<%# Eval("Body") %>'></asp:Literal>
                </div>
                <hr />
                <!-- <asp:HyperLink ID="HyperLinkPermalink" runat="server" NavigateUrl='<%# "~/Blog/"+ Eval("EntryId") + "/" + Eval("BrowserTitle") %>'>Permalink</asp:HyperLink> -->
            </ItemTemplate>
        </asp:Repeater>
        <div class="hidden">
            <asp:Label ID="Label1" runat="server" CssClass="LabelNormal" Font-Bold="True"
                Text="Blog"></asp:Label>
        </div>
    </div>

</asp:Content>


