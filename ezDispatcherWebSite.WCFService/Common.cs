using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ezDispatcherWebsite.WCF.DataAccessLayer;
using System.Data;
using System.Web.Script.Serialization;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ezDispatcherWebSite.WCFService
{
    public class Common
    {
        private const string c_EncryptionKey = "&#&#!&#!&#%%%";
        private static byte[] byKey = { };
        private static readonly byte[] IV =
            {
                55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23,
                200, 58, 173, 10, 121, 222, 88, 09, 45, 67, 94, 12, 34, 5
            };

        public static string GetCredXml()
        {
            string msg;
            try
            {
                var currentUser = (LoggedUserInformation)HttpContext.Current.Session["CurrentUser"];
                currentUser.Url = HttpContext.Current.Request.Url.AbsolutePath;
                //currentUser.CompanyTypeID = GetCompanyCode();
                if (currentUser == null)
                {
                    currentUser.CompanyTypeID = -1;
                }

                msg =
                    SerializationHelper.ConvertObjectToXml(currentUser, null, "CREDENTIAL").
                        InnerXml;
            }
            catch (Exception)
            {

                msg = "";
            }
            return msg;
        }
                
        public static List<DispatcherCall> GetJson(DataTable dt)
        {
            List<DispatcherCall> lstcurCall = new List<DispatcherCall>();

            foreach (DataRow dr in dt.Rows)
            {
                DispatcherCall obj = new DispatcherCall();
                
                obj.CallStatus = dr.Table.Columns.Contains("CALLSTATUS") ? dr["CALLSTATUS"].ToString() : "";
                obj.LastName =dr.Table.Columns.Contains("LastName")? dr["LastName"].ToString(): "";
                obj.FirstName =dr.Table.Columns.Contains("FirstName")? dr["FirstName"].ToString(): "";
                obj.ContractNumber = dr.Table.Columns.Contains("ContactNumber") ? dr["ContactNumber"].ToString() : "";
                obj.Email = dr.Table.Columns.Contains("Email") ? dr["Email"].ToString() : "";
                obj.Address = dr.Table.Columns.Contains("Address") ? dr["Address"].ToString() : "";
                obj.CallIntakeID = dr.Table.Columns.Contains("CALLINTAKEID") ? dr["CALLINTAKEID"].ToString() : "";
                obj.PickupTime = dr.Table.Columns.Contains("PICKUPTIME") ? dr["PICKUPTIME"].ToString() : "";
                obj.CallType = dr.Table.Columns.Contains("CALLTYPE") ? dr["CALLTYPE"].ToString() : "";
                obj.CallId = dr.Table.Columns.Contains("CALLID") ? dr["CALLID"].ToString() : "";
                obj.ProviderName = dr.Table.Columns.Contains("PROVIDERNAME") ? dr["PROVIDERNAME"].ToString() : "";
                obj.AsignUnit = dr.Table.Columns.Contains("ASSIGNEDUNIT") ? dr["ASSIGNEDUNIT"].ToString() : "";
                obj.CreatedDate = dr.Table.Columns.Contains("CREATEDDATE") ? dr["CREATEDDATE"].ToString() : "";
                obj.PickupDate = dr.Table.Columns.Contains("PICKUPDATE") ? dr["PICKUPDATE"].ToString() : "";
                obj.ReturnTime = dr.Table.Columns.Contains("RETURN_TIME") ? dr["RETURN_TIME"].ToString() : "";
                obj.FullName = dr.Table.Columns.Contains("FULL_NAME") ? dr["FULL_NAME"].ToString() : "";
                obj.FromAddress = dr.Table.Columns.Contains("FROMADDRESS") ? dr["FROMADDRESS"].ToString() : "";
                obj.ToAddress = dr.Table.Columns.Contains("TOADDRESS") ? dr["TOADDRESS"].ToString() : "";
                obj.CurrentCallId = dr.Table.Columns.Contains("CurrentCallId") ? dr["CurrentCallId"].ToString() : "";
                obj.UnitStatus = dr.Table.Columns.Contains("UnitStatus") ? dr["UnitStatus"].ToString() : "";
                obj.IsComplete = dr.Table.Columns.Contains("IsComplete") ? dr["IsComplete"].ToString() : "";
                
                lstcurCall.Add(obj);
            }
            return lstcurCall;
        }

        public static List<DispatcherCallDetails> GetCallDetailsJson(DataTable dt)
        {
            List<DispatcherCallDetails> lstDispCall = new List<DispatcherCallDetails>();

            foreach (DataRow dr in dt.Rows)
            {
                DispatcherCallDetails obj = new DispatcherCallDetails();
                TimeStamps objTimeStamp = new TimeStamps();

                obj.CallId = dr.Table.Columns.Contains("CallId") ? dr["CallId"].ToString() : "";
                obj.PickupTime = dr.Table.Columns.Contains("PickupTime") ? dr["PickupTime"].ToString() : "";
                obj.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                obj.Trip = dr.Table.Columns.Contains("Trip") ? dr["Trip"].ToString() : "";
                obj.PickUpDetails = dr.Table.Columns.Contains("PickUpDetails") ? dr["PickUpDetails"].ToString() : "";
                obj.FromAddress = dr.Table.Columns.Contains("FromAddress") ? dr["FromAddress"].ToString() : "";
                obj.ToAddress = dr.Table.Columns.Contains("ToAddress") ? dr["ToAddress"].ToString() : "";
                obj.ContactName = dr.Table.Columns.Contains("ContactName") ? dr["ContactName"].ToString() : "";
                obj.Contact = dr.Table.Columns.Contains("Contact") ? dr["Contact"].ToString() : "";
                obj.CurrentStatus = dr.Table.Columns.Contains("CurrentStatus") ? dr["CurrentStatus"].ToString() : "";
                obj.CurrentCallID = dr.Table.Columns.Contains("CurrentCallID") ? dr["CurrentCallID"].ToString() : "";
                obj.CurrentStatusString = dr.Table.Columns.Contains("CurrentStatusString") ? dr["CurrentStatusString"].ToString() : "";
                obj.LevelOfService = dr.Table.Columns.Contains("LevelOfService") ? dr["LevelOfService"].ToString() : "";
                obj.LevelOfResponse = dr.Table.Columns.Contains("LevelOfResponse") ? dr["LevelOfResponse"].ToString() : "";
                obj.LevelOfTransport = dr.Table.Columns.Contains("LevelOfTransport") ? dr["LevelOfTransport"].ToString() : "";
                obj.ISNOE = dr.Table.Columns.Contains("ISNOE") ? dr["ISNOE"].ToString() : "";
                obj.NatureOfEmergency = dr.Table.Columns.Contains("NatureOfEmergency") ? dr["NatureOfEmergency"].ToString() : "";
                obj.Alerts = dr.Table.Columns.Contains("Alerts") ? dr["Alerts"].ToString() : "";
                obj.IsPatientInfoProvidedByCrew = dr.Table.Columns.Contains("IsPatientInfoProvidedByCrew") ? dr["IsPatientInfoProvidedByCrew"].ToString() : "";

                objTimeStamp.Assigned = dr.Table.Columns.Contains("TsAssigned") ? dr["TsAssigned"].ToString() : "";
                objTimeStamp.Dispatched = dr.Table.Columns.Contains("TsDispatched") ? dr["TsDispatched"].ToString() : "";
                objTimeStamp.Enroute = dr.Table.Columns.Contains("TsEnroute") ? dr["TsEnroute"].ToString() : "";
                objTimeStamp.OnScene = dr.Table.Columns.Contains("TsOnScene") ? dr["TsOnScene"].ToString() : "";
                objTimeStamp.Contact = dr.Table.Columns.Contains("TsContact") ? dr["TsContact"].ToString() : "";
                objTimeStamp.Transport = dr.Table.Columns.Contains("TsTransport") ? dr["TsTransport"].ToString() : "";
                objTimeStamp.Arrived = dr.Table.Columns.Contains("TsArrived") ? dr["TsArrived"].ToString() : "";
                objTimeStamp.Clear = dr.Table.Columns.Contains("TsClear") ? dr["TsClear"].ToString() : "";

                obj.TimeStamp = objTimeStamp;

                lstDispCall.Add(obj);
            }
            return lstDispCall;
        }


        public static List<CallDetails> GetCallFullInfoJson(DataTable dt)
        {
            List<CallDetails> lstCall = new List<CallDetails>();

            foreach (DataRow dr in dt.Rows)
            {
                CallDetails obj = new CallDetails();
                
                obj.CallId = dr.Table.Columns.Contains("CallId") ? dr["CallId"].ToString() : "";
                obj.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                obj.Trip = dr.Table.Columns.Contains("Trip") ? dr["Trip"].ToString() : "";
                obj.PLastName = dr.Table.Columns.Contains("PLastName") ? dr["PLastName"].ToString() : "";
                obj.PFirstName = dr.Table.Columns.Contains("PFirstName") ? dr["PFirstName"].ToString() : "";
                obj.PDOB = dr.Table.Columns.Contains("PDOB") ? dr["PDOB"].ToString() : "";                
                obj.PPhone = dr.Table.Columns.Contains("PPhone") ? dr["PPhone"].ToString() : "";
                obj.PAlerts = dr.Table.Columns.Contains("PAlerts") ? dr["PAlerts"].ToString() : "";
                obj.PStreet = dr.Table.Columns.Contains("PStreet") ? dr["PStreet"].ToString() : "";
                obj.PAppartment = dr.Table.Columns.Contains("PAppartment") ? dr["PAppartment"].ToString() : "";
                obj.PCity = dr.Table.Columns.Contains("PCity") ? dr["PCity"].ToString() : "";
                obj.PState = dr.Table.Columns.Contains("PState") ? dr["PState"].ToString() : "";
                obj.PZip = dr.Table.Columns.Contains("PZip") ? dr["PZip"].ToString() : "";
                obj.TransportationType = dr.Table.Columns.Contains("TransportationType") ? dr["TransportationType"].ToString() : "";
                obj.RoundTrip = dr.Table.Columns.Contains("RoundTrip") ? dr["RoundTrip"].ToString() : "";
                obj.PickupDate = dr.Table.Columns.Contains("PickupDate") ? dr["PickupDate"].ToString() : "";
                obj.PickupTime = dr.Table.Columns.Contains("PickupTime") ? dr["PickupTime"].ToString() : "";
                obj.PickFromFacility = dr.Table.Columns.Contains("PickFromFacility") ? dr["PickFromFacility"].ToString() : "";
                obj.PickFromPhone = dr.Table.Columns.Contains("PickFromPhone") ? dr["PickFromPhone"].ToString() : "";
                obj.PickFromAddress = dr.Table.Columns.Contains("PickFromAddress") ? dr["PickFromAddress"].ToString() : "";
                obj.PickFromCity = dr.Table.Columns.Contains("PickFromCity") ? dr["PickFromCity"].ToString() : "";
                obj.PickFromState = dr.Table.Columns.Contains("PickFromState") ? dr["PickFromState"].ToString() : "";
                obj.PickFromZip = dr.Table.Columns.Contains("PickFromZip") ? dr["PickFromZip"].ToString() : "";
                obj.PickFromFloor = dr.Table.Columns.Contains("PickFromFloor") ? dr["PickFromFloor"].ToString() : "";
                obj.PickFromRoom = dr.Table.Columns.Contains("PickFromRoom") ? dr["PickFromRoom"].ToString() : "";
                obj.PickFromStairs = dr.Table.Columns.Contains("PickFromStairs") ? dr["PickFromStairs"].ToString() : "";
                obj.PickFromObstacle = dr.Table.Columns.Contains("PickFromObstacle") ? dr["PickFromObstacle"].ToString() : "";
                obj.DropToFacility = dr.Table.Columns.Contains("DropToFacility") ? dr["DropToFacility"].ToString() : "";
                obj.DropToPhone = dr.Table.Columns.Contains("DropToPhone") ? dr["DropToPhone"].ToString() : "";
                obj.DropToAddress = dr.Table.Columns.Contains("DropToAddress") ? dr["DropToAddress"].ToString() : "";
                obj.DropToCity = dr.Table.Columns.Contains("DropToCity") ? dr["DropToCity"].ToString() : "";
                obj.DropToState = dr.Table.Columns.Contains("DropToState") ? dr["DropToState"].ToString() : "";
                obj.DropToZip = dr.Table.Columns.Contains("DropToZip") ? dr["DropToZip"].ToString() : "";
                obj.DropToFloor = dr.Table.Columns.Contains("DropToFloor") ? dr["DropToFloor"].ToString() : "";
                obj.DropToRoom = dr.Table.Columns.Contains("DropToRoom") ? dr["DropToRoom"].ToString() : "";
                obj.DropToStairs = dr.Table.Columns.Contains("DropToStairs") ? dr["DropToStairs"].ToString() : "";
                obj.DropToObstacle = dr.Table.Columns.Contains("DropToObstacle") ? dr["DropToObstacle"].ToString() : "";
                obj.ReasonForTx = dr.Table.Columns.Contains("ReasonForTx") ? dr["ReasonForTx"].ToString() : "";
                obj.AttentdentInfo = dr.Table.Columns.Contains("AttentdentInfo") ? dr["AttentdentInfo"].ToString() : "";
                obj.SpecialNeeds = dr.Table.Columns.Contains("SpecialNeeds") ? dr["SpecialNeeds"].ToString() : "";
                obj.IsO2Requierd = dr.Table.Columns.Contains("IsO2Requierd") ? dr["IsO2Requierd"].ToString() : "";
                obj.IsOver200IBS = dr.Table.Columns.Contains("IsOver200IBS") ? dr["IsOver200IBS"].ToString() : "";
                obj.TsAssigned = dr.Table.Columns.Contains("TsAssigned") ? dr["TsAssigned"].ToString() : "";
                obj.TsDispatched = dr.Table.Columns.Contains("TsDispatched") ? dr["TsDispatched"].ToString() : "";
                obj.TsEnroute = dr.Table.Columns.Contains("TsEnroute") ? dr["TsEnroute"].ToString() : "";
                obj.TsOnScene = dr.Table.Columns.Contains("TsOnScene") ? dr["TsOnScene"].ToString() : "";
                obj.TsContact = dr.Table.Columns.Contains("TsContact") ? dr["TsContact"].ToString() : "";
                obj.TsTransport = dr.Table.Columns.Contains("TsTransport") ? dr["TsTransport"].ToString() : "";
                obj.TsArrived = dr.Table.Columns.Contains("TsArrived") ? dr["TsArrived"].ToString() : "";
                obj.TsClear = dr.Table.Columns.Contains("TsClear") ? dr["TsClear"].ToString() : "";
                obj.Caller = dr.Table.Columns.Contains("Caller") ? dr["Caller"].ToString() : "";
                obj.CallerFac = dr.Table.Columns.Contains("CallerFac") ? dr["CallerFac"].ToString() : "";
                obj.CallerPhone = dr.Table.Columns.Contains("CallerPhone") ? dr["CallerPhone"].ToString() : "";
                obj.CallerMob = dr.Table.Columns.Contains("CallerMob") ? dr["CallerMob"].ToString() : "";
                obj.LevelOfService = dr.Table.Columns.Contains("LevelOfService") ? dr["LevelOfService"].ToString() : "";
                obj.LevelOfResponse = dr.Table.Columns.Contains("LevelOfResponse") ? dr["LevelOfResponse"].ToString() : "";
                obj.LevelOfTransport = dr.Table.Columns.Contains("LevelOfTransport") ? dr["LevelOfTransport"].ToString() : "";
                obj.OutCome = dr.Table.Columns.Contains("OutCome") ? dr["OutCome"].ToString() : "";
                obj.MedicalNParam = dr.Table.Columns.Contains("MedicalNParam") ? dr["MedicalNParam"].ToString() : "";

                lstCall.Add(obj);
            }
            return lstCall;
        }


        public static List<CallDetailsForTimeStamp> GetJsonforTimeStamp(DataSet ds)
        {
            List<CallDetailsForTimeStamp> lstcalldetailsTS = new List<CallDetailsForTimeStamp>();
            List<ValueForDDL> lstLOT = new List<ValueForDDL>();
            List<ValueForDDL> lstOutCome = new List<ValueForDDL>();

            if (ds.Tables.Contains("Table1"))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    ValueForDDL LOT = new ValueForDDL();
                    LOT.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                    LOT.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                    lstLOT.Add(LOT);
                }
            }

            if (ds.Tables.Contains("Table2"))
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    ValueForDDL OutCome = new ValueForDDL();
                    OutCome.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                    OutCome.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                    lstOutCome.Add(OutCome);
                }
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CallDetailsForTimeStamp calld = new CallDetailsForTimeStamp();
                calld.CallId = dr.Table.Columns.Contains("CallId") ? dr["CallId"].ToString() : "";
                calld.CurrentCallID = dr.Table.Columns.Contains("CurrentCallID") ? dr["CurrentCallID"].ToString() : "";
                calld.CurrentLOT = dr.Table.Columns.Contains("CurrentLOT") ? dr["CurrentLOT"].ToString() : "";
                calld.CurrentOutCome = dr.Table.Columns.Contains("CurrentOutCome") ? dr["CurrentOutCome"].ToString() : "";
                calld.CurrentLOTID = dr.Table.Columns.Contains("CurrentLOTID") ? dr["CurrentLOTID"].ToString() : "";
                calld.CurrentOutComeID = dr.Table.Columns.Contains("CurrentOutComeID") ? dr["CurrentOutComeID"].ToString() : "";
                calld.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                calld.Trip = dr.Table.Columns.Contains("Trip") ? dr["Trip"].ToString() : "";
                calld.LOTString = lstLOT;
                calld.OutComeString = lstOutCome;
                lstcalldetailsTS.Add(calld);
            }
            return lstcalldetailsTS; 
        }


        public static List<UnitDelayStatus> GetJsonforUnitDelayStatus(DataSet ds)
        {
            List<UnitDelayStatus> lstUnitDelayStatus = new List<UnitDelayStatus>();
            UnitDelayStatus1 ActUnitStatus = new UnitDelayStatus1();
            
            if (ds.Tables.Contains("Table1"))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {                    
                    ActUnitStatus.IsDelay = dr.Table.Columns.Contains("IsDelay") ? dr["IsDelay"].ToString() : "";
                    ActUnitStatus.Status = dr.Table.Columns.Contains("Status") ? dr["Status"].ToString() : "";                    
                }                
            }

            if (ds.Tables.Contains("Table"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UnitDelayStatus UnitDelayStatus = new UnitDelayStatus();
                        UnitDelayStatus.IsDelay = dr.Table.Columns.Contains("IsDelay") ? dr["IsDelay"].ToString() : "";
                        UnitDelayStatus.Status = dr.Table.Columns.Contains("Status") ? dr["Status"].ToString() : "";
                        UnitDelayStatus.MasterFieldID = dr.Table.Columns.Contains("MasterFieldID") ? dr["MasterFieldID"].ToString() : "";
                        UnitDelayStatus.MasterTypeID = dr.Table.Columns.Contains("MasterTypeID") ? dr["MasterTypeID"].ToString() : "";
                        UnitDelayStatus.FieldName = dr.Table.Columns.Contains("FieldName") ? dr["FieldName"].ToString() : "";
                        UnitDelayStatus.FieldDescription = dr.Table.Columns.Contains("FieldDescription") ? dr["FieldDescription"].ToString() : "";
                        UnitDelayStatus.FieldType = dr.Table.Columns.Contains("FieldType") ? dr["FieldType"].ToString() : "";
                        UnitDelayStatus.IsRequired = dr.Table.Columns.Contains("IsRequired") ? dr["IsRequired"].ToString() : "";
                        UnitDelayStatus.IsHidden = dr.Table.Columns.Contains("IsHidden") ? dr["IsHidden"].ToString() : "";
                        UnitDelayStatus.IsAllow = dr.Table.Columns.Contains("IsAllow") ? dr["IsAllow"].ToString() : "";
                        UnitDelayStatus.ActUnitDelayStatus = ActUnitStatus;
                        lstUnitDelayStatus.Add(UnitDelayStatus);
                    }
                }
                else
                {
                    UnitDelayStatus UnitDelayStatus = new UnitDelayStatus();
                    UnitDelayStatus.ActUnitDelayStatus = ActUnitStatus;
                    lstUnitDelayStatus.Add(UnitDelayStatus);
                }
            }
            else
            {
                UnitDelayStatus UnitDelayStatus = new UnitDelayStatus();
                UnitDelayStatus.ActUnitDelayStatus = ActUnitStatus;
                lstUnitDelayStatus.Add(UnitDelayStatus);
            }
            return lstUnitDelayStatus;
        }


        public static List<DynamicFields> GetDynamicFieldsJson(DataTable dt)
        {
            List<DynamicFields> lstDynamicFields = new List<DynamicFields>();

            foreach (DataRow dr in dt.Rows)
            {
                DynamicFields DynamicField = new DynamicFields();
                DynamicField.IsDelay = dr.Table.Columns.Contains("IsDelay") ? dr["IsDelay"].ToString() : "";
                DynamicField.Status = dr.Table.Columns.Contains("Status") ? dr["Status"].ToString() : "";
                DynamicField.MasterFieldID = dr.Table.Columns.Contains("MasterFieldID") ? dr["MasterFieldID"].ToString() : "";
                DynamicField.MasterTypeID = dr.Table.Columns.Contains("MasterTypeID") ? dr["MasterTypeID"].ToString() : "";
                DynamicField.FieldName = dr.Table.Columns.Contains("FieldName") ? dr["FieldName"].ToString() : "";
                DynamicField.FieldDescription = dr.Table.Columns.Contains("FieldDescription") ? dr["FieldDescription"].ToString() : "";
                DynamicField.FieldType = dr.Table.Columns.Contains("FieldType") ? dr["FieldType"].ToString() : "";
                DynamicField.IsRequired = dr.Table.Columns.Contains("IsRequired") ? dr["IsRequired"].ToString() : "";
                DynamicField.IsHidden = dr.Table.Columns.Contains("IsHidden") ? dr["IsHidden"].ToString() : "";
                DynamicField.IsAllow = dr.Table.Columns.Contains("IsAllow") ? dr["IsAllow"].ToString() : "";
                DynamicField.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                DynamicField.Trip = dr.Table.Columns.Contains("Trip") ? dr["Trip"].ToString() : "";
                lstDynamicFields.Add(DynamicField);
            }
            return lstDynamicFields;
        }


        public static List<TransportDynamicFields> GetTransportDynamicFieldsJson(DataSet ds)
        {
            List<TransportDynamicFields> lstDynamicFields = new List<TransportDynamicFields>();
            List<ValueForDDL> lstLOT = new List<ValueForDDL>();
            List<ValueForDDL> lstOutCome = new List<ValueForDDL>();

            if (ds.Tables.Contains("Table1"))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    ValueForDDL LOT = new ValueForDDL();
                    LOT.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                    LOT.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                    lstLOT.Add(LOT);
                }
            }

            if (ds.Tables.Contains("Table2"))
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    ValueForDDL OutCome = new ValueForDDL();
                    OutCome.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                    OutCome.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                    lstOutCome.Add(OutCome);
                }
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TransportDynamicFields DynamicField = new TransportDynamicFields();
                DynamicField.IsDelay = dr.Table.Columns.Contains("IsDelay") ? dr["IsDelay"].ToString() : "";
                DynamicField.Status = dr.Table.Columns.Contains("Status") ? dr["Status"].ToString() : "";
                DynamicField.MasterFieldID = dr.Table.Columns.Contains("MasterFieldID") ? dr["MasterFieldID"].ToString() : "";
                DynamicField.MasterTypeID = dr.Table.Columns.Contains("MasterTypeID") ? dr["MasterTypeID"].ToString() : "";
                DynamicField.FieldName = dr.Table.Columns.Contains("FieldName") ? dr["FieldName"].ToString() : "";
                DynamicField.FieldDescription = dr.Table.Columns.Contains("FieldDescription") ? dr["FieldDescription"].ToString() : "";
                DynamicField.FieldType = dr.Table.Columns.Contains("FieldType") ? dr["FieldType"].ToString() : "";
                DynamicField.IsRequired = dr.Table.Columns.Contains("IsRequired") ? dr["IsRequired"].ToString() : "";
                DynamicField.IsHidden = dr.Table.Columns.Contains("IsHidden") ? dr["IsHidden"].ToString() : "";
                DynamicField.IsAllow = dr.Table.Columns.Contains("IsAllow") ? dr["IsAllow"].ToString() : "";
                DynamicField.CurrentLOT = dr.Table.Columns.Contains("CurrentLOT") ? dr["CurrentLOT"].ToString() : "";
                DynamicField.CurrentOutCome = dr.Table.Columns.Contains("CurrentOutCome") ? dr["CurrentOutCome"].ToString() : "";
                DynamicField.CurrentCallID = dr.Table.Columns.Contains("CurrentCallID") ? dr["CurrentCallID"].ToString() : "";
                DynamicField.CurrentLOTID = dr.Table.Columns.Contains("CurrentLOTID") ? dr["CurrentLOTID"].ToString() : "";
                DynamicField.CurrentOutComeID = dr.Table.Columns.Contains("CurrentOutComeID") ? dr["CurrentOutComeID"].ToString() : "";
                DynamicField.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                DynamicField.Trip = dr.Table.Columns.Contains("Trip") ? dr["Trip"].ToString() : "";

                DynamicField.LOTString = lstLOT;
                DynamicField.OutComeString = lstOutCome;
                lstDynamicFields.Add(DynamicField);
            }
            return lstDynamicFields;
        }


        public static string EncryptText(string strTxt)
        {
            return Encrypt(strTxt, c_EncryptionKey);
        }


        public static string Encrypt(string strTxt, string encryptKey)
        {
            var _strEncryptedData = String.Empty;
            byte[] inputByteArray;
            MemoryStream _objectMemoryStream;
            CryptoStream _objectCryptoStream;
            RijndaelManaged _objectRajinderManaged;
            try
            {
                byKey = Encoding.UTF8.GetBytes(encryptKey);
                _objectRajinderManaged = new RijndaelManaged();
                _objectRajinderManaged.BlockSize = 256;
                _objectRajinderManaged.IV = IV;
                _objectRajinderManaged.KeySize = 256;
                _objectRajinderManaged.Key = new SHA256Managed().ComputeHash(byKey);
                inputByteArray = Encoding.UTF8.GetBytes(strTxt);
                _objectMemoryStream = new MemoryStream();
                _objectCryptoStream = new CryptoStream(_objectMemoryStream, _objectRajinderManaged.CreateEncryptor(),
                                                       CryptoStreamMode.Write);
                _objectCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                _objectCryptoStream.FlushFinalBlock();
                _strEncryptedData = Convert.ToBase64String(_objectMemoryStream.ToArray());
                _strEncryptedData = _strEncryptedData.Replace("/", "786");
                return _strEncryptedData;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static String DecryptText(string strTxt)
        {
            return Decrypt(strTxt, c_EncryptionKey);
        }

        public static String Decrypt(string strTxt, string decryptKey)
        {
            var _str = String.Empty;
            byte[] inputByteArray;
            MemoryStream _objectMemoryStream;
            CryptoStream _objectCryptoStream;
            RijndaelManaged _objectRajinderManaged;
            try
            {
                byKey = Encoding.UTF8.GetBytes(decryptKey);
                _objectRajinderManaged = new RijndaelManaged();
                _objectRajinderManaged.BlockSize = 256;
                _objectRajinderManaged.IV = IV;
                _objectRajinderManaged.KeySize = 256;
                _objectRajinderManaged.Key = new SHA256Managed().ComputeHash(byKey);
                _str = strTxt.Replace(" ", "+");
                _str = _str.Replace("786", "/");
                inputByteArray = Convert.FromBase64String(_str);

                _objectMemoryStream = new MemoryStream();
                _objectCryptoStream = new CryptoStream(_objectMemoryStream, _objectRajinderManaged.CreateDecryptor(),
                                                       CryptoStreamMode.Write);
                _objectCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                _objectCryptoStream.FlushFinalBlock();
                var encoding = Encoding.UTF8;
                return encoding.GetString(_objectMemoryStream.ToArray());
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static List<ValueForDDL> GetFacilities(DataTable dt)
        {
            List<ValueForDDL> Facilities = new List<ValueForDDL>();
            foreach (DataRow dr in dt.Rows)
            {
                ValueForDDL Facility = new ValueForDDL();
                Facility.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                Facility.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                Facilities.Add(Facility);
            }
            return Facilities;
        }


        public static List<DeviceDetails> GetDeviceDetails(DataTable dt)
        {
            List<DeviceDetails> DeviceDetails = new List<DeviceDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                DeviceDetails Device = new DeviceDetails();
                Device.DeviceId = dr.Table.Columns.Contains("DeviceId") ? dr["DeviceId"].ToString() : "";
                Device.DevicePlatform = dr.Table.Columns.Contains("DevicePlatform") ? dr["DevicePlatform"].ToString() : "";
                Device.ParamedicDetailsID = dr.Table.Columns.Contains("ParamedicDetailsID") ? dr["ParamedicDetailsID"].ToString() : "";
                Device.Email = dr.Table.Columns.Contains("Email") ? dr["Email"].ToString() : "";
                Device.Password = dr.Table.Columns.Contains("Password") ? dr["Password"].ToString() : "";
                DeviceDetails.Add(Device);
            }
            return DeviceDetails;
        }


        public static List<PatientAddressNoteDetails> GetPatientAddressNoteDetails(DataSet ds)
        {
            List<PatientAddressNoteDetails> lstPatientAddrNote = new List<PatientAddressNoteDetails>();
            List<ValueForDDL> lstStateString = new List<ValueForDDL>();

            if (ds.Tables.Contains("Table1"))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    ValueForDDL State = new ValueForDDL();
                    State.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                    State.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                    lstStateString.Add(State);
                }
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PatientAddressNoteDetails AddrNote = new PatientAddressNoteDetails();
                AddrNote.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                AddrNote.Trip = dr.Table.Columns.Contains("Trip") ? dr["Trip"].ToString() : "";
                AddrNote.CallId = dr.Table.Columns.Contains("CallId") ? dr["CallId"].ToString() : "";
                AddrNote.ParamedicDetailsID = dr.Table.Columns.Contains("ParamedicDetailsID") ? dr["ParamedicDetailsID"].ToString() : "";
                AddrNote.StateString = lstStateString;
                lstPatientAddrNote.Add(AddrNote);
            }
            return lstPatientAddrNote;
        }

        public static List<SuperVisorUnits> GetSuperVisorUnits(DataSet ds)
        {
            List<SuperVisorUnits> lstSuperVUs = new List<SuperVisorUnits>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SuperVisorUnits SuperVU = new SuperVisorUnits();
                SuperVU.UnitAssignedID = dr.Table.Columns.Contains("UnitAssignedID") ? dr["UnitAssignedID"].ToString() : "";
                SuperVU.UnitId = dr.Table.Columns.Contains("UnitId") ? dr["UnitId"].ToString() : "";
                SuperVU.SuperVisorId = dr.Table.Columns.Contains("SuperVisorId") ? dr["SuperVisorId"].ToString() : "";
                SuperVU.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                SuperVU.SuperVisorName = dr.Table.Columns.Contains("SuperVisorName") ? dr["SuperVisorName"].ToString() : "";
                lstSuperVUs.Add(SuperVU);
            }
            return lstSuperVUs;
        }

        public static List<PrimaryCrewLocation> GetPrimaryCrewLoc(DataTable dt)
        {
            List<PrimaryCrewLocation> lstcrews = new List<PrimaryCrewLocation>();
            foreach (DataRow dr in dt.Rows)
            {
                PrimaryCrewLocation crewloc = new PrimaryCrewLocation();
                crewloc.Unit = dr.Table.Columns.Contains("Unit") ? dr["Unit"].ToString() : "";
                crewloc.Latitude = dr.Table.Columns.Contains("Latitude") ? dr["Latitude"].ToString() : "";
                crewloc.Longitude = dr.Table.Columns.Contains("Longitude") ? dr["Longitude"].ToString() : "";
                crewloc.CallId = dr.Table.Columns.Contains("CallId") ? dr["CallId"].ToString() : "";
                lstcrews.Add(crewloc);
            }
            return lstcrews;
        }


        public static List<ValueForDDL> GetSupVCallsCount(DataTable dt)
        {
            List<ValueForDDL> SupvCallsCount = new List<ValueForDDL>();
            foreach (DataRow dr in dt.Rows)
            {
                ValueForDDL SupvCall = new ValueForDDL();
                SupvCall.value = dr.Table.Columns.Contains("value") ? dr["value"].ToString() : "";
                SupvCall.text = dr.Table.Columns.Contains("text") ? dr["text"].ToString() : "";
                SupvCallsCount.Add(SupvCall);
            }
            return SupvCallsCount;
        }


    }
  

}