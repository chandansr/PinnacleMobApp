using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace ezDispatcherWebSite.WCFService.ServiceInterface
{
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// This is the interface method we are using for authentication purpose.
        /// </summary>
        /// <param name="username">User name for Sign in </param>
        /// <param name="password">Password for Sign in</param>
        /// <returns>This method only returns message that user is valid or not</returns>
        


        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ValidateCrew?Username={Username}&Password={Password}&DeviceId={DeviceId}&DeviceType={DeviceType}&Offset={Offset}")]
        List<ezLogin> ValidateCrew(string Username, string Password, string DeviceId, string DeviceType, int Offset);


        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetDispatcherCalls?PDID={PDID}&CallId={CallId}")]
        Stream GetDispatcherCalls(string PDID, string CallId);

        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetDispatcherCallList?Id={Id}&Offset={Offset}")]
        Stream GetDispatcherCallList(string Id, int Offset);


        /// <summary>
        /// This is the interface method we are using for getting all the details related to call.
        /// </summary>
        /// <returns>This method only returns list of details of Dispatcher Calls</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetCallDetails?CallId={CallId}")]
        Stream GetCallDetails(string CallId);


        /// <summary>
        /// This is the interface method we are using for getting the current call details of the crew.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetCurrentCallDetails?Id={Id}")]
        Stream GetCurrentCallDetails(string Id);

        
        /// <summary>
        /// This is the interface method we are using for saving the time stamp details of the user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveTimeStampsInfo?Id={Id}&CallId={CallId}&Status={Status}&CallNo={CallNo}&Milege={Milege}&Desc={Desc}&offset={offset}&IsDelay={IsDelay}&DelayReason={DelayReason}")]
        Stream SaveTimeStampsInfo(int Id, int CallId, string Status, string CallNo, string Milege, string Desc, int offset, string IsDelay, string DelayReason);


        /// <summary>
        /// This is the interface method we are using for sending password through email.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SendPassword?EmailId={EmailId}")]
        Stream SendPassword(string EmailId);


        /// <summary>
        /// This is the interface method we are using for saving the current location of the Primary crew.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveCrewLocation?Id={Id}&Lat={Lat}&Lnt={Lnt}&Addr={Addr}&Offset={Offset}")]
        Stream SaveCrewLocation(int Id, string Lat, string Lnt, string Addr, int Offset);


        /// <summary>
        /// This is the interface method we are using for getting the details of calls for TimeStamp page.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetTimeStampDetails?Id={Id}&CallId={CallId}")]
        Stream GetTimeStampDetails(int Id, int CallId);


        /// <summary>
        /// This is the interface method we are using for getting the Delay status for the unit.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetDelayStatus?Id={Id}&CallId={CallId}&Status={Status}&Offset={Offset}")]
        Stream GetDelayStatus(int Id, int CallId, string Status, int Offset);


        /// <summary>
        /// This is the interface method we are using for saving the time stamp details of the user in General Format(Without Milege).
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveGeneralTimeStampsInfo?Id={Id}&CallId={CallId}&Status={Status}&offset={offset}&JsonData={JsonData}")]
        Stream SaveGeneralTimeStampsInfo(int Id, int CallId, string Status, int offset, string JsonData);


        /// <summary>
        /// This is the interface method we are using for saving the Transport time stamp details of the user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveTransportTimeStampsInfo?Id={Id}&CallId={CallId}&Status={Status}&offset={offset}&JsonData={JsonData}")]
        Stream SaveTransportTimeStampsInfo(int Id, int CallId, string Status, int offset, string JsonData);


        /// <summary>
        /// This is the interface method we are using for saving the Clear time stamp details of the user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveClearTimeStampsInfo?Id={Id}&CallId={CallId}&Status={Status}&offset={offset}&PCR={PCR}")]
        Stream SaveClearTimeStampsInfo(int Id, int CallId, string Status, int offset, string PCR);


        /// <summary>
        /// This is the interface method we are using for getting the dynamic fields for the status.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetStatusWiseDynamicFields?Id={Id}&CallId={CallId}&Status={Status}&Offset={Offset}")]
        Stream GetStatusWiseDynamicFields(int Id, int CallId, string Status, int Offset);
        
        
        /// <summary>
        /// This is the interface method we are using for getting the dynamic fields for the status(Transport).
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetTransportDynamicFields?Id={Id}&CallId={CallId}&Status={Status}&Offset={Offset}")]
        Stream GetTransportDynamicFields(int Id, int CallId, string Status, int Offset);


        /// <summary>
        /// This is the interface method we are using for getting the Call Details for the Patient address nOte page.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPatientAddressNoteDetails?Id={Id}&CallId={CallId}")]
        Stream GetPatientAddressNoteDetails(int Id, int CallId);



        /// <summary>
        /// This is the interface method we are using for saving the Patient Address notes to dispatcher.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SavePatientAddressNote?Id={Id}&CallId={CallId}&Status={Status}&jsonData={jsonData}&InsuranceInfo={InsuranceInfo}")]
        Stream SavePatientAddressNote(int Id, int CallId, string Status, string jsonData, string InsuranceInfo);


        /// <summary>
        /// This is the interface method we are using for getting the facilities.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetFacilities?Id={Id}&Facility={Facility}")]
        Stream GetFacilities(int Id, string Facility);


        /// <summary>
        /// This is the interface method we are using for getting the Device details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetDeviceDetails?DeviceId={DeviceId}&DeviceType={DeviceType}")]
        Stream GetDeviceDetails(string DeviceId, string DeviceType);


        /// <summary>
        /// This is the interface method we are using for sending the Push Notification to Android.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SendNotificationToAndroid?deviceId={deviceId}&message={message}")]
        string SendNotificationToAndroid(string deviceId, string message);


        /// <summary>
        /// This is the interface method we are using for sending the Push Notification to Iphone.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SendNotificationToIos?deviceId={deviceId}&message={message}")]
        string SendNotificationToIos(string deviceId, string message);



        /// <summary>
        /// This is the interface method we are using for saving the device Token needed for PushNotification.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveDeviceToken?Id={Id}&DeviceId={DeviceId}&DeviceToken={DeviceToken}&DevicePlatform={DevicePlatform}&jsonData={jsonData}")]
        Stream SaveDeviceToken(int Id, string DeviceId, string DeviceToken, string DevicePlatform, string jsonData);

        
        
        /// <summary>
        /// This is the interface method we are using for getting the SuperVisor Units.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSuperVisorUnits?Id={Id}")]
        Stream GetSuperVisorUnits(int Id);


        /// <summary>
        /// This is the interface method we are using for getting the SuperVisor Call List.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSuperVisorCallList?Id={Id}&Status={Status}&UnitIds={UnitIds}&Offset={Offset}")]
        Stream GetSuperVisorCallList(int Id, string Status, string UnitIds, int Offset);



        /// <summary>
        /// This is the interface method we are using for getting the current location of the crew(SuperVisor option).
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPrimaryCrewLocation?Id={Id}&UnitIds={UnitIds}&Offset={Offset}&CallId={CallId}")]
        Stream GetPrimaryCrewLocation(int Id, string UnitIds, int Offset, int CallId);        



        
        /// <summary>
        /// This is the interface method we are using for getting the Calls count for the SUpervisors.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSupVCallsCount?PId={PId}&PUnits={PUnits}")]
        Stream GetSupVCallsCount(int PId, string PUnits);        

    }


    [DataContract]
    public class ezLogin
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Response { get; set; }
        [DataMember]
        public string Result { get; set; }
    }
   
}