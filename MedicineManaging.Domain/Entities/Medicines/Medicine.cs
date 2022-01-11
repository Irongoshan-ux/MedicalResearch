using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MedicineManaging.Domain.Entities.Medicines
{
    public class Medicine
    {
        private const string IdErrorMessage = "ID must be a 24-digit number";

        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [StringLength(24, MinimumLength = 24, ErrorMessage = IdErrorMessage)]
        [RegularExpression("^[0-9]+$", ErrorMessage = IdErrorMessage)]
        public string Id { get; set; }
        public MedicineType Type { get; set; }
        public string? Description { get; set; }
        public DosageForm DosageForm { get; set; }
        public Container Container { get; set; }
        public State State { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
