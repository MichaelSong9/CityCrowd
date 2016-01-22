//convert em to px
function px(input) {
    var emSize = parseFloat($("body").css("font-size"));
    return (input * emSize);
}

//email validation
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function validateName(name) {
    var re = /^[^±!@£$%^&*_+§¡€#¢§¶•ªº«\\/<>?:;|=.,]{1,20}$/;
    return re.test(name);
}
function validateUsername(name) {
    var re = /^[a-z0-9_-]{3,16}$/;
    return re.test(name);
}

//popup menu function (logo, boxes class, closeByClick)
//'.nDotsLogo', '.nDotsBox', 
//call the function again to close the menu manually
function popUp(targetLogo, targetBoxes, closeByClick) {

    var dotsOpened = false;
    if (closeByClick) {
        var selector = targetBoxes + " div";
        $(targetBoxes).find('div').click(function () {
            $(targetBoxes).addClass("hidden");
            dotsOpened = false;
        });
    }
    $(targetBoxes).addClass("hidden")
    //dots function
    $(targetLogo).click(function () {
        event.stopPropagation();
        if (!dotsOpened) {
            $(targetBoxes).removeClass("hidden")
            dotsOpened = true;
        }
    });
    $(targetBoxes).click(function () {
        event.stopPropagation();
    });
    $('html').click(function () {
        $(targetBoxes).addClass("hidden");
        dotsOpened = false;
        $('.nDotBoxLinks').removeClass('hide');
        $('.nDotsBoxShare').addClass('hide');
    });
}

//optional selected tab by number
function tabbedContent(tabNumber) {
    //without preselected tab
    if (typeof tabNumber === 'undefined') {
        $(".nTabs").click(function () {
            if ($(this).hasClass("nExploreSelected")) {
            }
            else {
                $(".nTabs").removeClass("nExploreSelected");
                $(this).addClass("nExploreSelected");
                var n = $(this).attr("nTabNumber");
                $(".nTabContents").addClass("hidden");
                var selector = ".nExploreContentHolder .nTabContents:nth-child(" + n + ")";
                $(selector).removeClass("hidden");
            }
        });
    } else {
        $(".nTabs").removeClass("nExploreSelected");
        var selector = ".nTabs:eq(" + tabNumber + ")";
        console.log(selector);
        $(selector).addClass("nExploreSelected");
        $(".nTabContents").addClass("hidden");
        tabNumber++;
        var selector = ".nExploreContentHolder .nTabContents:nth-child(" + tabNumber + ")";
        $(selector).removeClass("hidden");
    }
}


/*time format: 1/30/2015 12:00:00 AM
    create message for remained time 
    id of the value, id of the target
    */
function convertDate(givenDate, targetDiv) {
    //setting the the remained time in days and hours and minutes
    var remainedMinutes = 0;
    var remainedHours = 0;
    var remainedDays = 0;
    var AMorPM = "unknown";
    //console.log($(givenDate).val());
    var str = $(givenDate).val();
    var startDateOriginalFormat = str.substring(0, str.indexOf(" "));
    var startHourOriginalFormat = str.substring(str.indexOf(" ") + 1, str.lastIndexOf(" "));
    var AMorPM = str.substring(str.lastIndexOf(" ") + 1);

    //converting the starting date to datestring
    fromDate = startDateOriginalFormat.split("/");
    fromHours = startHourOriginalFormat.split(":");
    if (AMorPM == "PM") {
        f = new Date(fromDate[2], fromDate[0] - 1, fromDate[1], fromHours[2] + 12, fromHours[1], fromDate[0], 0);
    } else if (AMorPM == "AM") {
        f = new Date(fromDate[2], fromDate[0] - 1, fromDate[1], fromHours[2], fromHours[1], fromDate[0], 0);
    } else {
        console.log("ERROR: couldn't read AM or PM!");
    }

    //testing the start time
    //console.log("****" + f.toLocaleDateString("en-US") + "****" + f.toLocaleTimeString("en-US"));
    //converting the start date datestring to miliseconds
    var startTime = f.getTime();
    //console.log("f: " + f.getTime());

    function updateRemainingTimeText() {
        var now = $.now();

        //subtracting now from start time
        var differenceInMS = Math.abs(startTime - now);
        
        //check if event is passed
        if (startTime > now) {
            $(targetDiv).html("This event is passed!");
            return; 
        }
        //calculating difference in days, hours, and minutes
        var x = differenceInMS / 1000;
        var seconds = x % 60;
        x /= 60;
        var minutes = Math.floor(x % 60);
        x /= 60;
        var hours = Math.floor(x % 24);
        x /= 24;
        var days = Math.floor(x);
        //console.log("days:" + days + "hours:" + hours + "minutes:" + minutes);

        //translating difference time to text
        var onlyParameter = "",
            daysParameters = "",
            daysAndParameter = "",
            hoursParameters = "",
            hoursAndParameter = "",
            minutesParameter = "";

        // write only if it's only minutes remaining
        if (days == 0 && hours == 0) { onlyParameters = "only " };
        //determining day parameters, daysAndParameter
        if (hours != 0 || minutes != 0) {
            daysAndParameter = "and ";
        }
        if (days == 1) {
            daysParameters = "One day " + daysAndParameter;
        } else if (days > 1) {
            daysParameters = days + " days " + daysAndParameter;
        }

        //determining hoursAndParameter
        if (minutes != 0) {
            hoursAndParameter = "and ";
        }
        //determining hoursParameters
        if (hours == 1) {
            hoursParameters = "One hour " + hoursAndParameter;
        } else if (hours > 1) {
            hoursParameters = hours + " hours " + hoursAndParameter;
        }


        //determining minutsParameters
        //console.log("***********" + minutes);
        if (minutes == 1) {
            minutesParameter = "One minute";
        } else if (minutes > 1) {
            minutesParameter = minutes + " minutes";
        }
        remainedTimeMessage = onlyParameter + daysParameters + hoursParameters + minutesParameter + " later";
        $(targetDiv).html(remainedTimeMessage);
    };
    updateRemainingTimeText();
    //calculate difference time every 60 seconds
    setInterval(function () { updateRemainingTimeText() }, 60000);
}

function getRootUrl() {
    return window.location.origin ? window.location.origin + '/' : window.location.protocol + '/' + window.location.host + '/';
}

function menu() {

    $(".nMenuAdd").click(function () {
        var url = getRootUrl() + "events/add";
        window.location.href = url;
    })
    $(".nEmailBut").parent().click(function () {
        window.location.href = "../messages";
    })
    $(".nNewEventBut").parent().click(function () {
        window.location.href = "../Events/add";
    })
    $(".nPersonBut").parent().click(function () {
        var profileIDlink = "../profile/" + profileId;
        window.location.href = profileIDlink;
    })
    $(".nExploreBut").parent().click(function () {
        window.location.href = "../explore";
    })
    $(".nFeedBut").parent().click(function () {
        window.location.href = "../feed";
    })
    $(".nEventsBut").parent().click(function () {
        window.location.href = "../events";
    })
    $(".nSearchBut").parent().click(function () {
        window.location.href = "../search";
    })
    $(".nCalendarBut").parent().click(function () {
        window.location.href = "../calendar";
    })
    $(".nSettingsBut").parent().click(function () {
        window.location.href = "../settings ";
    })
    $(".nLogoutBut").parent().click(function () {
        window.location.href = "../logout";
    })


    var photoUrl = $('#HiddenFieldPhotoUrl').val();
    photoUrl = '../' + photoUrl;
    $('.nProfilePicture').css('background-image', 'url(' + photoUrl + ')');

    var profileId = $('#HiddenFieldUsername').val();
    $("#nPanelBox1").click(function () {
        var profileIDlink = "../profile/" + profileId;
        console.log("profileIDlink" + profileIDlink);
        window.location.href = profileIDlink;
    });
}

//selected tab by number
function setEventPages(pageNumber) {

    $(".nAEPage").addClass("hidden");
    var selector = ".nAEPage:eq(" + pageNumber + ")";
    $(selector).removeClass("hidden");

    //displaying previous button
    if (pageNumber == 0) {
        $('.nProfileFooterContainerOneButton').removeClass("hidden");
        $('.nProfileFooterContainerTwoButton').addClass("hidden");
    }

    //displaying previous button
    if (pageNumber == 1) {
        $('.nProfileFooterContainerOneButton').addClass("hidden");
        $('.nProfileFooterContainerTwoButton').removeClass("hidden");
        if ($('.nEAtime').val() == "") {
            $('.nEATimeMessage').html("");
        }

    }

    if (pageNumber == 2) {
        $('.nEATimeMessage').html("");
        $('.nProfileFooterContainerOneButton').addClass("hidden");
        $('.nProfileFooterContainerTwoButton').removeClass("hidden");
        $('.nProfileNextButton b').html("Next");
    }

    // finish button is displayed
    if (pageNumber == 3) {
        $('.nProfileNextButton b').html("Finish");
    }
    //finish pressed
    if (pageNumber == 4) {
        $("#form1").submit();
    }
}

function settingsPhotos() {

    //displaying error message
    if ($(".nWrongLogin b").text() != "") {
        $('.nWrongLogin').removeClass("hidden");
    }

    var photoUrl = $('#ContentPlaceHolder1_HiddenFieldPhotoUrl').val();
    photoUrl = '../' + photoUrl;
    $('.nProfilePicture').css('background-image', 'url(' + photoUrl + ')');

    $(".nSPChooseFileButton").click(function () {
        $(".nSPAspChooseButton").trigger("click");
    })
    $(".nSPUploadButton").click(function () {
        $(".nSPAspUploadButton").trigger("click");
    })
    $(".nSPRemoveButton").click(function () {
        $(".nSPAspRemoveButton").trigger("click");
    })
}
function settings() {

    $(".nSIview").click(function () {
        $(this).addClass('invisible');
        $(this).next().removeClass('invisible');
    })

    var url;
    $(".nProfileMessageButton").click(function () {
        url = "Settings";
        window.location.href = "../settings";
    })
    $(".nSettingsListInformation").click(function () {
        url = "Settings/Information";
        window.location.href = url;
    })
    $(".nSettingsListPhoto").click(function () {
        url = "Settings/Photo";
        window.location.href = url;
    })
    $(".nSettingsListLocation").click(function () {
        url = "Settings/Location";
        window.location.href = url;
    })
    $(".nSettingsListPreferences").click(function () {
        url = "Settings/Preferences";
        window.location.href = url;
    })
    $(".nSettingsListAccount").click(function () {
        url = "Settings/Account";
        window.location.href = url;
    })
    $(".nSettingsListPrivacy").click(function () {
        url = "Settings/Privacy";
        window.location.href = url;
    })
}

function completion() {

    $('#nMainPanelLi').addClass('nMainPanelScrollable');
    $('#nMainPanelLi').css('background-color', 'rgb(215,67,46)');
    $('.nAll').css('background-color', 'rgb(215,67,46)');

    //$('#nMainPanelLi').css('height', '29em');

    //setting date of birth
    //var dateOfBirth = console.log("asdfkhsdafsdaf: " + $('#ContentPlaceHolder1_HiddenFieldDOB').val());
    $('.nCompletionDateInput').val($('#ContentPlaceHolder1_HiddenFieldDOB').val());
    $('.nCompletionDateInput').change(function () {
        console.log($('.nCompletionDateInput').val());
        $('#ContentPlaceHolder1_HiddenFieldDOB').val($('.nCompletionDateInput').val());
    })
    setLogos();

    //opening location if user is setting it
    if ($('#ContentPlaceHolder1_DropDownListCountry').val() != 0) {
        $('.nCompletionBoxSecondLocation').css('height', '8em');
        $('.nCompletionBoxSecondLocation').parent().css('height', '8em');
        $('.nCompletionBoxFirstLocation').addClass('invisible');
        $('.nCompletionBoxSecondLocation').removeClass('invisible');
    }
    //prevent form submit by enter
    $('.nCompletionBox').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13)
            event.preventDefault();
    });



    $('.nValidatorButton').click(function () {
        $('.nCompletionErrorMessage').html("");
        var valid = true;
        //validation for required fields
        var firstNameValue = $('#ContentPlaceHolder1_TextBoxFirstName').val();
        var lastNameValue = $('#ContentPlaceHolder1_TextBoxLastName').val();
        var usernameValue = $('#ContentPlaceHolder1_TextBoxUsername').val()

        //first name

        if ($(this).hasClass('nCompletionArrowFN') || $(this).hasClass('nFooterButton')) {
            console.log("FN validation called");
            if (!validateName(firstNameValue)) {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionFNError').removeClass('invisible');
            } else {
                $('.nCompletionFNError').addClass('invisible');
            }

        }

        //last name
        if ($(this).hasClass('nCompletionArrowLN') || $(this).hasClass('nFooterButton')) {
            if (!validateName(lastNameValue)) {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionLNError').removeClass('invisible');
            } else {
                $('.nCompletionLNError').addClass('invisible');
            }
        }

        //username
        if ($(this).hasClass('nCompletionArrowUN') || $(this).hasClass('nFooterButton')) {
            if (!validateUsername(usernameValue)) {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionUNError').removeClass('invisible');

            } else {
                $('.nCompletionUNError').addClass('invisible');
            }
        }

        //gender
        if ($(this).hasClass('nCompletionArrowGender') || $(this).hasClass('nFooterButton')) {
            if ($('#ContentPlaceHolder1_DropDownListGender').val() == "0") {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionGenderError').removeClass('invisible');

            } else {
                $('.nCompletionGenderError').addClass('invisible');
            }
        }

        //birthdate
        if ($(this).hasClass('nCompletionArrowBD') || $(this).hasClass('nFooterButton')) {
            if ($('#ContentPlaceHolder1_HiddenFieldDOB').val() == "") {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionBDError').removeClass('invisible');

            } else {
                $('.nCompletionBDError').addClass('invisible');
            }
        }

        //location
        if ($(this).hasClass('nCompletionArrowLocation') || $(this).hasClass('nFooterButton')) {
            if ($('#ContentPlaceHolder1_DropDownListCity').val() == "0") {
                valid = false;
                if (!$(this).hasClass('nFooterButton')) {
                    $('.nCompletionValidationErrorMessage').addClass('invisible');
                }
                $('.nCompletionLocationError').removeClass('invisible');

            } else {
                $('.nCompletionLocationError').addClass('invisible');
            }
        }

        if (valid && $(this).hasClass('nFooterButton')) {
            $('.nCompletionErrorMessage').html("done");
            $("#ContentPlaceHolder1_ButtonSubmit").trigger("click");
        }
    })

    $('.nCompletionBoxFirst').click(function () {
        $(this).addClass('invisible');
        $(this).next().removeClass('invisible');
        if ($(this).hasClass('nCompletionBoxFirstLocation')) {
            $(this).next().css('height', '8em');
            $(this).parent().css('height', '8em');
        }
    })
    $('.nCompletionArrow').click(function () {
        setLogos();
        $(this).parent().addClass('invisible');
        $(this).parent().parent().find('.nCompletionBoxFirst').removeClass('invisible');
        if ($(this).parent().hasClass('nCompletionBoxSecond')) {
            $(this).parent().parent().find('.nCompletionBoxFirst').css('height', '2em');
            $(this).parent().parent().find('.nCompletionBoxSecond').css('height', '2em');
            $(this).parent().parent().css('height', '2em');
        }
    })
    //set logos
    function setLogos() {
        console.log("called");
        //firstname
        if (!validateName($('#ContentPlaceHolder1_TextBoxFirstName').val())) {
            $('.nCompletionBoxFirstFN').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstFN').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //last name
        if (!validateName($('#ContentPlaceHolder1_TextBoxLastName').val())) {
            $('.nCompletionBoxFirstLN').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstLN').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //user name
        if (!validateUsername($('#ContentPlaceHolder1_TextBoxUsername').val())) {
            $('.nCompletionBoxFirstUN').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstUN').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //gender
        if ($('#ContentPlaceHolder1_DropDownListGender').val() == "0") {
            $('.nCompletionBoxFirstGender').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstGender').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //date
        if ($('#date').val() == "") {
            $('.nCompletionBoxFirstBD').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstBD').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
        //location
        if ($('#ContentPlaceHolder1_DropDownListCity').val() == "0") {
            $('.nCompletionBoxFirstLocation').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/ERROR_icon.png')");
        } else {
            $('.nCompletionBoxFirstLocation').find('.nCompletionStatus').css('background-image', "url('../Images/Icons/OK_icon.png')");
        }
    }
}

function notifications() {
    //setting size of notification row
    function messageResize() {
        var width = $(window).width() - $('.nMessagesPicture').width() - $('.nMessagesNumberContainer').width() - 50;
        $('.nMessagesInsideDiv').width(width);
    }
    messageResize();
    $(window).resize(function () {
        messageResize();
    });

    //setting background and show number
    $('.fullNotificationContainer').each(function () {
        var isMessageNew = $(this).find('.nNotificationUnread').text();
        console.log(isMessageNew);
        if (isMessageNew == "False") {
            $(this).css("background-color", "rgb(255,255,204)");
            $(this).find('.nMessagesNumberContainer').removeClass("hidden");
        }
    });

}

function requestsPage() {
    //setting image links
    $(".nRequestPicture").click(function () {
        console.log($(this).parent().parent().find('.requestSenderId').text());
        var url = getRootUrl() + "profile/" + $(this).parent().parent().find('.requestSenderId').text();
        window.location.href = url;
    })

    tabbedContent();    
    //setting the graphs
    $('.nEachRequest').each(function () {

        //setting the id of canvas
        var requestID = $(this).find('.LabelRequestId').text();
        var requestRate = $(this).find('.LabelRequestRate').text();
        requestID = "canvas_" + requestID;
        $(this).find('.nRequestCanvas').attr('id', requestID);
        //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
        explore(requestID, false, requestRate, 100, 2.2, 0.3, "rgb(215,67,46)", null);

        var myText = $(this).find('.nRequestMessage span').text();
        if (!myText) {
            $(this).find('.nRequestMessage').css('background-color', 'transparent');
        }

    });

    nRequestRemainedTime
    //setting button links
    /*
    Requests/{RequestId}/{ActionCode}
    action code 1 for accept
    action code 2 for reject
    */
    $(".nNotButton").click(function () {
        var id = $(this).parent().parent().parent().find('.invisible').find('.LabelRequestId').text();
        var url = "requests/" + id + "/2";
        window.location.href = url;
    })
    $(".nRequestButton").click(function () {
        var id = $(this).parent().parent().parent().find('.invisible').find('.LabelRequestId').text();
        var url = "requests/" + id + "/1";
        window.location.href = url;
    })
    
}

function messagesShow() {
    //setting size of textarea width
    function chatResize() {
        var width = $(window).width() - $('.nMSSendButton').width() - 25;
        $('.nMSTextArea').width(width);
    }
    $('.nMainPanelScrollable').scrollTop($('.nMainPanelScrollable').height());

    $('.nMSTextArea').focus();

    //setting size of whitediv2

    function Div2Resize() {
        $('.nMSWhiteDiv2').each(function () {
            var divHeight = $(this).parent().height() + 120;
            console.log(divHeight);
            $(this).height(divHeight);
        });
    }
    Div2Resize();
    //auto enter
    $(".nMSTextArea").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $(".invisible").find('input').trigger("click");
        }
    });

    chatResize();
    $(window).resize(function () {
        chatResize();
        Div2Resize();
    });


    $(".nMSSendButton").click(function () {
        $(".invisible").find('input').trigger("click");
    })
    $(".nFooterButton").click(function () {
        window.location.href = "/Messages";
    })
    $('.nMessagesPicture').each(function () {
        //var photoUrl = "asdfghjkldfghjkl";
        var photoUrl = $(this).find('input').val();
        if (typeof photoUrl != 'undefined') {
            photoUrl = photoUrl.substring(1);
            $(this).css('background-image', 'url(' + photoUrl + ')');
        }
    });

    $('.eachMessage').each(function () {
        var sender = $(this).find('.EachMessageSender').text();
        $(this).find('.nMSWhiteDiv2').css('height', $(this).height());
        if (sender == "False") {
            $(this).css('background-color', 'rgb(81,191,135)');
            $(this).addClass('nRight');
            $(this).find('.nMessagesPicture').addClass('nMSOtherSenderPicture');
            $(this).find('.nMSWhiteDiv1').addClass('nRight');
            $(this).find('.nMSWhiteDiv2').addClass('nMSOtherSenderWD');
            $(this).find('.nMSMessage').addClass('nMSOtherSenderMessage');
            $(this).next().next('.MessageListDate').addClass('nMSOtherSenderDate');
        }
    })
}

function blog() {
    //back to blog button
    $(".nProfileMessageButton").click(function () {
        window.location.href = "../../blog";
    });
}

function search() {
    tabbedContent();
    //setting size of search row
    function searchResize() {
        var width = $('.nDescriptionContent').width() - $('.nSearchButton').width() - 15;
        $('.nSearchSearchBox').width(width);
    }
    searchResize();
    $(window).resize(function () {
        searchResize();
    });

    $(".nSearchButton").click(function () {
        $("#ButtonSearch").trigger("click");
    });
    $(".nSearchLogo").click(function () {
        $(".nSearchLogo").css("background-color", "rgb(222,222,222)");
        activity = $(this).attr("data");
        $(this).css("background-color", "rgb(255,187,5)");
    })
}



//error page
function errorPage(pageNumber) {
    function loginResize() {
        //taking care of middle window size
        if ($(window).width() > 301) {
            $(".nMiddleWindow").width('300px');
        } else {
            $(".nMiddleWindow").width('90%');
        }
        //error image size
        logoWidth = $(".nErrorImage").width();
        $(".nErrorImage").css("height", logoWidth);
    }

    loginResize();
    $(window).resize(function () {
        loginResize();
    });

    //Write a message button
    $(".nProfileMessageButton").click(function () {
        var url = getRootUrl() + "explore";
        window.location.href = url;
    });
}

//messages
function messages() {
    //setting size of message row
    function messageResize() {
        var width = $(window).width() - $('.nMessagesPicture').width() - $('.nMessagesNumberContainer').width() - 50;
        console.log("*" + width);
        $('.nMessagesInsideDiv').width(width);
    }
    messageResize();
    $(window).resize(function () {
        messageResize();
    });

    //setting background and show number
    $('.fullNotificationContainer').each(function () {
        var isMessageNew = $(this).find('.nMessagesNew').text();
        if (isMessageNew == "0") {
            $(this).css("background-color", "rgb(255,255,204)");
            $(this).find('.nMessagesNumberContainer').removeClass("hidden");
        }
    });

}

function loginResize() {
    //taking care of logo size
    $(".loginMainWindow").css("height", $(window).innerHeight());
    var loginTop = $(window).innerHeight() * 30 / 100;
    $(".nLoginBox").css("top", loginTop);
    if ($(window).width() > 450) {
        $(".nMiddleWindow").width('400px');
        $(".nLoginFooterSecondary").width('400px');
    } else {
        $(".nMiddleWindow").width('90%');
        $(".nLoginFooterSecondary").width('90%');
    }
    logoWidth = $(".nLoginLogo").width();

    var logoHeight = logoWidth / 4.2475;
    $(".nLoginLogo").css("height", logoHeight);
}
//displaying server errors in login, register, forgot password
function errorMessage() {
    if ($("#LabelError").text() != "") {
        $('.nWrongLogin').removeClass("hidden");
    }
}

function forgotPage() {
    loginResize();
    $(window).resize(function () {
        loginResize();
    });

    function forgotButtonClicked() {
        var valid = true;
        if (!validateEmail($('#TextBoxEmail').val())) {
            valid = false;
            $('.nForgotEmailError').removeClass('invisible');
        } else {
            $('.nForgotEmailError').addClass('invisible');
        }
        if (valid) {
            $("#ButtonRequest").trigger("click");
        }
    };
    //prevent form submit by enter
    $('#TextBoxEmail').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            forgotButtonClicked();
        }
    });
    
    errorMessage();

    //forgot password button
    $(".nFPButton").click(function () {
        forgotButtonClicked()
    });


    //*************recover**********
    function recoverButtonClicked() {
        valid = true;
        if ($('#TextBoxPassword1').val()=="") {
            $('.nForgotPass1Error').removeClass('invisible');
        } else {
            $('.nForgotPass1Error').addClass('invisible');
        }
        if ($('#TextBoxPassword1').val() != $('#TextBoxPassword2').val()) {
            $('.nForgotPass2Error').removeClass('invisible');
        } else {
            $('.nForgotPass2Error').addClass('invisible');
        }
        if (valid) {
            $("#ButtonRecover").trigger("click");
        }
    }
    //recover password button
    $(".nFPRecoverButton").click(function () {
        recoverButtonClicked();
    });

    //prevent enter button
    $('#TextBoxPassword1').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            recoverButtonClicked();
        }
    });
    $('#TextBoxPassword2').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            recoverButtonClicked();
        }
    });
    //displaying error message in forgot password page
    if ($("#LabelMessage").text() != "") {
        $('.nLabelMessageCopy').text($("#LabelMessage").text());
        $('.nWrongLogin').removeClass("hidden");
    }

    //login button in forgot page
    $(".nForgotLogin").click(function () {
        window.location.href = "../login";
    });

    //register button in forgot page
    $(".nForgotRegister").click(function () {
        window.location.href = "../Register";
    });
}



function registerPage() {
    loginResize();
    $(window).resize(function () {
        loginResize();
    });

    function registerButtonClicked() {
        var valid = true;
        if (!validateEmail($('#TextBoxEmail').val())) {
            valid = false;
            $('.nRegisterEmailError').removeClass('invisible');
        } else {
            $('.nRegisterEmailError').addClass('invisible');
        }

        if ($('#TextBoxPassword1').val()=="") {
            valid = false;
            $('.nRegisterPasswordError').removeClass('invisible');
        } else {
            $('.nRegisterPasswordError').addClass('invisible');
        }
        if (($('#TextBoxPassword1').val() != $('#TextBoxPassword2').val()) || ($('#TextBoxPassword2').val()=="")) {
            valid = false;
            $('.nRegisterRepeatPassError').removeClass('invisible');
        } else {
            $('.nRegisterRepeatPassError').addClass('invisible');
        }
        if (valid == true) {
            $("#ButtonRegister").trigger("click");
        }
    }
    //prevent form submit by enter
    $('#TextBoxEmail').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            registerButtonClicked();
        }
    });
    $('#TextBoxPassword1').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            registerButtonClicked();
        }
    });
    $('#TextBoxPassword2').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            registerButtonClicked();
        }
    });
    //login button in register page
    $(".nRegisterLogin").click(function () {
        window.location.href = "Login";
    });

    //register button
    $(".nRegisterButton").click(function () {
        registerButtonClicked();
    });

    //displaying error message
    errorMessage();
}
//login, forgot password. register
function login() {
    loginResize();
    $(window).resize(function () {
        loginResize();
    });
    function loginbuttonClicked() {
        var valid = true;
        //validation
        if (!validateEmail($('#TextBoxUsername').val())) {
            $('.nLoginEmailError').removeClass('invisible');
            valid = false;
        } else {
            $('.nLoginEmailError').addClass('invisible');
        }
        if ($('#TextBoxPassword').val() == "") {
            $('.nLoginPasswordError').removeClass('invisible');
            valid = false;
        } else {
            $('.nLoginPasswordError').addClass('invisible');
        }
        if (valid)
            $("#ButtonLogin").trigger("click");
    }
    //prevent form submit by enter
    $('#TextBoxUsername').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            loginbuttonClicked();
        }
    });

    //prevent form submit by enter
    $('#TextBoxPassword').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13) {
            event.preventDefault();
            loginbuttonClicked();
        }
    });

    //login button
    $(".nLoginButton").click(function () {
        loginbuttonClicked();
    });

    //forget button
    $(".nLoginForgot").click(function () {
        window.location.href = "ForgotPassword/Request";
    });

    //register button in login page
    $(".nLoginRegister").click(function () {
        window.location.href = "Register";
    });

    errorMessage();

    

}

/*returns nothing
(the class of objects, bgcolor of selected, bgcolor of all,optional innerclass)
*/
function nRadioButton(givenClass, selected, all, innerClass) {
    //setting activity name and id

    if (innerClass == null) {
        $(givenClass).click(function () {
            $(givenClass).css("background-color", all);
            $(this).css("background-color", selected);
        })
    }
    else {
        $(givenClass).click(function () {
            $(givenClass).find(innerClass).css("background-color", all);
            $(this).find(innerClass).css("background-color", selected);
        })
    }
}
function eventsModify() {

    var activity;
    var activityID;
    var timeType;
    var currentPage;
    //if the page is reloaded in changing location part
    if ($('#ContentPlaceHolder1_TextBoxName').val() != "") {
        currentPage = 0;

        //setting activity type and icon
        activityID = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
        var selector = ".nActivityLogo[activity-id=" + activityID + "]";
        $(selector).css("background-color", "rgb(255,187,5)");
        activity = $(selector).attr("data");
        $('.nEAACtivityType').text(activity);

        //setting the participat slider
        $(".participantsSlider").val($('#ContentPlaceHolder1_HiddenFieldParticipants').val());
        $(".participantsSlider").slider("refresh");

        //setting the date
        var eventDateFull = $('#ContentPlaceHolder1_HiddenFieldDate').val();
        var eventDate = eventDateFull.split(" ");
        console.log(eventDate[0]);
        //30/1/2015
        var eventDateSeperated = eventDate[0].split("/");
        console.log(eventDateSeperated[2]);
        console.log(eventDateSeperated[1]);
        console.log(eventDateSeperated[0]);
        var d = new Date(eventDateSeperated[2], eventDateSeperated[1]-1, eventDateSeperated[0]);
        console.log(d.toDateString());
        //console.log("$('#ContentPlaceHolder1_HiddenFieldDate').val() " + $('#ContentPlaceHolder1_HiddenFieldDate').val());
        
        //setting the date
        var eventMonth = d.getMonth()+1;
        if (eventMonth < 10) {
            eventMonth = "0" + eventMonth;
        } else {
        }
        var eventDate;
        if (d.getDate() < 10) {
            eventDate = "0" + d.getDate();
        } else {
            eventDate = d.getDate();
        }
        var finalDate = "2015" + "-" + eventMonth + "-" + eventDate;
        $('.nEAdate').val(finalDate);

        //setting the duration slider
        $(".nEADurationSlider").val($('#ContentPlaceHolder1_HiddenFieldDuration').val());
        $(".nEADurationSlider").slider("refresh");

        //setting the duration type
        timeType = $('#ContentPlaceHolder1_HiddenFieldDurationType').val();
        var selector = ".nTimeTypeContainer[time-type=" + timeType + "]";
        $(selector).find('.nRadioCircle').css("background-color", "rgb(215,67,46)");
    }
        //if the page is loaded for the first time
    else {
        currentPage = 0;
        activity = "";
        activityID = "0";
        timeType = "Unknown";
        //$(".nActivityMore").css("background-color", "rgb(255,187,5)");
        $(".nTimeTypeContainerUnknown").find('.nRadioCircle').css("background-color", "rgb(215,67,46)");
        //$('#ContentPlaceHolder1_HiddenFieldTypeId').val("8");

        //setting date to the current date
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());

        //setting duration type to unknown
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val("Unknown");

    }

    setEventPages(currentPage);

    //choosing activity type
    nRadioButton('.nActivityLogo', "rgb(255,187,5)", 'rgb(222, 222, 222)');
    $('.nActivityLogo').click(function () {
        activity = $(this).attr("data");
        activityID = $(this).attr("activity-id");
        $('.nEAACtivityType').text(activity);
        $('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
        $('.nEAACtivityType').removeClass('nGeneralErrorMessage');
    })

    //setting participants
    $('.participantsSlider').change(function () {
        console.log($('.participantsSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldParticipants').val($('.participantsSlider').val());
    })

    //setting start time
    $('.nEAtime').change(function () {
        console.log($('.nEAtime').val());
        $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
    })

    //setting duration
    $('.nEADurationSlider').change(function () {
        console.log($('.nEADurationSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldDuration').val($('.nEADurationSlider').val());
    })

    //setting duration type
    nRadioButton('.nTimeTypeContainer', "rgb(215,67,46)", 'white', ".nRadioCircle");
    $('.nTimeTypeContainer').click(function () {
        timeType = $(this).attr("time-type");
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val(timeType);
    })

    //setting participants
    $(".participantsSlider").change(function () {
        $('#ContentPlaceHolder1_TextBoxParticipants').val($('.participantsSlider').val());
    });

    //setting date
    $(".nEAdate").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());
    });
    var languages = [];
    popUp('.nEAlanguageButtonsAdd', '.nDotsBox', true);

    //adding the languages
    $('.nDotsBox').find('div').click(function () {
        var language = $(this).attr('data-language');
        if ($.inArray(language, languages) == -1) {
            languages.push(language);
            var appendText = '<div class="nEAlanguageButtons ' + 'nEAlanguageButtons' + language + '">' + language + '</div>';
            $('.nEAlanguagesAll').append(appendText);
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });

    //removing the laguages
    $('.nEAlanguagesAll').on('click', '.nEAlanguageButtons', function () {
        var language = $(this).html();
        console.log(language);
        if ($.inArray(language, languages) != -1) {
            languages.splice($.inArray(language, languages), 1);
            var removeSelector = ".nEAlanguageButtons" + language;
            $(removeSelector).remove();
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });

    //setting the time of the event
    $(".newEventTime").change(function () {
        console.log($('.newEventTime').val());
        $('#ContentPlaceHolder1_HiddenFieldTime').val($('.newEventTime').val());
    });


    //prevent form submit by enter
    $('.nFooterButton').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13)
            event.preventDefault();
    });

    //validation of each page
    function nAEpageButtonPressed() {
        if (currentPage < 4) {
            //validation of fields
            if (currentPage == 0) {
                if (activityID == "0") {
                    $('.nEAACtivityType').html('Please choose activity type :-)');
                    $('.nEAACtivityType').addClass('nGeneralErrorMessage');
                    return;
                }
                else if ($('#ContentPlaceHolder1_TextBoxName').val() == "") {
                    $('#ContentPlaceHolder1_TextBoxName').attr('placeholder', 'Please assign a name for the event :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
            }
            if (currentPage == 1) {
                //HiddenFieldTime
                console.log($('.newEventTime').val());
                if ($('.nEAtime').val() == "") {
                    $('.nEATimeMessage').html('Please set the starting time :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
            }
            currentPage++;
            setEventPages(currentPage);
        }

    }


    $(".nProfileNextButton").click(function () {
        nAEpageButtonPressed();
    })
    $(".nNotButton").click(function () {
        currentPage--;
        setEventPages(currentPage);
    })
    //preventing post submit by click
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            nAEpageButtonPressed();
            return false;
        }
    });

}
function eventsAdd() {

    var activity;
    var activityID;
    var timeType;
    var currentPage;
    //if the page is reloaded in changing location part
    if ($('#ContentPlaceHolder1_TextBoxName').val() != "") {
        currentPage = 2;

        //setting activity type and icon
        activityID = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
        var selector = ".nActivityLogo[activity-id=" + activityID + "]";
        $(selector).css("background-color", "rgb(255,187,5)");
        activity = $(selector).attr("data");
        $('.nEAACtivityType').text(activity);

        //setting the participat slider
        $(".participantsSlider").val($('#ContentPlaceHolder1_HiddenFieldParticipants').val());
        $(".participantsSlider").slider("refresh");

        //setting the date
        $('.nEAdate').val($('#ContentPlaceHolder1_HiddenFieldDate').val());

        //setting the time
        $('.nEAtime').val($('#ContentPlaceHolder1_HiddenFieldTime').val());


        //setting the duration slider
        $(".nEADurationSlider").val($('#ContentPlaceHolder1_HiddenFieldDuration').val());
        $(".nEADurationSlider").slider("refresh");

        //setting the duration type
        timeType = $('#ContentPlaceHolder1_HiddenFieldDurationType').val();
        var selector = ".nTimeTypeContainer[time-type=" + timeType + "]";
        $(selector).find('.nRadioCircle').css("background-color", "rgb(215,67,46)");
    }
        //if the page is loaded for the first time
    else {
        currentPage = 0;
        activity = "";
        activityID = "0";
        timeType = "Unknown";
        //$(".nActivityMore").css("background-color", "rgb(255,187,5)");
        $(".nTimeTypeContainerUnknown").find('.nRadioCircle').css("background-color", "rgb(215,67,46)");
        //$('#ContentPlaceHolder1_HiddenFieldTypeId').val("8");

        //setting date to the current date
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $('.nEAdate').val(today);
        console.log(today);
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());

        //setting duration type to unknown
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val("Unknown");

    }

    setEventPages(currentPage);

    //choosing activity type
    nRadioButton('.nActivityLogo', "rgb(255,187,5)", 'rgb(222, 222, 222)');
    $('.nActivityLogo').click(function () {
        activity = $(this).attr("data");
        activityID = $(this).attr("activity-id");
        $('.nEAACtivityType').text(activity);
        $('#ContentPlaceHolder1_HiddenFieldTypeId').val(activityID);
        $('.nEAACtivityType').removeClass('nGeneralErrorMessage');
    })

    //setting participants
    $('.participantsSlider').change(function () {
        console.log($('.participantsSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldParticipants').val($('.participantsSlider').val());
    })

    //setting start time
    $('.nEAtime').change(function () {
        console.log($('.nEAtime').val());
        $('#ContentPlaceHolder1_HiddenFieldTime').val($('.nEAtime').val());
    })

    //setting duration
    $('.nEADurationSlider').change(function () {
        console.log($('.nEADurationSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldDuration').val($('.nEADurationSlider').val());
    })

    //setting duration type
    nRadioButton('.nTimeTypeContainer', "rgb(215,67,46)", 'white', ".nRadioCircle");
    $('.nTimeTypeContainer').click(function () {
        timeType = $(this).attr("time-type");
        $('#ContentPlaceHolder1_HiddenFieldDurationType').val(timeType);
    })

    //setting participants
    $(".participantsSlider").change(function () {
        $('#ContentPlaceHolder1_TextBoxParticipants').val($('.participantsSlider').val());
    });

    //setting date
    $(".nEAdate").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('.nEAdate').val());
    });
    var languages = [];
    popUp('.nEAlanguageButtonsAdd', '.nDotsBox', true);

    //adding the languages
    $('.nDotsBox').find('div').click(function () {
        var language = $(this).attr('data-language');
        if ($.inArray(language, languages) == -1) {
            languages.push(language);
            var appendText = '<div class="nEAlanguageButtons ' + 'nEAlanguageButtons' + language + '">' + language + '</div>';
            $('.nEAlanguagesAll').append(appendText);
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });

    //removing the laguages
    $('.nEAlanguagesAll').on('click', '.nEAlanguageButtons', function () {
        var language = $(this).html();
        console.log(language);
        if ($.inArray(language, languages) != -1) {
            languages.splice($.inArray(language, languages), 1);
            var removeSelector = ".nEAlanguageButtons" + language;
            $(removeSelector).remove();
            $('#ContentPlaceHolder1_TextBoxLanguages').val(languages);
            $('#ContentPlaceHolder1_HiddenFieldLanguages').val(languages);
            console.log($('#ContentPlaceHolder1_HiddenFieldLanguages').val());
        }
    });

    //setting the time of the event
    $(".newEventTime").change(function () {
        console.log($('.newEventTime').val());
        $('#ContentPlaceHolder1_HiddenFieldTime').val($('.newEventTime').val());
    });


    //prevent form submit by enter
    $('.nFooterButton').keypress(function (event) {
        if (event.keyCode == 10 || event.keyCode == 13)
            event.preventDefault();
    });

    //validation of each page
    function nAEpageButtonPressed() {
        if (currentPage < 4) {
            //validation of fields
            if (currentPage == 0) {
                if (activityID == "0") {
                    $('.nEAACtivityType').html('Please choose activity type :-)');
                    $('.nEAACtivityType').addClass('nGeneralErrorMessage');
                    return;
                }
                else if ($('#ContentPlaceHolder1_TextBoxName').val() == "") {
                    $('#ContentPlaceHolder1_TextBoxName').attr('placeholder', 'Please assign a name for the event :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
            }
            if (currentPage == 1) {
                //HiddenFieldTime
                console.log($('.newEventTime').val());
                if ($('.nEAtime').val() == "") {
                    $('.nEATimeMessage').html('Please set the starting time :-)');
                    $('#ContentPlaceHolder1_TextBoxName').focus();
                    console.log("no");
                    return;
                }
            }
            currentPage++;
            setEventPages(currentPage);
        }

    }


    $(".nProfileNextButton").click(function () {
        nAEpageButtonPressed();
    })
    $(".nNotButton").click(function () {
        currentPage--;
        setEventPages(currentPage);
    })
    //preventing post submit by click
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            nAEpageButtonPressed();
            return false;
        }
    });

}


function events() {
    var txt = $("#ContentPlaceHolder1_LabelTitle").text();
    if (txt == "Created Events") {
        tabbedContent(0);
    } else if (txt == "Accepted Events") {
        tabbedContent(1);
    } else if (txt == "Requested Events") {
        tabbedContent(2);
    }
}

function barGraph() {
    $('.nGraphBar').each(function () {
        var maxWidth = $(this).parent().width();
        var width = $(this).attr("data-score");
        if (width == 0) { width = 1 };
        width *= maxWidth / 100;
        width -= width / 10;
        $(this).css("width", width);
    });
}



function profilePage() {

    //setting flag background
    var bgURL = $('#ContentPlaceHolder1_HiddenFieldFlagId').val();
    console.log("flag: " + bgURL);
    var fullURL = "url('" + "../Images/Flags/" + bgURL + ".png')";
    $('.nCountryFlag').css("background-image", fullURL);

    //setting verification
    if ($('#ContentPlaceHolder1_HiddenFieldProfileVerified').val() == "False") {
        $('.arrow-right').addClass('hidden');
    }

    var ownProfile = false;
    //showing tab contents
    tabbedContent();
    //$('#ContentPlaceHolder1_LabelUsername').text('asdfs');
    //check if it's own profile
    if ($('#HiddenFieldUsername').val() == $('#ContentPlaceHolder1_LabelUsername').text()) {
        ownProfile = true;

        //changing bottom button
        $('.nProfileMessageButton').text('Edit Profile');
        //hiding follow button
        $('.nProfileFollowButton').addClass('invisible');
        //changing dots menu
        $('.nDotsBoxInsideContainerLinks').addClass('invisible');
        $('.nDotsBoxInsideContainerEdit').removeClass('invisible');
    }

    //setting followers pictures
    $('.nMessagesPicture').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('input').val();
        var bgURL = bgURL.substring(2);
        fullURL = "url('" + "../../../" + bgURL + "')";
        $(this).css("background-image", fullURL);
    });

    //verified logo button
    $('.nVerified').click(function () {
        tabbedContent(1);
    })

    //followers label button
    $('.nProfileFollowersLabel').click(function () {
        tabbedContent(2);
    })

    //followings label button
    $('.nProfileFollowingsLabel').click(function () {
        tabbedContent(3);
    })

    //message button
    $('.nFooterButton').click(function () {
        if (ownProfile == true) {
            var messageUrl = "../../settings/" + $('#ContentPlaceHolder1_HiddenFieldUserId').val();
        } else {
            var messageUrl = "../../messages/" + $('#ContentPlaceHolder1_HiddenFieldUserId').val();
        }
        location.href = messageUrl;
    })

    //displaying no record message
    if ($('#ContentPlaceHolder1_LabelNoRecord').text() != "") {
        $('.nProfileErrorMessage').html($('#ContentPlaceHolder1_LabelNoRecord').text());
    }

    //edit  button
    $('.nDotsEditProfile').next().click(function () {
        window.location.href = "../settings/";
    })

    //clicking on profile the picture
    $('.nProfpagePicture, .nEVOwnerName').click(function () {
        if ($('#HiddenFieldUsername').val() == $('#ContentPlaceHolder1_LabelUsername').text()) {
            window.location.href = "../settings/";
        } else {
            tabbedContent(1);
        }
    })


    //follow
    $(".nProfileFollowButton").click(function () {
        $("#ContentPlaceHolder1_ButtonFollow").trigger("click");
    })

    //setting the link of the profile image
    var photoUrl = $('#ContentPlaceHolder1_HiddenFieldProfilePhoto').val();
    //$('.nProfilePicture').css('background-image', 'url(../../' + photoUrl+ ')');

    popUp('.nDotsLogo', '.nDotsBox', true);

    var score = $('#ContentPlaceHolder1_HiddenFieldOwnerRateScore').val();
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("canvas2", false, score, 100, 3.3, 0.3, "rgb(215,67,46)", null);

    //converting and assigning number of
    var scores_array = [];
    $('.nGraphBar').each(function () {
        var score = $(this).find('span').text();
        //$(this).attr('data-score',score);
        scores_array.push(score);
    })
    var scoreArrayMax = Math.max.apply(Math, scores_array);
    var factor = 100 / scoreArrayMax;
    var counter = 0;
    $('.nGraphBar').each(function () {
        var finalScore = scores_array[counter] * factor;
        $(this).attr('data-score', finalScore);
        counter++;
    });
    console.log("maxxxxxxx" + scoreArrayMax);
    $('.reviewButton').click(function () {
        barGraph();
    });

}


function explorePage() {
    var score = $('#ContentPlaceHolder1_HiddenFieldOwnerRateScore').val();
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("canvas2", false, score, 100, 2.3, 0.3, "rgb(215,67,46)", null);

    var participants = $('#ContentPlaceHolder1_LabelParticipantsAvailable').text();
    var all = $('#ContentPlaceHolder1_LabelParticipants').text();
    newGraph(participants, all, ".nNewGraphTop", ".nNewGraphBottom");
}

// eventsview page
function eventsView() {
    tabbedContent();
    var isMenuCloseble = false;

    //clicking on owner picture
    $('.nProfilePicture').click(function () {
        console.log($('#ContentPlaceHolder1_HiddenFieldOwnerId').val());
        var ownerUrl = "../../profile/" + $('#ContentPlaceHolder1_HiddenFieldOwnerId').val();
        console.log(ownerUrl);
        location.href = ownerUrl;
    })

    //clicking on paticipants graph
    $('#EVcanvas').click(function () {
        tabbedContent(2);
    })


    //click on profile's photo
    $(document).on("click", ".nMessagesPicture", function () {
        var ownerUrl = "../../profile/" + $(this).find('.nEVIid').find('input').val();
        location.href = ownerUrl;
    });

    //setting bg color of messeages
    $('.eachMessage').each(function () {
        if ($(this).find('.nEVIsOwner').find('input').val() == "true") {
            $(this).css('background-color', 'rgb(81, 191, 135)');
        }
    });

    $('.nDotsBoxShare').click(function () {
        popUp(".nDotsLogo", ".nDotsBox", false);
    })

    var fbLink = "http://www.facebook.com/share.php?u=" + document.URL;
    $('.nEVShareButtonFB').attr('href', fbLink);

    var twitterLink = "http://twitter.com/home?status=" + document.URL;
    $('.nEVShareButtonTwitter').attr('href', twitterLink);

    var imgDim = px(3);
    $('.nEVshareImg').attr('width', imgDim);
    $('.nEVshareImg').attr('height', imgDim);

    $('.nEVshareImg').click(function () {
        $('.nDotsBox').addClass("hidden");
        dotsOpened = false;
        $('.nDotBoxLinks').removeClass('hide');
        $('.nDotsBoxShare').addClass('hide');
    })

    $('.nDotsBoxEachLogoShare').next().click(function () {
        $('.nDotBoxLinks').addClass('hide');
        $('.nDotsBoxShare').removeClass('hide');
    })

    var eventId = $('#ContentPlaceHolder1_HiddenFieldOwnerId').val();
    $('.nDotsBoxEachLogoReport').next().click(function () {
        //~/Report/Events/EventId
        var reportLink = "../Report/Events/" + eventId;
        window.location.href = reportLink;
    })

    var activityId = $('#ContentPlaceHolder1_HiddenFieldTypeId').val();
    //console.log(activityId);
    $('.nActivityPicture').css('background-image', 'url(../../Images/Icons/activity' + activityId + '.png)');

    convertDate("#ContentPlaceHolder1_HiddenFieldDate", ".nEVdate");
    $(".nEVbookmarkButton").click(function () {
        $("#ContentPlaceHolder1_ButtonBookmark").trigger("click");
    });

    $('.nMessagesPicture').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('input').val();
        var bgURL = bgURL.substring(2);
        fullURL = "url('" + "../../../" + bgURL + "')";
        $(this).css("background-image", fullURL);
    });

    popUp(".nDotsLogo", ".nDotsBox", false);

    var participants = $('#ContentPlaceHolder1_LabelParticipantsAvailable').text();
    $('.nParticipantAvailable').text(participants);
    var ProfilePictureUrl = $('#ContentPlaceHolder1_HiddenFieldOwnerPhotoUrl').val();
    //console.log(ProfilePictureUrl);
    $('.nProfilePicture').css('background-image', 'url(../../' + ProfilePictureUrl + ')');

    var EventPictureID = $('#ContentPlaceHolder1_HiddenFieldCoverId').val();
    $('.nContentCover').css('background-image', 'url(../../images/event' + EventPictureID + '.jpg)');

    //footer buttons actions
    $(".nRequestButton").click(function () {
        $("#ContentPlaceHolder1_ButtonActionYes").trigger("click");
    });
    $(".nNotButton").click(function () {
        $("#ContentPlaceHolder1_ButtonActionNo").trigger("click");
    });

    $(".hiddenButtons").addClass("hidden");

    var score = $('#ContentPlaceHolder1_HiddenFieldOwnerRateScore').val();
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("canvas2", false, score, 100, 2.3, 0.3, "rgb(215,67,46)", null);

    var participants = $('#ContentPlaceHolder1_LabelParticipantsAvailable').text();
    var all = $('#ContentPlaceHolder1_LabelParticipants').text();
    //canvasId,text,taken, all, radius,width,firstColor,secondColor (all the sizes are in em)
    explore("EVcanvas", false, participants, all, 2.05, 0.2, "rgb(52,108,15)", null);
}

function setPositions() {
    $(".nAll").css("height", window.innerHeight);
    var mainLeft = $('#nMainPanelLi').offset().left;
    var ulLeft = $('.nAll').offset().left;
    $('.nAll').offset({ left: ulLeft - mainLeft });
}



function generalLook() {
    var menuOpen = false;
    setPositions();
    $(window).resize(function () {
        setPositions();
    });

    //setting the main div position
    $('.nMenuButton').click(function () {
        if (menuOpen == false) {
            event.stopPropagation();
            menuOpen = true;
            $('.nAll').offset({ left: 0 });
        }
    })
    //closing the menu
    $('html').click(function (e) {
        if (menuOpen == true) {
            menuOpen = false;
            var mainLeft = $('#nMainPanelLi').offset().left;
            var ulLeft = $('.nAll').offset().left;
            $('.nAll').offset({ left: ulLeft - mainLeft });
        }
    });

}

function newGraph(taken, all, topElement, botElement) {
    var botRatio = 100 * taken / all;
    var topRatio = 100 * (1 - (taken / all));
    console.log(topRatio);
    $(botElement).css("height", botRatio + "%");
    $(topElement).css("height", topRatio + "%");
}

//drawing canvases on explore page
//radius and width would be in em
function explore(canvasName, textBool, taken, all, radius, width, firstColor, secondColor) {
    if (!taken || isNaN(taken)) {
        taken = 2;
    }
    //console.log($('#HiddenFieldOwnerId').val());
    var currentEndAngle = -0.5;
    var currentStartAngle = -0.5;
    var lineRadius = px(radius);

    var lineWidth = px(width);
    var canvasWidth = 2 * (lineRadius + lineWidth);
    var canvas = document.getElementById(canvasName);


    //setting canvas size
    $(canvas).prop("width", canvasWidth);
    $(canvas).prop("height", canvasWidth);



    var context = canvas.getContext("2d");
    if (textBool) {
        context.font = "1em Arial";
        context.fillStyle = 'lightblue';
        context.fillText(taken + "/" + all, canvasWidth / 4, 3 * canvasWidth / 5);
    }
    var myVar = setInterval(function () { draw() }, 5);

    //drawing the circle
    function draw() { /***************/

        var x = canvas.width / 2;
        var y = canvas.height / 2;
        var radius;
        var width;
        var secondColorPoint = 2 * taken / all - 0.5;

        var startAngle = currentStartAngle * Math.PI;
        var endAngle = (currentEndAngle) * Math.PI;

        currentStartAngle = currentEndAngle - 0.01;
        currentEndAngle = currentEndAngle + 0.01;

        if (currentStartAngle > secondColorPoint) {
            if (secondColor == null) {
                clearInterval(myVar);
            }
            currentColor = secondColor;
            radius = lineRadius;
            width = lineWidth;
            //width = lineWidth + 3;
        } else {
            currentColor = firstColor;
            radius = lineRadius;
            width = lineWidth;
        }

        var counterClockwise = false;
        context.beginPath();
        context.arc(x, y, radius, startAngle, endAngle, counterClockwise);
        context.lineWidth = width;
        // line color
        context.strokeStyle = currentColor;
        context.stroke();
        //console.log(currentStartAngle);

        if (endAngle > 1.5 * 3.14) { clearInterval(myVar); }
        /************************************************/
    }
}

$(document).on('pageinit', '#notifications', function () {

    $('.notificationImage span').hide();

    $('.notificationImage').each(function () {
        bgURL = "";
        fullURL = "";
        bgURL = $(this).find('span').text();
        fullURL = "url('" + bgURL + "')";
        $(this).css("background-image", fullURL);
    });


});


$(document).on('pageshow', '#eventsModify', function () {

    //setting date field
    var str = $('#ContentPlaceHolder1_HiddenFieldDate').val();
    var DateOriginalFormat = str.substring(0, str.indexOf(" "));
    var fromDate = DateOriginalFormat.split("/");
    var year = fromDate[2];
    var day = ('0' + fromDate[1]).slice(-2);
    var month = ('0' + fromDate[0]).slice(-2);
    var finalYear = year + "-" + month + "-" + day;
    console.log(finalYear);
    $("#modifyEventDate").val(finalYear);

    //saving date field if modified
    $("#modifyEventDate").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDate').val($('#modifyEventDate').val());
        console.log($('#modifyEventDate').val());
    });

    //set participant numbers values
    var participantNumber = $('#ContentPlaceHolder1_HiddenFieldParticipants').val();
    var acceptedParticipants = $('#ContentPlaceHolder1_HiddenFieldParticipantsAccepted').val();
    $('.participantsSlider').val(participantNumber).attr("min", acceptedParticipants).slider("refresh");

    $(".participantsSlider").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldParticipants').val($('.participantsSlider').val());

    });


    //set languages
    var languages = $('#ContentPlaceHolder1_HiddenFieldLanguages').val();
    console.log(languages);
    var languagesArray = languages.split(",");
    for (i = 0; i < languagesArray.length; i++) {
        $('#languages option[value=' + languagesArray[i] + ']').attr('selected', 'selected');
    };
    $('#languages').selectmenu('refresh');


    $("#languages").change(function () {
        var multipleValues = $("#languages").val() || [];
        $('#ContentPlaceHolder1_HiddenFieldLanguages').val(multipleValues);
        console.log(multipleValues.toString());
    });


    //set price ranges
    var priceFrom = $('#ContentPlaceHolder1_HiddenFieldPriceFrom').val();
    var priceTo = $('#ContentPlaceHolder1_HiddenFieldPriceTo').val();

    $('.priceRangeFrom').val(priceFrom);
    $('.priceRangeTo').val(priceTo);
    $(".priceRange").rangeslider("refresh");


    $(".priceRangeFrom").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldPriceFrom').val($('.priceRangeFrom').val());
        console.log($('.priceRangeFrom').val());
    });

    $(".priceRangeTo").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldPriceTo').val($('.priceRangeTo').val());
        console.log($('.priceRangeTo').val());
    });
});



//getting and setting values of add event page
$(document).on('pageshow', '#EventsView', function () {

    //applying theme
    var moodId = $('#ContentPlaceHolder1_HiddenFieldMoodId').val();
    selectedMood = moodId;
    applyTheme('#EventsView');


    //setting owner picture
    var ownerPhotoUrl = $('#ContentPlaceHolder1_HiddenFieldOwnerPhotoUrl').val();
    $('.ownerPicture').css('background-image', 'url(../../' + ownerPhotoUrl + ')');

    //click on owner's photo
    $(document).on("click", ".ownerPicture", function () {
        var ownerUrl = "../../profile/" + $('#ContentPlaceHolder1_HiddenFieldUsername').val();
        console.log("tryyyyyyyyyyyy" + ownerUrl);
        location.href = ownerUrl;
    });

    //setting the the remained time in days and hours and minutes
    var remainedMintes = 0;
    var remainedHours = 0;
    var remainedDays = 0;
    var AMorPM = "unknown";
    console.log($('#ContentPlaceHolder1_HiddenFieldDate').val());
    var str = $('#ContentPlaceHolder1_HiddenFieldDate').val();
    var startDateOriginalFormat = str.substring(0, str.indexOf(" "));
    var startHourOriginalFormat = str.substring(str.indexOf(" ") + 1, str.lastIndexOf(" "));
    var AMorPM = str.substring(str.lastIndexOf(" ") + 1);


    //converting the starting date to datestring
    fromDate = startDateOriginalFormat.split("/");
    fromHours = startHourOriginalFormat.split(":");
    if (AMorPM == "PM") {
        f = new Date(fromDate[2], fromDate[0] - 1, fromDate[1], fromHours[2] + 12, fromHours[1], fromDate[0], 0);
    } else if (AMorPM == "AM") {
        f = new Date(fromDate[2], fromDate[0] - 1, fromDate[1], fromHours[2], fromHours[1], fromDate[0], 0);
    } else {
        console.log("ERROR: couldn't read AM or PM!");
    }

    //testing the start time
    //console.log("****" + f.toLocaleDateString("en-US") + "****" + f.toLocaleTimeString("en-US"));

    //converting the start date datestring to miliseconds
    var startTime = f.getTime();
    console.log("f: " + f.getTime());


    function updateRemainingTimeText() {
        var now = $.now();

        //subtracting now from start time
        var differenceInMS = Math.abs(startTime - now);

        //calculating difference in days, hours, and minutes
        var x = differenceInMS / 1000;
        var seconds = x % 60;
        x /= 60;
        var minutes = Math.floor(x % 60);
        x /= 60;
        var hours = Math.floor(x % 24);
        x /= 24;
        var days = Math.floor(x);
        //console.log("days:" + days + "hours:" + hours + "minutes:" + minutes);

        //translating difference time to text
        var onlyParameter = "",
            daysParameters = "",
            daysAndParameter = "",
            hoursParameters = "",
            hoursAndParameter = "",
            minutesParameter = "";

        // write only if it's only minutes remaining
        if (days == 0 && hours == 0) { onlyParameters = "only " };
        //determining day parameters, daysAndParameter
        if (hours != 0 || minutes != 0) {
            daysAndParameter = "and ";
        }
        if (days == 1) {
            daysParameters = "One day " + daysAndParameter;
        } else if (days > 1) {
            daysParameters = days + " days " + daysAndParameter;
        }


        //determining hoursAndParameter
        if (minutes != 0) {
            hoursAndParameter = "and ";
        }
        //determining hoursParameters
        if (hours == 1) {
            hoursParameters = "One hour " + hoursAndParameter;
        } else if (hours > 1) {
            hoursParameters = hours + " hours " + hoursAndParameter;
        }


        //determining minutsParameters
        console.log("***********88" + minutes);
        if (minutes == 1) {
            minutesParameter = "One minute";
        } else if (minutes > 1) {
            minutesParameter = minutes + " minutes";
        }
        remainedTimeMessage = onlyParameter + daysParameters + hoursParameters + minutesParameter + " left.";
        $('.eventEachInformationStartTime').html(remainedTimeMessage);
    };
    updateRemainingTimeText();
    //calculate difference time every 60 seconds
    setInterval(function () { updateRemainingTimeText() }, 60000);


    //setting the duration from hours to days and hours
    var finalHours = 0;
    var finalDays = 0;
    var durationInHours = $('#ContentPlaceHolder1_HiddenFieldDuration').val();
    durationInHours = 48;
    if (durationInHours >= 24) {
        finalDays = Math.floor(durationInHours / 24);
        finalHours = durationInHours - finalDays * 24;
    }

    switch (finalDays) {
        case 0:
            $('.eventEachInformationDuration').html("It takes " + durationInHours + " hours");
            break;
        case 1:
            switch (finalHours) {
                case 0:
                    $('.eventEachInformationDuration').html("It takes " + finalDays + " day");
                    break;
                default:
                    $('.eventEachInformationDuration').html("It takes " + finalDays + " day and " + finalHours + " hours");
            }
            break;
        default:
            switch (finalHours) {
                case 0:
                    $('.eventEachInformationDuration').html("It takes " + finalDays + " days");
                    break;
                default:
                    $('.eventEachInformationDuration').html("It takes " + finalDays + " days and " + finalHours + " hours");
            }
            break;
    }
});


//setting the mood of event creation

$(document).on('click', '.artisticButton', function () {
    selectedMood = "1";
    $('#ContentPlaceHolder1_HiddenFieldMood').val(1);
});
$(document).on('click', '.geekButton', function () {
    selectedMood = "2";
    $('#ContentPlaceHolder1_HiddenFieldMood').val(2);
});
$(document).on('click', '.naughtyButton', function () {
    selectedMood = "3";
    $('#ContentPlaceHolder1_HiddenFieldMood').val(3);
});
$(document).on('click', '.romanticButton', function () {
    selectedMood = "4";
    $('#ContentPlaceHolder1_HiddenFieldMood').val(4);
});


var progressBar = {
    setValue: function (id, value) {
        $(id).val(value);
        $(id).slider("refresh");
    }
};


function createProgressBar(percent) {
    $(".progressBar").empty();
    $('<input>').appendTo('.progressBar').attr({ 'name': 'slider', 'class': 'progresSlider', 'data-highlight': 'true', 'min': '0', 'max': '100', 'value': '50', 'type': 'range' }).slider({
        create: function (event, ui) {
            $(this).parent().find('input').hide();
            $(this).parent().find('input').css('margin-left', '-9999px'); // Fix for some FF versions
            $(this).parent().find('.ui-slider-track').css('margin', '0 15px 0 15px');
            $(this).parent().find('.ui-slider-handle').hide();
        }
    }).slider("refresh");
    progressBar.setValue('.progresSlider', percent);
};

$(document).on('pageshow', '#Moods', function () {
    //creating progressbar    
    createProgressBar(33);
});





$(document).on('pageinit', '#settings_pref_edit', function () {

    var notification = $('#ContentPlaceHolder1_HiddenFieldNotifications').val();
    if (notification == "True") {
        $('#emailNotificationFlip').val('on').slider('refresh');
    } else {
        $('#emailNotificationFlip').val('off').slider('refresh');
    }

    $('#emailNotificationFlip').change(function () {
        if ($('#emailNotificationFlip').val() == 'on') {
            $('#ContentPlaceHolder1_HiddenFieldNotifications').val("True");
        }
        else {
            $('#ContentPlaceHolder1_HiddenFieldNotifications').val("False");
        }
    });



    var notification = $('#ContentPlaceHolder1_HiddenFieldNotifications').val();
    if (notification == "True") {
        $('#soundFlip').val('on').slider('refresh');
    } else {
        $('#soundFlip').val('off').slider('refresh');
    }

    $('#soundFlip').change(function () {
        if ($('#soundFlip').val() == 'on') {
            $('#ContentPlaceHolder1_HiddenFieldSound').val("True");
        }
        else {
            $('#ContentPlaceHolder1_HiddenFieldSound').val("False");
        }
    });

});

$(document).on('pageinit', '#settings_profPic_edit', function () {

    var photoURL = $('#ContentPlaceHolder1_HiddenFieldPhotoUrl').val();

    var photoURL = photoURL.substring(2);
    var photoURLString = "url(../" + photoURL + ")";
    $('.profilePicSmall').css("background-image", photoURLString);


    $("#date").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDOB').val($('#date').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldDOB').val());
    });

});


$(document).on('pageinit', '#settings_information_edit', function () {
    $.mobile.selectmenu.prototype.options.nativeMenu = false;
    var date = $('#ContentPlaceHolder1_HiddenFieldDOB').val();
    $('#date').val(date);


    $("#date").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDOB').val($('#date').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldDOB').val());
    });

});




$(document).on('pageinit', '#settings_information_edit', function () {
    $.mobile.selectmenu.prototype.options.nativeMenu = false;
    var date = $('#ContentPlaceHolder1_HiddenFieldDOB').val();
    $('#date').val(date);


    $("#date").change(function () {
        $('#ContentPlaceHolder1_HiddenFieldDOB').val($('#date').val());
        console.log($('#ContentPlaceHolder1_HiddenFieldDOB').val());
    });

});


$(document).on('pageinit', '#settings_location_edit', function () {

    var currentCity = $('#ContentPlaceHolder1_HiddenFieldCurrentCity').val();
    $('.ui-filterable input').attr("placeholder", currentCity);

    $(".ui-filterable input").change(function () {
        //console.log($('.ui-filterable input').attr("data-lastval"));
        //console.log($('.ui-filterable input').val());
        var city = $('.ui-filterable input').val();
        $('#ContentPlaceHolder1_HiddenFieldCurrentCity').val(city);
        //console.log($('#ContentPlaceHolder1_HiddenFieldCurrentCity').val());
    });


    var distance = $('#ContentPlaceHolder1_HiddenFieldDistance').val();
    $('#distanceSlider').val(distance).slider("refresh");

    $('#distanceSlider').change(function () {
        console.log($('#distanceSlider').val());
        $('#ContentPlaceHolder1_HiddenFieldDistance').val($('#distanceSlider').val());
    });




    var detect = $('#ContentPlaceHolder1_HiddenFieldLocationDetect').val();
    if (detect == "True") {
        $('#locationDetectionFlip').val('on').slider('refresh');
    } else {
        $('#locationDetectionFlip').val('off').slider('refresh');
    }

    $('#locationDetectionFlip').change(function () {
        if ($('#locationDetectionFlip').val() == 'on') {
            $('#ContentPlaceHolder1_HiddenFieldLocationDetect').val("True");
        }
        else {
            $('#ContentPlaceHolder1_HiddenFieldLocationDetect').val("False");
        }
    });
});


$(function () {

    //using external panel
    var profileLink = $('#HiddenFieldUsername').val();
    var completeLink = "../profile/" + profileLink;
    $('.profileLink').attr("href", completeLink);
    //using external header and footer
    $("[data-role='header'], [data-role='footer']").toolbar({ theme: "a" });
    $("[data-role=panel]").enhanceWithin().panel();

});


$(document).on("pageinit", "#settings_location_edit", function () {
    //location autoComplete
    $("#autocomplete").on("filterablebeforefilter", function (e, data) {
        var $ul = $(this),
            $input = $(data.input),
            value = $input.val(),
            html = "";
        $ul.html("");
        if (value && value.length > 2) {
            $ul.html("<li><div class='ui-loader'><span class='ui-icon ui-icon-loading'></span></div></li>");
            $ul.listview("refresh");
            $.ajax({
                url: "http://gd.geobytes.com/AutoCompleteCity",
                dataType: "jsonp",
                crossDomain: true,
                data: {
                    q: $input.val()
                }
            })
            .then(function (response) {
                $.each(response, function (i, val) {
                    html += "<li>" + val + "</li>";
                });
                $ul.html(html);
                $ul.listview("refresh");
                $ul.trigger("updatelayout");
            });
        }
    });
});