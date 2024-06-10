namespace ProjMongoDBAPI.Utils
{
    public interface IDataBaseSettings
    {
        string CustomerCollectionName { get; set; }
        string AddressCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
