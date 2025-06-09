// Models/ConsolidatedInvoiceDto.cs
using System;
using Newtonsoft.Json;

namespace InvoiceConsolidationApp.Models
{
    public class ConsolidatedInvoiceDto
    {
        [JsonProperty("SF_Id")]
        public string SfId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("FIN_Invoice_Date__c")]
        public DateTime FinInvoiceDate { get; set; }

        [JsonProperty("Total_Amount__c")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("FIN_Due_Date__c")]
        public DateTime FinDueDate { get; set; }

        [JsonProperty("Bill_To_Account_SF_Id")]
        public string BillToAccountSfId { get; set; }

        [JsonProperty("Bill_To_AccountNumber")]
        public string BillToAccountNumber { get; set; }

        [JsonProperty("Bill_To_AccountName")]
        public string BillToAccountName { get; set; }

        [JsonProperty("BillingCity")]
        public string BillingCity { get; set; }

        [JsonProperty("BillingState")]
        public string BillingState { get; set; }

        [JsonProperty("BillingPostalCode")]
        public string BillingPostalCode { get; set; }

        [JsonProperty("BillingStreet")]
        public string BillingStreet { get; set; }

        [JsonProperty("BillingCountry")]
        public string BillingCountry { get; set; }

        [JsonProperty("FIN_Langauge__c")]
        public string FinLanguage { get; set; }

        [JsonProperty("FIN_Invoice_Delivery_Method__c")]
        public string FinInvoiceDeliveryMethod { get; set; }
    }
}
