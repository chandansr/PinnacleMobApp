
$(document).ready(function () {

});




function GetPatientAddressNoteDetails() {

    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetPatientAddrNoteDetails;
    var Id = getParameterByName("Id");
    var CallId = getParameterByName("CallId");
    var status = getParameterByName("Status");
    var message = "";    
    var Page = $.mobile.activePage.data('mypage');
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        url: GetDataUrl,
        data: { Id: Id, CallId: CallId },
        dataType: "json",
        beforeSend: function () {
            new ezphonemessege().show({
                msg: 'Loading..'
            });
        },
        success: function (data) {
            var result = data.Data;
            if (result.length > 0) {
                for (i = 0; i < result.length; i++) {
                    $("#SpanAddNoteUnit").html(result[i].Unit);
                    $("#SpanTripAddNote").html(result[i].Trip);
                }

                if (result[0].StateString != "") {
                    var selectlist = $('#selectState');
                    selectlist.empty();

                    $.each(result[0].StateString, function (i1, v1) {
                        if (v1.value != "") {
                            $('<option></option>').val(v1.value).text(v1.text).appendTo(selectlist);
                        }
                    });
                }
            }
            $("#StatusHeadAddNote").html("Status: " + status);
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
            $('#selectState').selectmenu('refresh', true);
            new ezphonemessege().hide();
        }
    });

}


function showConfirm() {
    if (ValidateDetails()) {
        MessageDialogController.showConfirm('Are you sure!', SavePatientAddressNotesDetails(), 'Save, Cancel', 'Confirm');        
    }
}

function SavePatientAddressNotesDetails() {

    if (ValidateDetails()) {
        if (ValidateInsuranceInfo()) {
            new ezphonemessege().show({
                msg: 'Loading..'
            });
            //getPatientAddressFromLatLang(21.145799999999998, 79.088155);
            navigator.geolocation.getCurrentPosition(function (pos) {
                var lat = pos.coords.latitude;
                var lng = pos.coords.longitude;
                getPatientAddressFromLatLang(lat, lng);
            });
        }
    }
}

function ValidateDetails() {
    if ($('#txtFName').val() == '') {
        showNoteAlert("Please enter First Name.", "Info");
        $('#txtFName').focus();
        return false;
    }
    else if ($('#txtSSN').val() == '') {
        showNoteAlert("Please enter SSN.", "Info");
        $('#txtSSN').focus();
        return false;
    }
    else if ($('#txtCity').val() == '') {
        showNoteAlert("Please enter City Name.", "Info");
        $('#txtCity').focus();
        return false;
    }
    else if ($('#selectState').val() == "0") {
        showNoteAlert("Please select the State.", "Info");
        $('#selectState').focus();
        return false;
    }
    else {
        return true;
    }
}

function ValidateInsuranceInfo() {
    var isTrue = true;
    if ($('#chkInsuranceInfo').prop('checked')) {
        if ($('#flpUnknown').val() == "off") {
            $('.InsuranceSwitch').each(function () {
                if (this.value == "on") {
                    var Div = this.name + 'Div';
                    $('#' + Div + ' :text[required]').each(function () {
                        if (this.value == "") {
                            showNoteAlert("Please provide complete information.", "Info");
                            isTrue = false;
                            this.focus();
                            return false;
                        }
                    });
                }
            });
        }
    }
    return isTrue;
}
var x2js = new X2JS();
function SaveFullPatientAddressNotesDetails(lat, lng, addr) {

    new ezphonemessege().show({
        msg: 'Loading..'
    });
    var GetDataUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SavePatientAddressNote;
    var CallId = getParameterByName("CallId");
    var ParamedicId = getParameterByName("Id");
    var status = getParameterByName("Status");
    var LocReq = getParameterByName("LocReq");
    var time = new Date().getTimezoneOffset();
    var CurrentCallId = getParameterByName("CurrentCallId");
    var sucess = "0";
    var message = "";
    var insuranceinfo = '';
    var jsondata = new Object();
    var insuranceJson = new Object();
    var InsObjs = [];

    jsondata.CallId = CallId;
    jsondata.Id = ParamedicId;
    jsondata.Status = status;

    jsondata.NoteType = "RequestAddtionOfPatientAddress";
    jsondata.FirstName = $('#txtFName').val();
    jsondata.LastName = $('#txtLName').val();
    jsondata.DOB = $('#txtDOB').val();
    jsondata.Gender = $('#flpGender').val() == 'M'? 'Male' : 'Female';
    jsondata.SSN = $('#txtSSN').val();
    jsondata.PatientWeight = $('#txtPatientWeight').val();
    jsondata.PatientHeight = $('#txtPatientHeight').val();
    jsondata.Street = $('#txtStreet').val();
    jsondata.Appartment = $('#txtApartment').val();
    jsondata.City = $('#txtCity').val();
    jsondata.State = $('#selectState').val();
    jsondata.Zip = $('#txtZIP').val();
    jsondata.Phone = $('#txtPhone').val();
    jsondata.PatientAlerts = $('#txtPatientAlerts').val();
    jsondata.Latitude = lat;
    jsondata.Longitude = lng;
    jsondata.Address = addr;
    jsondata.Offset = (-1) * time;
    jsondata.SameAsPickUp = $('#chkSameForPick').prop('checked');
    jsondata.SameAsDropUp = $('#chkSameForDrop').prop('checked');

    if ($('#chkInsuranceInfo').prop('checked')) {
        jsondata.hasInsuranceInfo = "1";
        //jsondata.InsurenceProviders = [];
        //jsondata.InsurenceProvidersothers = [];
        var xmlins = "<Is>";
        var xmlinsother = "<OInsurences>";
        var Div;
        var InsPrvObj = new Object();
        $('.InsuranceSwitchAll').each(function () {
            Div = this.name + 'Div';

            if (this.value == 'on') {
                InsPrvObj = new Object();
                if (!(this.name == 'InsuranceOther')) {
                    var Instype = this.attributes['InsType'].value;
                    InsPrvObj.IPID = this.attributes['InsuranceProviderID'].value;
                    InsPrvObj.IsRe = true;
                    InsPrvObj.Na = Instype;
                    if (!!$('#' + Div).find('input[InsType="PolicyNo"]').val()) {
                        InsPrvObj.PN = $('#' + Div).find('input[InsType="PolicyNo"]').val();
                    }
                    if (!!$('#' + Div).find('input[InsType="TypeOrProvider"]').val()) {
                        InsPrvObj.TOP = $('#' + Div).find('input[InsType="TypeOrProvider"]').val();
                    }
                    if (!!$('#' + Div).find('input[InsType="AuthorizationNo"]').val()) {
                        InsPrvObj.AN = $('#' + Div).find('input[InsType="AuthorizationNo"]').val();
                    }
                    if (!!$('#' + Div).find('input[InsType="ApprovedBy"]').val()) {
                        InsPrvObj.ABy = $('#' + Div).find('input[InsType="ApprovedBy"]').val();
                    }
                    InsObjs.push(InsPrvObj);
                }
                else {
                    for (a = 500; a < 504; a++) {                        
                        InsPrvObj = new Object();                        
                        if (CheckTextBoxes(a) > 1) {
                            InsPrvObj.IPID = this.attributes['InsuranceProviderID'].value;
                            InsPrvObj.IsRe = true;
                            InsPrvObj.Na = this.attributes['InsType'].value;
                            InsPrvObj.IsOR = (CheckTextBoxes(a) > 1);
                            InsPrvObj.OID = a;
                            if (!!$('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="PolicyNo"]').val()) {
                                InsPrvObj.PN = $('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="PolicyNo"]').val();
                            }
                            if (!!$('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="TypeOrProvider"]').val()) {
                                InsPrvObj.TOP = $('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="TypeOrProvider"]').val();
                            }
                            if (!!$('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="AuthorizationNo"]').val()) {
                                InsPrvObj.AN = $('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="AuthorizationNo"]').val();
                            }
                            InsObjs.push(InsPrvObj);
                        }
                    }
                }
            }
        });
        xmlins = xmlins + "</Insurences>";        

        for (a = 500; a < 504; a++) {            
            var InsurenceProvidersothersobj = new Object();
            InsurenceProvidersothersobj.OID = a;
            InsurenceProvidersothersobj.IsOR = (CheckTextBoxes(a) > 1);
            InsurenceProvidersothersobj.OPN = (!!$('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="PolicyNo"]').val()) ? $('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="PolicyNo"]').val() : '';
            InsurenceProvidersothersobj.OTOP = (!!$('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="TypeOrProvider"]').val()) ? $('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="TypeOrProvider"]').val() : '';
            InsurenceProvidersothersobj.OAN = (!!$('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="AuthorizationNo"]').val()) ? $('.InsurenceProvidersothersDiv[otherid="' + a + '"]').find('input[InsType="AuthorizationNo"]').val() : '';
            
            xmlinsother = xmlinsother + x2js.json2xml_str({
                otherInsurance: InsurenceProvidersothersobj
            });
        }
        xmlinsother = xmlinsother + "</OtherInsurences>";        
        insuranceinfo = '<InsInfo>' + xmlins + xmlinsother + '</InsInfo>';
    }
    else {
        jsondata.hasInsuranceInfo = "0";        
        insuranceinfo = '';
    }
    //var sendJson = '';
    var sendJson = JSON.stringify(jsondata);
    var sendinsuranceJson = JSON.stringify(InsObjs);
    //insuranceinfo = '<InsInfo><Is><insurence><IPID>72</IPID><IsRe>false</IsRe></insurence></Is></InsInfo>';

    
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        url: GetDataUrl,
        data: { Id: ParamedicId, CallId: CallId, Status: status, jsonData: sendJson, InsuranceInfo: sendinsuranceJson },
        dataType: "json",
        beforeSend: function () {
            new ezphonemessege().show({
                msg: 'Loading..'
            });
        },
        success: function (data) {
            var result = data.Data;

            if (result.length > 0) {

                var xml = $.parseXML(result);
                var $xml = $(xml);

                if ($xml.find('ErrorProcedure').text() == "SUCCESS") {
                    sucess = "1";
                    UpdateCrewLoc();
                }
                else {
                    $("#StatusHeadAddNote").html("Some error occured. Please try again after some time.");
                }
            }
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
            new ezphonemessege().hide();
            
            if (sucess == "1") {
                $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": ParamedicId, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
            }
        }
    });

}

function CheckTextBoxes(a) {
    var returnval = '';
    $('.InsurenceProvidersothersDiv[otherid="' + a + '"] :text').each(function () {
        if ($.trim($(this).val()) != '') {
            returnval = returnval + $.trim($(this).val()) + ',';
        }
    });
    return returnval.split(",").length;
}

function getPatientAddressFromLatLang(lat, lng) {
    
    var geocoder = new google.maps.Geocoder();
    var latLng = new google.maps.LatLng(lat, lng);
    //SaveFullPatientAddressNotesDetails(21.145799999999998, 79.088155, '');
    geocoder.geocode({ 'latLng': latLng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            
            if (results[1]) {
                SaveFullPatientAddressNotesDetails(lat, lng, results[1].formatted_address);
            }
        } else {            
            SaveFullPatientAddressNotesDetails(lat, lng, '');
        }
    });
}


function showNoteAlert(message, title) {
    if (navigator.notification) {
        navigator.notification.alert(message, null, title, 'OK');
    } else {
        alert(title ? (title + ": " + message) : message);
    }
}




var MessageDialogController = (function () {

    var that = {};

    /**
     * Invokes the method 'fun' if it is a valid function. In case the function
     * method is null, or undefined then the error will be silently ignored.
     *
     * @param fun  the name of the function to be invoked.
     * @param args the arguments to pass to the callback function.
     */
    var invoke = function (fun, args) {
        if (fun && typeof fun === 'function') {
            fun(args);
        }
    };

    that.showMessage = function (message, callback, title, buttonName) {

        title = title || "Confirm Save";
        buttonName = buttonName || 'OK';

        if (navigator.notification && navigator.notification.alert) {

            navigator.notification.alert(
                message,    // message
                callback,   // callback
                title,      // title
                buttonName  // buttonName
            );

        } else {

            alert(message);
            invoke(callback);
        }

    };

    that.showConfirm = function (message, callback, buttonLabels, title) {

        //Set default values if not specified by the user.
        buttonLabels = buttonLabels || 'OK,Cancel';
        var buttonList = buttonLabels.split(',');

        title = title || "Confirm Save";

        //Use Cordova version of the confirm box if possible.
        if (navigator.notification && navigator.notification.confirm) {

            var _callback = function (index) {
                if (callback) {
                    //The ordering of the buttons are different on iOS vs. Android.
                    if (navigator.userAgent.match(/(iPhone|iPod|iPad)/)) {
                        index = buttonList.length - index;
                    }
                    callback(index == 1);
                }
            };

            navigator.notification.confirm(
                message,      // message
                _callback,    // callback
                title,        // title
                buttonLabels  // buttonName
            );

            //Default to the usual JS confirm method.
        } else {
            invoke(callback, confirm(message));
        }

    };

    return that;

})();




