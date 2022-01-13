using MedicineManaging.Domain.Entities.Patients;

namespace MedicineManaging.API.GraphQL.Types
{
    public class PatientType : ObjectType<Patient>
    {
        protected override void Configure(IObjectTypeDescriptor<Patient> descriptor)
        {
            descriptor.Field(_ => _.Number);
            descriptor.Field(_ => _.Sex);
            descriptor.Field(_ => _.BirthDay);
            descriptor.Field(_ => _.LastVisitDate);
            descriptor.Field(_ => _.Status);
            descriptor.Field(_ => _.MedicineType);
            descriptor.Field(_ => _.Visits);
            descriptor.Field(_ => _.VisitId);
        }
    }
}
