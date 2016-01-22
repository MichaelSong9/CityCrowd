<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).bind("mobileinit", function () {
            $.mobile.ajaxEnabled = false;
        });
    </script>
    <script src="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.js"></script>

    <script src="../JS/script.js"></script>
    <script>
        $(document).ready(function () {
            //login();
            registerPage();
        });
    </script>
    <link rel="stylesheet" href="Styles/style.css" />
</head>
<body>

    <div class="loginMainWindow">
        <div class="nMiddleWindow">
            <form id="form1" runat="server">
                <div class="nLoginLogo"></div>
                <div class="nLoginBox">
                    <asp:TextBox ID="TextBoxEmail" runat="server" placeholder="Email" data-mini="true"></asp:TextBox>
                    <div class="nLoginValidationMessage invisible nRegisterEmailError">Please enter valid email address</div>
                    <asp:TextBox ID="TextBoxPassword1" runat="server" TextMode="Password" placeholder="Password" data-mini="true"></asp:TextBox>
                    <div class="nLoginValidationMessage invisible nRegisterPasswordError">Please enter the password</div>
                    <asp:TextBox ID="TextBoxPassword2" runat="server" TextMode="Password" placeholder="Re-type password" data-mini="true"></asp:TextBox>
                    <div class="nLoginValidationMessage invisible nRegisterRepeatPassError">Please retype the password</div>
                    <div class="nRegisterButton"><b>Register</b></div>
                    <div class="nWrongLogin hidden">
                        <div class="nWrongLoginIcon"></div>
                        <b><asp:Label ID="LabelError" runat="server"></asp:Label></b>
                    </div>
                </div>
                <div class="hidden">
                    <asp:Button ID="ButtonRegister" runat="server" Text="Register" OnClick="ButtonRegister_Click" />
                </div>
                <div class="nExploreFooter nLoginFooter">
                    <div class="nLoginFooterSecondary">
                        <div class="nRegisterLogin nFooterButton2small nFooterButton2SmallRight">Login</div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <!--<asp:Label ID="LabelUsername" runat="server" Text="Email:" AssociatedControlID="TextBoxEmail"></asp:Label>
        <asp:Label ID="LabelPassword" runat="server" Text="Password:" AssociatedControlID="TextBoxPassword1"></asp:Label>
        <asp:Label ID="LabelPassword2" runat="server" Text="Re-type Password:" AssociatedControlID="TextBoxPassword2"></asp:Label>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login">I already have an account</asp:HyperLink>
    -->
</body>
</html>
