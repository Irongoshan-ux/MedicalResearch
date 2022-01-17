using System.ComponentModel.DataAnnotations;

namespace MedicineManaging.Domain.Entities.Clinics
{
    public class AddClinicModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
