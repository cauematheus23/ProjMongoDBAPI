using MongoDB.Driver;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Utils;

namespace ProjMongoDBAPI.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;
     

        public CustomerService(IDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customers = database.GetCollection<Customer>(settings.CustomerCollectionName);
        }
        public List<Customer> GetAll() =>
            _customers.Find(customer => true).ToList();

        public Customer GetById(string id) =>
            _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefault();

        public Customer Create(Customer customer)
        {
            _customers.InsertOne(customer);
            return customer;
        }
        public Customer Update(Customer customer) 
        { _customers.ReplaceOne(c => c.Id == customer.Id, customer); 
            return customer; 
        }

    }
}
