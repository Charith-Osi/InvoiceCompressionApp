// client/Client.cs

using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace InvoiceProcessorCSharp.Client
{
    public static class Client
    {
        [FunctionName("ClientFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            string requestBody = await req.ReadAsStringAsync();
            log.LogInformation("Client trigger received payload (length {len})", requestBody.Length);

            string instanceId = await starter.StartNewAsync("Orchestrator", null, requestBody);
            log.LogInformation("Started orchestration with Instance ID = {id}", instanceId);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
