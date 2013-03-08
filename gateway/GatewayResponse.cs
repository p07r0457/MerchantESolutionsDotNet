using System;
using System.Text;
using Mes.Core;

namespace Mes.Gateway
{
    public class GatewayResponse : ResponseObject
    {
        ///<summary>A response object encapsulating a Payment Gateway response string.</summary>
        public GatewayResponse(int httpCode, string responseString, float duration) : base(httpCode, responseString, duration)
        {

        }

        ///<summary>Returns whether the request was approved. Always false when the HTTP code is not 200.</summary>
        public Boolean isApproved()
        {
            if (httpCode == 200)
            {
                string ec = getErrorCode();
                if (ec == null)
                    return false;
                if (ec.Equals("000") || ec.Equals("085"))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        ///<summary>Returns the error code recieved from the gateway. Returns null if the request failed.</summary>
        public string getErrorCode()
        {
            if (httpCode == 200)
                return getResponseValue("error_code");
            else
                return null;
        }

        ///<summary>Returns the authorization code recieved from the gateway. Returns null if the request was not approved.</summary>
        public string getAuthCode()
        {
            if (isApproved())
                return getResponseValue("auth_code");
            else
                return null;
        }


        ///<summary>Returns the address verification result recieved from the gateway. Returns null if the request was not approved.</summary>
        public string getAvsResult()
        {
            if (isApproved())
                return getResponseValue("avs_result");
            else
                return null;
        }

        ///<summary>Returns the card verification result recieved from the gateway. Returns null if the request was not approved.</summary>
        public string getCvvResult()
        {
            if (isApproved())
                return getResponseValue("cvv2_result");
            else
                return null;
        }

        ///<summary>Returns the plain text response recieved during the request. Returns null if the request failed. This is a human-readable string, not intended to be parsed.</summary>
        public string getResponseText()
        {
            return getResponseValue("auth_response_text");
        }

        ///<summary>Returns the transaction ID of the gateway request. Returns null if the request failed.</summary>
        public String getTransactionId()
        {
            if (httpCode == 200)
                return getResponseValue("transaction_id");
            else
                return null;
        }

    }
}
