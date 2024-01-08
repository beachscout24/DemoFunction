using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DemoFunction
{
    public class DemoFunction
    {
        private readonly ILogger _logger;

        public DemoFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DemoFunction>();
        }

        [Function("DemoFunction")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] 
            HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.WriteString("Hello Function");
            return response;
        }
    }
}
