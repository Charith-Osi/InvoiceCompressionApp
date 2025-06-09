using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace InvoiceConsolidationApp.Functions
{
    public static class Consolidation1bFunction
    {
        [FunctionName("Consolidation1b")]
        public static string Run([ActivityTrigger] string payload, ILogger log)
        {
            log.LogInformation("Consolidation1b stub IN (length {len}): {body}", payload.Length, payload);
            
            // pass-through stub:
            return payload;
        }
    }
}
