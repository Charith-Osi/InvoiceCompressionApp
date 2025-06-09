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
    public static class Consolidation1aFunction
    {
        [FunctionName("Consolidation1a")]
        public static string Run([ActivityTrigger] string payload, ILogger log)
        {
            log.LogInformation("Consolidation1a IN (length {len}): {body}", payload.Length, payload);

            var invoicePayload = JsonConvert.DeserializeObject<InvoicePayloadDto>(payload);

            if (invoicePayload?.InvoiceDetails == null)
            {
                log.LogWarning("InvoiceDetails is null or empty.");
                return payload; // or return an empty result or error
            }

            // Find parent lines where SBQQ__RequiredBy__c is null or empty
            var parents = invoicePayload.InvoiceDetails
                .Where(line => string.IsNullOrEmpty(line.SbqqRequiredBy))
                .ToList();

            // Sum amounts grouped by SBQQ__RequiredBy__c
            var sums = invoicePayload.InvoiceDetails
                .Where(line => !string.IsNullOrEmpty(line.SbqqRequiredBy))
                .GroupBy(line => line.SbqqRequiredBy)
                .ToDictionary(g => g.Key, g => g.Sum(line => line.BlngTotalAmount));

            // Update parent's blng__TotalAmount__c if externalId found in sums
            foreach (var parent in parents)
            {
                var externalId = parent.OrderProductSfId;
                if (!string.IsNullOrEmpty(externalId) && sums.ContainsKey(externalId))
                {
                    parent.BlngTotalAmount = sums[externalId];
                }
            }

            // Build new payload to return (only parents in InvoiceDetails)
            var result = new InvoicePayloadDto
            {
                ConsolidatedInvoice = invoicePayload.ConsolidatedInvoice,
                InvoiceDetails = parents
            };

            var outputJson = JsonConvert.SerializeObject(result);
            log.LogInformation("Consolidation1a OUT: {out}", outputJson);
            return outputJson;
        }
    }
}
