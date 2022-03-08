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
using Azure.Storage.Blobs;

namespace TestFuncApp1
{
    public class GetPostBlob
    {
        private BlobServices service;
        public GetPostBlob(BlobServices service)
        {
            this.service = service;
        }

        [FunctionName("GetPostBlob")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, Route = "users/{iduser}/attachments")] HttpRequest req,
            string iduser,
            ILogger log)
        {

            string responseMessage;
            log.LogInformation($"C# HTTP trigger function processed a {req.Method}.");
            if (req.Method == "POST")
            {

                responseMessage = "BLOBBING BOBBY BLOB";
            }else if (req.Method == "GET")
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
