using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using InvoiceConsolidationApp.Models;
using Newtonsoft.Json;

namespace InvoiceConsolidationApp.Functions
{
    public static class InvoiceHistoryFunction
    {
        [FunctionName("InvoiceHistory")]
        public static string Run([ActivityTrigger] string payload, ILogger log)
        {
            log.LogInformation("InvoiceHistory IN (length {len}): {body}", payload.Length, payload);

            var invoicePayload = JsonConvert.DeserializeObject<InvoicePayloadDto>(payload);

            // Your business logic here — currently just pass through
            var outputJson = JsonConvert.SerializeObject(invoicePayload);

            log.LogInformation("InvoiceHistory OUT: {out}", outputJson);
            return outputJson;
        }
    }
}
