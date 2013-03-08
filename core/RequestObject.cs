using System;
using System.Collections;

namespace Mes.Core
{
    public class RequestObject
    {
        internal Hashtable requestTable;

        protected RequestObject()
        {
            requestTable = new Hashtable();
        }

        public void addRequestField(string key, string value)
        {
            requestTable.Add(key, value);
        }

        protected void addRequestField(string key, bool value)
        {
            requestTable.Add(key, value);
        }

        protected void removeField(string key)
        {
            if (requestTable.Contains(key))
                requestTable.Remove(key);
        }
    }
}
