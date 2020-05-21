using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Messaging.RabbitMQ;

namespace Messaging.RabbitMQ.Webhook
{
    public static class QueueWriter
    {
        [FunctionName("QueueWriter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "QueueWriter/{queue}")] HttpRequest req,
            string queueName,
            ILogger logger)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var writer = ConnectionInfo.Default //ToDo: pass actual config
                .CreateChannelFactory()
                .CreateWriter<string>(queueName);

            var body = await req.Body
                .ReadAllTextAsync()
                .ConfigureAwait(false);

            writer.Write(body);

            ((IDisposable)writer)?.Dispose();

            return new OkObjectResult($"Successfully wrote to queue.");
        }
    }
}
