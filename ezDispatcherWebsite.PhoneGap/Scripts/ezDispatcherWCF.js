

/*------------------------- ezDispatcherWCF js detail  ------------------------------------------------------------*/

// Created By:-Smartdata Developer
// Created Date:-07-02-2013
// Purpose:-declare and implement wcf method call by json
//          so we can access it on all page

/*--------------------------End ezDispatcherWCF js detail-----------------------------------------------------------*/


$(function () {

});


function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}


function NumberTrace(number) {

    var ValidateUrl = "http://wwwa.way2sms.com/jsp/LocateMobile.jsp?mobile1=" + number;
    var message = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        url: ValidateUrl,
        success: function (result) {
            var jsonAOData = JSON.stringify(result);
            return jsonAOData
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        }

    });

}

function Login(userName, password, DeviceId, DeviceType) {

    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._ValidateCrew;
    var time = new Date().getTimezoneOffset();
    var msg;
    var CrewDetails = new Object();

    userName = $('#txtUserName').val();
    password = $('#txtPassword').val();
    
    CrewDetails = {
        Username: userName,
        Password: password,
        DeviceId: DeviceId,
        DeviceType: DeviceType,
        Offset: time
    };
    
    //alert(ValidateUrl);
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        url: ValidateUrl,
        //data: '{"Username":"' + userName + '","Password":"' + password + '","DeviceId":"' + DeviceId + '","DeviceType":"' + DeviceType + '","Offset":"' + time + '"}',
        contentType: "application/json; charset=utf-8",
        //url: 'UserService.svc/ValidateCrew',
        //data: JSON.stringify(CrewDetails),
        data: JSON.stringify({ "CrewDetails": CrewDetails }),
        processData: false,
        beforeSend: function () {
            new ezphonemessege().show({
                msg: 'Processing.. Please wait..'
            });
        },
        success: function (result) {            
            msg = result[0];
        },
        error: function (xhr) {
            // message=xhr.responseBody;
            //alert(xhr.text);
        },
        complete: function () {
            new ezphonemessege().hide();
        }
    });

    return msg;
}

function SendPassword() {
    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SendPassword;
    var msg;
    var EmailId = $("#txtEmail").val();
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "EmailId": EmailId }),
        url: ValidateUrl,
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $.mobile.showPageLoadingMsg("a", "Loading...");
        },
        success: function (result) {

            if (result.Data == "" && result.Message != "Failure") {
                $("#divError").html("<span style='color: red'>The email id doesn't have any account in the application.</span>");
                $("#divError").css("display", "block");
            }
            else if (result.Data == "" && result.Message == "Failure") {
                $("#divError").html("<span style='color: red'>some error occured. Please try again after some time.</span>");
                $("#divError").css("display", "block");
            }
            else {
                $("#divError").html("<span style='color: green'>Your login credentials has been send to the provided Email address.</span>");
                $("#divError").css("display", "block");
                $("#lnkPasswordSendConfirm").click();
            }
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
            $.mobile.loading('hide');
        }
    });
    return msg;
}


function GetCurrentCall() {
    new ezphonemessege().show({
        msg: 'Please wait.. Getting data from server.. '
    });
    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._CurrentCall;
    var Id = getParameterByName("Id");
    var LocReq = getParameterByName("LocReq");

    var sucess = "0";
    var CallId = "";
    var path = _SiteUrl.CallDetail;
    var DispatcherCallsList = { Id: Id };

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "DispatcherCallsList": DispatcherCallsList }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
            new ezphonemessege().show({
                msg: 'Please wait.. Getting data from server.. '
            });
        },
        success: function (result) {
            if (result.Data != "") {
                sucess = "1";
                CallId = $.trim(result.Data);
            }
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {

            if (sucess == "1") {
                $.mobile.changePage(path, { data: { "Id": Id, "CallId": CallId, "LocReq": LocReq, "CurrentCallId": CallId }, transition: "slide" });
            }
            else {
                showAlert('You are not assigned to any of the call', 'Info');
            }
            new ezphonemessege().hide();
        }
    });
}



function SendNotificationToDevice(DeviceId, Message, IsAndroid) {
    if (IsAndroid == "1") {
        var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SendNotificationToAndroid;
    }
    else {
        var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SendNotificationToiOS;
    }
    var DispatcherCallsList = { deviceId: DeviceId, message: Message };
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "DispatcherCallsList": DispatcherCallsList }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
            new ezphonemessege().show({
                msg: 'Processing.. Please wait..'
            });
        },
        success: function (result) {

        },
        error: function (xhr) {
            showAlert('Error while sending Notification', 'Info');
        },
        complete: function () {
            new ezphonemessege().hide();
        }
    });
}




function GetCrewCurrentCallId() {
    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._CurrentCall;
    var Id = getParameterByName("Id");
    var sucess = "0";
    var CallId = "";
    var path = _SiteUrl.CallDetail;
    var DispatcherCallsList = { Id: Id };

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "DispatcherCallsList": DispatcherCallsList }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
        },
        success: function (result) {
            if (result.Data != "") {
                sucess = "1";
                CallId = $.trim(result.Data);
            }
            else {
                CallId = "0";
            }

        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
        }
    });
    return CallId;
}


function GetLoginCrewCurrentCallId(Id) {
    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._CurrentCall;
    var Id = Id;
    var sucess = "0";
    var CallId = "";
    var path = _SiteUrl.CallDetail;
    var DispatcherCallsList = { Id: Id };
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "DispatcherCallsList": DispatcherCallsList }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
        },
        success: function (result) {
            if (result.Data != "") {
                sucess = "1";
                CallId = $.trim(result.Data);
            }
            else {
                CallId = "0";
            }
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
        }
    });
    return CallId;
}

function saveDeviceToken(ParamedicID, DeviceId, DeviceToken, DevicePlatform) {

    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._SaveDeviceToken;
    var time = new Date().getTimezoneOffset();
    var jsonData = new Object();
    jsonData.Offset = (-1) * time;
    var sendJson = JSON.stringify(jsonData);
    var Device = { Id: ParamedicID, DeviceId: DeviceId, DeviceToken: DeviceToken, DevicePlatform: DevicePlatform, jsonData: sendJson };
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "Device": Device }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
        },
        success: function (result) {

        },
        error: function (xhr) {
        },
        complete: function () {

        }
    });

}


function X2JS() {
    var VERSION = "1.0.11";
    var escapeMode = false;

    var DOMNodeTypes = {
        ELEMENT_NODE: 1,
        TEXT_NODE: 3,
        CDATA_SECTION_NODE: 4,
        DOCUMENT_NODE: 9
    };

    function getNodeLocalName(node) {
        var nodeLocalName = node.localName;
        if (nodeLocalName == null) // Yeah, this is IE!! 
            nodeLocalName = node.baseName;
        if (nodeLocalName == null || nodeLocalName == "") // =="" is IE too
            nodeLocalName = node.nodeName;
        return nodeLocalName;
    }

    function getNodePrefix(node) {
        return node.prefix;
    }

    function escapeXmlChars(str) {
        if (typeof (str) == "string")
            return str.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#x27;').replace(/\//g, '&#x2F;');
        else
            return str;
    }

    function unescapeXmlChars(str) {
        return str.replace(/&amp;/g, '&').replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&quot;/g, '"').replace(/&#x27;/g, "'").replace(/&#x2F;/g, '\/')
    }

    function parseDOMChildren(node) {
        if (node.nodeType == DOMNodeTypes.DOCUMENT_NODE) {
            var result = new Object;
            var child = node.firstChild;
            var childName = getNodeLocalName(child);
            result[childName] = parseDOMChildren(child);
            return result;
        }
        else
            if (node.nodeType == DOMNodeTypes.ELEMENT_NODE) {
                var result = new Object;
                result.__cnt = 0;

                var nodeChildren = node.childNodes;

                // Children nodes
                for (var cidx = 0; cidx < nodeChildren.length; cidx++) {
                    var child = nodeChildren.item(cidx); // nodeChildren[cidx];
                    var childName = getNodeLocalName(child);

                    result.__cnt++;
                    if (result[childName] == null) {
                        result[childName] = parseDOMChildren(child);
                        result[childName + "_asArray"] = new Array(1);
                        result[childName + "_asArray"][0] = result[childName];
                    }
                    else {
                        if (result[childName] != null) {
                            if (!(result[childName] instanceof Array)) {
                                var tmpObj = result[childName];
                                result[childName] = new Array();
                                result[childName][0] = tmpObj;

                                result[childName + "_asArray"] = result[childName];
                            }
                        }
                        var aridx = 0;
                        while (result[childName][aridx] != null) aridx++;
                        (result[childName])[aridx] = parseDOMChildren(child);
                    }
                }

                // Attributes
                for (var aidx = 0; aidx < node.attributes.length; aidx++) {
                    var attr = node.attributes.item(aidx); // [aidx];
                    result.__cnt++;
                    result["_" + attr.name] = attr.value;
                }

                // Node namespace prefix
                var nodePrefix = getNodePrefix(node);
                if (nodePrefix != null && nodePrefix != "") {
                    result.__cnt++;
                    result.__prefix = nodePrefix;
                }

                if (result.__cnt == 1 && result["#text"] != null) {
                    result = result["#text"];
                }

                if (result["#text"] != null) {
                    result.__text = result["#text"];
                    if (escapeMode)
                        result.__text = unescapeXmlChars(result.__text)
                    delete result["#text"];
                    delete result["#text_asArray"];
                }
                if (result["#cdata-section"] != null) {
                    result.__cdata = result["#cdata-section"];
                    delete result["#cdata-section"];
                    delete result["#cdata-section_asArray"];
                }

                if (result.__text != null || result.__cdata != null) {
                    result.toString = function () {
                        return (this.__text != null ? this.__text : '') + (this.__cdata != null ? this.__cdata : '');
                    }
                }
                return result;
            }
            else
                if (node.nodeType == DOMNodeTypes.TEXT_NODE || node.nodeType == DOMNodeTypes.CDATA_SECTION_NODE) {
                    return node.nodeValue;
                }
    }

    function startTag(jsonObj, element, attrList, closed) {
        var resultStr = "<" + ((jsonObj != null && jsonObj.__prefix != null) ? (jsonObj.__prefix + ":") : "") + element;
        if (attrList != null) {
            for (var aidx = 0; aidx < attrList.length; aidx++) {
                var attrName = attrList[aidx];
                var attrVal = jsonObj[attrName];
                resultStr += " " + attrName.substr(1) + "='" + attrVal + "'";
            }
        }
        if (!closed)
            resultStr += ">";
        else
            resultStr += "/>";
        return resultStr;
    }

    function endTag(jsonObj, elementName) {
        return "</" + (jsonObj.__prefix != null ? (jsonObj.__prefix + ":") : "") + elementName + ">";
    }

    function endsWith(str, suffix) {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }

    function jsonXmlSpecialElem(jsonObj, jsonObjField) {
        if (endsWith(jsonObjField.toString(), ("_asArray"))
				|| jsonObjField.toString().indexOf("_") == 0
				|| (jsonObj[jsonObjField] instanceof Function))
            return true;
        else
            return false;
    }

    function jsonXmlElemCount(jsonObj) {
        var elementsCnt = 0;
        if (jsonObj instanceof Object) {
            for (var it in jsonObj) {
                if (jsonXmlSpecialElem(jsonObj, it))
                    continue;
                elementsCnt++;
            }
        }
        return elementsCnt;
    }

    function parseJSONAttributes(jsonObj) {
        var attrList = [];
        if (jsonObj instanceof Object) {
            for (var ait in jsonObj) {
                if (ait.toString().indexOf("__") == -1 && ait.toString().indexOf("_") == 0) {
                    attrList.push(ait);
                }
            }
        }
        return attrList;
    }

    function parseJSONTextAttrs(jsonTxtObj) {
        var result = "";

        if (jsonTxtObj.__cdata != null) {
            result += "<![CDATA[" + jsonTxtObj.__cdata + "]]>";
        }

        if (jsonTxtObj.__text != null) {
            if (escapeMode)
                result += escapeXmlChars(jsonTxtObj.__text);
            else
                result += jsonTxtObj.__text;
        }
        return result
    }

    function parseJSONTextObject(jsonTxtObj) {
        var result = "";

        if (jsonTxtObj instanceof Object) {
            result += parseJSONTextAttrs(jsonTxtObj)
        }
        else
            if (jsonTxtObj != null) {
                if (escapeMode)
                    result += escapeXmlChars(jsonTxtObj);
                else
                    result += jsonTxtObj;
            }

        return result;
    }

    function parseJSONArray(jsonArrRoot, jsonArrObj, attrList) {
        var result = "";
        if (jsonArrRoot.length == 0) {
            result += startTag(jsonArrRoot, jsonArrObj, attrList, true);
        }
        else {
            for (var arIdx = 0; arIdx < jsonArrRoot.length; arIdx++) {
                result += startTag(jsonArrRoot[arIdx], jsonArrObj, parseJSONAttributes(jsonArrRoot[arIdx]), false);
                result += parseJSONObject(jsonArrRoot[arIdx]);
                result += endTag(jsonArrRoot[arIdx], jsonArrObj);
            }
        }
        return result;
    }

    function parseJSONObject(jsonObj) {
        var result = "";

        var elementsCnt = jsonXmlElemCount(jsonObj);

        if (elementsCnt > 0) {
            for (var it in jsonObj) {

                if (jsonXmlSpecialElem(jsonObj, it))
                    continue;

                var subObj = jsonObj[it];

                var attrList = parseJSONAttributes(subObj)

                if (subObj == null || subObj == undefined) {
                    result += startTag(subObj, it, attrList, true)
                }
                else
                    if (subObj instanceof Object) {

                        if (subObj instanceof Array) {
                            result += parseJSONArray(subObj, it, attrList)
                        }
                        else {
                            var subObjElementsCnt = jsonXmlElemCount(subObj);
                            if (subObjElementsCnt > 0 || subObj.__text != null || subObj.__cdata != null) {
                                result += startTag(subObj, it, attrList, false);
                                result += parseJSONObject(subObj);
                                result += endTag(subObj, it);
                            }
                            else {
                                result += startTag(subObj, it, attrList, true);
                            }
                        }
                    }
                    else {
                        result += startTag(subObj, it, attrList, false);
                        result += parseJSONTextObject(subObj);
                        result += endTag(subObj, it);
                    }
            }
        }
        result += parseJSONTextObject(jsonObj);

        return result;
    }

    this.parseXmlString = function (xmlDocStr) {
        var xmlDoc;
        if (window.DOMParser) {
            var parser = new window.DOMParser();
            xmlDoc = parser.parseFromString(xmlDocStr, "text/xml");
        }
        else {
            // IE :(
            if (xmlDocStr.indexOf("<?") == 0) {
                xmlDocStr = xmlDocStr.substr(xmlDocStr.indexOf("?>") + 2);
            }
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.loadXML(xmlDocStr);
        }
        return xmlDoc;
    }

    this.xml2json = function (xmlDoc) {
        return parseDOMChildren(xmlDoc);
    }

    this.xml_str2json = function (xmlDocStr) {
        var xmlDoc = this.parseXmlString(xmlDocStr);
        return this.xml2json(xmlDoc);
    }

    this.json2xml_str = function (jsonObj) {
        return parseJSONObject(jsonObj);
    }

    this.json2xml = function (jsonObj) {
        var xmlDocStr = this.json2xml_str(jsonObj);
        return this.parseXmlString(xmlDocStr);
    }

    this.getVersion = function () {
        return VERSION;
    }

    this.escapeMode = function (enabled) {
        escapeMode = enabled;
    }
}



function GetSupVPrimaryCrewlocation() {

    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetPrimaryCrewLocation;

    var Id = getParameterByName("Id");
    var UnitID = getParameterByName("UnitIds");
    var time = new Date().getTimezoneOffset();
    var SuperVisorUnitsDetails = { Id: Id, UnitIds: UnitID, Offset: time };
    $.ajax({
        cache: false,
        type: "GET",
        async: false,
        dataType: "json",
        data: { Id: Id, UnitIds: UnitID, Offset: time },
        // contentType: "application/json; charset=utf-8",
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



function GetSupVCallsCount() {

    var ValidateUrl = _ServicesUrl._SecondServicePath + _WcfFunctionUrl._GetSuperVisorCallsCount;
    var Id = getParameterByName("Id");
    var units = _SuperVisorUnits._CurrentSelected.toString();
    var SupVCalls = { PId: Id, PUnits: units };
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        dataType: "json",
        data: JSON.stringify({ "SupVCalls": SupVCalls }),
        contentType: "application/json; charset=utf-8",
        url: ValidateUrl,
        beforeSend: function () {
        },
        success: function (result) {
            var data = result.Data;
            $.each($.mobile.activePage.find("a:jqmData(statusid)"), function () {
                var id = $(this).data('statusid');
                var sp = $('#SupVCallCount' + id);
                var el = '<span class="ui-btn-up-c ui-btn-corner-all" id="SupVCallCount' + id + '" '
                          + 'style="font-size: 11px; font-weight: bold; padding: 0.2em 0.5em; position: absolute; right:10px;">0</span>';
                $.mobile.activePage.find('a:jqmData(statusid="' + id + '") .ui-btn-text').after(el);
            });

            $.each(data, function (i, v) {
                var sp = $('#SupVCallCount' + v.text);
                $.mobile.activePage.find(sp).text(v.value);
            });
        },
        error: function (xhr) {
            // message=xhr.responseBody;
        },
        complete: function () {
            $.mobile.activePage.find("a:jqmData(role='button')").buttonMarkup("refresh");
        }
    });

}


function runtimePopupForSupAvlUnits() {
    var template = "<div data-role='popup' id='popupSupAvlUnits' data-theme='a' data-overlay-theme='a' class='ui-corner-all ui-content messagePopup'>"
                   + "<form><div style='padding: 10px 20px;'> <h1 id='spAvl-unitname'></h1>"
                   + "<a href='#' data-rel='back' data-role='button' data-theme='a' data-icon='delete' data-iconpos='notext' class='ui-btn-right right' style='border-top-width: 1px; top: -35px; right: -20px;'>Close</a> <br />"
                   + "<strong>Primary Crew: </strong><span id='spAvl-priparaname'></span>"
                   + " <br />"
                   + "<strong>Available Since: </strong><span id='spAvl-availablesince'></span>"
                   + " <br />"
                   + "<strong>Unit Location: </strong><span id='spAvl-unitlocation'></span>"
                   + " <br /><br />"
                   + "<a data-role='button' data-theme='b' id='btnshowavlunitlocation' data-icon='forward' onclick='GetSupAvlUnitGoogleLocation();' data-shadow='true'>Show Location</a>"
                   + "<a data-role='button' data-rel='back' data-theme='b' data-icon='check' data-shadow='true'>Close</a>"
                   + "</div> </form> </div>"

    $.mobile.activePage.append(template).trigger("create");
}

function IsValidLatlng(lat, lng) {
    if (lat == -1 && lng == -1) {
        return false;
    }
    else if (lat == 0 && lng == 0) {
        return false;
    }
    else if ((lat == -1 && lng == 0) || (lat == 0 && lng == -1)) {
        return false;
    }
    else if (lat == undefined || lng == undefined) {
        return false;
    }
    else {
        return true;
    }
}

function GetmidPoint(latlng1, latlng2) {
    var ltln = new Object();
    ltln.Latitude = (latlng1.Latitude + latlng2.Latitude) / 2;
    ltln.Longitude = (latlng1.Longitude + latlng2.Longitude) / 2;
    return (ltln);
}


function GetLocationForAddress(source, destination) {
    var obj = new Object();
    var exist = 0;
    var fromgeocoder = new google.maps.Geocoder();
    fromgeocoder.geocode({ 'address': source }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            obj.Fac = source;
            obj.Latitude = results[0].geometry.location.lat();
            obj.Longitude = results[0].geometry.location.lng();
            $.each(FromToFacLatLng, function (i, dt) {
                if (dt.Fac == source) {
                    exist = 1;
                }
            });
            if (exist == 0) {
                FromToFacLatLng.push(obj);
            }
        }
    });

    var togeocoder = new google.maps.Geocoder();
    togeocoder.geocode({ 'address': destination }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            obj = new Object();
            obj.Fac = destination;
            obj.Latitude = results[0].geometry.location.lat();
            obj.Longitude = results[0].geometry.location.lng();
            $.each(FromToFacLatLng, function (i, dt) {
                if (dt.Fac == destination) {
                    exist = 1;
                }
            });
            if (exist == 0) {
                FromToFacLatLng.push(obj);
            }
        }
    });
}
