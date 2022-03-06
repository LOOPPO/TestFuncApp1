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
            [HttpTrigger(AuthorizationLevel.Function, Route = "users/{iduser}")] HttpRequest req,
            string iduser,
            ILogger log)
        {
            string responseMessage;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if(req.Method == "GET")
            {
                this.service.ReadData("users", iduser);
                responseMessage = $"Name : , Surname : ";
                responseMessage = "STATUS CODE : 204";
                    
            }
            else if(req.Method == "DELETE")
            {
                string response = this.service.RemoveData("users", iduser);
                log.LogInformation(response);
                responseMessage = $"STATUS CODE : {response}";

            }
            else
            {
                responseMessage = $"BAD REQUEST STATUS CODE : 400";
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
