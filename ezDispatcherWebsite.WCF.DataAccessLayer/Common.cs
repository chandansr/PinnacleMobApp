using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using ezDispatcherWebsite.WCF.DataAccessLayer;
using System.Data;
using System.Reflection;

namespace ezDispatcherWebsite.WCF.DataAccessLayer
{
    public class Common
    {
        public static string ConnString
        {
            get
            {
                //return ConfigurationManager.AppSettings["connString"].ToString();
                //return ConfigurationSettings.AppSettings["connString"].ToString();
                return ConfigurationManager.ConnectionStrings["connString"].ToString();
            }
        }
        public static string FilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogPath"].ToString();
            }
        }

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
        

    }
}
