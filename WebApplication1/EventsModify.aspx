<%@ Page Title="Modify Event" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="EventsModify.aspx.cs" Inherits="WebApplication1.EventsModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../JS/script.js"></script>
    <script>
        $(document).ready(function () {
            eventsModify();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
        </div>
        <div class="nPageTitle">Modify Event</div>
        <div class="nMenuAdd">+</div>
    </div>

    <!-- first page -->
    <div class="nAEPage hidden">
        <div>
            <div class="nContentTitle nEAContentTitle">Basic Information</div>
            <div class="nEventsAddDots">
                <div class="nOrangeEllipse"></div>
                <div class="nGreyEllipse"></div>
                <div class="nGreyEllipse"></div>
                <div class="nGreyEllipse"></div>
            </div>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft">Type</div>
            <div class="nEAACtivityType nRight"></div>
            <div class="clear"></div>
            <div class="nEAAType">
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityTheater" data="Art / culture" activity-id="1"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityBook" data="Education" activity-id="2"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityDance" data="Entertainment" activity-id="3"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityRestaurant" data="Food/drink" activity-id="4"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityNature" data="Outdoor" activity-id="5"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityShopping" data="Shopping" activity-id="6"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivitySport" data="Sports" activity-id="7"></div>
                </div>
                <div class="nActivityLogoContainer">
                    <div class="nActivityLogo nActivityMore" data="Others" activity-id="8"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <div>Name</div>
            <asp:TextBox ID="TextBoxName" runat="server" data-mini="true" required="required" CssClass="nRedPlaceHolder"></asp:TextBox>
        </div>

        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft">Participants</div>
            <div class="clear"></div>
            <input class="participantsSlider" type="range" name="slider-fill" id="slider-fill" value="1" min="1" max="100" data-highlight="true" />
        </div>
    </div>

    <!-- second page  -->
    <div class="nAEPage hidden">
        <div>
            <div class="nContentTitle nEAContentTitle">Date and time</div>
            <div class="nEventsAddDots">
                <div class="nGreyEllipse"></div>
                <div class="nOrangeEllipse"></div>
                <div class="nGreyEllipse"></div>
                <div class="nGreyEllipse"></div>
            </div>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft">Date</div>
            <div class="clear"></div>
            <!-- m/d/y -->
            <asp:TextBox ID="TextBoxDate" type="date" runat="server" data-mini="true" CssClass="nEAdate"></asp:TextBox>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft nEAeachLable">Time</div>
            <div class="nEATimeMessage"></div>
            <div class="clear"></div>
            <input type="time" name="date" class="nEAtime" data-mini="ture" />
        </div>
    </div>
    <!--third page -->
    <div class="nAEPage ">
        <div>
            <div class="nContentTitle nEAContentTitle">Location</div>
            <div class="nEventsAddDots">
                <div class="nGreyEllipse"></div>
                <div class="nGreyEllipse"></div>
                <div class="nOrangeEllipse"></div>
                <div class="nGreyEllipse"></div>
            </div>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <asp:DropDownList ID="DropDownListCountry" runat="server" AutoPostBack="True" data-mini="true" data-theme="b" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged">
                <asp:ListItem Value="0">Select Country</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListState" runat="server" AutoPostBack="True" data-mini="true" data-theme="b" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged">
                <asp:ListItem Value="0">Select State</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListCity" data-mini="true" runat="server" data-theme="b">
                <asp:ListItem Value="0">Select City</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <!-- Fourth page  -->
    <div class="nAEPage hidden">
        <div>
            <div class="nContentTitle nEAContentTitle">Finalizing</div>
            <div class="nEventsAddDots">
                <div class="nGreyEllipse"></div>
                <div class="nGreyEllipse"></div>
                <div class="nGreyEllipse"></div>
                <div class="nOrangeEllipse"></div>
            </div>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft">Languages</div>
            <div class="nRight nEAlanguagesAll">
                <div class="nEAlanguageButtons nEAlanguageButtonsAdd">
                    <div class="nEAlanguageButtonsPlus">+</div>
                    <div class="clear"></div>
                    <div class="nDotsBox nDotBoxLinks">
                        <div class="nDotsBoxEach" data-language="EN">English</div>
                        <div class="nDotsBoxEach" data-language="FI">Finnish</div>
                        <div class="nDotsBoxEach" data-language="FA">Farsi</div>
                    </div>

                </div>

            </div>
            <div class="clear"></div>
        </div>
        <div class="clear"></div>
        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft">public</div>
            <div class="nRight">
                <asp:DropDownList ID="DropDownListPrivacy" runat="server" data-role="slider" data-mini="true">
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="1" Selected="selected">Yes</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="clear"></div>

            <div class="clear"></div>
        </div>
        <hr />
        <div class="nEAContentHolder">
            <div class="nLeft">Description</div>
            <!-- <div class="nEAACtivityType nRight">1</div> -->
            <div class="clear"></div>
            <asp:TextBox ID="TextBoxDescriptions" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Literal ID="LiteralMessage" runat="server" Visible="False"></asp:Literal>
        </div>
    </div>

    <div class="nExploreFooter">
        <div class="nProfileFooterContainer nProfileFooterContainerOneButton ">
            <div class="nFooterButton nProfileMessageButton nProfileNextButton"><b>&nbsp Next</b></div>
        </div>
        <div class="nProfileFooterContainer nProfileFooterContainerTwoButton  hidden">
            <div class="nFooterButton nNotButton"><b>&nbsp Previous</b></div>
            <div class="nFooterButton nRequestButton nProfileNextButton"><b>&nbsp Next</b></div>
        </div>
    </div>

    <div class="invisible">
        <asp:Panel ID="PanelInfo" runat="server">
            <asp:Label ID="LabelName" runat="server" Text="Event Name:" AssociatedControlID="TextBoxName"></asp:Label>
            <asp:Label ID="LabelDate" runat="server" Text="Date:" AssociatedControlID="TextBoxDate"></asp:Label>
            
            <asp:Label ID="LabelTime" runat="server" AssociatedControlID="DropDownListTimeHour" Text="Time:"></asp:Label>
            <asp:DropDownList ID="DropDownListTimeHour" runat="server">
                <asp:ListItem Value="H">Hour</asp:ListItem>
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
                <asp:ListItem Value="18">18</asp:ListItem>
                <asp:ListItem Value="19">19</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="21">21</asp:ListItem>
                <asp:ListItem Value="22">22</asp:ListItem>
                <asp:ListItem Value="23">23</asp:ListItem>
            </asp:DropDownList>

            <asp:DropDownList ID="DropDownListTimeMinute" runat="server">
                <asp:ListItem Value="M">Minute</asp:ListItem>
                <asp:ListItem Value="0">0</asp:ListItem>
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
                <asp:ListItem Value="18">18</asp:ListItem>
                <asp:ListItem Value="19">19</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="21">21</asp:ListItem>
                <asp:ListItem Value="22">22</asp:ListItem>
                <asp:ListItem Value="23">23</asp:ListItem>
                <asp:ListItem Value="24">24</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="26">26</asp:ListItem>
                <asp:ListItem Value="27">27</asp:ListItem>
                <asp:ListItem Value="28">28</asp:ListItem>
                <asp:ListItem Value="29">29</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="31">31</asp:ListItem>
                <asp:ListItem Value="32">32</asp:ListItem>
                <asp:ListItem Value="33">33</asp:ListItem>
                <asp:ListItem Value="34">34</asp:ListItem>
                <asp:ListItem Value="35">35</asp:ListItem>
                <asp:ListItem Value="36">36</asp:ListItem>
                <asp:ListItem Value="37">37</asp:ListItem>
                <asp:ListItem Value="38">38</asp:ListItem>
                <asp:ListItem Value="39">39</asp:ListItem>
                <asp:ListItem Value="40">40</asp:ListItem>
                <asp:ListItem Value="41">41</asp:ListItem>
                <asp:ListItem Value="42">42</asp:ListItem>
                <asp:ListItem Value="43">43</asp:ListItem>
                <asp:ListItem Value="44">44</asp:ListItem>
                <asp:ListItem Value="45">45</asp:ListItem>
                <asp:ListItem Value="46">46</asp:ListItem>
                <asp:ListItem Value="47">47</asp:ListItem>
                <asp:ListItem Value="48">48</asp:ListItem>
                <asp:ListItem Value="49">49</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
                <asp:ListItem Value="51">51</asp:ListItem>
                <asp:ListItem Value="52">52</asp:ListItem>
                <asp:ListItem Value="53">53</asp:ListItem>
                <asp:ListItem Value="54">54</asp:ListItem>
                <asp:ListItem Value="55">55</asp:ListItem>
                <asp:ListItem Value="56">56</asp:ListItem>
                <asp:ListItem Value="57">57</asp:ListItem>
                <asp:ListItem Value="58">58</asp:ListItem>
                <asp:ListItem Value="59">59</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox ID="CheckBoxTimeUnknown" runat="server" Text="Unknown" />
            <asp:Label ID="LabelDuration" runat="server" AssociatedControlID="TextBoxDuration" Text="Duration:"></asp:Label>
            <asp:TextBox ID="TextBoxDuration" runat="server"></asp:TextBox>
            <asp:DropDownList ID="DropDownListDurationType" runat="server">
                <asp:ListItem Value="1">Hours</asp:ListItem>
                <asp:ListItem Value="24">Days</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox ID="CheckBoxDurationUnknown" runat="server" Text="Unknown" />
            <asp:Label ID="LabelParticipants" runat="server" AssociatedControlID="TextBoxParticipants" Text="Participants:"></asp:Label>
            <asp:TextBox ID="TextBoxParticipants" runat="server">1</asp:TextBox>
            -->
                        <asp:Label ID="LabelLocation" runat="server" Text="Location:"></asp:Label>


            <asp:Label ID="LabelType" runat="server" AssociatedControlID="DropDownListType" Text="Type:"></asp:Label>
            <asp:DropDownList ID="DropDownListType" runat="server">
                <asp:ListItem Value="1">Fun</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="LabelLanguages" runat="server" AssociatedControlID="TextBoxLanguages" Text="Languages:"></asp:Label>
            <asp:TextBox ID="TextBoxLanguages" runat="server"></asp:TextBox>
            <asp:Label ID="LabelDescriptions" runat="server" Text="Descriptions:" AssociatedControlID="TextBoxDescriptions"></asp:Label>
            <asp:Label ID="LabelPriceRange" runat="server" Text="Price Range:" AssociatedControlID="TextBoxPriceFrom"></asp:Label>
            <asp:Label ID="LabelPriceFrom" runat="server" Text="From:" AssociatedControlID="TextBoxPriceFrom"></asp:Label>
            <asp:TextBox ID="TextBoxPriceFrom" runat="server"></asp:TextBox>
            <asp:Label ID="LabelPriceTo" runat="server" Text="To:" AssociatedControlID="TextBoxPriceTo"></asp:Label>
            <asp:TextBox ID="TextBoxPriceTo" runat="server"></asp:TextBox>
            <asp:Label ID="LabelCurrency" runat="server" Text="Currency:" AssociatedControlID="DropDownListCurrency"></asp:Label>
            <asp:DropDownList ID="DropDownListCurrency" runat="server">
                <asp:ListItem Value="EUR">Euro</asp:ListItem>
                <asp:ListItem Value="USD">Dollar</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="LabelRepeat" runat="server" Text="Repeat:" AssociatedControlID="DropDownListRepeat"></asp:Label>
            <asp:DropDownList ID="DropDownListRepeat" runat="server">
                <asp:ListItem Value="1">One-time</asp:ListItem>
                <asp:ListItem Value="1">Daily</asp:ListItem>
                <asp:ListItem Value="3">Weekly</asp:ListItem>
                <asp:ListItem Value="4">Monthly</asp:ListItem>
                <asp:ListItem Value="5">Annualy</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="LabelPrivacy" runat="server" AssociatedControlID="DropDownListPrivacy" Text="Type:"></asp:Label>

            <asp:Button ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Delete" />
            <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Modify" />
            <asp:LinkButton ID="LinkButtonAdvance" runat="server" OnClick="LinkButtonAdvance_Click">Show Advance</asp:LinkButton>
            <asp:Panel ID="PanelAdvance" runat="server" Visible="False">
            </asp:Panel>
            <asp:HiddenField ID="HiddenFieldAdvance" runat="server" Value="0" />
        </asp:Panel>
        <asp:HiddenField ID="HiddenFieldParticipants" runat="server" />
        <asp:HiddenField ID="HiddenFieldParticipantsAccepted" runat="server" />
        <asp:HiddenField ID="HiddenFieldDate" runat="server" />
        <asp:HiddenField ID="HiddenFieldLanguages" runat="server" />
    </div>
</asp:Content>

