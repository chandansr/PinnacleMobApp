#region

using System;
using System.Web;
using System.Xml.Serialization;

#endregion

namespace ezDispatcherWebsite.WCF.DataAccessLayer
{
    ///<summary>
    ///  <Description>Currently Logged User Information</Description>
    ///  <Author>Prashant Kumar Verma</Author>
    ///  <CreatedOn>April 17,2012</CreatedOn>
    ///</summary>
    [Serializable]
    [XmlRoot("CREDENTIAL")]
    public class LoggedUserInformation
    {
        private string _address1;
        private string _address2;
        private string _companyName;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _password;
        private string _url;

        [XmlElement("UserID")]
        public long UserID { get; set; }

        [XmlElement("LoginUserID")]
        public long LoginUserID { get; set; }

        [XmlElement("CompanyID")]
        public long CompanyID { get; set; }

        [XmlElement("CompanyTypeID")]
        public long CompanyTypeID { get; set; }

        [XmlElement("FirstName")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _firstName = value;
            }
        }

        [XmlElement("LastName")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _lastName = value;
            }
        }

        [XmlElement("Email")]
        public string Email
        {
            get { return _email; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _email = value;
            }
        }

        [XmlElement("Password")]
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _password = value;
            }
        }

        [XmlElement("CompanyName")]
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _companyName = value;
            }
        }

        [XmlElement("CurrentTimeStamp")]
        public string CurrentTimeStamp
        {
            get { return DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"); }
            set { }
        }

        [XmlElement("Address1")]
        public string Address1
        {
            get { return _address1; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _address1 = value;
            }
        }

        [XmlElement("Address2")]
        public string Address2
        {
            get { return _address2; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _address2 = value;
            }
        }

        [XmlElement("loginType")]
        public long loginType { get; set; }

        [XmlElement("UserIp")]
        public string UserIp
        {
            get
            {
                var ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip != null)
                {
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else
                {
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            set { }
        }

        [XmlElement("Url")]
        public string Url
        {
            get { return _url; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _url = value;
            }
        }

        [XmlElement("LoginDateTime")]
        public string LoginDateTime
        {
            get
            {
                if (HttpContext.Current.Session["_loginAccessTime"] == null)
                {
                    HttpContext.Current.Session["_loginAccessTime"] = DateTime.UtcNow;
                }
                return ((DateTime)HttpContext.Current.Session["_loginAccessTime"]).ToString("MM/dd/yyyy HH:MM:ss");
            }
            set { }
        }
        public int ClientTimeOffset
        {
            get
            {
                if (HttpContext.Current.Session["G_CLIENT_TIME_ZONE_OFFSET"] == null)
                {
                    HttpContext.Current.Session["G_CLIENT_TIME_ZONE_OFFSET"] = -240;
                }
                return ((int)HttpContext.Current.Session["G_CLIENT_TIME_ZONE_OFFSET"]);
            }
            set
            {

            }
        }
    }
}