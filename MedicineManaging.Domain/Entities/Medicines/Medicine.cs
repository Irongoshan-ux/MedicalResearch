using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MedicineManaging.Domain.Constants;

namespace MedicineManaging.Domain.Entities.Medicines
{
    public class Medicine
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [StringLength(24, MinimumLength = 24, ErrorMessage = ConstantValues.ID_ERROR_MESSAGE)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ConstantValues.ID_ERROR_MESSAGE)]
        public string Id { get; set; }
        public MedicineType Type { get; set; }
        public string? Description { get; set; }
        public DosageForm DosageForm { get; set; }
        public Container Container { get; set; }
        public State State { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
