using System;

namespace Mes.Core
{
    public abstract class Settings
    {
        public static String URL_TEST_404 = "http://httpstat.us/404";
	    public static String URL_TEST_403 = "http://httpstat.us/403";
	    public static String URL_TEST_500 = "http://httpstat.us/500";
	    public static String URL_TEST_503 = "http://httpstat.us/503";
        public enum Method { POST, GET }

        private string hostUrl;
        private Method method;
        private int timeout;
        private Boolean verbose;

        public Settings()
        {
            method = Method.POST;
            timeout = 20000;
        }

        ///<summary>Sets the full URL of the host.</summary>
        public Settings setHostUrl(string hostUrl)
        {
            this.hostUrl = hostUrl;
            return this;
        }

        ///<summary>Sets the HTTP method to GET or POST.</summary>
        public Settings setMethod(Method method)
        {
            this.method = method;
            return this;
        }

        ///<summary>Sets request / response data posting to the console when true.</summary>
        public Settings setVerbose(Boolean verbose)
        {
            this.verbose = verbose;
            return this;
        }

        ///<summary>Returns the full host URL.</summary>
        public string getHostUrl()
        {
            return hostUrl;
        }

        public Method getMethod()
        {
            return method;
        }

        public int getTimeout() {
            return timeout;
        }

        ///<summary>Returns whether the request will post details to the console.</summary>
        public Boolean isVerbose()
        {
            return verbose;
        }
    }
}
