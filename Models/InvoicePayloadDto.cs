// Models/InvoicePayloadDto.cs
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InvoiceConsolidationApp.Models
{
    public class InvoicePayloadDto
    {
        [JsonProperty("Consolidated_Invoice")]
        public ConsolidatedInvoiceDto ConsolidatedInvoice { get; set; }

        [JsonProperty("Invoice_Details")]
        public List<InvoiceLineDto> InvoiceDetails { get; set; }
    }
}
