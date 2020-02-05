using FxStreetDeveloper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FxStreetDeveloper.DataAccess.EntityConfiguration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matchs");
            builder.HasKey(p => p.Id).HasName("Match_PK");
        }
    }
}
