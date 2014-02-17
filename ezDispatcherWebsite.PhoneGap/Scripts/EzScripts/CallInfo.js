
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
    var DispatcherCalls = { CallId: Id };
    if (Page == "CallInfo") {

        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: GetDataUrl,
            data: JSON.stringify({ "DispatcherCalls": DispatcherCalls }),
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
                    var fnsec = new fn_security();

                    for (i = 0; i < result.length; i++) {

                        if (globalVar._IsEncryptionReq == "1") {

                            result[i].PLastName = fnsec.decrypt({ value: result[i].PLastName });
                            result[i].PFirstName = fnsec.decrypt({ value: result[i].PFirstName });
                            result[i].PDOB = fnsec.decrypt({ value: result[i].PDOB });
                            result[i].PPhone = fnsec.decrypt({ value: result[i].PPhone });
                            result[i].PAlerts = fnsec.decrypt({ value: result[i].PAlerts });
                            result[i].PStreet = fnsec.decrypt({ value: result[i].PStreet });
                            result[i].PAppartment = fnsec.decrypt({ value: result[i].PAppartment });
                            result[i].PCity = fnsec.decrypt({ value: result[i].PCity });
                            result[i].PState = fnsec.decrypt({ value: result[i].PState });
                            result[i].PZip = fnsec.decrypt({ value: result[i].PZip });
                            result[i].PickFromFacility = fnsec.decrypt({ value: result[i].PickFromFacility });
                            result[i].PickFromPhone = fnsec.decrypt({ value: result[i].PickFromPhone });
                            result[i].PickFromAddress = fnsec.decrypt({ value: result[i].PickFromAddress });
                            result[i].PickFromCity = fnsec.decrypt({ value: result[i].PickFromCity });
                            result[i].PickFromState = fnsec.decrypt({ value: result[i].PickFromState });
                            result[i].PickFromZip = fnsec.decrypt({ value: result[i].PickFromZip });
                            result[i].PickFromFloor = fnsec.decrypt({ value: result[i].PickFromFloor });
                            result[i].PickFromRoom = fnsec.decrypt({ value: result[i].PickFromRoom });
                            result[i].PickFromStairs = fnsec.decrypt({ value: result[i].PickFromStairs });
                            result[i].PickFromObstacle = fnsec.decrypt({ value: result[i].PickFromObstacle });
                            result[i].DropToFacility = fnsec.decrypt({ value: result[i].DropToFacility });
                            result[i].DropToPhone = fnsec.decrypt({ value: result[i].DropToPhone });
                            result[i].DropToAddress = fnsec.decrypt({ value: result[i].DropToAddress });
                            result[i].DropToCity = fnsec.decrypt({ value: result[i].DropToCity });
                            result[i].DropToState = fnsec.decrypt({ value: result[i].DropToState });
                            result[i].DropToZip = fnsec.decrypt({ value: result[i].DropToZip });
                            result[i].DropToFloor = fnsec.decrypt({ value: result[i].DropToFloor });
                            result[i].DropToRoom = fnsec.decrypt({ value: result[i].DropToRoom });
                            result[i].DropToStairs = fnsec.decrypt({ value: result[i].DropToStairs });
                            result[i].DropToObstacle = fnsec.decrypt({ value: result[i].DropToObstacle });
                            result[i].ReasonForTx = fnsec.decrypt({ value: result[i].ReasonForTx });
                            result[i].AttentdentInfo = fnsec.decrypt({ value: result[i].AttentdentInfo });
                            result[i].SpecialNeeds = fnsec.decrypt({ value: result[i].SpecialNeeds });
                            result[i].IsO2Requierd = fnsec.decrypt({ value: result[i].IsO2Requierd });
                            result[i].IsOver200IBS = fnsec.decrypt({ value: result[i].IsOver200IBS });

                        }


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