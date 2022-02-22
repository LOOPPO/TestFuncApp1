using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using System;
using Azure;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFuncApp1.Services
{
    public class StorageAccountService
    {
        readonly TableClient tabellautenti;
        public StorageAccountService(IConfiguration config)
        {
            var section = config.GetSection("ConnectionStrings");
            this.tabellautenti = new TableClient(section.GetValue<string>("CosmosTableApi"), section.GetValue<string>("usertable"));
        
        }
        public void UpsertExpandableData(UserModel user)
        {
            TableEntity entity = new TableEntity();

            entity.PartitionKey = user.UserName;
            entity.RowKey = user.UserSurname;

            this.tabellautenti.UpsertEntity(entity);
        }

        public void RemoveUser(string partitionKey, string rowKey)
        {
            this.tabellautenti.DeleteEntity(partitionKey, rowKey);
        }
    }
}
