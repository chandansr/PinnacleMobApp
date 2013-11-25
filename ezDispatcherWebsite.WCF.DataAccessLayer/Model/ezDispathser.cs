/* 
 *  Author:      SmartData    
 *  Create date: <02-11-2013>    
 *  Description: <Calls List>    
 *  Purpose:     to Get and manipulation data from 
 *               databse using sqlhelper repository for PhoneGap
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ezDispatcherWebsite.WCF.DataAccessLayer
{
    public class ezDispathser
    {

        #region GetDispatcherCalls
        /// <summary>
        /// this method is used to get all call list by ParamedicDetailsID
        /// </summary>
        /// <param name="ParamedicDetailsID"></param>
        /// <returns>dataset</returns>
        public DataTable GetDispatcherCalls(int PDID, int CallID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CallId", SqlDbType.BigInt);
            parameter[0].Value = CallID;
            parameter[1] = new SqlParameter("@PDID", SqlDbType.BigInt);
            parameter[1].Value = PDID;
            
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetDispatcherCalls", parameter);
            return ds.Tables[0];
        }
        #endregion

        #region GetDispatcherCallList
        /// <summary>
        /// this method is used to get all call list by ParamedicDetailsID
        /// </summary>
        /// <param name="ParamedicDetailsID"></param>
        /// <returns>dataset</returns>
        public DataTable GetDispatcherCallList(int ParamedicDetailsID,DateTime date, int Offset)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@PDID", SqlDbType.BigInt);
            parameter[0].Value = ParamedicDetailsID.ToString(); 
            parameter[1] = new SqlParameter("@DATE", SqlDbType.DateTime);
            parameter[1].Value = date;
            var credxml = Common.GetCredXml();
            parameter[2] = new SqlParameter("@CREDXML", SqlDbType.Xml);
            parameter[2].Value = credxml;
            parameter[3] = new SqlParameter("@p_Offset", SqlDbType.BigInt);
            parameter[3].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetDispatcherCallList", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region GetCrewLogin
        /// <summary>
        /// this method is used to get all call list by ParamedicDetailsID
        /// </summary>
        /// <param name="ParamedicDetailsID"></param>
        /// <returns>dataset</returns>
        public DataTable ValidateCrew(string UserName, string Password, string DeviceId, string DeviceType, int Offset)
        {
            SqlConnection conn = new SqlConnection();            
            DataSet ds = new DataSet();

            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@p_UserName", SqlDbType.NVarChar);
            parameter[0].Value = UserName;
            parameter[1] = new SqlParameter("@p_Password", SqlDbType.NVarChar);
            parameter[1].Value = Password;
            parameter[2] = new SqlParameter("@p_DeviceId", SqlDbType.NText);
            parameter[2].Value = DeviceId;
            parameter[3] = new SqlParameter("@p_DeviceType", SqlDbType.NVarChar);
            parameter[3].Value = DeviceType;
            parameter[4] = new SqlParameter("@p_Offset", SqlDbType.NVarChar);
            parameter[4].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_CrewLogin", parameter);

            HttpContext.Current.Session.Add("Paramedic", ds.Tables[0]);
            
            LoggedUserInformation validuser;
            Boolean IsUser = GetUserCred(ds.Tables[0], out validuser);
            if (IsUser)
            {                
                HttpContext.Current.Session.Add("CurrentUser", validuser);
            }
            return ds.Tables[0];           
        }
        #endregion


        #region GetCallDetails
        /// <summary>
        /// this method is used to get the details for the call
        /// </summary>
        /// <param name="CallID"></param>
        /// <returns>dataset</returns>
        public DataTable GetCallDetails(int CallId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CallId", SqlDbType.BigInt);
            parameter[0].Value = CallId.ToString();
            var credxml = Common.GetCredXml();
            parameter[1] = new SqlParameter("@CREDXML", SqlDbType.Xml);
            parameter[1].Value = credxml;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetCallDetails", parameter);
            return ds.Tables[0];
        }
        #endregion   

        #region GetCurrentUser Credentials
        protected Boolean GetUserCred(DataTable User, out LoggedUserInformation validUser)
        {
            var isUserExists = false;

            var validuser = new LoggedUserInformation();
            try
            {
                var user = User.Rows[0]["UserName"].ToString();
                if (user != null)
                {
                    validuser.FirstName = User.Rows[0]["FName"].ToString();
                    validuser.LastName = User.Rows[0]["LName"].ToString();
                    validuser.CompanyID = Convert.ToInt64(User.Rows[0]["CompanyId"]);
                    validuser.Address1 = User.Rows[0]["Address1"].ToString();
                    validuser.CompanyTypeID = Convert.ToInt64(User.Rows[0]["CompanyTypeId"]);
                    validuser.CompanyName = User.Rows[0]["CompanyName"].ToString();
                    validuser.UserID = Convert.ToInt64(User.Rows[0]["UserID"]);
                    validuser.Email = User.Rows[0]["Email"].ToString();
                    validuser.loginType = Convert.ToInt64(User.Rows[0]["LoginType"]);
                    validuser.LoginUserID = Convert.ToInt64(User.Rows[0]["LoginUserID"]);
                    validuser.Password = User.Rows[0]["Passward"].ToString();                        
                    isUserExists = true;
                }               
                
            }
            catch (Exception)
            {
                isUserExists = false;
            }
            validUser = validuser;
            return isUserExists;
        }
        #endregion


        #region GetCurrentCallDetails
        /// <summary>
        /// this method is used to get the details of the current call
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>dataset</returns>
        public DataTable GetCurrentCallDetails(int Id)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id.ToString();            
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetCurrentCall", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region SaveTimeStampsInfo
        /// <summary>
        /// this method is used to save the Time stamp details of the user
        /// </summary>        
        /// <returns>dataset</returns>
        public DataTable SaveTimeStampsInfo(int Id, int CallId, string Status, string CallNo, string Milege, string Desc, int offset, string IsDelay, string DelayReason)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[10];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id.ToString();
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId.ToString();
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status.ToString();
            parameter[3] = new SqlParameter("@p_TimeStamp", SqlDbType.Text);
            parameter[3].Value = DateTime.Now.ToShortTimeString();
            parameter[4] = new SqlParameter("@p_Milege", SqlDbType.Text);
            parameter[4].Value = Milege;
            parameter[5] = new SqlParameter("@p_CallNo", SqlDbType.Text);
            parameter[5].Value = CallNo;
            parameter[6] = new SqlParameter("@p_Description", SqlDbType.Text);
            parameter[6].Value = Desc;
            parameter[7] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[7].Value = (-1) * offset;
            parameter[8] = new SqlParameter("@p_IsDelay", SqlDbType.Text);
            parameter[8].Value = IsDelay;
            parameter[9] = new SqlParameter("@p_DelayReason", SqlDbType.Text);
            parameter[9].Value = DelayReason;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SaveTimeStamps", parameter);
            return ds.Tables[0];
        }
        #endregion



        #region VerifyEmail
        /// <summary>
        /// this method is used for verifying user Email.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataTable VerifyEmail(string EmailId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@p_Email", SqlDbType.Text);
            parameter[0].Value = EmailId.ToString();
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_VerifyUserEmail", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region SaveCrewLocation
        /// <summary>
        /// this method is used to save the current location of the Primary Crew.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet SaveCrewCurrentLocation(int Id, string Lat, string Lnt, string Addr, int Offset)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id.ToString();
            parameter[1] = new SqlParameter("@p_Lat", SqlDbType.Text);
            parameter[1].Value = Lat.ToString();
            parameter[2] = new SqlParameter("@p_Lnt", SqlDbType.Text);
            parameter[2].Value = Lnt.ToString();
            parameter[3] = new SqlParameter("@p_Addr", SqlDbType.NText);
            parameter[3].Value = Addr.ToString();
            parameter[4] = new SqlParameter("@p_Offset", SqlDbType.Text);
            parameter[4].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SaveCrewLocation", parameter);
            return ds;
        }
        #endregion


        #region GetTimeStampDetails
        /// <summary>
        /// this method is used to get the Call details for the Time Stamp page.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetCallDetailsForTimeStamp(int Id, int CallId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CallId", SqlDbType.BigInt);
            parameter[0].Value = CallId;
            parameter[1] = new SqlParameter("@PDID", SqlDbType.BigInt);
            parameter[1].Value = Id;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetDetailsForTimeStamp", parameter);
            return ds;
        }
        #endregion


        #region GetDelayStatus
        /// <summary>
        /// this method is used to get the Delay status of the unit for call.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetDelayStatus(int Id, int CallId, string Status, int Offset)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId;
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status;
            parameter[3] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[3].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetUnitDelayStatus", parameter);
            return ds;
        }
        #endregion


        #region SaveGeneralTimeStampsInfo
        /// <summary>
        /// this method is used to save the Time stamp details of the user without Milege.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataTable SaveGeneralTimeStampsInfo(int Id, int CallId, string Status, int offset, string JsonData)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id.ToString();
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId.ToString();
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status.ToString();
            parameter[3] = new SqlParameter("@p_TimeStamp", SqlDbType.Text);
            parameter[3].Value = DateTime.Now.ToShortTimeString();
            parameter[4] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[4].Value = (-1) * offset;
            parameter[5] = new SqlParameter("@p_JsonData", SqlDbType.Text);
            parameter[5].Value = JsonData;            
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SaveGeneralTimeStamps", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region SaveTransportTimeStampsInfo
        /// <summary>
        /// this method is used to save the Time stamp details of the user (Tranporting)
        /// </summary>        
        /// <returns>dataset</returns>
        public DataTable SaveTransportTimeStampsInfo(int Id, int CallId, string Status, int offset, string JsonData)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id.ToString();
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId.ToString();
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status.ToString();
            parameter[3] = new SqlParameter("@p_TimeStamp", SqlDbType.Text);
            parameter[3].Value = DateTime.Now.ToShortTimeString();
            parameter[4] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[4].Value = (-1) * offset;
            parameter[5] = new SqlParameter("@p_JsonData", SqlDbType.Text);
            parameter[5].Value = JsonData;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SaveTransportTimeStamps", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region SaveClearTimeStampsInfo
        /// <summary>
        /// this method is used to save the Time stamp details of the user (Clear)
        /// </summary>        
        /// <returns>dataset</returns>
        public DataTable SaveClearTimeStampsInfo(int Id, int CallId, string Status, int offset, string PCR)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id.ToString();
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId.ToString();
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status.ToString();
            parameter[3] = new SqlParameter("@p_TimeStamp", SqlDbType.Text);
            parameter[3].Value = DateTime.Now.ToShortTimeString();
            parameter[4] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[4].Value = (-1) * offset;
            parameter[5] = new SqlParameter("@p_PCR", SqlDbType.Text);
            parameter[5].Value = PCR;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SaveClearTimeStamps", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region GetStatusWiseDynamicFields
        /// <summary>
        /// this method is used to get the Dynamic fields for the provided status.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataTable GetStatusWiseDynamicFields(int Id, int CallId, string Status, int Offset)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId;
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status;
            parameter[3] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[3].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetStatusWiseDynamicFields", parameter);
            return ds.Tables[0];
        }
        #endregion


        #region GetTransportDynamicFields
        /// <summary>
        /// this method is used to get the Dynamic fields for the provided status(Transport).
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetTransportDynamicFields(int Id, int CallId, string Status, int Offset)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId;
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[2].Value = Status;
            parameter[3] = new SqlParameter("@p_offset", SqlDbType.BigInt);
            parameter[3].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetTransportDynamicFields", parameter);
            return ds;
        }
        #endregion


        #region GetPatientAddressNoteDetails
        /// <summary>
        /// this method is used to get the Call Details for Patient Address Note Details page.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetPatientAddressNoteDetails(int Id, int CallId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetPatientAddressNoteDetails", parameter);
            return ds;
        }
        #endregion

        #region SavePatientAddressNote
        /// <summary>
        /// this method is used to save the patient address as a note to dispathcer.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet SavePatientAddressNote(int Id, int CallId, string Status, string jsonData, string InsuranceInfo)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[1].Value = CallId;
            parameter[2] = new SqlParameter("@p_Status", SqlDbType.NVarChar);
            parameter[2].Value = Status;
            parameter[3] = new SqlParameter("@p_Data", SqlDbType.NText);
            parameter[3].Value = jsonData;
            parameter[4] = new SqlParameter("@p_InsuranceInfo", SqlDbType.NText);
            parameter[4].Value = InsuranceInfo;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SavePatientAddressNote", parameter);
            return ds;
        }
        #endregion


        #region GetFacilities
        /// <summary>
        /// this method is used to get the facilites.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetFacilities(int Id, string Facility)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_text", SqlDbType.Text);
            parameter[1].Value = Facility;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetFacilites", parameter);
            return ds;
        }
        #endregion


        #region GetDeviceDetails
        /// <summary>
        /// this method is used to get the Device Details.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetDeviceDetails(string DeviceId, string DeviceType)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@p_DeviceId", SqlDbType.NText);
            parameter[0].Value = DeviceId;
            parameter[1] = new SqlParameter("@p_DeviceType", SqlDbType.Text);
            parameter[1].Value = DeviceType;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetCrewDevice", parameter);
            return ds;
        }
        #endregion


        #region SaveDeviceToken
        /// <summary>
        /// this method is used to save the device Token needed for Push Notification.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet SaveDeviceToken(int Id, string DeviceId, string DeviceToken, string DevicePlatform, string jsonData)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            parameter[1] = new SqlParameter("@p_DeviceId", SqlDbType.NText);
            parameter[1].Value = DeviceId;
            parameter[2] = new SqlParameter("@p_DevicToken", SqlDbType.NText);
            parameter[2].Value = DeviceToken;
            parameter[3] = new SqlParameter("@p_DevicePlatform", SqlDbType.Text);
            parameter[3].Value = DevicePlatform;
            parameter[4] = new SqlParameter("@p_jsonData", SqlDbType.NText);
            parameter[4].Value = jsonData;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_SaveDeviceToken", parameter);
            return ds;
        }
        #endregion


        #region GetSuperVisorUnits
        /// <summary>
        /// this method is used to get the SuperVisor's Unit Details.
        /// </summary>        
        /// <returns>dataset</returns>
        public DataSet GetSuperVisorUnits(int Id)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[0].Value = Id;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetSuperVisorUnits", parameter);
            return ds;
        }
        #endregion


        #region GetSuperVisorCallList
        /// <summary>
        /// this method is used to get the SuperVisor's Call List.
        /// </summary>        
        /// <returns>dataset</returns>        
        public DataSet GetSuperVisorCallList(int Id, string Status, string UnitIds, int Offset)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@p_Status", SqlDbType.Text);
            parameter[0].Value = Status;
            parameter[1] = new SqlParameter("@p_Units", SqlDbType.NText);
            parameter[1].Value = UnitIds;
            parameter[2] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[2].Value = Id;
            parameter[3] = new SqlParameter("@p_Offset", SqlDbType.BigInt);
            parameter[3].Value = (-1) * Offset;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetSupVCallList", parameter);
            return ds;
        }
        #endregion


        #region GetPrimaryCrewLocation
        /// <summary>
        /// this method is used to get the current location of the crew(SuperVisor option).
        /// </summary>        
        /// <returns>dataset</returns>        
        public DataSet GetPrimaryCrewLoc(int Id, string UnitIds, int Offset, int CallId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[4];            
            parameter[0] = new SqlParameter("@p_Units", SqlDbType.NText);
            parameter[0].Value = UnitIds;
            parameter[1] = new SqlParameter("@p_Id", SqlDbType.BigInt);
            parameter[1].Value = Id;
            parameter[2] = new SqlParameter("@p_Offset", SqlDbType.BigInt);
            parameter[2].Value = (-1) * Offset;
            parameter[3] = new SqlParameter("@p_CallId", SqlDbType.BigInt);
            parameter[3].Value = CallId;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetCrewLocation", parameter);
            return ds;
        }
        #endregion


        #region GetSupVCallsCount
        /// <summary>
        /// this method is used to get the SuperVisor's Calls Count.
        /// </summary>        
        /// <returns>dataset</returns>        
        public DataSet GetSupVCallsCount(int PId, string PUnits)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@p_ID", SqlDbType.BigInt);
            parameter[0].Value = PId;
            parameter[1] = new SqlParameter("@P_Units", SqlDbType.NText);
            parameter[1].Value = PUnits;
            ds = SqlHelper.ExecuteDataset(Common.ConnString, CommandType.StoredProcedure, "ssp_PhoneGap_GetSupVCallCount", parameter);
            return ds;
        }
        #endregion
    }
}
