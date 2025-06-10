using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using InvoiceConsolidationApp.Models;
using Newtonsoft.Json;

namespace InvoiceConsolidationApp.Functions
{
    public static class Consolidation1bFunction
    {
        [FunctionName("Consolidation1b")]
        public static string Run([ActivityTrigger] string payload, ILogger log)
        {
            log.LogInformation("Consolidation1b IN (length {len}): {body}", payload.Length, payload);

            var invoicePayload = JsonConvert.DeserializeObject<InvoicePayloadDto>(payload);

            if (invoicePayload?.InvoiceDetails == null || invoicePayload.InvoiceDetails.Count == 0)
            {
                log.LogWarning("InvoiceDetails is null or empty.");
                return payload;
            }

            var monthlyLines = invoicePayload.InvoiceDetails
                .Where(line => string.Equals(line.BlngBillingFrequency, "monthly", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var groupedLines = monthlyLines
                .GroupBy(line => line.ContractLine)
                .Select(group =>
                {
                    var first = group.First();

                    return new InvoiceLineDto
                    {
                        InvoiceSfId = first.InvoiceSfId,
                        InvoiceNumber = first.InvoiceNumber,
                        InvoiceDate = first.InvoiceDate,
                        InvoiceLineSfId = first.InvoiceLineSfId,
                        LegacyBtCmf = first.LegacyBtCmf,
                        LegacyStCmf = first.LegacyStCmf,
                        BlngStartDate = first.BlngStartDate,
                        BlngEndDate = first.BlngEndDate,
                        ShipToAccountSfId = first.ShipToAccountSfId,
                        ShipToAccountNumber = first.ShipToAccountNumber,
                        ShipToAccountName = first.ShipToAccountName,
                        BlngBillingFrequency = first.BlngBillingFrequency,
                        OrderProductSfId = first.OrderProductSfId,
                        ContractLine = first.ContractLine,
                        ExternalOrderItemId = first.ExternalOrderItemId,
                        SbqqRequiredBy = first.SbqqRequiredBy,
                        ProductSfId = first.ProductSfId,
                        ProductCode = first.ProductCode,
                        Description = first.Description,
                        BlngChargeType = first.BlngChargeType,
                        BlngQuantity = group.Sum(x => x.BlngQuantity),
                        BlngSubtotal = group.Sum(x => x.BlngSubtotal),
                        BlngTaxAmount = group.Sum(x => x.BlngTaxAmount),
                        BlngTotalAmount = group.Sum(x => x.BlngTotalAmount)
                    };
                })
                .ToList();

            var result = new InvoicePayloadDto
            {
                ConsolidatedInvoice = invoicePayload.ConsolidatedInvoice,
                InvoiceDetails = groupedLines
            };

            var outputJson = JsonConvert.SerializeObject(result);
            log.LogInformation("Consolidation1b OUT: {out}", outputJson);
            return outputJson;
        }
    }
}
