using DemoAsp.Net8_Session01_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoAsp.Net8_Session01_.FluentConfigration
{
    public class PlanConfigration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(P => P.Name)
               .HasColumnType("Varchar")
               .HasMaxLength(30);

            builder.Property(P => P.Description)
                .HasMaxLength(200);


            builder.Property(P => P.Price)
                .HasPrecision(10, 2); //decimal(10,2)


            builder.Property(P => P.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            //Check Constrant => Must on Table
            //Duration Must Be (1-365)Days
            builder.ToTable<Plan>(TB =>

            {
                TB.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 and 365");

            });
        }
    }
}
