using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Domain.Entities.Patients.Visits
{
    public class Visit
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Number { get; set; }
        public DateTime Date { get; set; }
        
        [ForeignKey("MedicineId")]
        public ICollection<Medicine>? Medicines { get; set; }
        public string? MedicineId { get; set; }
    }
}
