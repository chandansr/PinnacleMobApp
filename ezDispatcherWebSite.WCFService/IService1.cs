using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ezDispatcherWebSite.WCFService
{

    #region SERVICECONTRACT
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        #region TestMethod
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        UserDetails[] GetUserDetails(string Username);
        #endregion


        #region ValidateCrew
        /// <summary>
        /// This is the interface method we are using for authentication purpose.
        /// </summary>
        /// <param name="username">User name for Sign in </param>
        /// <param name="password">Password for Sign in</param>
        /// <returns>This method only returns message that user is valid or not</returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<ezLogin> ValidateCrew(CrewDetails CrewDetails);
        #endregion


        #region GetDispatcherCalls
        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDispatcherCalls(DispatcherCalls DispatcherCalls);
        #endregion


        #region GetDispatcherCallList
        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDispatcherCallList(DispatcherCallsList DispatcherCallsList);
        #endregion


        #region GetCallDetails
        /// <summary>
        /// This is the interface method we are using for getting all the details related to call.
        /// </summary>
        /// <returns>This method only returns list of details of Dispatcher Calls</returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetCallDetails(DispatcherCalls DispatcherCalls);
        #endregion


        #region GetCurrentCallDetails
        /// <summary>
        /// This is the interface method we are using for getting the current call details of the crew.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetCurrentCallDetails(DispatcherCallsList DispatcherCallsList);
        #endregion


        #region SaveTimeStampInfo
        /// <summary>
        /// This is the interface method we are using for saving the time stamp details of the user.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveTimeStampsInfo(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region SendPassword
        /// <summary>
        /// This is the interface method we are using for sending password through email.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SendPassword(string EmailId);
        #endregion


        #region SaveCrewLocation
        /// <summary>
        /// This is the interface method we are using for saving the current location of the Primary crew.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveCrewLocation(CrewLocation CrewLocation);
        #endregion


        #region GetTimeStampDetails
        /// <summary>
        /// This is the interface method we are using for getting the details of calls for TimeStamp page.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetTimeStampDetails(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region GetDelayStatus
        /// <summary>
        /// This is the interface method we are using for getting the Delay status for the unit.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDelayStatus(DelayStatus DelayStatus);
        #endregion


        #region SaveGeneralTimeStampsInfo
        /// <summary>
        /// This is the interface method we are using for saving the time stamp details of the user in General Format(Without Milege).
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveGeneralTimeStampsInfo(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region SaveTransportTimeStampsInfo
        /// <summary>
        /// This is the interface method we are using for saving the Transport time stamp details of the user.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveTransportTimeStampsInfo(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region SaveClearTimeStampsInfo
        /// <summary>
        /// This is the interface method we are using for saving the Clear time stamp details of the user.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveClearTimeStampsInfo(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region GetStatusWiseDynamicFields
        /// <summary>
        /// This is the interface method we are using for getting the dynamic fields for the status.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetStatusWiseDynamicFields(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region GetTransportDynamicFields
        /// <summary>
        /// This is the interface method we are using for getting the dynamic fields for the status(Transport).
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetTransportDynamicFields(TimeStampsInfo TimeStampsInfo);
        #endregion


        #region GetPatientAddressNoteDetails
        /// <summary>
        /// This is the interface method we are using for getting the Call Details for the Patient address nOte page.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetPatientAddressNoteDetails(PatientAddressNote PatientAddressNote);
        #endregion


        #region SavePatientAddressNote
        /// <summary>
        /// This is the interface method we are using for saving the Patient Address notes to dispatcher.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SavePatientAddressNote(PatientAddressNote PatientAddressNote);
        #endregion


        #region GetFacilities
        /// <summary>
        /// This is the interface method we are using for getting the facilities.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetFacilities(int Id, string Facility);
        #endregion


        #region GetDeviceDetails
        /// <summary>
        /// This is the interface method we are using for getting the Device details.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDeviceDetails(string DeviceId, string DeviceType);
        #endregion


        #region SendNotificationToAndroid
        /// <summary>
        /// This is the interface method we are using for sending the Push Notification to Android.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SendNotificationToAndroid(string deviceId, string message);
        #endregion


        #region SendNotificationToIos
        /// <summary>
        /// This is the interface method we are using for sending the Push Notification to Iphone.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SendNotificationToIos(string deviceId, string message);
        #endregion


        #region SaveDeviceToken
        /// <summary>
        /// This is the interface method we are using for saving the device Token needed for PushNotification.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveDeviceToken(Device Device);
        #endregion


        #region GetSuperVisorUnits
        /// <summary>
        /// This is the interface method we are using for getting the SuperVisor Units.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetSuperVisorUnits(int Id);
        #endregion


        #region GetSuperVisorCallList
        /// <summary>
        /// This is the interface method we are using for getting the SuperVisor Call List.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetSuperVisorCallList(SuperVisorUnitsDetails SuperVisorUnitsDetails);
        #endregion


        #region GetPrimaryCrewLocation
        /// <summary>
        /// This is the interface method we are using for getting the current location of the crew(SuperVisor option).
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetPrimaryCrewLocation(int Id, string UnitIds, int Offset, int CallId);
        #endregion


        #region GetSupVCallsCount
        /// <summary>
        /// This is the interface method we are using for getting the Calls count for the SUpervisors.
        /// </summary>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetSupVCallsCount(SupVCalls SupVCalls);
        #endregion

        // TODO: Add your service operations here

        // TODO: Add your service operations here
    }
    #endregion


    #region DATA CONTRACT
    // Use a data contract as illustrated in the sample below to add composite types to service operations.

    #region UserDetails
    [DataContract]
    public class UserDetails
    {
        string userid = string.Empty;
        string username = string.Empty;
        string location = string.Empty;
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Role { get; set; }
    }
    #endregion

    #region ezLogin
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
    #endregion

    #region CrewDetails
    [DataContract]
    public class CrewDetails
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string DeviceId { get; set; }
        [DataMember]
        public string DeviceType { get; set; }
        [DataMember]
        public int Offset { get; set; }

    }
    #endregion

    #region DispatcherCalls
    [DataContract]
    public class DispatcherCalls
    {
        [DataMember]
        public string PDID { get; set; }
        [DataMember]
        public string CallId { get; set; }
    }
    #endregion

    #region DispatcherCallsList
    [DataContract]
    public class DispatcherCallsList
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public int Offset { get; set; }

    }
    #endregion

    #region TimeStampsInfo
    [DataContract]
    public class TimeStampsInfo
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CallId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string CallNo { get; set; }
        [DataMember]
        public string Milege { get; set; }
        [DataMember]
        public string Desc { get; set; }
        [DataMember]
        public int offset { get; set; }
        [DataMember]
        public string IsDelay { get; set; }
        [DataMember]
        public string DelayReason { get; set; }
        [DataMember]
        public string JsonData { get; set; }
        [DataMember]
        public string PCR { get; set; }
        [DataMember]
        public string LOT { get; set; }
        [DataMember]
        public string OutCome { get; set; }

    }
    #endregion

    #region CrewLocation
    [DataContract]
    public class CrewLocation
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Lat { get; set; }
        [DataMember]
        public string Lnt { get; set; }
        [DataMember]
        public string Addr { get; set; }
        [DataMember]
        public int Offset { get; set; }
    }
    #endregion

    #region DelayStatus
    [DataContract]
    public class DelayStatus
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CallId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int Offset { get; set; }
    }
    #endregion

    #region PatientAddressNote
    [DataContract]
    public class PatientAddressNote
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CallId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string jsonData { get; set; }
        [DataMember]
        public string InsuranceInfo { get; set; }
    }
    #endregion

    #region Facilities
    [DataContract]
    public class Facilities
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Facility { get; set; }
    }
    #endregion

    #region Device
    [DataContract]
    public class Device
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string DeviceId { get; set; }
        [DataMember]
        public string DeviceToken { get; set; }
        [DataMember]
        public string DevicePlatform { get; set; }
        [DataMember]
        public string jsonData { get; set; }
        [DataMember]
        public string DeviceType { get; set; }
    }
    #endregion

    #region SuperVisorUnitsDetails
    [DataContract]
    public class SuperVisorUnitsDetails
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string UnitIds { get; set; }
        [DataMember]
        public int Offset { get; set; }
        [DataMember]
        public int CallId { get; set; }
    }
    #endregion

    #region SupVCalls
    [DataContract]
    public class SupVCalls
    {
        [DataMember]
        public int PId { get; set; }
        [DataMember]
        public string PUnits { get; set; }
    }
    #endregion

    #endregion
}
