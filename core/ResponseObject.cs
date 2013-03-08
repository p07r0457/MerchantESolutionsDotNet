using System;
using System.Collections;

namespace Mes.Core
{
    public abstract class ResponseObject
    {
        protected int httpCode;
        protected String responseString;
        protected float duration;
        protected Hashtable responseTable;

        ///<summary>Contains details about the response.</summary>
        public ResponseObject(int httpCode, string responseString, float duration)
        {
            responseTable = new Hashtable();
            this.httpCode = httpCode;
            this.responseString = responseString;
            this.duration = duration;
        }

        internal void addResponseValue(String key, String value)
        {
            responseTable.Add(key, value);
        }

        ///<summary>Returns a specific value from the response given a name. Returns null if it is not found.</summary>
        public String getResponseValue(string key)
        {
            if (responseTable.ContainsKey(key))
                return (String)responseTable[key];
            else
                return null;
        }

        ///<summary>Returns the duration the request took to process (full round trip time in ms).</summary>
        public float getDuration()
        {
            return duration;
        }

        ///<summary>Returns the HTTP response code recieved during the request.</summary>
        public int getHttpCode()
        {
            return httpCode;
        }
    }
}
