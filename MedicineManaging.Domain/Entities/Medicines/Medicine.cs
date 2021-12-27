using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicineManaging.Domain.Entities.Medicines
{
    public class Medicine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MedicineType Type { get; set; }
        public string Description { get; set; }
        public DosageForm DosageForm { get; set; }
        public Container Container { get; set; }
        public State State { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
