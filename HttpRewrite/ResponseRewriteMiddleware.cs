using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace HttpRewrite
{
    public class ResponseRewriteMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var logger = context.GetLogger<ResponseRewriteMiddleware>();
            logger.LogInformation($"Running {nameof(ResponseRewriteMiddleware)}...");

            var req = await context.GetHttpRequestDataAsync();

            var res = req.CreateResponse(HttpStatusCode.Unauthorized);
            await res.WriteAsJsonAsync(new { Status = "Unauthorized", Message = "Unauthorized access." }, res.StatusCode);

            context.GetInvocationResult().Value = res;
            return;

            // throw new UnauthorizedAccessException("Unauthorized access.");
            // await next(context);
        }
    }
}
