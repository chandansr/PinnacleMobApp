
$(document).ready(function () {

    GetSupVCallDetails();
    
    $(".SidePanel").panel({
        open: function (event, ui) {
            $(this).find('.CloseForFocus').focus();
        }
    });

    $("#btnGetScene").bind("click", function (event, ui) {
        GetScene();
    });
    $("#btnGetDestination").bind("click", function (event, ui) {
        GetDestination();
    });

});


function GetSupVCallDetails() {
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetData;
    var PdId = getParameterByName("Id");
    var Loc = getParameterByName("LocReq");
    var CallID = getParameterByName("CallId");        
    //var Page = $('[data-mypage]:visible').data('mypage');
    var Page = $.mobile.activePage.data('mypage');
    var message = "";
    var ulhtml = "";
    var IsShow = "0";
    var DispatcherCalls = {
        PDID:PdId,
        CallId: CallID,
    };
    if (Page == "sup-CallDetails") {
        
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
                    var fnsec = new fn_security();
                    for (i = 0; i < result.length; i++) {

                        if (globalVar._IsEncryptionReq == "1") {
                            result[i].PickUpDetails = fnsec.decrypt({ value: result[i].PickUpDetails });
                            result[i].FromAddress = fnsec.decrypt({ value: result[i].FromAddress });
                            result[i].ToAddress = fnsec.decrypt({ value: result[i].ToAddress });
                            result[i].ContactName = fnsec.decrypt({ value: result[i].ContactName });
                            result[i].Contact = fnsec.decrypt({ value: result[i].Contact });
                            result[i].Alerts = fnsec.decrypt({ value: result[i].Alerts });
                        }

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
                        $('#currentstatus').html('<b> Status :</b> <span style="color:brown;">' + result[i].CurrentStatus + '</span>');                                                
                    }
                }
                else {
                    $("#ChangeStatusDiv").hide();
                }

            },
            error: function (xhr) {                
            },
            complete: function () {                                
                new ezphonemessege().hide();                
            }
        });
    }
}


function GetSupVCallInfoUrl(path) {
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var LocReq = getParameterByName("LocReq");
    var CurrentCallId = GetCrewCurrentCallId();

    $.mobile.changePage(path, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });    
}



function SupVSetGoogleLocation() {
    var from = $('.FromAddress').text();
    var to = $('.ToAddress').text();    
    var Unit = $('#UnitName').text();
    
    $.mobile.changePage(_SiteUrl.sup_ToFromMaps, {
        data: { 'origin': from, 'Destination': to, 'Id': getParameterByName("Id"), 'CallId': getParameterByName("CallId"), 'LocReq': getParameterByName("LocReq"), 'Unit': Unit },
        transition: 'flip'
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
