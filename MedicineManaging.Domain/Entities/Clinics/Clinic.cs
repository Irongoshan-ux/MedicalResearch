using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MedicineManaging.Domain.Entities.Clinics
{
    public class Clinic
    {
        private const string IdErrorMessage = "ID must be a 24-digit number";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [StringLength(24, MinimumLength = 24, ErrorMessage = IdErrorMessage)]
        [RegularExpression("^[0-9]+$", ErrorMessage = IdErrorMessage)]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<MedicinesCountForClinic>? Medicines { get; set; }
    }
}
