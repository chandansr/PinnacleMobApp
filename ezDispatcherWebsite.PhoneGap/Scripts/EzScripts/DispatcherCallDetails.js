
$(document).ready(function () {

});


function GetCallDetails() {
    
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetData;
    var PdId = getParameterByName("Id");
    var Loc = getParameterByName("LocReq");
    var CallID = getParameterByName("CallId");       
    
    var Page = $.mobile.activePage.data('mypage');
    var message = "";
    var ulhtml = "";
    var IsShow = "0";
    var DispatcherCalls = {
        PDID: PdId,
        CallId: CallID,
    };
    if (Page == "CallDetails") {
        $.ajax({
            type: "POST",
            async: false,
            url: GetDataUrl,
            data: JSON.stringify({ "DispatcherCalls": DispatcherCalls }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                new ezphonemessege().show({
                    msg: 'Processing Please wait..'
                });
            },
            success: function (data) {
                var result = data.Data;                
                if (result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        $("#UnitName").html('<span style="color:#ADEF6F;">' + result[i].Unit + '</span>');
                        $("#Trip").html('<span style="color:#ADEF6F;">' + result[i].Trip + '</span>');
                        $("#PickUpTime").html(result[i].PickupTime);
                        $("#PickUpDetails").html(result[i].PickUpDetails);                        
                        $("#ToAddress").html(result[i].ToAddress);
                        $("#ContactName").html(result[i].ContactName);

                        $("#QDPatientName").html(result[i].ContactName);
                        $("#QDLevelOfService").html(result[i].LevelOfService);
                        $("#QDLevelOfResponse").html(result[i].LevelOfResponse);
                        $("#QDLevelOfTransport").html(result[i].LevelOfTransport);
                        $("#QDNOEHead").html(result[i].ISNOE == "True" ? "Nature of Emergency" : "Reason for Transportation");
                        $("#QDNatureOfEmergency").html(result[i].NatureOfEmergency);
                        $("#QDAlerts").html(result[i].Alerts);
                        
                        if (result[i].CurrentCallID != result[i].CallId) {
                            $("#ChangeStatusDiv").hide();
                            $("#AddPatientAddressNote").hide();
                            $('#currentstatus').hide();
                            $('#currentstatus').html('<b>Current Status :</b> <span style="color:brown;">' + result[i].CurrentStatus + '</span>');                                                        
                            if (result[i].IsPatientInfoProvidedByCrew == "False" && result[i].CurrentStatus != "Cancelled") {
                                $("#AddPatientAddressNote").show();
                            }
                        }
                        else {
                            var status = result[i].CurrentStatusString.split(',');
                            // the first status is the curent status
                            $('#currentstatus').html('<b>Current Status :</b> <span style="color:brown;">' + status[0] + '</span>');
                            $('#LeftPanelCurrentStatus').html(status[0]);
                            $("#ChangeStatusDiv").show();

                            if (result[i].IsPatientInfoProvidedByCrew == "False") {
                                $("#AddPatientAddressNote").show();
                            }
                            IsShow = "1";
                            ulhtml = "";                           
                            
                            switch (status[0]) {
                                case "Dispatched":
                                    $('#btnCSEnroute').show();
                                    $('#btnCSOnScene').show();
                                    $('#btnCSContact').show();
                                    $('#btnCSTransport').show();
                                    $('#btnCSArrived').show();
                                    $('#btnCSClear').show();
                                    break;
                                case "Enroute":
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').show();
                                    $('#btnCSContact').show();
                                    $('#btnCSTransport').show();
                                    $('#btnCSArrived').show();
                                    $('#btnCSClear').show();
                                    break;
                                case "OnScene":
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').closest('.ui-btn').hide();
                                    $('#btnCSContact').show();
                                    $('#btnCSTransport').show();
                                    $('#btnCSArrived').show();
                                    $('#btnCSClear').show();
                                    break;
                                case "Contact":                                    
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').closest('.ui-btn').hide();
                                    $('#btnCSContact').closest('.ui-btn').hide();
                                    $('#btnCSTransport').show();
                                    $('#btnCSArrived').show();
                                    $('#btnCSClear').show();
                                    break;
                                case "Transport":
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').closest('.ui-btn').hide();
                                    $('#btnCSContact').closest('.ui-btn').hide();
                                    $('#btnCSTransport').closest('.ui-btn').hide();
                                    $('#btnCSArrived').show();
                                    $('#btnCSClear').show();
                                    break;
                                case "Arrived":                                    
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').closest('.ui-btn').hide();
                                    $('#btnCSContact').closest('.ui-btn').hide();
                                    $('#btnCSTransport').closest('.ui-btn').hide();
                                    $('#btnCSArrived').closest('.ui-btn').hide();
                                    $('#btnCSClear').show();
                                    break;
                                case "Clear":
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').closest('.ui-btn').hide();
                                    $('#btnCSContact').closest('.ui-btn').hide();
                                    $('#btnCSTransport').closest('.ui-btn').hide();
                                    $('#btnCSArrived').closest('.ui-btn').hide();
                                    $('#btnCSClear').closest('.ui-btn').hide();
                                    break;
                                default:
                                    $('#btnCSEnroute').closest('.ui-btn').hide();
                                    $('#btnCSOnScene').closest('.ui-btn').hide();
                                    $('#btnCSContact').closest('.ui-btn').hide();
                                    $('#btnCSTransport').closest('.ui-btn').hide();
                                    $('#btnCSArrived').closest('.ui-btn').hide();
                                    $('#btnCSClear').closest('.ui-btn').hide();
                                    break;
                            }
                           
                        }
                    }
                }
                else {
                    $("#ChangeStatusDiv").hide();
                }

            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {                
                new ezphonemessege().hide();                
            }
        });
    }
}


function GetCallInfoUrl(path) {
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var LocReq = getParameterByName("LocReq");
    var CurrentCallId = GetCrewCurrentCallId();

    $.mobile.changePage(path, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });    
}


function GetTimeStampUrl(path, link) {
    new ezphonemessege().show({
        msg: 'Processing..Please wait..'
    });
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");    
    var status = link;
    var LocReq = getParameterByName("LocReq");
    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetDelayStatus;
    var time = new Date().getTimezoneOffset();
    var CurrentCallId = GetCrewCurrentCallId();
    var DelayStatus = { Id: Id, CallId: CallId, Status: status, Offset: time };
    
    var Ajax = $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "JSON",
        data: JSON.stringify({ "DelayStatus": DelayStatus }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
            new ezphonemessege().show({
                 msg: 'Processing..Please wait..'
            });
        },
        error: function (xhr) {

        }
    });

    Ajax.done(function (result) {
        
        new ezphonemessege().hide();
        if (result.Data[0].ActUnitDelayStatus.Status == "Clear") {
            $("#StatusInfoMessage").fadeIn("slow");
            GetCallDetails();
        }
        else {
            if (status == "Dispatched" && result.Data[0].MasterFieldID != null) {
                $.mobile.changePage(_SiteUrl.TimeStampDispatched, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else if (status == "Enroute" && result.Data[0].MasterFieldID != null) {
                $.mobile.changePage(_SiteUrl.TimeStampEnroute, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else if (status == "OnScene" && result.Data[0].MasterFieldID != null) {
                $.mobile.changePage(_SiteUrl.TimeStampOnScene, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else if (status == "Contact" && result.Data[0].MasterFieldID != null) {
                $.mobile.changePage(_SiteUrl.TimeStampContact, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else if (status == "Transport") {
                $.mobile.changePage(_SiteUrl.TimeStampTransport, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else if (status == "Arrived" && result.Data[0].MasterFieldID != null) {
                $.mobile.changePage(_SiteUrl.TimeStampArrived, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else if (status == "Clear" && result.Data[0].MasterFieldID != null) {
                $.mobile.changePage(_SiteUrl.TimeStampClear, { data: { "Id": Id, "CallId": CallId, "Status": status, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
            else {
                SaveTimeStampsWithoutMilege(status);
            }
        }

    });
}



function SetGoogleLocation(link) {

    var argValue = 'origin=' + $("#FromAddress").html() + '&Destination=' + $("#ToAddress").html() + '&Id=' + getParameterByName("Id") + '&CallId=' + getParameterByName("CallId") + '&LocReq=' + getParameterByName("LocReq");    
    var from = $('.FromAddress').text();
    var to = $('.ToAddress').text();
    
    $.mobile.changePage(_SiteUrl.ToFromMaps, {
        data: { 'origin': from, 'Destination': to, 'Id': getParameterByName("Id"), 'CallId': getParameterByName("CallId"), 'LocReq': getParameterByName("LocReq") },
        transition: 'flip'
    });
}


function SaveTimeStampsWithoutMilege(status) {

    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTimeStampWithoutMilege;
    var CallId = getParameterByName("CallId");
    var ParamedicId = getParameterByName("Id");
    var status = status;
    var LocReq = getParameterByName("LocReq");
    var time = new Date().getTimezoneOffset();
    var sucess = "0";
    var IsDelay = "0";
    var DelayReason = "";

    var hasJson = 0;
    var jsondata = new Object();
    jsondata.CallId = CallId;
    jsondata.Obj = [];
    jsondata.HasJson = hasJson;
    var senddata = JSON.stringify(jsondata);
    var TimeStampsInfo = { Id: ParamedicId, CallId: CallId, Status: status, offset: time, JsonData: senddata };

    var message = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: GetDataUrl,
        data: JSON.stringify({ "TimeStampsInfo": TimeStampsInfo }),
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

                var xml = $.parseXML(result);
                var $xml = $(xml);
                //$test = $xml.find('ErrorProcedure');

                if ($xml.find('ErrorProcedure').text() == "SUCCESS") {
                    sucess = "1";
                    
                    navigator.geolocation.getCurrentPosition(function (pos) {
                        lat = pos.coords.latitude;
                        lng = pos.coords.longitude;
                        getAddressFromLatLang(lat, lng);                        
                    });                    
                }
                else {
                    $("#StatusHead").html("Some error occured. Please try again after some time.");
                }
            }
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
            if (sucess == "1") {            
                $("#GenInfoMessage").fadeIn("slow");
                $("#PanelForChangingStatus").panel("close");
                GetCallDetails();
            }
        }
    });
}


function UpdateCrewLocAfterStatus(lat, lng) {

    var Id = getParameterByName("Id");    
    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveCrewLocation;
    var CrewLocation={ Id: Id, Lat: lat, Lnt: lng };
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "CrewLocation": CrewLocation }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {            
        },
        success: function (result) {

        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {            
        }
    });
}



function SetSceneDesLocation(link) {

    var argValue = 'origin=' + $("#FromAddress").html() + '&Destination=' + $("#ToAddress").html() + '&Id=' + getParameterByName("Id") + '&CallId=' + getParameterByName("CallId") + '&LocReq=' + getParameterByName("LocReq");
    var from = '-1';
    var to = $('#ToAddress').html().split('<b>Address:</b><br>')[1].replace('<br>', ' ');
    $.mobile.changePage(_SiteUrl.Maps, {
        data: { 'origin': from, 'Destination': to, 'Id': getParameterByName("Id"), 'CallId': getParameterByName("CallId"), 'LocReq': getParameterByName("LocReq") },
        transition: 'flip'
    });
}


function GetScene() {
    if (navigator.notification) {
        var scene = $(".FromAddress").text();
        switch (device.platform) {
            case "Android": 
                var url = 'geo:0,0?q=' + scene;
                window.location = url;
                break;
            case "iOS":
                var url = 'maps:q=' + scene;
                window.location = url;
                break;
        }
    } else {

    }
}

function GetDestination() {
    if (navigator.notification) {
        var destination = $(".ToAddress").text();
        switch (device.platform) {
            case "Android":
                var url = 'geo:0,0?q=' + destination;                
                window.location = url;
                break;
            case "iOS":
                var url = 'maps:q=' + destination;
                window.location = url;
                break;  
        }
    } else {

    }
}

function AddPatientAddressNote() {
    var CallId = getParameterByName("CallId");
    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");
    var Status = $('#currentstatus').text().split(':')[1].trim();
    var CurrentCallId = GetCrewCurrentCallId();
    $.mobile.changePage(_SiteUrl.Notes_RequestPatientAddress, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "Status": Status, "CurrentCallId": CurrentCallId }, transition: "slide" });
}

