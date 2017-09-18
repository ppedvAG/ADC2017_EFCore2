using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore_1_1_2_QuerySpeedTest.Configurations
{
    public interface IEntityTypeConfiguration<T> where T : class
    {
        void Configure(EntityTypeBuilder<T> builder);
    }
}