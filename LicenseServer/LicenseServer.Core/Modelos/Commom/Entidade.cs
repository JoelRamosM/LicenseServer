using MongoDB.Bson.Serialization.Attributes;

namespace LicenseServer.Core.Modelos.Commom
{
    public class Entidade
    {
        [BsonId]
        public string Id { get; set; }
    }
}
