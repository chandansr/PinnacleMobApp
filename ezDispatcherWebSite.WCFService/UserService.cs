using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ezDispatcherWebSite.WCFService.ServiceInterface;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using ezDispatcherWebsite.WCF.DataAccessLayer;
using System.ServiceModel.Activation;
using System.Web.SessionState;
using System.ServiceModel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;
using System.Net.Sockets;
using MoonAPNS;


namespace ezDispatcherWebSite.WCFService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class UserService : IUserService
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();


        #region GetDispatcherCalls
        /// <summary>
        /// This is the method we are using for getting the details of Dispatcher Calls.
        /// </summary>
        /// <returns></returns>
        ///         
        public Stream GetDispatcherCalls(string PDID, string CallId)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string CallID = CallId;
                if (CallId == "")
                {
                    CallID = "2861";
                }
                //TraceService("GetDispatcherCalls entry at " + DateTime.Now);
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetDispatcherCalls(Convert.ToInt32(PDID), Convert.ToInt32(CallID));
                List<DispatcherCallDetails> lstcurCallr = Common.GetCallDetailsJson(dt);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstcurCallr);

            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);

        }
        #endregion


        #region GetDispatcherCallsList
        /// <summary>
        /// This is the interface method we are using for get all list of Dispatcher Calls.
        /// </summary>
        /// <returns>This method only returns list of users  Dispatcher Calls</returns>
        public Stream GetDispatcherCallList(string Id, int Offset)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string ParamedicDetailsID = Id;
                DateTime date = new DateTime();
                date = DateTime.UtcNow;
                //TraceService("GetDispatcherCalls entry at " + DateTime.Now);
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetDispatcherCallList(Convert.ToInt32(ParamedicDetailsID), date, Offset);
                List<DispatcherCall> lstcurCallr = Common.GetJson(dt);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstcurCallr);

            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region ValidateCrew
        /// <summary>
        /// This is the method we are using for validating the crew.
        /// </summary>
        /// <returns></returns>
        public List<ezLogin> ValidateCrew(string Username, string Password, string DeviceId, string DeviceType, int Offset)
        {
            ezLogin ez = new ezLogin();
            List<ezLogin> lstez = new List<ezLogin>();
            try
            {
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                var password = Common.EncryptText(Password);
                dt = clsezDispathser.ValidateCrew(Username, password, DeviceId, DeviceType, Offset);
                string str = dt.Rows[0]["UserID"].ToString();

                if (str != "")
                {
                    ez.Message = "Success";
                    ez.Response = "1";
                    ez.Result = dt.Rows[0]["UserID"].ToString() + "," + dt.Rows[0]["LocReq"].ToString() + "," + dt.Rows[0]["IsSuperVisor"].ToString();
                }
                else
                {
                    ez.Message = "Fail";
                    ez.Response = "0";
                    ez.Result = dt.Rows[0]["UserID"].ToString();
                }
            }
            catch (Exception ex)
            {
                ez.Message = "Fail";
                ez.Response = "0";
                ez.Result = ex.Message;
            }

            lstez.Add(ez);
            return lstez;
        }
        #endregion


        #region GetCallsDetails
        /// <summary>
        /// This is the method we are using for getting all details related to call.
        /// </summary>
        /// <returns></returns>
        public Stream GetCallDetails(string CallId)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string CallID = CallId;

                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetCallDetails(Convert.ToInt32(CallID));
                List<CallDetails> lstCallDetails = Common.GetCallFullInfoJson(dt);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstCallDetails);

            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetCurrentCallDetails
        /// <summary>
        /// This is the method we are using for getting details of the current call for the crew.
        /// </summary>
        /// <returns></returns>
        public Stream GetCurrentCallDetails(string Id)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string ID = Id;

                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetCurrentCallDetails(Convert.ToInt32(ID));

                retDict.Add("Message", "Success");
                if (dt.Rows.Count > 0)
                {
                    retDict.Add("Data", dt.Rows[0]["CurrentCallId"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }

            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region SaveTimeStampsInfo
        /// <summary>
        /// This is the method we are using for getting details of the current call for the crew.
        /// </summary>
        /// <returns></returns>
        public Stream SaveTimeStampsInfo(int Id, int CallId, string Status, string CallNo, string Milege, string Desc, int offset, string IsDelay, string DelayReason)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveTimeStampsInfo(Convert.ToInt32(Id), Convert.ToInt32(CallId), Status, CallNo, Milege, Desc, offset, IsDelay, DelayReason);

                retDict.Add("Message", "Success");
                if (dt.Rows.Count > 0)
                {
                    retDict.Add("Data", dt.Rows[0]["INSERTSTATUS"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion



        #region SendPassword
        /// <summary>
        /// This is the method we are using for Verifying and then sending user password through email.
        /// </summary>
        /// <returns></returns>
        public Stream SendPassword(string EmailId)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.VerifyEmail(EmailId);

                if (dt.Rows.Count > 0)
                {
                    var Password = Common.DecryptText(dt.Rows[0]["Password"].ToString());
                    var Email = dt.Rows[0]["Email"].ToString();
                    if (dt.Rows[0]["Email"].ToString() != "")
                    {

                        var body = "<p>Dear User,</p><p>We have received your request for reminder of your password.</p><p>Your Credentials are:</p> "
                                   + "<p>Username: <span style=\"text-decoration: underline;\"><strong> " + Email + "</strong></span></p>"
                                   + "<p>Password:<span style=\"text-decoration: underline;\"><strong> " + Password + "</strong></span></p>"
                                   + "<p>&nbsp;</p><p>From,</p><p><strong>Pinnacle Response Systems</strong></p>";

                        SendEmail(Email, body, "Password Notification", null, null, null);  
                    }
                }

                retDict.Add("Message", "Success");
                if (dt.Rows.Count > 0)
                {
                    retDict.Add("Data", dt.Rows[0]["Email"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }


        #region SendUserPassword
        /// <summary>
        /// This are the methods we are using for sending user password through email.
        /// </summary>
        /// <returns></returns>
        /// 
        public static void SendEmail(string emailid, string BodyWithoutTemplate, string ASubject, string emailAddress, string smtppassword, string host)
        {
            if (String.IsNullOrEmpty(emailAddress))
            {
                emailAddress = "info@thetransfercenter.com";
            }
            {
                if (String.IsNullOrEmpty(smtppassword))
                {
                    smtppassword = "Ambulance01";
                }

                if (String.IsNullOrEmpty(host))
                {
                    host = "smtpout.secureserver.net";
                }
                SmtpClient SmtpServer = GetSMTPClient(host);
                SmtpServer.Host = host;
                MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailAddress, smtppassword);
                SmtpServer.Port = 25;
                mail.From = new MailAddress(emailAddress);
                mail.To.Add(emailid);
                mail.Subject = ASubject;
                mail.Body = BodyWithoutTemplate;
                mail.IsBodyHtml = true;
                //  SmtpServer.Send(mail);
                SmtpServer.SendAsync(mail, null);
            }
        }

        public static SmtpClient GetSMTPClient(string Host)
        {
            switch (Host)
            {
                case "smtp.gmail.com":
                    return new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false
                    };
                default:
                    return new System.Net.Mail.SmtpClient();
            }
        }

        #endregion

        #endregion


        #region SaveCrewLocation
        /// <summary>
        /// This is the method we are using for saving the current location of the Primary Crew.
        /// </summary>
        /// <returns></returns>
        public Stream SaveCrewLocation(int Id, string Lat, string Lnt, string Addr, int Offset)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.SaveCrewCurrentLocation(Convert.ToInt32(Id), Lat, Lnt, Addr, Offset);
                List<DispatcherCall> lstcurCallr = new List<DispatcherCall>(); 
                if (ds.Tables.Contains("Table1"))
                {
                    lstcurCallr = Common.GetJson(ds.Tables[1]);
                }
                retDict.Add("Message", "Success");
                retDict.Add("Data", lstcurCallr);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetTimeStampDetails
        /// <summary>
        /// This is the method we are using for getting the details of call for timestamp page.
        /// </summary>
        /// <returns></returns>
        public Stream GetTimeStampDetails(int Id, int CallId)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetCallDetailsForTimeStamp(Id, CallId);
                List<CallDetailsForTimeStamp> lstcallD = Common.GetJsonforTimeStamp(ds);
                retDict.Add("Message", "Success");
                retDict.Add("Data", lstcallD);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion



        #region GetDelayStatus
        /// <summary>
        /// This is the method we are using for getting the Delay status of the unit.
        /// </summary>
        /// <returns></returns>
        public Stream GetDelayStatus(int Id, int CallId, string Status, int Offset)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetDelayStatus(Id, CallId, Status, Offset);
                List<UnitDelayStatus> lstUnitDelayS = Common.GetJsonforUnitDelayStatus(ds);
                retDict.Add("Message", "Success");
                retDict.Add("Data", lstUnitDelayS);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion



        #region SaveGeneralTimeStampsInfo
        /// <summary>
        /// This is the method we are using for saving the Time stamp details of the user (without Milege).
        /// </summary>
        /// <returns></returns>
        public Stream SaveGeneralTimeStampsInfo(int Id, int CallId, string Status, int offset, string JsonData)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveGeneralTimeStampsInfo(Convert.ToInt32(Id), Convert.ToInt32(CallId), Status, offset, JsonData);

                retDict.Add("Message", "Success");
                if (dt.Rows.Count > 0)
                {
                    retDict.Add("Data", dt.Rows[0]["INSERTSTATUS"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region SaveTransportTimeStampsInfo
        /// <summary>
        /// This is the method we are using for saving the Transport timestamp of the user.
        /// </summary>
        /// <returns></returns>
        public Stream SaveTransportTimeStampsInfo(int Id, int CallId, string Status, int offset, string JsonData)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveTransportTimeStampsInfo(Convert.ToInt32(Id), Convert.ToInt32(CallId), Status, offset, JsonData);

                retDict.Add("Message", "Success");
                if (dt.Rows.Count > 0)
                {
                    retDict.Add("Data", dt.Rows[0]["INSERTSTATUS"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region SaveClearTimeStampsInfo
        /// <summary>
        /// This is the method we are using for saving the Clear timestamp of the user.
        /// </summary>
        /// <returns></returns>
        public Stream SaveClearTimeStampsInfo(int Id, int CallId, string Status, int offset, string PCR)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveClearTimeStampsInfo(Convert.ToInt32(Id), Convert.ToInt32(CallId), Status, offset, PCR);

                retDict.Add("Message", "Success");
                if (dt.Rows.Count > 0)
                {
                    retDict.Add("Data", dt.Rows[0]["INSERTSTATUS"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetStatusWiseDynamicFields
        /// <summary>
        /// This is the method we are using for getting the Dynamic fields for the status.
        /// </summary>
        /// <returns></returns>
        public Stream GetStatusWiseDynamicFields(int Id, int CallId, string Status, int Offset)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetStatusWiseDynamicFields(Id, CallId, Status, Offset);
                List<DynamicFields> lstDynamicFields = Common.GetDynamicFieldsJson(dt);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstDynamicFields);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetTransportDynamicFields
        /// <summary>
        /// This is the method we are using for getting the Dynamic fields for the status(Transport).
        /// </summary>
        /// <returns></returns>
        public Stream GetTransportDynamicFields(int Id, int CallId, string Status, int Offset)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetTransportDynamicFields(Id, CallId, Status, Offset);
                List<TransportDynamicFields> lstTransportDynamicFields = Common.GetTransportDynamicFieldsJson(ds);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstTransportDynamicFields);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetPatientAddressNoteDetails
        /// <summary>
        /// This is the method we are using for getting the Call Details for the Patient Address note details page.
        /// </summary>
        /// <returns></returns>
        public Stream GetPatientAddressNoteDetails(int Id, int CallId)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetPatientAddressNoteDetails(Id, CallId);
                List<PatientAddressNoteDetails> lstPatientAddrNote = Common.GetPatientAddressNoteDetails(ds);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstPatientAddrNote);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region SavePatientAddressNote
        /// <summary>
        /// This is the method we are using for saving the patient address as a note to dispatcher.
        /// </summary>
        /// <returns></returns>
        public Stream SavePatientAddressNote(int Id, int CallId, string Status, string jsonData, string InsuranceInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.SavePatientAddressNote(Id, CallId, Status, jsonData, InsuranceInfo);

                retDict.Add("Message", "Success");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retDict.Add("Data", ds.Tables[0].Rows[0]["INSERTSTATUS"].ToString());
                }
                else
                {
                    retDict.Add("Data", "");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetFacilities
        /// <summary>
        /// This is the method we are using for getting the facilities.
        /// </summary>
        /// <returns></returns>
        public Stream GetFacilities(int Id, string Facility)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetFacilities(Id, Facility);
                List<ValueForDDL> Facilities = Common.GetFacilities(ds.Tables[0]);

                retDict.Add("Message", "Success");
                retDict.Add("Data", Facilities);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region GetDeviceDetails
        /// <summary>
        /// This is the method we are using for getting the Device details.
        /// </summary>
        /// <returns></returns>
        public Stream GetDeviceDetails(string DeviceId, string DeviceType)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetDeviceDetails(DeviceId, DeviceType);
                List<DeviceDetails> DeviceDetails = Common.GetDeviceDetails(ds.Tables[0]);

                retDict.Add("Message", "Success");
                retDict.Add("Data", DeviceDetails);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region Send Notifications
        /// <summary>
        /// This is the method we are using for sending the push notification to the device.
        /// </summary>
        /// <returns></returns>
        public string SendNotificationToAndroid(string deviceId, string message)
        {
            string GoogleAppID = "AIzaSyBu8Q1QccprAOTdQzEYwJ8rjyJu1pS53pM";
            var SENDER_ID = "169794752891";
            var value = message;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value 
                                + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";


            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }


        public string SendNotificationToIos(string deviceId, string message)
        {
            var payload1 = new NotificationPayload(deviceId, message, 1, "default");
            //payload1.AddCustom("RegionID", "IDQ10150");

            var p = new List<NotificationPayload> { payload1 };

            var push = new PushNotification(false, HttpContext.Current.Server.MapPath("~") + "/nitin.p12", "iphone");
            var rejected = push.SendToApple(p);
            foreach (var item in rejected)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
            return "";
        }

        #endregion


        #region SaveDeviceToken
        /// <summary>
        /// This is the method we are using for saving the device Token needed for PushNotification.
        /// </summary>
        /// <returns></returns>
        public Stream SaveDeviceToken(int Id, string DeviceId, string DeviceToken, string DevicePlatform, string jsonData)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.SaveDeviceToken(Id, DeviceId, DeviceToken, DevicePlatform, jsonData);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    retDict.Add("Message", "Success");
                    retDict.Add("Data", ds.Tables[0].Rows[0]["ParamedicId"]);
                }
                else
                {
                    retDict.Add("Message", "Failure");
                    retDict.Add("Data", "0");
                }
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region Get SuperVisor Units
        /// <summary>
        /// This is the method we are using for getting the SuperVisor Units.
        /// </summary>
        /// <returns></returns>
        public Stream GetSuperVisorUnits(int Id)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetSuperVisorUnits(Id);
                List<SuperVisorUnits> SuperVisorUnits = Common.GetSuperVisorUnits(ds);
                retDict.Add("Message", "Success");
                retDict.Add("Data", SuperVisorUnits);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region Get SuperVisor Call List
        /// <summary>
        /// This is the method we are using for getting the SuperVisor Call List.
        /// </summary>
        /// <returns></returns>
        public Stream GetSuperVisorCallList(int Id, string Status, string UnitIds, int Offset)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetSuperVisorCallList(Id, Status, UnitIds, Offset);
                List<SupervisorCalls> lstSupVCallList = Common.GetSupvCallListJson(ds.Tables[0]);
                
                retDict.Add("Message", "Success");
                retDict.Add("Data", lstSupVCallList);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion


        #region Get Primary Crew Location
        /// <summary>
        /// This is the method we are using for getting the current location of the crew(SuperVisor option).
        /// </summary>
        /// <returns></returns>
        public Stream GetPrimaryCrewLocation(int Id, string UnitIds, int Offset, int CallId)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetPrimaryCrewLoc(Id, UnitIds, Offset, CallId);
                List<PrimaryCrewLocation> lstprmcrew = Common.GetPrimaryCrewLoc(ds.Tables[0]);

                retDict.Add("Message", "Success");
                retDict.Add("Data", lstprmcrew);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion



        #region Get Primary Crew Location
        /// <summary>
        /// This is the method we are using for getting the SuperVisor calls count.
        /// </summary>
        /// <returns></returns>
        public Stream GetSupVCallsCount(int PId, string PUnits)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetSupVCallsCount(PId, PUnits);
                List<ValueForDDL> lstsupvCount = Common.GetSupVCallsCount(ds.Tables[0]);
                
                retDict.Add("Message", "Success");
                retDict.Add("Data", lstsupvCount);
            }
            catch (Exception ex)
            {
                if (retDict.ContainsKey("Message"))
                {
                    retDict["Message"] = "Failure;" + ex.Message;
                }
                else
                {
                    retDict.Add("Message", "Failure;" + ex.Message);
                }
            }
            string sResponse = jsSerializer.Serialize(retDict);
            byte[] bResponse = Encoding.UTF8.GetBytes(sResponse);
            return new MemoryStream(bResponse);
        }
        #endregion

    }
}