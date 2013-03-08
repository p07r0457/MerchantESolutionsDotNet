using System;
using System.Text;
using Mes.Core;

namespace Mes.Reporting
{
    ///<summary>A request object for use with the Payment Gateway.</summary>
    public class ReportingRequest : RequestObject
    {
        public enum ReportType { BATCH, SETTLEMENT, DEPOSIT, RECONSCILE, CHARGEBACKADJUSTMENTS, CHARGEBACKPRENOT, RETRIEVAL, INTERCHANGE, CUSTOM, FXBATCH, ITLCHARGEBACK, ITLRETRIEVAL, FXINTERCHANGE, INTLDETAILS, AUTHLOG, GATEWAYREQUESTLOG, ACHSETTLEMENT, ACHRETURN, TRIDENTBATCH }
        public enum ReportMode { SUMMARY, DETAIL }
        public enum ResponseFormat { CSV, XML1, XML2 }

        private ReportType type;
        private ReportMode mode;

        ///<summary>The type of report being requested.</summary>
        public ReportingRequest(ReportType type, ReportMode mode)
            : base()
        {
            this.type = type;
            this.mode = mode;
        }

        ///<summary>Sets the starting date of the report.</summary>
        public ReportingRequest setStartDate(string mm, string dd, string yyyy)
        {
            addRequestField("reportDateBegin", mm + "%2F" + dd + "%2F" + yyyy);
            return this;
        }

        ///<summary>Sets the ending date of the report.</summary>
        public ReportingRequest setEndDate(string mm, string dd, string yyyy)
        {
            addRequestField("reportDateEnd", mm + "%2F" + dd + "%2F" + yyyy);
            return this;
        }

        ///<summary>Sets the MID or association to use.</summary>
        public ReportingRequest setNode(string nodeId)
        {
            addRequestField("nodeId", nodeId);
            return this;
        }

        ///<summary>Requests the Trident Transaction ID to be in the response data.</summary>
        public ReportingRequest includeTranId(bool inc)
        {
            addRequestField("includeTridentTranId", inc);
            return this;
        }

        ///<summary>Requests the Purchase ID (Invoice Number) to be in the response data.</summary>
        public ReportingRequest includePurchId(bool inc)
        {
            addRequestField("includePurchaseId", inc);
            return this;
        }

        ///<summary>Requests the Client Reference Number to be in the response data.</summary>
        public ReportingRequest includeClientRefNum(bool inc)
        {
            addRequestField("includeClientRefNum", inc);
            return this;
        }

        ///<summary>Sets the profile ID, only for use with the Payment Gateway Request Log report.</summary>
        public ReportingRequest setProfileId(string profileId)
        {
            addRequestField("profileId", profileId);
            return this;
        }

        ///<summary>Sets the custom query ID, only for use with the Custom Query Report.</summary>
        public ReportingRequest setQueryId(string profileId)
        {
            addRequestField("profileId", profileId);
            return this;
        }

        ///<summary>Sets the response data type.</summary>
        public ReportingRequest setResponseFormat(ResponseFormat format)
        {
            switch (format)
            {
                case ResponseFormat.XML1:
                    addRequestField("xmlEncoding", "0");
                    break;
                case ResponseFormat.XML2:
                    addRequestField("xmlEncoding", "1");
                    break;
                case ResponseFormat.CSV:
                default:
                    removeField("xmlEncoding");
                    break;
            }
            return this;
        }

        ///<summary>Returns the current report type.</summary>
        public ReportType getType()
        {
            return type;
        }

        ///<summary>Returns the current report mode.</summary>
        public ReportMode getMode()
        {
            return mode;
        }
    }
}
