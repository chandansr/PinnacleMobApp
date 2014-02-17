using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ezDispatcherWebsite.WCF.DataAccessLayer;
using MoonAPNS;


namespace ezDispatcherWebSite.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service1 : IService1
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        #region GetUserDetailsDummy
        /// <summary>
        /// Test Method
        /// </summary>
        /// <returns></returns>
        ///         
        public UserDetails[] GetUserDetails(string Username)
        {
            List<UserDetails> tt = new List<UserDetails>();
            UserDetails uu = new UserDetails();
            uu.UserName = "dsfd";
            uu.UserId = "11";
            uu.Role = "sds";
            tt.Add(uu);
            return tt.ToArray();
        }
        #endregion


        #region GetDispatcherCalls
        /// <summary>
        /// This is the method we are using for getting the details of Dispatcher Calls.
        /// </summary>
        /// <returns></returns>
        ///         
      
        public Stream GetDispatcherCalls(DispatcherCalls DispatcherCalls)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string CallID = DispatcherCalls.CallId;                
                //TraceService("GetDispatcherCalls entry at " + DateTime.Now);
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetDispatcherCalls(Convert.ToInt32(DispatcherCalls.PDID), Convert.ToInt32(CallID));
                List<DispatcherCallDetails> lstcurCallr = ezDispatcherWebSite.WCFService.Common.GetCallDetailsJson(dt);

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
        public Stream GetDispatcherCallList(DispatcherCallsList DispatcherCallsList)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string ParamedicDetailsID = DispatcherCallsList.Id;
                DateTime date = new DateTime();
                date = DateTime.UtcNow;
                //TraceService("GetDispatcherCalls entry at " + DateTime.Now);
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetDispatcherCallList(Convert.ToInt32(ParamedicDetailsID), date, DispatcherCallsList.Offset);
                List<DispatcherCall> lstcurCallr = ezDispatcherWebSite.WCFService.Common.GetJson(dt);

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
        /// This is the method we are using for validating the crew while login to the Application.
        /// </summary>
        /// <returns></returns>
        ///         
        public List<ezLogin> ValidateCrew(CrewDetails CrewDetails)
        {
            ezLogin ez = new ezLogin();
            List<ezLogin> lstez = new List<ezLogin>();
            try
            {
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                var password = ezDispatcherWebSite.WCFService.Common.EncryptText(CrewDetails.Password);
                dt = clsezDispathser.ValidateCrew(CrewDetails.Username, password, CrewDetails.DeviceId, CrewDetails.DeviceType, CrewDetails.Offset);
                string str = dt.Rows[0]["UserID"].ToString();

                if (str != "")
                {
                    ez.Message = "Success";
                    ez.Response = "1";
                    ez.Result = dt.Rows[0]["UserID"].ToString() + "," + dt.Rows[0]["LocReq"].ToString() + "," + dt.Rows[0]["IsSuperVisor"].ToString() + "," 
                                    + dt.Rows[0]["EncryptionAllowed"].ToString();
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
        public Stream GetCallDetails(DispatcherCalls DispatcherCalls)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string CallID = DispatcherCalls.CallId;

                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetCallDetails(Convert.ToInt32(CallID));
                List<CallDetails> lstCallDetails = ezDispatcherWebSite.WCFService.Common.GetCallFullInfoJson(dt);

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
        public Stream GetCurrentCallDetails(DispatcherCallsList DispatcherCallsList)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string ID = DispatcherCallsList.Id;

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
        public Stream SaveTimeStampsInfo(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveTimeStampsInfo(Convert.ToInt32(TimeStampsInfo.Id), Convert.ToInt32(TimeStampsInfo.CallId), TimeStampsInfo.Status, TimeStampsInfo.CallNo, TimeStampsInfo.Milege, TimeStampsInfo.Desc, TimeStampsInfo.offset, TimeStampsInfo.IsDelay, TimeStampsInfo.DelayReason);

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
                    var Password = ezDispatcherWebSite.WCFService.Common.DecryptText(dt.Rows[0]["Password"].ToString());
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
      
        public Stream SaveCrewLocation(CrewLocation CrewLocation)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.SaveCrewCurrentLocation(Convert.ToInt32(CrewLocation.Id), CrewLocation.Lat, CrewLocation.Lnt, CrewLocation.Addr, CrewLocation.Offset);
                List<DispatcherCall> lstcurCallr = new List<DispatcherCall>();
                if (ds.Tables.Contains("Table1") && ds.Tables[1].Rows.Count > 0)
                {
                    lstcurCallr = ezDispatcherWebSite.WCFService.Common.GetJson(ds.Tables[1]);
                }
                else
                {
                    lstcurCallr = ezDispatcherWebSite.WCFService.Common.GetEncryptionDetails(ds);
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
        public Stream GetTimeStampDetails(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetCallDetailsForTimeStamp(TimeStampsInfo.Id, TimeStampsInfo.CallId);
                List<CallDetailsForTimeStamp> lstcallD = ezDispatcherWebSite.WCFService.Common.GetJsonforTimeStamp(ds);
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
        public Stream GetDelayStatus(DelayStatus DelayStatus)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetDelayStatus(DelayStatus.Id, DelayStatus.CallId, DelayStatus.Status, DelayStatus.Offset);
                List<UnitDelayStatus> lstUnitDelayS = ezDispatcherWebSite.WCFService.Common.GetJsonforUnitDelayStatus(ds);
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
        public Stream SaveGeneralTimeStampsInfo(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveGeneralTimeStampsInfo(Convert.ToInt32(TimeStampsInfo.Id), Convert.ToInt32(TimeStampsInfo.CallId), TimeStampsInfo.Status, TimeStampsInfo.offset, TimeStampsInfo.JsonData);

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
        public Stream SaveTransportTimeStampsInfo(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveTransportTimeStampsInfo(Convert.ToInt32(TimeStampsInfo.Id), Convert.ToInt32(TimeStampsInfo.CallId), TimeStampsInfo.Status, TimeStampsInfo.offset, TimeStampsInfo.JsonData);

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
        public Stream SaveClearTimeStampsInfo(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.SaveClearTimeStampsInfo(Convert.ToInt32(TimeStampsInfo.Id), Convert.ToInt32(TimeStampsInfo.CallId), TimeStampsInfo.Status, TimeStampsInfo.offset, TimeStampsInfo.PCR);

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
        public Stream GetStatusWiseDynamicFields(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataTable dt = new DataTable();
                ezDispathser clsezDispathser = new ezDispathser();
                dt = clsezDispathser.GetStatusWiseDynamicFields(TimeStampsInfo.Id, TimeStampsInfo.CallId, TimeStampsInfo.Status, TimeStampsInfo.offset);
                List<DynamicFields> lstDynamicFields = ezDispatcherWebSite.WCFService.Common.GetDynamicFieldsJson(dt);

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
        public Stream GetTransportDynamicFields(TimeStampsInfo TimeStampsInfo)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetTransportDynamicFields(TimeStampsInfo.Id, TimeStampsInfo.CallId, TimeStampsInfo.Status, TimeStampsInfo.offset);
                List<TransportDynamicFields> lstTransportDynamicFields = ezDispatcherWebSite.WCFService.Common.GetTransportDynamicFieldsJson(ds);

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
        public Stream GetPatientAddressNoteDetails(PatientAddressNote PatientAddressNoteDetails)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetPatientAddressNoteDetails(PatientAddressNoteDetails.Id, PatientAddressNoteDetails.CallId);
                List<PatientAddressNoteDetails> lstPatientAddrNote = ezDispatcherWebSite.WCFService.Common.GetPatientAddressNoteDetails(ds);

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
        public Stream SavePatientAddressNote(PatientAddressNote PatientAddressNoteDetails)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.SavePatientAddressNote(PatientAddressNoteDetails.Id, PatientAddressNoteDetails.CallId, PatientAddressNoteDetails.Status, PatientAddressNoteDetails.jsonData, PatientAddressNoteDetails.InsuranceInfo);

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
                List<ValueForDDL> Facilities = ezDispatcherWebSite.WCFService.Common.GetFacilities(ds.Tables[0]);

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
                List<DeviceDetails> DeviceDetails = ezDispatcherWebSite.WCFService.Common.GetDeviceDetails(ds.Tables[0]);

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
        public Stream SaveDeviceToken(Device DeviceDetails)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.SaveDeviceToken(DeviceDetails.Id, DeviceDetails.DeviceId, DeviceDetails.DeviceToken, DeviceDetails.DevicePlatform, DeviceDetails.jsonData);

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
                List<SuperVisorUnits> SuperVisorUnits = ezDispatcherWebSite.WCFService.Common.GetSuperVisorUnits(ds);
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
        public Stream GetSuperVisorCallList(SuperVisorUnitsDetails SuperVisorUnitsDetails)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetSuperVisorCallList(SuperVisorUnitsDetails.Id, SuperVisorUnitsDetails.Status, SuperVisorUnitsDetails.UnitIds, SuperVisorUnitsDetails.Offset);
                List<SupervisorCalls> lstSupVCallList = ezDispatcherWebSite.WCFService.Common.GetSupvCallListJson(ds.Tables[0]);

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
                List<PrimaryCrewLocation> lstprmcrew = ezDispatcherWebSite.WCFService.Common.GetPrimaryCrewLoc(ds.Tables[0]);

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
        public Stream GetSupVCallsCount(SupVCalls SupVCalls)
        {
            Dictionary<string, object> retDict = new Dictionary<string, object>();
            try
            {
                string returnValue = string.Empty;
                DataSet ds = new DataSet();
                ezDispathser clsezDispathser = new ezDispathser();
                ds = clsezDispathser.GetSupVCallsCount(SupVCalls.PId, SupVCalls.PUnits);
                List<ValueForDDL> lstsupvCount = ezDispatcherWebSite.WCFService.Common.GetSupVCallsCount(ds.Tables[0]);

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
