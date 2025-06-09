using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using InvoiceConsolidationApp.Models;
using Newtonsoft.Json;

namespace InvoiceConsolidationApp.Functions
{
    public static class ShippingRuleFunction
    {
        [FunctionName("ShippingRuleFunction")]
        public static string Run([ActivityTrigger] string payload, ILogger log)
        {
            log.LogInformation("ShippingRuleFunction IN (length {len}): {body}", payload.Length, payload);

            var invoicePayload = JsonConvert.DeserializeObject<InvoicePayloadDto>(payload);

            // Just return as is with logging
            var outputJson = JsonConvert.SerializeObject(invoicePayload);

            log.LogInformation("ShippingRuleFunction OUT: {out}", outputJson);
            return outputJson;
        }
    }
}
