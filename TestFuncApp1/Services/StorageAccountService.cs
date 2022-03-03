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
        public void AddExpandableData(UserModel user)
        {
            TableEntity entity = new TableEntity();

            entity.PartitionKey = user.Country;
            entity.RowKey = user.Mail;
            entity["Name"] = user.Name;
            entity["Surname"] = user.Surname;
            entity["Age"] = user.Age;
            entity["Sex"] = user.Sex;
            entity["Country"] = user.Country;


            this.tabellautenti.AddEntity(entity);
        }
        public void UpsertExpandableData(UserModel user)
        {
            TableEntity entity = new TableEntity();

            entity.PartitionKey = user.Country;
            entity.RowKey = user.Mail;
            entity["Name"] = user.Name;
            entity["Surname"] = user.Surname;
            entity["Age"] = user.Age;
            entity["Sex"] = user.Sex;
            entity["Country"] = user.Country;


            this.tabellautenti.UpsertEntity(entity);
        }
        public void readAll()
        {
            Pageable<TableEntity> queryResultsFilter = tabellautenti.Query<TableEntity>();

            foreach (TableEntity qEntity in queryResultsFilter)
            {
                Console.WriteLine($"{qEntity.GetString("Name")}: {qEntity.GetString("Surname")}: {qEntity.GetString("Age")}: {qEntity.GetString("Sex")}");
            }

            Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");
        }

        public void readRowKey(string rowkey)
        {
            Pageable<TableEntity> queryResultsFilter = tabellautenti.Query<TableEntity>(filter: $"RowKey eq '{rowkey}'");

            foreach (TableEntity qEntity in queryResultsFilter)
            {
                Console.WriteLine($"Name: {qEntity.GetString("Name")}, Surname: {qEntity.GetString("Surname")}, Age: {qEntity.GetString("Age")}, Sex: {qEntity.GetString("Sex")}, Country: {qEntity.GetString("Country")}");
            }

            Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");
        }
        public void RemoveEntity(string partitionKey, string rowKey)
        {
            this.tabellautenti.DeleteEntity(partitionKey, rowKey);
        }
    }
}
