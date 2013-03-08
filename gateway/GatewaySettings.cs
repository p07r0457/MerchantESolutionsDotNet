using System;
using System.Text;
using Mes.Core;

namespace Mes.Gateway
{
    public class GatewaySettings : Settings
    {
        ///<summary>Used by MeS to test new release code before being pushed to production. Uses the transaction simulator.</summary>
	    public static String URL_TEST = "https://test.merchante-solutions.com/mes-api/tridentApi";

        ///<summary>Uses the same codebase as the live URL, but is pointed to the transaction simulator.</summary>
	    public static String URL_CERT = "https://cert.merchante-solutions.com/mes-api/tridentApi";

        ///<summary>Used to process live transaction requests.</summary>
	    public static String URL_LIVE = "https://api.merchante-solutions.com/mes-api/tridentApi";
	
	    private String profileId;
	    private String profileKey;

        ///<summary>Gateway specific settings.</summary>
        public GatewaySettings() : base()
        {
            setHostUrl(URL_CERT);
        }

        ///<summary>Sets the profile ID and Key.</summary>
        public GatewaySettings setCredentials(string profileId, string profileKey) {
            this.profileId = profileId;
            this.profileKey = profileKey;
            return this;
        }

        ///<summary>Returns the currently set profileID.</summary>
        public string getProfileId()
        {
            return profileId;
        }

        ///<summary>Returns the currently set profile Key.</summary>
        public string getProfileKey()
        {
            return profileKey;
        }
    }
}
