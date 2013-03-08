using System;
using System.Text;
using Mes.Core;

namespace Mes.Gateway
{
    ///<summary>A request object for use with the Payment Gateway.</summary>
    public class GatewayRequest : RequestObject
    {
        public enum TransactionType { SALE, PREAUTH, SETTLE, REAUTH, OFFLINE, VOID, CREDIT, REFUND, VERIFY, TOKENIZE, DETOKENIZE, BATCHCLOSE }

        private TransactionType type;

        ///<summary>The type of transaction being sent to the gateway.</summary>
        ///Available transaction types are:
        ///<code>
        /// Sale: An authorization followed by an automatic capture.
        /// Preauth: An authorization only.
        /// Settle: Captures a preauthorization for funding.
        /// Reauth: A clone transaction (only valid within 28 days from the original authorization date).
        /// Offline: A forced-entry transaction using an approval code.
        /// Void: Cancels a transaction authorized same-day and attempts to reverse it to the issuer.
        /// Credit: An unmatched credit, requiring a full card number. This function is disabled at MeS by default.
        /// Refund: Using an authorization's transaction id, a void is performed (if same-day) or credit is given (if the transaction was settled).
        /// Verify: Validates the card account is open. Not supported by all issuers.
        /// Tokenize: The transaction id returned by this request may be sent in the card_id field, replacing any card number field.
        /// Detokenize: Removes the referenced token from the database.
        /// Batchclose: Attempts to settle the current batch. Must a supply batch_number.
        ///</code>
        public GatewayRequest(TransactionType type) : base()
        {
            this.type = type;
        }

        ///<summary>Sets the requested amount.</summary>
        public void setAmount(string amount) {
            addRequestField("transaction_amount", amount);
        }

        ///<summary>Sets the tax amount. This is NOT added to the total, and is for reporting purposes only.</summary>
        public void setTaxAmount(string amount)
        {
            addRequestField("tax_amount", amount);
        }

        ///<summary>Sets the card number and expiry date.</summary>
        public void setCardData(string cardNumber, string expdate)
        {
            addRequestField("card_number", cardNumber);
            addRequestField("card_exp_date", expdate);
        }

        ///<summary>Sets the card holder billing address. Must be set for preferred interchange.</summary>
        public void setBillingAddress(string streetAddress, string zip)
        {
            addRequestField("cardholder_street_address", streetAddress);
            addRequestField("cardholder_zip", zip);
        }

        ///<summary>Sets the token, and expiry date.</summary>
        public void setTokenData(string token, string expdate)
        {
            addRequestField("card_id", token);
            addRequestField("card_exp_date", expdate);
        }

        ///<summary>Sets the transaction ID required by some transaction types.</summary>
        public void setTranId(string id)
        {
            addRequestField("transaction_id", id);
        }

        ///<summary>Sets the custom reference number, which populates in reporting. Max 96 char (additional is truncated)</summary>
        public void setClientReference(string reference)
        {
            if(reference.Length > 96)
                reference = reference.Substring(0, 96);
            addRequestField("client_reference_number", reference);
        }

        ///<summary>Sets the transaction's invoice number, which populates in reporting and on merchant statements.  Must be set for preferred interchange. Max 17 char (additional is truncated).</summary>
        public void setInvoice(string invoice)
        {
            if (invoice.Length > 17)
                invoice = invoice.Substring(0, 17);
            addRequestField("invoice_number", invoice);
        }

        ///<summary>Sets the Authorization Code. Only used with the Offline transaction type.</summary>
        public void setAuthCode(string authCode)
        {
            addRequestField("auth_code", authCode);
        }

        ///<summary>Sets the 3D Secure Verified by Visa data obtained from a 3rd party MPI provider.</summary>
        public void set3DVBVData(string xid, string cavv)
        {
            addRequestField("xid", xid);
            addRequestField("cavv", cavv);
        }

        ///<summary>Sets the 3D Secure MasterCard Secure Code data obtained from a 3rd party MPI provider.</summary>
        public void set3DMSCData(string collectionIndicator, string authData)
        {
            addRequestField("ucaf_collection_ind",collectionIndicator);
            addRequestField("ucaf_auth_data", authData);
        }

        ///<summary>Sets the ISO Currency Code for the transaction. 3-digit numeric or alpha codes are both accepted.</summary>
        public void setCurrencyCode(string code)
        {
            addRequestField("currency_code", code);
        }

        ///<summary>Returns the current transaction type.</summary>
        public TransactionType getType()
        {
            return type;
        }
    }
}
