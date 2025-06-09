using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace InvoiceConsolidationApp.Functions
{
    public static class Orchestrator
    {
        [FunctionName("Orchestrator")]
        public static async Task<string> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            string inputJson = context.GetInput<string>();

            var afterDataLoad = await context.CallActivityAsync<string>("DataLoadFunction", inputJson);
            var afterConsolidation1a = await context.CallActivityAsync<string>("Consolidation1a", afterDataLoad);
            var afterConsolidation1b = await context.CallActivityAsync<string>("Consolidation1b", afterConsolidation1a);
            var afterInvoiceHistory = await context.CallActivityAsync<string>("InvoiceHistory", afterConsolidation1b);
            var afterShippingRule = await context.CallActivityAsync<string>("ShippingRuleFunction", afterInvoiceHistory);

            return afterShippingRule;
        }
    }
}
