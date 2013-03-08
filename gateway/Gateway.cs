using System;
using System.Text;
using Mes.Core;
using System.Collections;

namespace Mes.Gateway
{
    ///<summary>An instance of the MeS Payment Gateway.</summary>
    public class Gateway
    {
        private Http http;
        private GatewaySettings settings;

        public Gateway(GatewaySettings settings)
        {
            http = new Http(settings);
            this.settings = settings;
        }

         ///<summary>
         ///Sends the given request to the gateway, and returns a response object.
         ///</summary>
        public GatewayResponse run(GatewayRequest request)
        {
            http.setRequest(parseRequest(request));
            if (settings.isVerbose())
                System.Console.WriteLine("Sending request: "+http.getRequest());
            http.run();
            if (settings.isVerbose())
                System.Console.WriteLine("Response: (HTTP "+http.getHttpCode()+" - "+http.getDuration()+"ms) " + http.getResponse());
            return parseResponse();
        }

        protected string parseRequest(GatewayRequest request)
        {
            StringBuilder requestString = new StringBuilder();
            requestString.Append("profile_id=").Append(settings.getProfileId());
            requestString.Append("&profile_key=").Append(settings.getProfileKey());

            string typeCode = null;
            switch (request.getType())
            {
                case GatewayRequest.TransactionType.SALE: typeCode = "D";  break;
                case GatewayRequest.TransactionType.PREAUTH: typeCode = "P"; break;
                case GatewayRequest.TransactionType.REAUTH: typeCode = "J"; break;
                case GatewayRequest.TransactionType.OFFLINE: typeCode = "O"; break;
                case GatewayRequest.TransactionType.SETTLE: typeCode = "S"; break;
                case GatewayRequest.TransactionType.VOID: typeCode = "V"; break;
                case GatewayRequest.TransactionType.CREDIT: typeCode = "C"; break;
                case GatewayRequest.TransactionType.REFUND: typeCode = "U"; break;
                case GatewayRequest.TransactionType.VERIFY: typeCode = "A"; break;
                case GatewayRequest.TransactionType.TOKENIZE: typeCode = "T"; break;
                case GatewayRequest.TransactionType.DETOKENIZE: typeCode = "X"; break;
                case GatewayRequest.TransactionType.BATCHCLOSE: typeCode = "Z"; break;
                default:
                    throw new MesRuntimeException("Transaction type unsupported: "+typeCode);
            }

            requestString.Append("&transaction_type=").Append(typeCode);
            
            foreach (DictionaryEntry entry in request.requestTable)
                requestString.Append("&").Append(entry.Key).Append("=").Append(entry.Value);
            return requestString.ToString();
        }

        protected GatewayResponse parseResponse()
        {
            GatewayResponse response = new GatewayResponse(http.getHttpCode(), http.getResponse(), http.getDuration());
            if (http.getHttpCode() == 200)
            {
                string[] tokens = http.getResponse().Split('&');

                for (int i = 0; i < tokens.Length; i++)
                {
                    string[] namePairs = tokens[i].Split('=');
                    response.addResponseValue(namePairs[0], namePairs[1]);
                }
            }
            return response;
        }
    }
}
