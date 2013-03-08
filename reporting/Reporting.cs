using System;
using System.Text;
using Mes.Core;
using System.Collections;

namespace Mes.Reporting
{
    ///<summary>An instance of the MeS Payment Gateway.</summary>
    public class Reporting
    {
        private Http http;
        private ReportingSettings settings;
        private bool success;

        public Reporting(ReportingSettings settings)
        {
            http = new Http(settings);
            this.settings = settings;
        }

         ///<summary>
         ///Sends the given request to the gateway, and returns a response object.
         ///</summary>
        public string run(ReportingRequest request)
        {
            success = false; // Assume false
            http.setRequest(parseRequest(request));
            if (settings.isVerbose())
                System.Console.WriteLine("Sending request: "+http.getRequest());
            http.run();

            string resp = parseResponse();
            if (settings.isVerbose())
                System.Console.WriteLine("Response: (HTTP "+http.getHttpCode()+" - "+http.getDuration()+"ms - Request Succeeded: "+success+") " + resp);
            return resp;
        }

        protected string parseRequest(ReportingRequest request)
        {
            StringBuilder requestString = new StringBuilder();
            requestString.Append("userId=").Append(settings.getUserName());
            requestString.Append("&userPass=").Append(settings.getUserPass());

            string reportId = null;
            switch (request.getType())
            {
                case ReportingRequest.ReportType.BATCH: reportId = "1"; break;
                case ReportingRequest.ReportType.SETTLEMENT: reportId = "2"; break;
                case ReportingRequest.ReportType.DEPOSIT: reportId = "3"; break;
                case ReportingRequest.ReportType.RECONSCILE: reportId = "4"; break;
                case ReportingRequest.ReportType.CHARGEBACKADJUSTMENTS: reportId = "5"; break;
                case ReportingRequest.ReportType.CHARGEBACKPRENOT: reportId = "6"; break;
                case ReportingRequest.ReportType.RETRIEVAL: reportId = "7"; break;
                case ReportingRequest.ReportType.INTERCHANGE: reportId = "8"; break;
                case ReportingRequest.ReportType.CUSTOM: reportId = "9"; break;
                case ReportingRequest.ReportType.FXBATCH: reportId = "10"; break;
                case ReportingRequest.ReportType.ITLCHARGEBACK: reportId = "11"; break;
                case ReportingRequest.ReportType.ITLRETRIEVAL: reportId = "12"; break;
                case ReportingRequest.ReportType.FXINTERCHANGE: reportId = "13"; break;
                case ReportingRequest.ReportType.INTLDETAILS: reportId = "14"; break;
                case ReportingRequest.ReportType.AUTHLOG: reportId = "15"; break;
                case ReportingRequest.ReportType.GATEWAYREQUESTLOG: reportId = "16"; break;
                case ReportingRequest.ReportType.ACHSETTLEMENT: reportId = "17"; break;
                case ReportingRequest.ReportType.ACHRETURN: reportId = "18"; break;
                case ReportingRequest.ReportType.TRIDENTBATCH: reportId = "19"; break;
                default:
                    throw new MesRuntimeException("Report type unsupported: "+request.getType());
            }

            requestString.Append("&dsReportId=").Append(reportId);

            string reportMode = null;
            switch (request.getMode())
            {
                case ReportingRequest.ReportMode.SUMMARY: reportMode = "0"; break;
                case ReportingRequest.ReportMode.DETAIL: reportMode = "1"; break;
                default:
                    throw new MesRuntimeException("Report mode unsupported: " + request.getType());
            }

            requestString.Append("&reportType=").Append(reportMode);
            
            foreach (DictionaryEntry entry in request.requestTable)
                requestString.Append("&").Append(entry.Key).Append("=").Append(entry.Value);
            return requestString.ToString();
        }

        protected string parseResponse()
        {
            // Bad conditions still return http code 200, with no other way to determine the request did not complete. We will have to result to ugly substrings.
            string s = http.getResponse();
            if (s.Substring(8, 6).Equals("<html>"))
            {
                if(s.Substring(227, 19).Equals("Insufficient rights"))
                    return "Insufficient Rights";
                if (s.Substring(227, 21).Equals("Invalid user/password"))
                    return "Invalid username or userpass";
                else
                    return "Report Request Failed";
            }
            success = true;
            return http.getResponse();
        }

        public bool wasSuccessful()
        {
            return success;
        }
    }
}
