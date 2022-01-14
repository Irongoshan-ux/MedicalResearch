using MedicineManaging.Domain.Entities.Clinics;

namespace MedicineManaging.API.GraphQL.Types
{
    public class ClinicType : ObjectType<Clinic>
    {
        protected override void Configure(IObjectTypeDescriptor<Clinic> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.Name);
            descriptor.Field(_ => _.Medicines);
            descriptor.Field(_ => _.City);
            descriptor.Field(_ => _.Address);
            descriptor.Field(_ => _.PhoneNumber);
        }
    }
}
