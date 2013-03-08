using System;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Mes.Core
{
    internal class Http
    {
        internal Settings settings;

        private string requestString;
        private int httpResponseCode;
        private string responseString;
        private float duration;
        private string contentType = "application/x-www-form-urlencoded";

        internal Http(Settings settings)
        {
            this.settings = settings;
        }

        internal void run()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            WebRequest request = WebRequest.Create(settings.getHostUrl());
            byte[] byteArray = Encoding.UTF8.GetBytes(requestString.ToString());
            request.Method = settings.getMethod().ToString();
            request.ContentLength = byteArray.Length;
            request.ContentType = contentType;
            request.Timeout = settings.getTimeout();

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Server responses 4xx+ can throw WebException
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                httpResponseCode = (int)webResponse.StatusCode;

                Stream responseStream = webResponse.GetResponseStream();

                StreamReader reader = new StreamReader(responseStream);
                responseString = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                webResponse.Close();
            }
            catch (WebException we)
            {
                httpResponseCode = (int)((HttpWebResponse)we.Response).StatusCode;
            }
            sw.Stop();
            duration = sw.ElapsedMilliseconds;
        }

        internal void setRequest(string request)
        {
            requestString = request;
        }

        internal string getResponse()
        {
            return responseString;
        }

        internal int getHttpCode()
        {
            return httpResponseCode;
        }

        internal float getDuration()
        {
            return duration;
        }

        internal string getRequest()
        {
            return requestString;
        }
    }
}
