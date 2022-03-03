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
    public class GetUserFunc
    {
        readonly StorageAccountService service;

        public GetUserFunc(StorageAccountService service)
        {
            this.service = service;
        }
        [FunctionName("GetUserFunc")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get","delete", Route = "{users}/{iduser}")] HttpRequest req,string users,
            string iduser,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if(req.Method == "GET")
            {
                log.LogInformation("C# HTTP trigger function processed a get.");
                this.service.readRowKey(iduser);
            }
            else if(req.Method == "DELETE")
            {
                this.service.RemoveEntity(users, iduser);
            }

            string responseMessage = $"Hello, Admin. This HTTP triggered function executed successfully.";
            return new OkObjectResult(responseMessage);
        }
    }
}
