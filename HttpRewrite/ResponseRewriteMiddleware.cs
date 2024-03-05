using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using System.Net;

namespace HttpRewrite
{
    public class ResponseRewriteMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var req = await context.GetHttpRequestDataAsync();

            var res = req.CreateResponse(HttpStatusCode.Unauthorized);

            var invocationResult = context.GetInvocationResult();

            invocationResult.Value = res;
        }
    }
}
