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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post","view", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a {req.Method}.");
            string name = req.Query["Name"];
            string surname = req.Query["Surname"];
            string age = req.Query["Age"];
            string country = req.Query["Country"];
            string sex = req.Query["Sex"];
            string mail = req.Query["Mail"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.Name;
            surname = surname ?? data?.Surname;
            age = age ?? data?.Age;
            country = country ?? data?.Country;
            sex = sex ?? data?.Sex;    
            mail= mail ?? data?.Mail;
            

            if(req.Method == "GET")
            {
                log.LogInformation("C# HTTP trigger function processed a get.");
                this.service.readCountry(country);
            }
            else if(req.Method == "VIEW")
            {
                this.service.readAll();
            }
            else if (req.Method == "POST")
            {
                var model = new UserModel();
                model.Country = country;
                model.GUID = Guid.NewGuid().ToString();
                model.Name = name;
                model.Surname = surname;
                model.Age = age;
                model.Sex = sex;
                this.service.UpsertExpandableData(model);
                log.LogInformation("C# HTTP trigger function processed a post.");
            }
            
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name},{surname},{age} from {country}. This HTTP triggered function executed successfully."; 
            

            return new OkObjectResult(responseMessage);
        }
    }
}
