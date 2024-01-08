using System.Net;
using System.Text.Json;
using DemoFunction.Models;
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
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] 
            HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var data = await JsonSerializer.DeserializeAsync<PersonModel>(req.Body);

            if(data is null)
            {
                _logger.LogError("Invalid request data");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.WriteString($"Hello {data.FirstName} {data.LastName}");
            return response;
        }
    }
}
