using FxStreetDeveloper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FxStreetDeveloper.DataAccess.EntityConfiguration
{
    public class RefereeConfiguration : IEntityTypeConfiguration<Referee>
    {
        public void Configure(EntityTypeBuilder<Referee> builder)
        {
            builder.ToTable("Referees");
            builder.HasKey(p => p.Id).HasName("Referee_PK");
        }
    }
}
