using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFuncApp1.Services
{
    public class BlobServices
    {
        private BlobContainerClient container;
        public BlobServices(IConfiguration config)
        {
            var section = config.GetSection("APIString");
            this.container = new BlobContainerClient(section.GetValue<string>("APIString"),null);
        }
        
        public string AddBlob()
        {

            return null;
        }
        public string UpdateBlob()
        {
            return null;
        }
        public string ReadBlob()
        {
            return null;
        }
        public string DeleteBlob()
        {
            return null;
        }






    }
}
