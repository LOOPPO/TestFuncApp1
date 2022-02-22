using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestFuncApp1.Services;

namespace TestFuncApp1
{
    public class Function1
    {
        readonly StorageAccountService service;

        public Function1(StorageAccountService service) {
           this.service=service;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> RunGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a {req.Method}.");
            if (req.Method == "GET")
            {
                log.LogInformation("C# HTTP trigger function processed a get.");
                // this.service.saveuser(String.Empty);
            }
            else if (req.Method == "POST")
            {
                var model = new UserModel();
                model.UserName = "PIPPO";
                model.UserSurname = "BAUDO";
                this.service.UpsertExpandableData(model);
                log.LogInformation("C# HTTP trigger function processed a post.");
            }
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully."; 
            

            return new OkObjectResult(responseMessage);
        }
    }
}
