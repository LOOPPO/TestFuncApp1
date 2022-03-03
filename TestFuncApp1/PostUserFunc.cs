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
    public class PostUserFunc
    {
        readonly StorageAccountService service;

        public PostUserFunc(StorageAccountService service) {
           this.service=service;
        }

        [FunctionName("PostUserFunc")]
        public async Task<IActionResult> RunGet(
            [HttpTrigger(AuthorizationLevel.Function,"put", "post", Route = null)] HttpRequest req,
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
            mail = mail ?? data?.Mail;
            name = name ?? data?.Name;
            surname = surname ?? data?.Surname;
            age = age ?? data?.Age;
            country = country ?? data?.Country;
            sex = sex ?? data?.Sex;    
            
            if (req.Method == "POST")
            {
                var model = new UserModel();
                model.Country = country;
                model.GUID = Guid.NewGuid().ToString();
                model.Name = name;
                model.Surname = surname;
                model.Age = age;
                model.Sex = sex;
                model.Mail = mail;
                this.service.AddExpandableData(model);
                log.LogInformation("C# HTTP trigger function processed a post.");
            }
            else if(req.Method == "PUT")
            {
                var model = new UserModel();
                model.Country = country;
                model.GUID = Guid.NewGuid().ToString();
                model.Name = name;
                model.Surname = surname;
                model.Age = age;
                model.Sex = sex;
                model.Mail = mail;
                this.service.UpsertExpandableData(model);
                log.LogInformation("C# HTTP trigger function processed a put.");
            }
            
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}, {surname}, {age} from {country}. This HTTP triggered function executed successfully."; 
            

            return new OkObjectResult(responseMessage);
        }
    }
}
