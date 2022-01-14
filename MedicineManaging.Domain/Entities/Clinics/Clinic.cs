using MedicineManaging.Domain.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicineManaging.Domain.Entities.Clinics
{
    public class Clinic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [StringLength(24, MinimumLength = 24, ErrorMessage = ConstantValues.ID_ERROR_MESSAGE)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ConstantValues.ID_ERROR_MESSAGE)]
        public string Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("MedicineId")]
        public ICollection<MedicinesCountForClinic>? Medicines { get; set; }
        public string MedicineId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
