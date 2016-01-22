<%@ Page Title="Completion" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Completion.aspx.cs" Inherits="WebApplication1.Completion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            completion();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nCompletionHeader">
        You are only seconds away of completing your profile :-)
    </div>
    <div class="nCompletionCircle">
        <div class="nCompletionPhotoIcon"></div>
        <div class="nCompletionPhotoText">Add Photo</div>
    </div>
    <div class="nCompletionBox">
        <div class="nCompletionBoxFirst nCompletionBoxFirstFN">
            <div class="nCompletionIcon nCompletionIconFN"></div>
            <div class="nCompletionName">First Name*</div>
            <div class="nCompletionStatus"></div>
        </div>
        <div class="nCompletionBoxSecond invisible">
            <div class="nCompletionIcon nCompletionIconFN"></div>
            <div class="nCompletionName">First Name*</div>
            <div class="nCompletionArrowFN nSIRowEnterArrow nCompletionArrow nValidatorButton"></div>
            <div class="nCompletionInput">
                <asp:TextBox ID="TextBoxFirstName" runat="server" CssClass="nCompletionTextInput" data-role="none"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="nCompletionValidationErrorMessage invisible nCompletionFNError">Please enter a valid first name</div>
    <div class="nCompletionBox">
        <div class="nCompletionBoxFirst nCompletionBoxFirstLN">
            <div class="nCompletionIcon nCompletionIconLN"></div>
            <div class="nCompletionName">Last Name*</div>
            <div class="nCompletionStatus"></div>
        </div>
        <div class="nCompletionBoxSecond invisible">
            <div class="nCompletionIcon nCompletionIconFN"></div>
            <div class="nCompletionName">Last Name*</div>
            <div class="nCompletionArrowLN nSIRowEnterArrow nCompletionArrow nValidatorButton "></div>
            <div class="nCompletionInput">
                <asp:TextBox ID="TextBoxLastName" runat="server" CssClass="nCompletionTextInput" data-role="none"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="nCompletionValidationErrorMessage invisible nCompletionLNError">Please enter a valid last name</div>
    <div class="nCompletionBox">
        <div class="nCompletionBoxFirst nCompletionBoxFirstUN">
            <div class="nCompletionIcon nCompletionIconUN"></div>
            <div class="nCompletionName">Username*</div>
            <div class="nCompletionStatus"></div>
        </div>
        <div class="nCompletionBoxSecond invisible">
            <div class="nCompletionIcon nCompletionIconUN"></div>
            <div class="nCompletionName">Username*</div>
            <div class="nCompletionArrowUN nSIRowEnterArrow nCompletionArrow nValidatorButton  "></div>
            <div class="nCompletionInput">
                <asp:TextBox ID="TextBoxUsername" CssClass="nCompletionTextInput" runat="server" data-role="none"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="nCompletionValidationErrorMessage invisible nCompletionUNError">Please enter a valid username</div>
    <div class="nCompletionBox">
        <div class="nCompletionBoxFirst nCompletionBoxFirstGender">
            <div class="nCompletionIcon nCompletionIconGender"></div>
            <div class="nCompletionName">Gender*</div>
            <div class="nCompletionStatus"></div>
        </div>
        <div class="nCompletionBoxSecond invisible">
            <div class="nCompletionIcon nCompletionIconGender"></div>
            <div class="nCompletionName">Gender*</div>
            <div class="nCompletionArrowGender nSIRowEnterArrow nCompletionArrow nValidatorButton"></div>
            <div class="nCompletionDropdown">
                <asp:DropDownList ID="DropDownListGender" runat="server" data-role="none">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="2">Female</asp:ListItem>
                    <asp:ListItem Value="1">Male</asp:ListItem>
                    <asp:ListItem Value="3">Other</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="nCompletionValidationErrorMessage invisible nCompletionGenderError">Please choose your gender</div>
    <div class="nCompletionBox">
        <div class="nCompletionBoxFirst nCompletionBoxFirstBD">
            <div class="nCompletionIcon nCompletionIconBD"></div>
            <div class="nCompletionName">Birth Date*</div>
            <div class="nCompletionStatus"></div>
        </div>
        <div class="nCompletionBoxSecond invisible">
            <div class="nCompletionIcon nCompletionIconBD"></div>
            <div class="nCompletionName">Birth Date*</div>
            <div class="nCompletionArrowBD nSIRowEnterArrow nCompletionArrow nValidatorButton"></div>
            <div class="nCompletionInput">
                <input type="date" name="date" id="date" value="" data-role="none" class="nCompletionDateInput" />
            </div>
        </div>
    </div>
    <div class="nCompletionValidationErrorMessage invisible nCompletionBDError">Please choose your birthdate</div>
    <div class="nCompletionBox">
        <div class="nCompletionBoxFirst nCompletionBoxFirstLocation nCompletionBoxFirstLocation">
            <div class="nCompletionIcon nCompletionIconLocation"></div>
            <div class="nCompletionName">Location*</div>
            <div class="nCompletionStatus"></div>
        </div>
        <div class="nCompletionBoxSecond nCompletionBoxSecondLocation invisible">
            <div class="nCompletionIcon nCompletionIconLocation"></div>
            <div class="nCompletionName">Location*</div>
            <div class="nCompletionArrowLocation nSIRowEnterArrow nCompletionArrow nValidatorButton"></div>
            <div class="nCompletionLocationContainer">
                <asp:DropDownList ID="DropDownListCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" data-role="none" CssClass="nCompletionLocationInput nCompletionLocationInputCountry">
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="DropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged" data-role="none" CssClass="nCompletionLocationInput">
                    <asp:ListItem Value="0">Select State</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="DropDownListCity" runat="server" data-role="none" CssClass="nCompletionLocationInput nCompletionLocationLastInput">
                    <asp:ListItem Value="0">Select City</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="nCompletionValidationErrorMessage invisible nCompletionLocationError">Please choose your location</div>
    <br />
    <div class="nCompletionErrorMessage"></div>
    <div class="nExploreFooter">
        <div class="nFooterButton nProfileMessageButton nValidatorButton">&nbsp CONTINUE</div>
    </div>
    <div class="invisible">
        <asp:HiddenField ID="HiddenFieldDOB" runat="server" />
        <asp:Button ID="ButtonSubmit" runat="server" Text="Save Changes" OnClick="ButtonSubmit_Click" />
    </div>
    <!--<asp:Label ID="LabelFirstName" runat="server" Text="First Name:" AssociatedControlID="TextBoxFirstName"></asp:Label>
    <asp:Label ID="LabelLastName" runat="server" AssociatedControlID="TextBoxLastName" Text="Last Name:"></asp:Label>
    <asp:Label ID="LabelUsername" runat="server" AssociatedControlID="TextBoxUsername" Text="Username:"></asp:Label>
    <asp:Label ID="LabelGender" runat="server" AssociatedControlID="DropDownListGender" Text="Gender:"></asp:Label>
    <asp:Label ID="LabelBirthdate" runat="server" Text="Birthdate:"></asp:Label>
    <asp:Label ID="LabelLocation" runat="server" Text="Current Location:"></asp:Label>
    -->
</asp:Content>
