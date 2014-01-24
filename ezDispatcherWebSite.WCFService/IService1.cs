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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        UserDetails[] GetUserDetails(string Username);



        /// <summary>
        /// This is the interface method we are using for authentication purpose.
        /// </summary>
        /// <param name="username">User name for Sign in </param>
        /// <param name="password">Password for Sign in</param>
        /// <returns>This method only returns message that user is valid or not</returns>



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<ezLogin> ValidateCrew(CrewDetails CrewDetails);


        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDispatcherCalls(DispatcherCalls DispatcherCalls);

        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDispatcherCallList(DispatcherCallsList DispatcherCallsList);


        /// <summary>
        /// This is the interface method we are using for getting all the details related to call.
        /// </summary>
        /// <returns>This method only returns list of details of Dispatcher Calls</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetCallDetails(DispatcherCalls DispatcherCalls);


        /// <summary>
        /// This is the interface method we are using for getting the current call details of the crew.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetCurrentCallDetails(DispatcherCallsList DispatcherCallsList);


        /// <summary>
        /// This is the interface method we are using for saving the time stamp details of the user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveTimeStampsInfo(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for sending password through email.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SendPassword(string EmailId);


        /// <summary>
        /// This is the interface method we are using for saving the current location of the Primary crew.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveCrewLocation(CrewLocation CrewLocation);


        /// <summary>
        /// This is the interface method we are using for getting the details of calls for TimeStamp page.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetTimeStampDetails(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for getting the Delay status for the unit.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDelayStatus(DelayStatus DelayStatus);


        /// <summary>
        /// This is the interface method we are using for saving the time stamp details of the user in General Format(Without Milege).
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveGeneralTimeStampsInfo(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for saving the Transport time stamp details of the user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveTransportTimeStampsInfo(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for saving the Clear time stamp details of the user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveClearTimeStampsInfo(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for getting the dynamic fields for the status.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetStatusWiseDynamicFields(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for getting the dynamic fields for the status(Transport).
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetTransportDynamicFields(TimeStampsInfo TimeStampsInfo);


        /// <summary>
        /// This is the interface method we are using for getting the Call Details for the Patient address nOte page.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetPatientAddressNoteDetails(PatientAddressNote PatientAddressNote);



        /// <summary>
        /// This is the interface method we are using for saving the Patient Address notes to dispatcher.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SavePatientAddressNote(PatientAddressNote PatientAddressNote);


        /// <summary>
        /// This is the interface method we are using for getting the facilities.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetFacilities(int Id, string Facility);


        /// <summary>
        /// This is the interface method we are using for getting the Device details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetDeviceDetails(Device Device);


        /// <summary>
        /// This is the interface method we are using for sending the Push Notification to Android.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SendNotificationToAndroid(string deviceId, string message);


        /// <summary>
        /// This is the interface method we are using for sending the Push Notification to Iphone.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SendNotificationToIos(string deviceId, string message);



        /// <summary>
        /// This is the interface method we are using for saving the device Token needed for PushNotification.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream SaveDeviceToken(Device Device);


        /// <summary>
        /// This is the interface method we are using for getting the SuperVisor Units.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetSuperVisorUnits(int Id);


        /// <summary>
        /// This is the interface method we are using for getting the SuperVisor Call List.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetSuperVisorCallList(SuperVisorUnitsDetails SuperVisorUnitsDetails);


        /// <summary>
        /// This is the interface method we are using for getting the current location of the crew(SuperVisor option).
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetPrimaryCrewLocation(int Id, string UnitIds, int Offset, int CallId);


        /// <summary>
        /// This is the interface method we are using for getting the Calls count for the SUpervisors.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Stream GetSupVCallsCount(SupVCalls SupVCalls);
        // TODO: Add your service operations here

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
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
    [DataContract]
    public class DispatcherCalls
    {
        [DataMember]
        public string PDID { get; set; }
        [DataMember]
        public string CallId { get; set; }
    }
    [DataContract]
    public class DispatcherCallsList
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public int Offset { get; set; }

    }
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
    [DataContract]
    public class Facilities
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Facility { get; set; }
    }
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
    [DataContract]
    public class SupVCalls
    {
        [DataMember]
        public int PId { get; set; }
        [DataMember]
        public string PUnits { get; set; }
    }

}
