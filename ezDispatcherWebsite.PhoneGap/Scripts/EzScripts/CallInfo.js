
$(document).ready(function () {

    
});


function GetCallInfoUrl(path) {
    var Id = getParameterByName("CallId");
    var LocReq = getParameterByName("LocReq");
    var CurrentCallId = getParameterByName("CurrentCallId");

    $.mobile.changePage(path, { data: { "CallId": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
}

function GetCallInfoTopUrl(path) {

    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");
    var CurrentCallId = getParameterByName("CurrentCallId");
    var Actpath = path + "?Id=" + Id + "&LocReq=" + getParameterByName("LocReq");
    if (path == "DispatcherCallList.html") {    
        $.mobile.changePage(path, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
    }
    else if (path == "Index.html") {
        window.location.href = path;
    }
    else {
        $.mobile.changePage(path, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
    }

}


function GetCallInfo() {
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._CallInfo;
    var Id = getParameterByName("CallId");
    var message = "";        
    var Page = $.mobile.activePage.data('mypage');
    
    if (Page == "CallInfo") {

        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            url: GetDataUrl,
            data: { CallId: Id },
            dataType: "json",
            beforeSend: function () {
                new ezphonemessege().show({
                    msg: 'Processing..Please wait..'
                });
            },
            success: function (data) {
                var result = data.Data;

                if (result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        $("#SpanCallInfoUnitName").html(result[i].Unit);
                        $("#SpanCallInfoTrip").html(result[i].Trip);
                        $("#PLName").html(result[i].PLastName);
                        $("#PFName").html(result[i].PFirstName);
                        $("#PDOB").html(result[i].PDOB);
                        $("#PPhone").html(result[i].PPhone);
                        $("#PAlerts").html(result[i].PAlerts);
                        $("#PStreet").html(result[i].PStreet);
                        $("#PApartment").html(result[i].PAppartment);
                        $("#PCity").html(result[i].PCity);
                        $("#PState").html(result[i].PState);
                        $("#PZIP").html(result[i].PZip);

                        $("#TransportationType").html(result[i].TransportationType);
                        $("#IsRoundTrip").html(result[i].RoundTrip);
                        $("#PickupDate").html(result[i].PickupDate);
                        $("#PickupTime").html(result[i].PickupTime);

                        $("#PickFromFacility").html(result[i].PickFromFacility);
                        $("#PickFromPhone").html(result[i].PickFromPhone);
                        $("#PickFromAddress").html(result[i].PickFromAddress);
                        $("#PickFromCity").html(result[i].PickFromCity);
                        $("#PickFromState").html(result[i].PickFromState);
                        $("#PickFromZip").html(result[i].PickFromZip);
                        $("#PickFromFloor").html(result[i].PickFromFloor);
                        $("#PickFromRoom").html(result[i].PickFromRoom);
                        $("#PickFromStairs").html(result[i].PickFromStairs);
                        $("#PickFromObstacle").html(result[i].PickFromObstacle);

                        $("#DropToFacility").html(result[i].DropToFacility);
                        $("#DropToPhone").html(result[i].DropToPhone);
                        $("#DropToAddress").html(result[i].DropToAddress);
                        $("#DropToCity").html(result[i].DropToCity);
                        $("#DropToState").html(result[i].DropToState);
                        $("#DropToZIP").html(result[i].DropToZip);
                        $("#DropToFloor").html(result[i].DropToFloor);
                        $("#DropToRoom").html(result[i].DropToRoom);
                        $("#DropToStairs").html(result[i].DropToStairs);
                        $("#DropToObstacle").html(result[i].DropToObstacle);

                        $("#ReasonForTx").html(result[i].ReasonForTx);
                        $("#PersonWithPatient").html(result[i].AttentdentInfo);
                        $("#AnySpecialNeeds").html(result[i].SpecialNeeds);
                        $("#RequireO2").html(result[i].IsO2Requierd);
                        $("#PatientIBS").html(result[i].IsOver200IBS);
                        $("#CallerName").html(result[i].Caller);
                        $("#CallerFacility").html(result[i].CallerFac);
                        $("#CallerMob").html(result[i].CallerMob);
                        $("#CallerPhone").html(result[i].CallerPhone);
                        $("#TsAssigned").html(result[i].TsAssigned);
                        $("#TsDispatched").html(result[i].TsDispatched);
                        $("#TsEnroute").html(result[i].TsEnroute);
                        $("#TsOnScene").html(result[i].TsOnScene);
                        $("#TsContact").html(result[i].TsContact);
                        $("#TsTransport").html(result[i].TsTransport);
                        $("#TsArrived").html(result[i].TsArrived);
                        $("#TsClear").html(result[i].TsClear);
                        $("#LevelOfService").html(result[i].LevelOfService);
                        $("#LevelOfTransport").html(result[i].LevelOfTransport);
                        $("#LevelOfResponse").html(result[i].LevelOfResponse);
                        $("#OutCome").html(result[i].OutCome);
                        $("#MedicalNecessityQ").html(result[i].MedicalNParam);
                    }
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