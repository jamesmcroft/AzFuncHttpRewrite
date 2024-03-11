using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace HttpRewrite
{
    public class TestFunction(ILogger<TestFunction> logger)
    {
        [Function(nameof(TestFunction))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            logger.LogInformation($"Running {nameof(TestFunction)}...");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(new { Status = "OK", Message = "Should not see this message due to middleware!" });
            return res;
        }
    }
}
