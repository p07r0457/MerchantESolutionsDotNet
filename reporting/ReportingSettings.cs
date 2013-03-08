using System;
using System.Text;
using Mes.Core;

namespace Mes.Reporting
{
    public class ReportingSettings : Settings
    {
        ///<summary>Used by MeS to test new release code before being pushed to production.</summary>
        public static String URL_TEST = "https://test.merchante-solutions.com/jsp/reports/report_api.jsp";

        ///<summary>Used to request reporting from the live database.</summary>
        public static String URL_LIVE = "https://www.merchante-solutions.com/jsp/reports/report_api.jsp";
	
	    private String userName;
	    private String userPass;

        ///<summary>Gateway specific settings.</summary>
        public ReportingSettings()
            : base()
        {
            setHostUrl(URL_LIVE);
        }

        ///<summary>Sets the reporting API Credentials.</summary>
        public ReportingSettings setCredentials(string userName, string userPass)
        {
            this.userName = userName;
            this.userPass = userPass;
            return this;
        }

        ///<summary>Returns the currently set username.</summary>
        public string getUserName()
        {
            return userName;
        }

        ///<summary>Returns the currently set user password.</summary>
        public string getUserPass()
        {
            return userPass;
        }
    }
}
