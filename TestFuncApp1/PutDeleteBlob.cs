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
    public class PutDeleteBlob
    {
        readonly BlobServices service;
        public PutDeleteBlob(BlobServices service)
        {
            this.service = service;
        }
        [FunctionName("PutDeleteBlob")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, Route = "users/{iduser}/attachments/{id}")] HttpRequest req,
            string iduser, string id,
            ILogger log)
        {
            string responseMessage;
            log.LogInformation($"C# HTTP trigger function processed a {req.Method}.");
            if (req.Method == "PUT")
            {

                responseMessage = "BLOBBING BOBBY BLOB";
            }
            else if(req.Method == "DELETE")
            {
                responseMessage = "BLOBBING BOBBY BLOB";
            }
            else
            {
                responseMessage = "BAD REQUEST STATUS CODE : 400";
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
