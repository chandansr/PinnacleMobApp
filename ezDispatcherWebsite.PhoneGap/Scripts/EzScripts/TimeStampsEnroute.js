﻿
$(document).ready(function () {
    
});


function GetEnrouteTimeStamp() {
    
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetStatusWiseDynamicFields;
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var status = getParameterByName("Status");
    var message = "";
    //var Page = $('[data-mypage]:visible').data('mypage');
    var Page = $.mobile.activePage.data('mypage');
    var DynamicFieldHtml = "";
    var time = new Date().getTimezoneOffset();
    var TimeStampsInfo = { Id: Id, CallId: CallId, Status: status, offset: time };
    if (Page == "TimeStampEnroute") {
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
                    $.each(result, function (i1, v1) {
                        if (v1.MasterFieldID != "") {
                            DynamicFieldHtml = DynamicFieldHtml + "<div class='EnrouteDynamicField' MasterFieldId='" + v1.MasterFieldID + "'>"
                                               + "<h5 style='margin-bottom: 5px;'>" + v1.FieldName + "</h5>"
                                               + "<input type='text' name='" + v1.FieldName + "' id='txt" + v1.FieldName + "' value='' placeholder='Enter the " + v1.FieldName + "' /></div><div class='spacer5'></div>";
                        }
                    });
                    if (result[0].IsDelay == "1") {
                        $("#EnrouteDelayReason").show();
                    }
                    else {
                        $("#EnrouteDelayReason").hide();
                    }
                    $('.SpanTripName:visible').html('Trip: ' + result[0].Trip);
                    $('.SpanUnitName:visible').html('Unit: ' + result[0].Unit);
                }
                //$("#StatusHead").html("Status: " + status);
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                $("#EnrouteDynamicFileds").html(DynamicFieldHtml);
                new ezphonemessege().hide();
            }
        });
    }
}


function GetCallInfoUrl(path) {
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var LocReq = getParameterByName("LocReq");
    var CurrentCallId = getParameterByName("CurrentCallId");
    $.mobile.changePage(path, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
}



function SaveEnrouteTimeStamps() {
    new ezphonemessege().show({
        msg: 'Processing..Please wait..'
    });
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTimeStampWithoutMilege;
    var CallId = getParameterByName("CallId");
    var ParamedicId = getParameterByName("Id");
    var status = getParameterByName("Status");
    var CurrentCallId = getParameterByName("CurrentCallId");

    var hasJson = 0;
    var jsondata = new Object();
    jsondata.CallId = CallId;
    jsondata.Obj = [];
    $(".EnrouteDynamicField").each(function () {
        hasJson = 1;
        var $this = $(this);
        jsondata.Obj.push({
            MasterFId: $this.attr('masterfieldid'),
            MasterFValue: $this.find('input[type="text"]').first().val()
        });
    });
    jsondata.HasJson = hasJson;
    if ($('#EnrouteDelayReason').is(':hidden')) {
        jsondata.IsDelay = "0";
    }
    else {
        jsondata.IsDelay = "1";
        jsondata.DelayReason = $('#txtDelayReasonEnroute').val();
    }
    var senddata = JSON.stringify(jsondata);
    var LocReq = getParameterByName("LocReq");
    var time = new Date().getTimezoneOffset();
    var sucess = "0";
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
                    UpdateCrewLoc();                    
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
                $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide"});
            }
        }
    });
}


