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
            this.tabellautenti = new TableClient(section.GetValue<string>("APIString"), section.GetValue<string>("usertable"));
        
        }
        public string AddData(UserModel user)
        {
            TableEntity entity = new TableEntity();

            entity.PartitionKey = "users";
            entity.RowKey = user.Mail;
            entity["Name"] = user.Name;
            entity["Surname"] = user.Surname;
            entity["Age"] = user.Age;
            entity["Sex"] = user.Sex;
            entity["Country"] = user.Country;

            try
            {
                return this.tabellautenti.AddEntity(entity).Status.ToString();
            }catch (RequestFailedException ex)
            {
                return ex.Status.ToString();
                throw new RequestFailedException(ex.Message);
            }
        }
        public string UpdateData(UserModel user)
        {
            TableEntity entity = new TableEntity();

            entity.PartitionKey = "users";
            entity.RowKey = user.Mail;
            entity["Name"] = user.Name;
            entity["Surname"] = user.Surname;
            entity["Age"] = user.Age;
            entity["Sex"] = user.Sex;
            entity["Country"] = user.Country;

            try
            {
                return this.tabellautenti.UpdateEntity(entity,ETag.All,TableUpdateMode.Merge).Status.ToString();
            }catch(RequestFailedException ex)
            {
                return ex.Status.ToString();
                throw new RequestFailedException(ex.Message);
            }
        }
        public UserModel MapTableEntityToUserModel(TableEntity entity)
        {
            UserModel user = new UserModel();
            var values = entity.Keys;
            foreach (var key in values)
            {
                user[key] = entity[key];
            }
            return user;
        }
        public IEnumerable<UserModel> ReadData(string partitionkey,string rowkey)
        { 
                Pageable<TableEntity> queryResultsFilter = tabellautenti.Query<TableEntity>(filter: $"(PartitionKey eq '{partitionkey}') and (RowKey eq '{rowkey}')");
            
                foreach (TableEntity qEntity in queryResultsFilter)
                {
                    Console.WriteLine($"Name: {qEntity.GetString("Name")}, Surname: {qEntity.GetString("Surname")}, Age: {qEntity.GetString("Age")}, Sex: {qEntity.GetString("Sex")}, Country: {qEntity.GetString("Country")}");
                }
                return queryResultsFilter.Select(e => MapTableEntityToUserModel(e));
  
        }
        public string RemoveData(string partitionkey,string rowKey)
        {
            try
            {
                return this.tabellautenti.DeleteEntity(partitionkey, rowKey).Status.ToString();
            }catch(RequestFailedException ex)
            {
                return ex.Status.ToString();
                throw new RequestFailedException(ex.Message);
            }
        }
    }
}
