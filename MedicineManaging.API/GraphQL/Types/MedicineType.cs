using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.API.GraphQL.Types
{
    public class MedicineType : ObjectType<Medicine>
    {
        protected override void Configure(IObjectTypeDescriptor<Medicine> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.Type);
            descriptor.Field(_ => _.Description);
            descriptor.Field(_ => _.Container);
            descriptor.Field(_ => _.State);
            descriptor.Field(_ => _.DosageForm);
            descriptor.Field(_ => _.ExpireAt);
        }
    }
}
