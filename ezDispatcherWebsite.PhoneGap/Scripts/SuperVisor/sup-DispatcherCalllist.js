
$(document).ready(function () {

});


function GetSupVCallList() {
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetSupVCallList;
    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");
    var message = "";
    var time = new Date().getTimezoneOffset();
    var status = getParameterByName("StatusId");
    var UnitId = getParameterByName("IsAllUnit") == 1 ? '' : _SuperVisorUnits._CurrentSelected.toString();
    var IsFilterByStatus = getParameterByName("IsFilterByStatus");
    
    var Page = $.mobile.activePage.data('mypage');
    var li = "";
    var count = 0;
    var SuperVisorUnitsDetails = { Id: Id, Status: status, UnitIds: UnitId, Offset: time };
    if (Page == "sup-CallList") {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: GetDataUrl,
            data: JSON.stringify({ "SuperVisorUnitsDetails": SuperVisorUnitsDetails }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                new ezphonemessege().show({
                    msg: 'Processing..Please wait..'
                });
            },
            success: function (data) {
                var result = data.Data;

                if (result.length > 0) {
                    count = 1;                    
                    for (i = 0; i < result.length; i++) {

                        if (result[i].CallIntakeID == '-1') {
                            li = li + '<li class="liList" data-theme="f" CallId= "' + result[i].CallIntakeID + '" Date= " ' + result[i].PickupDate + ' " Unit="' + result[i].AsignUnit + '" UnitId="' + result[i].AsignUnit + '" Unitstatus="' + result[i].UnitStatus + '" IsOnline="' + result[i].LevelResponse + '" CrewLat="' + result[i].Scene + '" CrewLong="' + result[i].Destination + '">';
                            li = li + ' <a class="Call" CallId ="' + result[i].CallIntakeID + '">';
                            li = li + ' <div style="float: left;" ><h3 style="width: 100%"> ' + result[i].AsignUnit + '</h3>'
                            li = li + '<span class="ui-li-desc">Available Since: <span id="ul-li-desc-avlsincespan">' + result[i].PickupDate + ' [' + result[i].ReturnTime + ']' + '</span> </span>'
                            li = li + '<div style="clear:both"></div>';
                            li = li + '<span class="ui-li-desc">Primary Crew: <span id="ul-li-desc-avlcrewname">' + result[i].FullName + ' </span></span></div></a>';
                            li = li + '  </a></li>';
                        }
                        else {
                            li = li + '<li class="liList"  CallId= "' + result[i].CallIntakeID + '" Date= " ' + result[i].PickupDate + ' " Unit="' + result[i].AsignUnit + '" UnitId="' + result[i].AsignUnit + '" Unitstatus="' + result[i].UnitStatus + '">';
                            li = li + ' <span class="ui-li-aside" style="margin-right:3%;width:17%;font-weight:bold;font-size:small;">' + result[i].PickupTime + '</span>';
                            li = li + ' <a class="Call" CallId ="' + result[i].CallIntakeID + '">';
                            li = li + ' <div style="float: left;" ><h3 style="width: 100%"> ' + result[i].CallId + '</h3>'
                            li = li + '<span class="ui-li-desc"><strong>' + result[i].CallType + '</strong></span>';
                            li = li + '<div style="clear:both"></div>';
                            li = li + '<span class="ui-li-desc">Pick-up Date: ' + result[i].PickupDate + ' </span>'
                            li = li + '<div style="clear:both"></div>';
                            li = li + ' <span class="ui-li-desc">Patient: ' + result[i].FullName + ' </span>'
                            li = li + ' <span class="ui-li-desc">Scene: ' + result[i].Scene + ' </span>'
                            li = li + ' <span class="ui-li-desc">Destination: ' + result[i].Destination + ' </span>'
                            li = li + ' <span class="ui-li-desc">Level & Response: ' + result[i].LevelResponse + ' </span>'
                            li = li + ' <span class="ui-li-desc">Reason for Tranportation: ' + result[i].ReasonForTrans + ' </span>'
                            li = li + '<span id="pAddress" style="display:none;" class="ui-li-desc GoogleLocation">' + result[i].FromAddress + '</span></div></a>'
                            li = li + '  </a></li>';
                        }

                        $('#sup-lstView').attr('data-NoCalls', '0');
                    }
                }
                else {
                    li = '<li class="liList" style="height:30px;"><div style="float: left;" ><h3 style="width: 100%"> No Calls Available.</h3></div>';
                    $('#sup-lstView').attr('data-NoCalls', '1');
                }
            },
            error: function (xhr) {
            },
            complete: function () {
                $('#sup-lstView').append(li).trigger('create');
                $("#sup-lstView").listview('refresh');
                if (count > 0) {
                    $("#sup-lstView").listview({
                        autodividers: true,
                        dividerTheme: "b",
                        autodividersSelector: function (li) {
                            var unitname = li.attr("unit").trim();
                            var statusname = li.attr('unitstatus').trim();                            
                            var out = unitname;
                            return ([out, '_#_' + statusname]);
                        }
                    }).listview("refresh").trigger('afterRefresh');
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

function AppendNewCallToSupList(result) {

    var li = $('#sup-lstView').find('li[callid="' + result.CurrentCallId + '"]').html();
    if (li == '' || li == null) {

        li = '<li class="liList" CallId= "' + result.CallIntakeID + '" Date= " ' + result.PickupDate + ' " Unit="' + result[i].ASSIGNEDUNIT + '">';
        li = li + '<span class="ui-li-aside" style="margin-right:2%;width:17%;font-weight:normal;font-size:small;">' + result.PickupTime + '</span>';
        li = li + ' <a class="Call" CallId ="' + result.CallIntakeID + '"><img class="ui-corner-all" src="images/Listimages/ambulance.jpg" style="margin-top:20px;margin-left:10px;"  />';
        li = li + ' <div style="float: left;" ><h3 style="width: 100%"> ' + result.CallId + '</h3>';
        li = li + '<span class="ui-li-desc">' + result.CallType + ' </span>';
        li = li + '<div style="clear:both"></div>';
        li = li + '<span class="ui-li-desc">' + result.PickupDate + ' </span>'
        li = li + '<div style="clear:both"></div>';
        li = li + ' <span class="ui-li-desc">' + result.FullName + ' </span>'
        li = li + '<span id="pAddress" class="ui-li-desc GoogleLocation">' + result.FromAddress + '</span></div></a>'
        li = li + '  </a></li>';

        $('#sup-lstView').prepend(li).trigger('create');
        $("#sup-lstView").listview({
            autodividers: true,
            autodividersSelector: function (li) {
                var out = li.attr("Unit");
                return out;
            }
        }).listview("refresh");
        $("#sup-lstView").listview('refresh');
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


function GetSupAvlUnitGoogleLocation() {    
    var Unit = $('#spAvl-unitname').text();
    $.mobile.changePage(_SiteUrl.sup_ToFromMaps, {
        data: { 'origin': '', 'Destination': '', 'Id': getParameterByName("Id"), 'CallId': '0', 'LocReq': getParameterByName("LocReq"), 'Unit': Unit },
        transition: 'slide'
    });
}