
$(document).ready(function () {
    
});


function GetTransportTimeStamp() {
    
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetCallDetailsForTimeStamp;
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var status = getParameterByName("Status");
    var message = "";        
    var Page = $.mobile.activePage.data('mypage');
    var TimeStampsInfo = { Id: Id, CallId: CallId };
    if (Page == "TimeStamp") {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: GetDataUrl,
            data: JSON.stringify({ "TimeStampsInfo": TimeStampsInfo }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $.mobile.showPageLoadingMsg("a", "Loading...");
            },
            success: function (data) {
                var result = data.Data;
                
                if (result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        $("#UnitName").html(result[i].Unit);
                        $("#Trip").html(result[i].Trip);
                    }
                    if (result[0].CurrentLOT == "") {
                        var selectlist = $('#selectLOT');
                        selectlist.empty();

                        $.each(result[0].LOTString, function (i1, v1) {
                            if (v1.value != "") {
                                $('<option></option>').val(v1.value).text(v1.text).appendTo(selectlist);
                            }
                        });
                        $('#selectLOT').selectmenu('refresh', true);                     
                    }
                    else {
                        var selectlist = $('#selectLOT');
                        selectlist.empty();
                        $('<option></option>').val(result[0].CurrentLOTID).text(result[0].CurrentLOT).appendTo(selectlist);
                        $('#selectLOT').selectmenu('refresh', true);
                        $('#selectLOT').selectmenu('disable');
                    }
                    if (result[0].CurrentOutCome == "") {
                        var selectlist1 = $('#selectOutCome');
                        selectlist1.empty();

                        $.each(result[0].OutComeString, function (i1, v1) {
                            if (v1.value != "") {
                                $('<option></option>').val(v1.value).text(v1.text).appendTo(selectlist1);
                            }
                        });

                        $('#selectOutCome').selectmenu('refresh', true);
                    }
                    else {
                        var selectlist = $('#selectOutCome');
                        selectlist.empty();
                        $('<option></option>').val(result[0].CurrentOutComeID).text(result[0].CurrentOutCome).appendTo(selectlist1);
                        $('#selectOutCome').selectmenu('refresh', true);
                        $('#selectOutCome').selectmenu('disable');
                    }
                }
                $("#StatusHead").html("Status: " + status);
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                $.mobile.loading('hide');
            }
        });
    }
}

function GetCallInfoUrl(path) {
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var LocReq = getParameterByName("LocReq");
    $.mobile.changePage(path, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
}




function SaveTransportTimeStamps() {

    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTransportTimeStamps;
    var CallId = getParameterByName("CallId");
    var ParamedicId = getParameterByName("Id");
    var status = getParameterByName("Status");
    var CallNo = null;
    var Milege = $("#txtMilege").val();
    var Desc = null;
    var LocReq = getParameterByName("LocReq");
    var time = new Date().getTimezoneOffset();
    var sucess = "0";
    var LOT = $("#selectLOT").val();
    var OutCome = $("#selectOutCome").val();
    var TimeStampsInfo = { Id: ParamedicId, CallId: CallId, Status: status, CallNo: CallNo, Milege: Milege, Desc: Desc, offset: time, LOT: LOT, OutCome: OutCome };
    debugger;
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
            $.mobile.showPageLoadingMsg("a", "Loading....");
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
                $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
            }
        }
    });
}


function UpdateCrewLoc() {
    
    var lat;
    var lng;
    var Id = getParameterByName("Id");
    navigator.geolocation.getCurrentPosition(function (pos) {
        lat = pos.coords.latitude;
        lng = pos.coords.longitude;        
    });

    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveCrewLocation;
    var CrewLocation = { Id: Id, Lat: lat, Lnt: lng };
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "CrewLocation": CrewLocation }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
            $.mobile.showPageLoadingMsg("a", "Loading...");
        },
        success: function (result) {

        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
            $.mobile.loading('hide');
        }
    });
}


function GetGeneralTimeStamp() {
 
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetCallDetailsForTimeStamp;
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var status = getParameterByName("Status");
    var message = "";        
    var Page = $.mobile.activePage.data('mypage');
    var TimeStampsInfo = { Id: Id, CallId: CallId };
    
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: GetDataUrl,
            data: JSON.stringify({ "TimeStampsInfo": TimeStampsInfo }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $.mobile.showPageLoadingMsg("a", "Loading...");
            },
            success: function (data) {
                var result = data.Data;
                if (result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        $(".SpanUnitName").html(result[i].Unit);
                        $(".SpanTripName").html(result[i].Trip);
                    }                    
                }
               // $("#StatusHead").html("Status: " + status);
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                $.mobile.loading('hide');
            }
        });

    }



    function SaveArrivedTimeStamps() {

        var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTimeStamps;
        var CallId = getParameterByName("CallId");
        var ParamedicId = getParameterByName("Id");
        var status = getParameterByName("Status");
        var CallNo = null;
        var Milege = $("#txtMilegeArrived").val();
        var Desc = null;
        var LocReq = getParameterByName("LocReq");
        var time = new Date().getTimezoneOffset();
        var sucess = "0";
        var IsDelay = "0";
        var DelayReason = "";
        var TimeStampsInfo = {
            Id: ParamedicId, CallId: CallId, Status: status, CallNo: CallNo, Milege: Milege, Desc: Desc, offset: time, IsDelay: IsDelay, DelayReason: DelayReason
        };
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
                $.mobile.showPageLoadingMsg("a", "Loading....");
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
                        $("#StatusHeadArrived").html("Some error occured. Please try again after some time.");
                    }
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {                
                if (sucess == "1") {
                    $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
                }
            }
        });
    }



    function SaveArrivedWithDelayTimeStamps() {

        var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTimeStamps;
        var CallId = getParameterByName("CallId");
        var ParamedicId = getParameterByName("Id");
        var status = getParameterByName("Status");
        var CallNo = null;
        var Milege = $("#txtMilegeArrivedDelay").val();
        var Desc = null;
        var LocReq = getParameterByName("LocReq");
        var time = new Date().getTimezoneOffset();
        var IsDelay = "1";
        var DelayReason = $("#txtDelayReasonArrivedDelay").val();
        var sucess = "0";
        var TimeStampsInfo={ Id: ParamedicId, CallId: CallId, Status: status, CallNo: CallNo, Milege: Milege, Desc: Desc, offset: time, IsDelay: IsDelay, DelayReason: DelayReason };
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
                $.mobile.showPageLoadingMsg("a", "Loading....");
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
                        $("#StatusHeadArrivedDelay").html("Some error occured. Please try again after some time.");
                    }
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {                
                if (sucess == "1") {
                    $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
                }
            }
        });
    }


    function SaveEnrouteWithDelayTimeStamps() {

        var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTimeStampWithoutMilege;
        var CallId = getParameterByName("CallId");
        var ParamedicId = getParameterByName("Id");
        var status = getParameterByName("Status");
        var LocReq = getParameterByName("LocReq");
        var time = new Date().getTimezoneOffset();
        var IsDelay = "1";
        var DelayReason = $("#txtDelayReasonEnroute").val();
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
                $.mobile.showPageLoadingMsg("a", "Loading....");
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
                        $("#StatusHeadEnroute").html("Some error occured. Please try again after some time.");
                    }
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                if (sucess == "1") {
                    $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
                }
            }
        });
    }


    function SaveOnSceneWithDelayTimeStamps() {

        var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTimeStampWithoutMilege;
        var CallId = getParameterByName("CallId");
        var ParamedicId = getParameterByName("Id");
        var status = getParameterByName("Status");
        var LocReq = getParameterByName("LocReq");
        var time = new Date().getTimezoneOffset();
        var IsDelay = "1";
        var DelayReason = $("#txtDelayReasonOnScene").val();
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
                $.mobile.showPageLoadingMsg("a", "Loading....");
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
                        $("#StatusHeadOnScene").html("Some error occured. Please try again after some time.");
                    }
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                if (sucess == "1") {
                    $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
                }
            }
        });
    }


    function SaveClearTimeStamps() {

        var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveClearTimeStamps;
        var CallId = getParameterByName("CallId");
        var ParamedicId = getParameterByName("Id");
        var status = getParameterByName("Status");
        var LocReq = getParameterByName("LocReq");
        var time = new Date().getTimezoneOffset();
        var PCR = $("#txtPCRNOClear").val();        
        var sucess = "0";
        var TimeStampsInfo = { Id: ParamedicId, CallId: CallId, Status: status, offset: time, PCR: PCR };
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
                $.mobile.showPageLoadingMsg("a", "Loading....");
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
                        $("#StatusHeadClear").html("Some error occured. Please try again after some time.");
                    }
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                if (sucess == "1") {
                    $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq }, transition: "slide" });
                }
            }
        });
    }