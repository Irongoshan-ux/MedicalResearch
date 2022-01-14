using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Entities.Patients.Visits;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicineManaging.Domain.Entities.Patients
{
    public class Patient
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Number { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime LastVisitDate { get; set; }
        public Status Status { get; set; }
        public MedicineType MedicineType { get; set; }

        [ForeignKey("VisitId")]
        public ICollection<Visit>? Visits { get; set; }
        public int? VisitId { get; set; }
    }
}
