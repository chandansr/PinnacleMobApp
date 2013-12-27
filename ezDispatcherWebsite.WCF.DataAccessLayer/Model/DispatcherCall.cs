using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ezDispatcherWebsite.WCF.DataAccessLayer
{
    public class DispatcherCall
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContractNumber { get; set; }
        public string Address { get; set; }
        public string CallIntakeID { get; set; }
        public string PatientName { get; set; }
        public string CaseManagerName { get; set; }
        public string Service { get; set; }
        public string ToAddress { get; set; }
        public string PickupDate { get; set; }
        public string PickupTime { get; set; }
        public string CallStatus { get; set; }
        public string CallType { get; set; }
        public string CallId { get; set; }
        public string AsignUnit { get; set; }
        public string CreatedDate { get; set; }
        public string ReturnTime { get; set; }
        public string ProviderName { get; set; }
        public string FullName { get; set; }
        public string Requested { get; set; }
        public string Scheduled { get; set; }
        public string DOB { get; set; }
        public string FromAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrentCallId { get; set; }
        public string IsComplete { get; set; }
        public string UnitStatus { get; set; }
    }

    public class DispatcherCallDetails
    {
        public string CallId { get; set; }
        public string PickupTime { get; set; }
        public string Unit { get; set; }
        public string Trip { get; set; }
        public string PickUpDetails { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string ContactName { get; set; }
        public string Contact { get; set; }
        public string CurrentStatus { get; set; }
        public string CurrentCallID { get; set; }
        public string CurrentStatusString { get; set; }
        public string LevelOfService { get; set; }
        public string LevelOfResponse { get; set; }
        public string LevelOfTransport { get; set; }
        public string ISNOE { get; set; }
        public string NatureOfEmergency { get; set; }
        public string Alerts { get; set; }
        public string IsPatientInfoProvidedByCrew { get; set; }
        public TimeStamps TimeStamp { get; set; }
    }

    public class CallDetails
    {
        public string CallId { get; set; }
        public string Unit { get; set; }
        public string Trip { get; set; }
        public string PLastName { get; set; }
        public string PFirstName { get; set; }
        public string PDOB { get; set; }
        public string PPhone { get; set; }
        public string PAlerts { get; set; }
        public string PStreet { get; set; }
        public string PAppartment { get; set; }
        public string PCity { get; set; }
        public string PState { get; set; }
        public string PZip { get; set; }
        public string TransportationType { get; set; }
        public string RoundTrip { get; set; }
        public string PickupDate { get; set; }
        public string PickupTime { get; set; }
        public string PickFromFacility { get; set; }
        public string PickFromPhone { get; set; }
        public string PickFromAddress { get; set; }
        public string PickFromCity { get; set; }
        public string PickFromState { get; set; }
        public string PickFromZip { get; set; }
        public string PickFromFloor { get; set; }
        public string PickFromRoom { get; set; }
        public string PickFromStairs { get; set; }
        public string PickFromObstacle { get; set; }
        public string DropToFacility { get; set; }
        public string DropToPhone { get; set; }
        public string DropToAddress { get; set; }
        public string DropToCity { get; set; }
        public string DropToState { get; set; }
        public string DropToZip { get; set; }
        public string DropToFloor { get; set; }
        public string DropToRoom { get; set; }
        public string DropToStairs { get; set; }
        public string DropToObstacle { get; set; }
        public string ReasonForTx { get; set; }
        public string AttentdentInfo { get; set; }
        public string SpecialNeeds { get; set; }
        public string IsO2Requierd { get; set; }
        public string IsOver200IBS { get; set; }
        public string TsAssigned { get; set; }
        public string TsDispatched { get; set; }
        public string TsEnroute { get; set; }
        public string TsOnScene { get; set; }
        public string TsContact { get; set; }
        public string TsTransport { get; set; }
        public string TsArrived { get; set; }
        public string TsClear { get; set; }
        public string Caller { get; set; }
        public string CallerFac { get; set; }
        public string CallerPhone { get; set; }
        public string CallerMob { get; set; }
        public string LevelOfService { get; set; }
        public string LevelOfResponse { get; set; }
        public string LevelOfTransport { get; set; }
        public string OutCome { get; set; }
        public string MedicalNParam { get; set; }
    }

    public class TimeStamps
    {
        public string Assigned { get; set; }
        public string Dispatched { get; set; }
        public string Enroute { get; set; }
        public string OnScene { get; set; }
        public string Contact { get; set; }
        public string Transport { get; set; }
        public string Arrived { get; set; }
        public string Clear { get; set; }
    }

    public class CallDetailsForTimeStamp
    {
        public string CallId { get; set; }
        public string Unit { get; set; }
        public string Trip { get; set; }
        public string CurrentLOT { get; set; }
        public string CurrentOutCome { get; set; }
        public string CurrentCallID { get; set; }
        public string CurrentLOTID { get; set; }
        public string CurrentOutComeID { get; set; }
        public List<ValueForDDL> LOTString { get; set; }
        public List<ValueForDDL> OutComeString { get; set; }
    }

    public class ValueForDDL
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class UnitDelayStatus
    {
        public string Status { get; set; }
        public string IsDelay { get; set; }
        public string MasterFieldID { get; set; }
        public string MasterTypeID { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public string FieldType { get; set; }
        public string IsRequired { get; set; }
        public string IsHidden { get; set; }
        public string IsAllow { get; set; }
        public UnitDelayStatus1 ActUnitDelayStatus { get; set; }
    }

    public class DynamicFields
    {
        public string Status { get; set; }
        public string IsDelay { get; set; }
        public string MasterFieldID { get; set; }
        public string MasterTypeID { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public string FieldType { get; set; }
        public string IsRequired { get; set; }
        public string IsHidden { get; set; }
        public string IsAllow { get; set; }
        public string Unit { get; set; }
        public string Trip { get; set; }
    }

    public class TransportDynamicFields
    {
        public string Status { get; set; }
        public string IsDelay { get; set; }
        public string Unit { get; set; }
        public string Trip { get; set; }
        public string MasterFieldID { get; set; }
        public string MasterTypeID { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public string FieldType { get; set; }
        public string IsRequired { get; set; }
        public string IsHidden { get; set; }
        public string IsAllow { get; set; }
        public string CurrentLOT { get; set; }
        public string CurrentOutCome { get; set; }
        public string CurrentCallID { get; set; }
        public string CurrentLOTID { get; set; }
        public string CurrentOutComeID { get; set; }
        public List<ValueForDDL> LOTString { get; set; }
        public List<ValueForDDL> OutComeString { get; set; }
    }

    public class UnitDelayStatus1
    {
        public string IsDelay { get; set; }
        public string Status { get; set; }
    }

    public class DeviceDetails
    {
        public string ParamedicDetailsID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string DevicePlatform { get; set; }
    }

    public class PatientAddressNoteDetails
    {
        public string ParamedicDetailsID { get; set; }
        public string CallId { get; set; }
        public string Unit { get; set; }
        public string Trip { get; set; }
        public List<ValueForDDL> StateString { get; set; }
    }

    public class SuperVisorUnits
    {
        public string SuperVisorId { get; set; }
        public string UnitId { get; set; }
        public string UnitAssignedID { get; set; }
        public string Unit { get; set; }
        public string SuperVisorName { get; set; }
    }

    public class PrimaryCrewLocation
    {
        public string Unit { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CallId { get; set; }
    }

    public class SupervisorCalls
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContractNumber { get; set; }
        public string Address { get; set; }
        public string CallIntakeID { get; set; }
        public string PatientName { get; set; }
        public string CaseManagerName { get; set; }
        public string Service { get; set; }
        public string ToAddress { get; set; }
        public string PickupDate { get; set; }
        public string PickupTime { get; set; }
        public string CallStatus { get; set; }
        public string CallType { get; set; }
        public string CallId { get; set; }
        public string AsignUnit { get; set; }
        public string CreatedDate { get; set; }
        public string ReturnTime { get; set; }
        public string ProviderName { get; set; }
        public string FullName { get; set; }
        public string Requested { get; set; }
        public string Scheduled { get; set; }
        public string DOB { get; set; }
        public string FromAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrentCallId { get; set; }
        public string IsComplete { get; set; }
        public string UnitStatus { get; set; }
        public string Scene { get; set; }
        public string Destination { get; set; }
        public string LevelResponse { get; set; }
        public string ReasonForTrans { get; set; }
    }
}
