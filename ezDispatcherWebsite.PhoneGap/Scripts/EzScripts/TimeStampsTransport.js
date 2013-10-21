
$(document).ready(function () {
    
});


function GetTransportTimeStamp() {
    
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetTransportDynamicFields;
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var status = getParameterByName("Status");
    var message = "";
    //var Page = $('[data-mypage]:visible').data('mypage');
    var Page = $.mobile.activePage.data('mypage');
    var DynamicFieldHtml = "";
    var time = new Date().getTimezoneOffset();

    if (Page == "TimeStampTransport") {
        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: GetDataUrl,
            data: { Id: Id, CallId: CallId, Status: 'Transport', Offset: time },
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
                            DynamicFieldHtml = DynamicFieldHtml + "<div class='TransportDynamicField' MasterFieldId='" + v1.MasterFieldID + "'>"
                                               + "<h5 style='margin-bottom: 5px;'>" + v1.FieldName + "</h5>"
                                               + "<input type='text' name='" + v1.FieldName + "' id='txt" + v1.FieldName + "' value='' placeholder='Enter the " + v1.FieldName + "'/></div><div class='spacer5'></div>";
                        }
                    });

                    $('.SpanTripName:visible').html('Trip: ' + result[0].Trip);
                    $('.SpanUnitName:visible').html('Unit: ' + result[0].Unit);

                    if (result[0].CurrentLOT == "") {
                        var selectlist = $('#selectLOT');
                        selectlist.empty();

                        $.each(result[0].LOTString, function (i1, v1) {
                            if (v1.value != "") {
                                $('<option></option>').val(v1.value).text(v1.text).appendTo(selectlist);
                            }
                        });
                    }
                    else {
                        var selectlist = $('#selectLOT');
                        selectlist.empty();
                        $('<option></option>').val(result[0].CurrentLOTID).text(result[0].CurrentLOT).appendTo(selectlist);                        
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
                    }
                    else {
                        var selectlist = $('#selectOutCome');
                        selectlist.empty();
                        $('<option></option>').val(result[0].CurrentOutComeID).text(result[0].CurrentOutCome).appendTo(selectlist);                        
                        $('#selectOutCome').selectmenu('disable');
                    }                    
                }
                //$("#StatusHead").html("Status: " + status);
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                $("#TransportDynamicFileds").html(DynamicFieldHtml);                                
                if ($('#selectLOT option').length > 10) {
                    $('#selectLOT').selectmenu({ nativeMenu: "true" });
                } else {
                    $('#selectLOT').selectmenu({ nativeMenu: "false" });
                }
                if ($('#selectOutCome option').length > 10) {
                    $('#selectOutCome').selectmenu({ nativeMenu: "true" });
                } else {
                    $('#selectOutCome').selectmenu({ nativeMenu: "false" });
                }
                if ($('#selectLOT option').length > 0) {
                    $('#selectLOT').selectmenu('refresh', true);
                }
                if ($('#selectOutCome option').length > 0) {
                    $('#selectOutCome').selectmenu('refresh', true);
                }
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




function SaveTransportTimeStamps() {
    new ezphonemessege().show({
        msg: 'Processing..Please wait..'
    });
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveTransportTimeStamps;
    var CallId = getParameterByName("CallId");
    var ParamedicId = getParameterByName("Id");
    var status = getParameterByName("Status");
    var CurrentCallId = getParameterByName("CurrentCallId");
    var LOT = $('#selectLOT').val();
    var OutCome = $('#selectOutCome').val();
    var hasJson = 0;
    var jsondata = new Object();
    jsondata.CallId = CallId;
    jsondata.LOT = (LOT == 0 || LOT == null) ? 0 : LOT;
    jsondata.OutCome = (OutCome == 0 || OutCome == null) ? 0 : OutCome;
    jsondata.Milege = $("#txtMilege").val();    
    jsondata.Obj = [];
    $(".TransportDynamicField").each(function () {
        hasJson = 1;
        var $this = $(this);
        jsondata.Obj.push({
            MasterFId: $this.attr('masterfieldid'),
            MasterFValue: $this.find('input[type="text"]').first().val()
        });
    });
    jsondata.HasJson = hasJson;
    jsondata.DropupFacility = $("#searchFacility").val() == '' ? 0 : $("#searchFacility").attr("SelectedFacility");    
    var senddata = JSON.stringify(jsondata);    
    var LocReq = getParameterByName("LocReq");
    var time = new Date().getTimezoneOffset();
    var sucess = "0";
    
    var message = "";
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        url: GetDataUrl,
        data: { Id: ParamedicId, CallId: CallId, Status: status, offset: time, JsonData: senddata },
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
                $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
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
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        dataType: "json",
        data: { Id: Id, Lat: lat, Lnt: lng },
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

function GetFacilityDetails(Fac) {
        $("#searchFacility").attr("SelectedFacility", Fac.id);
        $("#searchFacility").val(Fac.textContent);
        $("#FacilitySuggestions").hide();
}