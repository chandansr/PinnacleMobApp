/*------------------------- ezDispatcher js detail  ------------------------------------------------------------*/

// Created By:-Smartdata Developer
// Created Date:-07-02-2013
// Purpose:-to define basic details of ezDispatcher PhoneGap apps
//          as all pages path, services path and wcf method 

/*--------------------------End ezDispatcher js detail-----------------------------------------------------------*/

$(document).bind('mobileinit', function () {
    $.mobile.loader.prototype.options.text = "Please wait..";
    $.mobile.loader.prototype.options.textVisible = true;
    $.mobile.loader.prototype.options.theme = "a";
    $.mobile.loader.prototype.options.html = "";
});

$(function () {


    setInterval(GetCrewLocation, 120000);
    setInterval(RefreshUnitLocation, 60000);
    setInterval(GetSuperVisorUnits, 480000);
    
    $('[type="submit"]').bind("click", function (event, ui) {
        new ezphonemessege().show({
            msg: 'Processing Please wait..'
        });
    });

    $(document).on('pagecreate pageinit pageshow', 'div:jqmData(role="page")', function (event) {

        switch (this.attributes['data-mypage'].value) {

            case "Dashboard":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Please wait..Getting Calls..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/ezDispatcherWCF.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    GetSuperVisorUnits();
                    BindNecessaryFunctions();
                    GetCrewLocation();
                    new ezphonemessege().hide();
                }
                break;
            case "CallList":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Please wait..Getting Calls..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/DispatcherCalllist.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    $('#lstView li').live('click', function () {
                        if ($('#lstView').data('nocalls') == "0") {
                            var CallId = $(this).attr('callid');
                            var Id = getParameterByName("Id");
                            var LocReq = getParameterByName("LocReq");
                            var CurrentCallId = getParameterByName("CurrentCallId");
                            $.mobile.changePage(_SiteUrl.CallDetail, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
                        }
                        else {
                            showAlert('No Call Available', 'Info');
                        }
                    });

                    BindNecessaryFunctions();
                    GetCallList();
                }
                break;
            case "CallDetails":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/DispatcherCallDetails.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    $("#btnGetScene").bind("click", function (event, ui) {
                        GetScene();
                    });
                    $("#btnGetDestination").bind("click", function (event, ui) {
                        GetDestination();
                    });
                    BindNecessaryFunctions();
                    GetCallDetails();
                }
                break;
            case "CallInfo":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Please wait..Retriving details..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/CallInfo.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetCallInfo();
                }
                break;
            case "Location":

                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                    var mobileDemo = { 'center': '57.7973333,12.0502107', 'zoom': 10 };
                    demo.add('directions_map', function () {
                        $('#map_canvas_1').gmap({
                            'center': mobileDemo.center, 'zoom': mobileDemo.zoom, 'disableDefaultUI': true, 'callback': function () {
                                var self = this;
                                self.set('getCurrentPosition', function () {
                                    self.refresh();
                                    self.getCurrentPosition(function (position, status) {
                                        if (status === 'OK') {
                                            var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude)
                                            self.get('map').panTo(latlng);
                                            self.search({ 'location': latlng }, function (results, status) {
                                                if (status === 'OK') {
                                                    $('#from').val(results[0].formatted_address);
                                                }
                                            });
                                        } else {
                                            //alert('Unable to get current position');
                                        }
                                    });
                                });
                                $('#submit').click(function () {
                                    self.displayDirections({ 'origin': $('#from').val(), 'destination': $('#to').val(), 'travelMode': google.maps.DirectionsTravelMode.DRIVING }, { 'panel': document.getElementById('directions') }, function (response, status) {
                                        (status === 'OK') ? $('#results').show() : $('#results').hide();
                                    });
                                    return false;
                                });
                            }
                        });
                    }).load('directions_map');
                }
                else if (event.type == 'pagecreate') {

                }
                else {
                    demo.add('directions_map', $('#map_canvas_1').gmap('get', 'getCurrentPosition')).load('directions_map');
                }
                break;
            case "ToFromLocation":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {

                }
                else {
                    BindNecessaryFunctions();
                    var mobileDemo = { 'center': '57.7973333,12.0502107', 'zoom': 10 };
                    var source = getParameterByName("origin");
                    var destination = getParameterByName("Destination");
                    $('#from1').val(source);
                    $('#to1').val(getParameterByName("Destination"));
                    demo.add('directions_map1', function () {
                        $('#map_canvas_2').gmap({
                            'center': mobileDemo.center, 'zoom': mobileDemo.zoom, 'disableDefaultUI': true, 'callback': function () {
                                var self = this;
                                self.displayDirections({ 'origin': $('#from1').val(), 'destination': $('#to1').val(), 'travelMode': google.maps.DirectionsTravelMode.DRIVING }, { 'panel': document.getElementById('directions1') }, function (response, status) {
                                    (status === 'OK') ? $('#results1').show() : $('#results1').hide();
                                });
                                $('#submit1').click(function () {
                                    self.displayDirections({ 'origin': $('#from1').val(), 'destination': $('#to1').val(), 'travelMode': google.maps.DirectionsTravelMode.DRIVING }, { 'panel': document.getElementById('directions1') }, function (response, status) {
                                        (status === 'OK') ? $('#results1').show() : $('#results1').hide();
                                    });
                                    return false;
                                });
                            }
                        });
                    }).load('directions_map1');
                    demo.add('directions_map1', $('#map_canvas_2').gmap('get', 'getCurrentPosition')).load('directions_map1');

                    if (globalVar._IsSuperVisor == "1") {

                        var Id = getParameterByName("Id");
                        var Unit = getParameterByName("Unit");
                        var time = new Date().getTimezoneOffset();
                        var UnitId = $.map(_SuperVisorUnits._SupUnitDetails, function (i, v) {
                            if (i.UnitName == Unit)
                                return i;
                        })[0].UnitId;

                        $('#map_canvas_2').gmap().bind('init', function () {
                            $.getJSON(_ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetPrimaryCrewLocation, { Id: Id, UnitIds: UnitId, Offset: time }, function (data) {
                                $.each(data.Data, function (i, marker) {
                                    if (marker.Latitude != "-1" && marker.Longitude != "-1") {
                                        $('#map_canvas_2').gmap('addMarker', {
                                            'position': new google.maps.LatLng(marker.Latitude, marker.Longitude),
                                            'bounds': true
                                        }).click(function () {
                                            $('#map_canvas_2').gmap('openInfoWindow', { 'content': marker.Unit }, this);
                                        });
                                    }
                                });
                            });
                        })
                    }

                }
                break;
            case "TimeStamp":

                if (event.type == 'pageinit') {

                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsInfo.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    GetTransportTimeStamp();
                }
                break;
            case "TimeStampDispatched":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsDispatched.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetDispatchedTimeStamp();
                }
                break;
            case "TimeStampEnroute":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsEnroute.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetEnrouteTimeStamp();
                }
                break;
            case "TimeStampOnScene":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsOnScene.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetOnSceneTimeStamp();
                }
                break;
            case "TimeStampContact":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsContact.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetContactTimeStamp();
                }
                break;
            case "TimeStampTransport":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsTransport.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetTransportTimeStamp();
                    $("#searchFacility").on("input", function (e) {
                        var text = $(this).val().trim();
                        var sugList = $("#FacilitySuggestions");
                        var Id = getParameterByName("Id");
                        if (text.length < 4) {
                            sugList.html("");
                            sugList.listview("refresh");
                        } else {
                            $.get(_ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetFacilities, { Id: Id, Facility: text }, function (res, code) {

                                var str = "";
                                for (var i = 0, len = res.Data.length; i < len; i++) {
                                    str += "<li class='suggestedFacility' onclick='GetFacilityDetails(this)' id=" + res.Data[i].value + ">" + res.Data[i].text + "</li>";
                                }

                                if (str != "") {
                                    sugList.html(str);
                                    sugList.show();
                                }
                                else {
                                    str += "<li>No Faciliies found. Try again..</li>";
                                    sugList.html(str);
                                    sugList.show();
                                }
                                sugList.listview("refresh");
                            }, "json");
                        }
                    });

                    jQuery.fn.clearable = function () {

                        $('.morelink').on('click', function () {
                            var $this = $(this);
                            if ($this.hasClass('less')) {
                                $this.removeClass('less');
                                $this.html(config.moreText);
                            } else {
                                $this.addClass('less');
                                $this.html(config.lessText);
                            }
                            $this.parent().prev().toggle();
                            $this.prev().toggle();
                            return false;
                        });

                        return this.each(function () {
                            $(this).css({ 'border-width': '0px', 'outline': 'none' })
                                .wrap('<div id="sq" class="divclearable"></div>')
                                .parent()
                                .attr('class', $(this).attr('class') + ' divclearable')
                                .append('<a class="clearlink" style="margin-left: 0px; float: right; margin-top: -25px; width: 20px; margin-right: 1px; height: 20px;" href="javascript:"></a>');
                            $('.clearlink')
                                .attr('title', 'Click to clear this textbox')
                                .click(function () {

                                    $(this).prev().val('').focus();
                                    $("#FacilitySuggestions").hide();
                                });
                        });
                    }
                    $('.clearable').clearable();
                }
                break;
            case "TimeStampArrived":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsArrived.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetArrivedTimeStamp();
                }
                break;
            case "TimeStampClear":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/TimeStampsClear.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    BindNecessaryFunctions();
                    GetClearTimeStamp();
                }
                break;
            case "AddRequestPatientAddressNotes":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                    var script = document.createElement('script'); script.type = 'text/javascript';
                    script.src = 'Scripts/EzScripts/PatientAddressNote.js';
                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
                }
                else {
                    $('#txtPhone').mask("(999)-999-9999");
                    $('#txtSSN').mask("999-99-9999");
                    $('#txtZIP').mask("99999?-9999");
                    $('#txtZIP1').mask("99999?-9999");
                    $('#chkInsuranceInfo').bind('click', function () {
                        $('#InsuranceInfoDiv').toggle();
                    });
                    $('#flpUnknown').bind('change', function (event, ui) {
                        if (this.value == "on") {
                            $('.InsuranceOtherInfoDiv').hide();
                            $('.InsuranceSwitch').val('off').slider("refresh");
                        }
                    });
                    $('.InsuranceSwitch').bind('change', function (event, ui) {
                        if (this.attributes["instype"].value != "Unknown") {
                            $('#flpUnknown').val('off').slider('refresh');
                        }
                        var Cntrl = this.name + 'Div';
                        $('#' + Cntrl).toggle();
                    });
                    $('#flpGender').next().css('width', '120');
                    BindNecessaryFunctions();
                    GetPatientAddressNoteDetails();
                }
                break;
            case "sup-Dashboard":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                }
                else {
                    BindNecessaryFunctions();
                    GetSupVCallsCount();
                    $.mobile.activePage.find("a:jqmData(role='button')").buttonMarkup("refresh");
                    new ezphonemessege().hide();
                }
                break;
            case "sup-CallList":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                }
                else {
                    $('#sup-lstView li').live('click', function () {
                        if ($('#sup-lstView').data('nocalls') == "0") {
                            var CallId = $(this).attr('callid');
                            var Id = getParameterByName("Id");
                            var LocReq = getParameterByName("LocReq");
                            var CurrentCallId = getParameterByName("CurrentCallId");
                            $.mobile.changePage(_SiteUrl.sup_CallDetail, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
                        }
                        else {
                            showAlert('No Call Available', 'Info');
                        }
                    });

                    BindNecessaryFunctions();
                    GetSupVCallList();
                }
                break;
            case "sup-CallDetails":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                }
                else {
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

                    BindNecessaryFunctions();
                    GetSupVCallDetails();
                }
                break;
            case "sup-CallInfo":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                }
                else {
                    BindNecessaryFunctions();
                    GetSupVCallInfo();
                }
                break;
            case "sup-ToFromLocation":
                if (event.type == 'pageinit') {
                    new ezphonemessege().show({
                        msg: 'Processing..Please wait..'
                    });
                }
                else if (event.type == 'pagecreate') {
                }
                else {
                    BindNecessaryFunctions();
                    var mobileDemo = { 'center': '57.7973333,12.0502107', 'zoom': 10 };
                    var source = getParameterByName("origin");
                    var destination = getParameterByName("Destination");
                    $('#from1').val(source);
                    $('#to1').val(getParameterByName("Destination"));
                    demo.add('directions_map1', function () {
                        $('#map_canvas_2').gmap({
                            'mapTypeId': google.maps.MapTypeId.ROADMAP, 'streetViewControl': true,
                            'center': mobileDemo.center, 'zoom': mobileDemo.zoom, 'disableDefaultUI': false, 'callback': function () {
                                var self = this;
                                self.displayDirections({ 'origin': $('#from1').val(), 'destination': $('#to1').val(), 'travelMode': google.maps.DirectionsTravelMode.DRIVING }, { 'panel': document.getElementById('directions1') }, function (response, status) {
                                    (status === 'OK') ? $('#results1').show() : $('#results1').hide();
                                });
                                $('#submit1').click(function () {
                                    self.displayDirections({ 'origin': $('#from1').val(), 'destination': $('#to1').val(), 'travelMode': google.maps.DirectionsTravelMode.DRIVING }, { 'panel': document.getElementById('directions1') }, function (response, status) {
                                        (status === 'OK') ? $('#results1').show() : $('#results1').hide();
                                    });
                                    return false;
                                });
                            }
                        });
                    }).load('directions_map1');
                    demo.add('directions_map1', $('#map_canvas_2').gmap('get', 'getCurrentPosition')).load('directions_map1');

                    google.maps.event.addDomListener(document.getElementById("map_canvas_2"), 'click', RemoveGoogleMoreLinks);

                    if (globalVar._IsSuperVisor == "1") {
                        var Id = getParameterByName("Id");
                        var Unit = getParameterByName("Unit");
                        var time = new Date().getTimezoneOffset();
                        var CallId = getParameterByName("CallId");
                        var UnitId = $.map(_SuperVisorUnits._SupUnitDetails, function (i, v) {
                            if (i.UnitName == Unit)
                                return i;
                        })[0].UnitId;
                        $.mobile.activePage.find('#map_canvas_2').gmap({ 'zoom': '8' }).bind('init', function () {
                            $.getJSON(_ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetPrimaryCrewLocation, { Id: Id, UnitIds: UnitId, Offset: time, CallId: CallId }, function (data) {

                                $.each(data.Data, function (i, marker) {
                                    if (marker.Latitude != "-1" && marker.Longitude != "-1") {
                                        $.mobile.activePage.find('#map_canvas_2').gmap('addMarker', {
                                            'position': new google.maps.LatLng(marker.Latitude, marker.Longitude),
                                            'bounds': true,
                                            'animation': google.maps.Animation.DROP,                                            
                                            'icon': new google.maps.MarkerImage('../images/Ambulance2.png')
                                        }).click(function () {
                                            $.mobile.activePage.find('#map_canvas_2').gmap('openInfoWindow', {
                                                'content': marker.Unit,
                                                'closeBoxMargin': "10px 2px 2px 2px",
                                                'closeBoxURL': "http://www.google.com/intl/en_us/mapfiles/close.gif"
                                            }, this);
                                        });
                                    }
                                });
                            });
                        });

                        $.mobile.activePage.find('#map_canvas_2').gmap('option', 'zoom', 10);
                        $.mobile.activePage.find('#map_canvas_2').gmap('refresh');
                    }
                    new ezphonemessege().hide();
                }
                break;
            default:
                if (event.type == 'pageinit') {
                    new ezphonemessege().hide();
                }
                else if (event.type == 'pagecreate') {
                    new ezphonemessege().hide();
                }
                else {
                    BindNecessaryFunctions();
                }
                break;
        }

    });

});

/*----------------------All pages path-------------------------*/
var _SiteUrl =
    {
        detail: "dashboard.html",
        Login: "Index.html",
        CurrentCall: "currencall_after.html",
        Location: "Location.html",
        CallList: "DispatcherCallList.html",
        CallDetail: "currencall.html",
        Logout: "Index.html",
        CallInfo: "CallDetails.html",
        TimeStamps: "TimeStamps.html",
        Maps: "maps.html",
        ToFromMaps: "ToFromMaps.html",
        TimeStampArrivedDelay: "TimeStampArrivedDelay.html",
        TimeStampDispatched: "TimeStampDispatched.html",
        TimeStampEnroute: "TimeStampEnroute.html",
        TimeStampOnScene: "TimeStampOnScene.html",
        TimeStampContact: "TimeStampContact.html",
        TimeStampTransport: "TimeStampTransport.html",
        TimeStampArrived: "TimeStampArrived.html",
        TimeStampClear: "TimeStampClear.html",
        Notes_RequestPatientAddress: "AddNote_RequestPatientAddress.html",

        /*-------------------SuperVisor Section-------------------------------*/
        sup_detail: "SuperVisor/Sup-dashboard.html",
        sup_Login: "../Index.html",
        sup_CallList: "SuperVisor/Sup-CallList.html",
        sup_CallListFrmDashboard: "Sup-CallList.html",
        sup_CallDetail: "Sup-currentcall.html",
        sup_Logout: "../Index.html",
        sup_CallInfo: "Sup-CallDetails.html",
        sup_detail1: "../dashboard.html",
        sup_CallList1: "../DispatcherCallList.html",
        sup_ToFromMaps: "Sup-ToFromMaps.html"
        /*-------------------SuperVisor Section-------------------------------*/
    };
/*---------------------End All Pages path---------------------*/

/*---------------------All Services base path------------------*/
var _ServicesUrl =
{
    _BaseServicePath: "http://50.23.221.50/EzDispatchermob/UserService/",
    //_SecondServicePath: "http://localhost:63124/UserService/"
    //_SecondServicePath: "http://50.23.221.50/EzDispatchermob/UserService/"
    _SecondServicePath: "http://72.13.12.113:8080/UserService/"
};
/*--------------------End Services base path-----------------*/

/*--------------------Wcf Method Name-----------------------*/
var _WcfFunctionUrl =
{
    _Validate: "Validate",
    _ValidateCrew: "ValidateCrew",
    _GetData: "GetDispatcherCalls",
    _CallList: "GetDispatcherCallList",
    _CallInfo: "GetCallDetails",
    _CurrentCall: "GetCurrentCallDetails",
    _SaveTimeStamps: "SaveTimeStampsInfo",
    _SendPassword: "SendPassword",
    _SaveCrewLocation: "SaveCrewLocation",
    _GetCallDetailsForTimeStamp: "GetTimeStampDetails",
    _GetDelayStatus: "GetDelayStatus",
    _SaveTimeStampWithoutMilege: "SaveGeneralTimeStampsInfo",
    _SaveTransportTimeStamps: "SaveTransportTimeStampsInfo",
    _SaveClearTimeStamps: "SaveClearTimeStampsInfo",
    _GetStatusWiseDynamicFields: "GetStatusWiseDynamicFields",
    _GetTransportDynamicFields: "GetTransportDynamicFields",
    _GetPatientAddrNoteDetails: "GetPatientAddressNoteDetails",
    _SavePatientAddressNote: "SavePatientAddressNote",
    _GetFacilities: "GetFacilities",
    _GetDevice: "GetDeviceDetails",
    _SendNotificationToAndroid: "SendNotificationToAndroid",
    _SendNotificationToiOS: "SendNotificationToIos",
    _SaveDeviceToken: "SaveDeviceToken",

    /*-------------------SuperVisor Section-------------------------------*/
    _GetSuperVisorUnits: "GetSuperVisorUnits",
    _GetSupVCallList: "GetSuperVisorCallList",
    _GetPrimaryCrewLocation: "GetPrimaryCrewLocation",
    _GetSuperVisorCallsCount: "GetSupVCallsCount"
    /*-------------------SuperVisor Section-------------------------------*/
};

var globalVar =
    {
        _GmapLocation: '',
        _IsSuperVisor: '',
        _SuperVisorID: '0',
        _SuperVisorName: ''
    }

var _SuperVisorUnits =
    {
        _CurrentSelected: [],
        _TotalUnits: [],
        _TotalSelection: '',
        _SupUnitDetails: []
    }


function GetUrl(path) {

    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");
    var Actpath = path + "?Id=" + Id + "&LocReq=" + getParameterByName("LocReq");
    var CurrentCallId = getParameterByName("CurrentCallId");
    if (path == "DispatcherCallList.html") {
        $.mobile.changePage(path, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
    }
    else if (path == "Index.html") {
        $.mobile.changePage(path, { data: { "LocReq": "0" }, transition: "fade" });
    }
    else {
        $.mobile.changePage(path, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId }, transition: "slide" });
    }
}
/*------------------End Wcf Method Name-------------------*/


/*----------------------Site path-------------------------*/
var _SiteFunctionalUrl =
    {
        _SaveCredentials: "SaveCredentials"
    };
/*---------------------End Site path---------------------*/



function GetCrewLocation() {
    
    if ($.mobile.activePage.data('mypage') != 'Index') {
        navigator.geolocation.getCurrentPosition(function (pos) {
            var lat = pos.coords.latitude;
            var lng = pos.coords.longitude;
            getAddressFromLatLang(lat, lng);
        });
    }    
}

function getAddressFromLatLang(lat, lng) {
    var geocoder = new google.maps.Geocoder();
    var latLng = new google.maps.LatLng(lat, lng);
    geocoder.geocode({ 'latLng': latLng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                SaveCrewLoc(getParameterByName("Id"), lat, lng, results[1].formatted_address);
            }
        } else {
            SaveCrewLoc(getParameterByName("Id"), lat, lng, '');
        }
    });
}

function SaveCrewLoc(Id, Lat, Lnt, addr) {

    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveCrewLocation;
    var time = new Date().getTimezoneOffset();
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        dataType: "json",
        data: { Id: Id, Lat: Lat, Lnt: Lnt, Addr: addr, Offset: time },
        url: ValidateUrl,
        beforeSend: function () {
        },
        success: function (result) {

            if (result.Data.toString() != '') {

                if (result.Data[0].CurrentCallId != '') {
                    if (getParameterByName("IsFirstLogin") == "1") {
                        if (result.Data[0].CallType.trim() == '[EMERGENCY CALL]') {
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
                    else if ($('div:jqmData(role="page"):visible').attr('data-mypage') == 'CallList') {
                        AppendNewCallToList(result.Data[0]);
                    }
                    else if (getParameterByName("CurrentCallId") != result.Data[0].CurrentCallId) {
                        if (result.Data[0].CallType.trim() == '[EMERGENCY CALL]') {
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
            }
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
        }
    });
}


function refreshPage() {
    jQuery.mobile.changePage(window.location.href, {
        //allowSamePageTransition: true,
        transition: 'none'
        //,reloadPage: true
    });
}

function closeNotice() {
    $(".InfoMessage").fadeOut("slow");
}


function showAlert(message, title) {
    if (navigator.notification) {
        navigator.notification.alert(message, null, title, 'OK');
    } else {
        alert(title ? (title + ": " + message) : message);
    }
}



function ezphonemessege() {
    var ethis = this;
    this.msg = "Processing. Please wait..";

    this.show = function (setting) {
        if (!!setting.msg) {
            this.msg = "";
        }
        $(".AlertMessage span").text(ethis.msg);
        $(".AlertMessage").show();
        return ethis;
    }
    this.hide = function () {
        $(".AlertMessage").hide();
        return ethis;
    }
}


function CheckSuperVisor() {
    if (globalVar._IsSuperVisor == "1") {
        $.mobile.activePage.find(".SuperVisorOptionTab").show();
        refreshNavbar($("#navbar"));
    }
    else {
        $.mobile.activePage.find(".SuperVisorOptionTab").hide();
        refreshNavbar($("#navbar"));
    }
}


function refreshNavbar(el) {

    var ul = el.children("ul"),
		childCount = 0, cls, visibleEl,
		children = ul.children("li"),
        clsList = ["ui-grid-solo", "ui-grid-a", "ui-grid-b", "ui-grid-c", "ui-grid-d", "ui-grid-duo"],
        clsArr = ["ui-grid-solo", "ui-grid-a", "ui-grid-b", "ui-grid-c", "ui-grid-d", "ui-grid-duo ui-grid-a"];

    for (var i = 0, j = children.length; i < j; i++) {
        child = $(children[i]);
        if ($(child).is(":visible")) {
            childCount++;
        }
    }

    cls = clsArr[childCount - 1];
    if (childCount == 1) {
        visibleEl = ul.children("li:visible");
        if (!visibleEl.hasClass("ui-block-a")) {
            visibleEl.addClass("ui-block-a");
        }
    } else {
        ul.children("li").removeClass("ui-block-a");
        ul.children("li:first").addClass("ui-block-a");
    }

    //remove existing grid class
    if (childCount > 0) {
        var rx = new RegExp(clsList.join("|"), "gi");
        var oldCls = ul.attr("class").replace(rx, function (matched) {
            return "";
        });
    }

    //set new grid class, preserving existing custom clases
    ul.attr("class", oldCls + " " + cls);

}



function GetSuperVisorUnits() {
    
    if ($.mobile.activePage.data('mypage') != 'Index') {
        var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetSuperVisorUnits;
        var count = 0;
        var Uhtml = '';
        var Mhtml = '';

        $.ajax({
            cache: false,
            type: "GET",
            async: false,
            dataType: "json",
            data: { Id: globalVar._SuperVisorID },
            url: ValidateUrl,
            beforeSend: function () {
            },
            success: function (result) {

                _SuperVisorUnits._TotalUnits = [];
                _SuperVisorUnits._SupUnitDetails = [];
                if (result.Data.toString() != '') {
                    globalVar._IsSuperVisor = "1";

                    if ($.isPlainObject(result.Data)) {
                        count++;
                        var Unit = new Object();
                        Unit.UnitId = result.Data[0].UnitId;
                        Unit.UnitName = result.Data[0].Unit;
                        _SuperVisorUnits._SupUnitDetails.push(Unit);
                        globalVar._SuperVisorName = result.Data[0].SuperVisorName;
                        _SuperVisorUnits._TotalUnits.push(result.Data[0].UnitId);
                        Uhtml = Uhtml + '<input type="checkbox" name="checkbox-Pick" id="chkUnit-' + result.Data[0].UnitId.trim() + '" class="custom" data-UnitId="' + result.Data[0].UnitId + '" data-UnitAssignedId="' + result.Data[0].UnitAssignedID + '" />'
                                    + '<label for="chkUnit-' + result.Data[0].UnitId.trim() + '">' + result.Data[0].Unit + '</label>';
                    }
                    else {
                        $(result.Data).each(function (i, v) {
                            count++;
                            var Unit = new Object();
                            Unit.UnitId = v.UnitId;
                            Unit.UnitName = v.Unit;
                            _SuperVisorUnits._SupUnitDetails.push(Unit);
                            globalVar._SuperVisorName = v.SuperVisorName;
                            _SuperVisorUnits._TotalUnits.push(v.UnitId);
                            Uhtml = Uhtml + '<input type="checkbox" name="checkbox-Pick" id="chkUnit-' + v.UnitId.trim() + '" class="custom"  data-UnitId="' + v.UnitId + '" data-UnitAssignedId="' + v.UnitAssignedID + '"  />'
                                    + '<label for="chkUnit-' + v.UnitId.trim() + '">' + v.Unit + '</label>';
                        });
                    }

                    if (count > 0) {
                        Mhtml = '<fieldset data-role="controlgroup"> ' + Uhtml + ' </fieldset>';
                    }
                    else {
                        Mhtml = '<fieldset data-role="controlgroup"> No Unit Assigned </fieldset>';
                    }
                }
            },
            error: function (xhr) {
                // message=xhr.responseBody;
            },
            complete: function () {
                _SuperVisorUnits._TotalSelection = Mhtml;
            }
        });
    }
}


function runtimePopupForSupUnits(message, popupafterclose) {
    var template = "<div data-role='popup' id='popupLogin' data-theme='a' data-overlay-theme='a' class='ui-corner-all ui-content messagePopup'>"
                   + "<form><div style='padding: 10px 20px;'> <h3 id='sv-selectedUnits'></h3>"
                   + "<a href='#' data-rel='back' data-role='button' data-theme='a' data-icon='delete' data-iconpos='notext' class='ui-btn-right right' style='border-top-width: 1px; top: -35px; right: -20px;'>Close</a> <br />"
                   + "<div data-role='collapsible-set' data-theme='b' data-content-theme='c' data-collapsed-icon='arrow-r' data-expanded-icon='arrow-d'"
                   + " style='margin: 0; width: 250px;'>"
                   + " <div data-role='collapsible' data-inset='false'> <h2>Select Unit</h2>"
                   + " <ul data-role='listview' id='sv-ul-AssignUnit'> <li>"
                   + " <div data-role='fieldcontain' id='sv-AssignUnits'> "
                   + (_SuperVisorUnits._TotalSelection == '' ? 'No Unit assigned.' : _SuperVisorUnits._TotalSelection)
                   + " </div> </li> </ul>"
                   + " </div> </div> <br />"
                   + "<a data-role='button' id='btnGotoUnit' onclick='javascript:GetSuperVisorUnitList();' data-theme='b' data-icon='check' data-shadow='true'>Go</a>"
                   + "<span id='SV-Popup-Validataion' style='color:red; display:none;'>*Please select the Unit</span></div> </form> </div>"

    popupafterclose = popupafterclose ? popupafterclose : function () { };
    $.mobile.activePage.append(template).trigger("create");    
}


function ShowSuperVisorUnitPopUp() {
    $.mobile.activePage.find("#popupLogin").popup("open", { positionTo: "center" });
}

function GetSuperVisorUnitList() {

    if ($.mobile.activePage.find('#sv-AssignUnits').find('input[type="checkbox"]:checked').length > 0) {
        _SuperVisorUnits._CurrentSelected = [];
        $.mobile.activePage.find('#sv-AssignUnits').find('input[type="checkbox"]:checked').each(function () {
            _SuperVisorUnits._CurrentSelected.push($(this).data('unitid'));
        });
        $.mobile.changePage($.mobile.activePage.data('issup') == "1" ? "Sup-dashboard.html" : _SiteUrl.sup_detail,
                                {
                                    data: {
                                        "Id": getParameterByName("Id"), "CallId": getParameterByName("CurrentCallId"), "LocReq": getParameterByName("LocReq"),
                                        "CurrentCallId": getParameterByName("CurrentCallId"), "IsFirstLogin": getParameterByName("IsFirstLogin")
                                    }, transition: "slide"
                                });
    }
    else if ($.mobile.activePage.find('#sv-AssignUnits').text().trim() == "No Unit assigned.") {
        $('#SV-Popup-Validataion').text('You are not assigned as a SuperVisor to any Unit.');
        $('#SV-Popup-Validataion').fadeIn();
    }
    else {
        $.mobile.activePage.find('#SV-Popup-Validataion').fadeIn();
    }
}

function BindNecessaryFunctions() {
    CheckSuperVisor();
    runtimePopupForSupUnits('', false);
    if (globalVar._IsSuperVisor == "1") {
        $.mobile.activePage.find(".SuperVisorName").text(globalVar._SuperVisorName);
    }
    $.mobile.activePage.find("#popupLogin").popup({
        afteropen: function (event, ui) {
            $('#SV-Popup-Validataion').hide();
            for (i = 0; i < _SuperVisorUnits._CurrentSelected.length; i++) {
                $('#sv-AssignUnits').find('input[data-unitid="' + _SuperVisorUnits._CurrentSelected[i] + '"]').prop('checked', true).checkboxradio("refresh");
            }
            $.mobile.activePage.find('#btnGotoUnit').width($.mobile.activePage.find('#popupLogin').find('.ui-collapsible-set').width());
        },
        afterclose: function (event, ui) {
            $('.SuperVisorOptionTab').attr('data-theme', 'a');
            refreshNavbar($("#navbar"));
        }
    });
}


function GetSupVCallListPath(status) {

    if (status != '') {
        var Id = getParameterByName("Id");
        var LocReq = getParameterByName("LocReq");
        var CurrentCallId = GetCrewCurrentCallId();
        $.mobile.changePage(_SiteUrl.sup_CallListFrmDashboard, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId, "StatusId": status }, transition: "slide" });
    }
    else if (status == '' && $.mobile.activePage.data('issup') == '1') {
        var Id = getParameterByName("Id");
        var LocReq = getParameterByName("LocReq");
        var CurrentCallId = GetCrewCurrentCallId();
        $.mobile.changePage(_SiteUrl.sup_CallListFrmDashboard, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId, "StatusId": status }, transition: "slide" });
    }
    else {
        var Id = getParameterByName("Id");
        var LocReq = getParameterByName("LocReq");
        var CurrentCallId = GetCrewCurrentCallId();
        $.mobile.changePage(_SiteUrl.sup_CallList, { data: { "Id": Id, "LocReq": LocReq, "CurrentCallId": CurrentCallId, "StatusId": status }, transition: "slide" });
    }
}


function RefreshUnitLocation() {

    if ($.mobile.activePage.data('mypage') == 'sup-ToFromLocation' && globalVar._IsSuperVisor == '1') {
        new ezphonemessege().show({
            msg: 'Processing..Please wait..'
        });
        var mobileDemo = { 'center': '57.7973333,12.0502107', 'zoom': 10 };
        var source = getParameterByName("origin");
        var destination = getParameterByName("Destination");
        $('#from1').val(source);
        $('#to1').val(getParameterByName("Destination"));

        var Id = getParameterByName("Id");
        var Unit = getParameterByName("Unit");
        var time = new Date().getTimezoneOffset();
        var CallId = getParameterByName("CallId");
        var UnitId = $.map(_SuperVisorUnits._SupUnitDetails, function (i, v) {
            if (i.UnitName == Unit)
                return i;
        })[0].UnitId;

        $.getJSON(_ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetPrimaryCrewLocation, { Id: Id, UnitIds: UnitId, Offset: time, CallId: CallId }, function (data) {
            var amb = 1;
            $.each(data.Data, function (i, marker) {
                if (amb > 10) {
                    amb = 1;
                }
                if (marker.Latitude != "-1" && marker.Longitude != "-1") {
                    $.mobile.activePage.find('#map_canvas_2').gmap('addMarker', {
                        'position': new google.maps.LatLng(marker.Latitude, marker.Longitude),
                        'bounds': true,
                        'animation': google.maps.Animation.DROP,
                        'icon': new google.maps.MarkerImage('../images/MapImages/Ambulance' + amb + '.png')
                    }).click(function () {
                        $.mobile.activePage.find('#map_canvas_2').gmap('openInfoWindow', {
                            'content': marker.Unit,
                            'closeBoxMargin': "10px 2px 2px 2px",
                            'closeBoxURL': "http://www.google.com/intl/en_us/mapfiles/close.gif"
                        }, this);                        
                    });
                }
            });
            $.mobile.activePage.find('#map_canvas_2').gmap('option', 'zoom', 8);
            $.mobile.activePage.find('#map_canvas_2').gmap('refresh');
        });

        new ezphonemessege().hide();
    }
}


function RemoveGoogleMoreLinks() {
    $.mobile.activePage.find("#map_canvas_2").find("a").removeAttr("href");    
    $(".gm-rev").hide();
    $(".gm-website").hide();
    setTimeout(function () { $(".gm-rev").hide() }, 1000);
    setTimeout(function () { $(".gm-website").hide() }, 1000);
    //$("[class^='gm-website']").hide();
}
