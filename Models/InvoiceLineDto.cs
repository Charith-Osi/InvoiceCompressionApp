// Models/InvoiceLineDto.cs
using System;
using Newtonsoft.Json;

namespace InvoiceConsolidationApp.Models
{
    public class InvoiceLineDto
    {
        [JsonProperty("Invoice_SF_Id")]
        public string InvoiceSfId { get; set; }

        [JsonProperty("Invoice_Number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("Invoice_Date")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("Invoice_Line_SF_Id")]
        public string InvoiceLineSfId { get; set; }

        [JsonProperty("Legacy_BT_CMF__c")]
        public string LegacyBtCmf { get; set; }

        [JsonProperty("Legacy_ST_CMF__c")]
        public string LegacyStCmf { get; set; }

        [JsonProperty("blng__StartDate__c")]
        public DateTime BlngStartDate { get; set; }

        [JsonProperty("blng__EndDate__c")]
        public DateTime BlngEndDate { get; set; }

        [JsonProperty("Ship_To_Account_SF_Id")]
        public string ShipToAccountSfId { get; set; }

        [JsonProperty("Ship_To_AccountNumber")]
        public string ShipToAccountNumber { get; set; }

        [JsonProperty("Ship_To_AccountName")]
        public string ShipToAccountName { get; set; }

        [JsonProperty("blng__BillingFrequency__c")]
        public string BlngBillingFrequency { get; set; }

        [JsonProperty("Order_Product_SF_ID")]
        public string OrderProductSfId { get; set; }

        [JsonProperty("Contract_Line__c")]
        public string ContractLine { get; set; }

        [JsonProperty("External_Order_Item_ID__c")]
        public string ExternalOrderItemId { get; set; }

        [JsonProperty("SBQQ__RequiredBy__c")]
        public string SbqqRequiredBy { get; set; }

        [JsonProperty("Product_SF_ID")]
        public string ProductSfId { get; set; }

        [JsonProperty("ProductCode")]
        public string ProductCode { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("blng__ChargeType__c")]
        public string BlngChargeType { get; set; }

        [JsonProperty("blng__Quantity__c")]
        public decimal BlngQuantity { get; set; }

        [JsonProperty("blng__Subtotal__c")]
        public decimal BlngSubtotal { get; set; }

        [JsonProperty("blng__TaxAmount__c")]
        public decimal BlngTaxAmount { get; set; }

        [JsonProperty("blng__TotalAmount__c")]
        public decimal BlngTotalAmount { get; set; }
    }
}
