
$(document).ready(function () {

});


function GetCallList() {
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._CallList;
    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");
    var message = "";
    var time = new Date().getTimezoneOffset();
    //var Page = $('[data-mypage]:visible').data('mypage');
    var Page = $.mobile.activePage.data('mypage');
    var li = "";
    var count = 0;

    if (Page == "CallList") {
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: GetDataUrl,
            data: { Id: Id, Offset: time },
            dataType: "json",
            beforeSend: function () {
                new ezphonemessege().show({
                    msg: 'Processing..Please wait..'
                });
            },
            success: function (data) {
                var result = data.Data;
                // var ul = $("#lstView");

                if (result.length > 0) {
                    count = 1;
                    for (i = 0; i < result.length; i++) {
                        var datatheme;
                        var NoteHistory = "";

                        if (result[i].CurrentCallId == result[i].CallIntakeID) {
                            datatheme = " data-theme='f'";                            
                        }
                        else if (result[i].IsComplete != "") {
                            datatheme = " data-theme='e'";
                        }
                        else {
                            datatheme = "";
                        }
                                                
                        li = '<li class="liList"' + datatheme + ' CallId= "' + result[i].CallIntakeID + '" Date= " ' + result[i].PickupDate + ' "><span class="ui-li-aside" style="margin-right:3%;width:17%;font-weight:normal;font-size:small;">' + result[i].PickupTime + '</span><a class="Call" CallId ="' + result[i].CallIntakeID + '"><img class="ui-corner-all" src="images/Listimages/ambulance.jpg" style="margin-top:20px;margin-left:10px;"  />';
                        li = li + ' <div style="float: left;" ><h3 style="width: 100%"> ' + result[i].CallId + '</h3>'
                        li = li + '<span class="ui-li-desc">' + result[i].CallType + ' </span>';
                        li = li + '<div style="clear:both"></div>';
                        li = li + '<span class="ui-li-desc">' + result[i].PickupDate + ' </span>'
                        li = li + '<div style="clear:both"></div>';
                        li = li + ' <span class="ui-li-desc">' + result[i].FullName + ' </span>'                        
                        li = li + '<span id="pAddress" class="ui-li-desc GoogleLocation">' + result[i].FromAddress + '</span></div></a>'
                        li = li + '  </a></li>';

                        $('#lstView').attr('data-NoCalls', '0');
                        $('#lstView').append(li).trigger('create');
                        $("#lstView").listview('refresh');
                    }
                }
                else {                    
                    li = '<li class="liList" style="height:30px;"><div style="float: left;" ><h3 style="width: 100%"> No Calls Available.</h3></div>';
                    $('#lstView').attr('data-NoCalls', '1');
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                $('#lstView').append(li).trigger('create');
                $("#lstView").listview('refresh');
                if (count > 0) {
                    $("#lstView").listview({
                        autodividers: true,
                        autodividersSelector: function (li) {
                            var out = li.attr("Date");
                            return out;
                        }
                    }).listview("refresh");
                }
                new ezphonemessege().hide();
            }
        });
    }
}

function SetGoogleLocationList(link) {

    var argValue = 'origin=-1&Destination=' + link.innerHTML + '&Id=' + getParameterByName("Id") + '&LocReq=' + getParameterByName("LocReq");

    $.mobile.changePage(_SiteUrl.Maps, {
        data: { 'origin': '-1', 'Destination': link.innerHTML, 'Id': getParameterByName("Id"), 'LocReq': getParameterByName("LocReq") },
        transition: 'flip'
    });
}


function GetInfoUrl(path, link) {

    var Id = getParameterByName("Id");
    var CallId = $(link).parent('a').attr('id');
    var LocReq = getParameterByName("LocReq");
    var CurrentCallId = GetCrewCurrentCallId();

    $.mobile.changePage(path, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
}

function requestPatientAddress() {
    var CurrentCallId = $('#lstView li[data-theme="f"]').attr('callid');
    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");
    $.mobile.changePage(_SiteUrl.Notes_RequestPatientAddress, { data: { "Id": Id, "CallId": CurrentCallId, "LocReq": LocReq }, transition: "slide" });
}

function AppendNewCallToList(result) {

    var li = $('#lstView').find('li[callid="' + result.CurrentCallId + '"]').html();
    if (li == '' || li == null) {
        var datatheme;
        if (result.CurrentCallId == result.CallIntakeID) {
            datatheme = " data-theme='f'";
        }
        else if (result.IsComplete != "") {
            datatheme = " data-theme='e'";
        }
        else {
            datatheme = "";
        }

        li = '<li class="liList"' + datatheme + ' CallId= "' + result.CallIntakeID + '" Date= " ' + result.PickupDate + ' "><span class="ui-li-aside" style="margin-right:2%;width:17%;font-weight:normal;font-size:small;">' + result.PickupTime + '</span><a class="Call" CallId ="' + result.CallIntakeID + '"><img class="ui-corner-all" src="images/Listimages/ambulance.jpg" style="margin-top:20px;margin-left:10px;"  />';
        li = li + ' <div style="float: left;" ><h3 style="width: 100%"> ' + result.CallId + '</h3>'
        li = li + '<span class="ui-li-desc">' + result.CallType + ' </span>';
        li = li + '<div style="clear:both"></div>';
        li = li + '<span class="ui-li-desc">' + result.PickupDate + ' </span>'
        li = li + '<div style="clear:both"></div>';
        li = li + ' <span class="ui-li-desc">' + result.FullName + ' </span>'
        li = li + '<span id="pAddress" class="ui-li-desc GoogleLocation">' + result.FromAddress + '</span></div></a>'
        li = li + '  </a></li>';

        $('#lstView').prepend(li).trigger('create');
        $("#lstView").listview({
            autodividers: true,
            autodividersSelector: function (li) {
                var out = li.attr("Date");
                return out;
            }
        }).listview("refresh");
        $("#lstView").listview('refresh');
        if (result.CallType.trim() == '[EMERGENCY CALL]') {
            $('#CallListAlertSpan').addClass('ECallListAlert');
        } else {
            $('#CallListAlertSpan').addClass('NECallListAlert');
        }
        $('#CallListAlertSpan').html('New Call').fadeIn();
        if (navigator.notification) {
            navigator.notification.beep(4);
            navigator.notification.vibrate(6000);
        }
    }

}