using FxStreetDeveloper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FxStreetDeveloper.DataAccess.EntityConfiguration
{
    public class MatchsPlayersHouseConfiguration : IEntityTypeConfiguration<MatchsPlayersHouse>
    {
        public void Configure(EntityTypeBuilder<MatchsPlayersHouse> builder)
        {
            builder.ToTable("MatchsPlayersHouse");
            builder.HasKey(mp => new { mp.MatchId, mp.PlayerId });
            builder.HasOne(mp => mp.Match).WithMany(m => m.HouseTeamPlayers).HasForeignKey(p => p.MatchId);

            builder.HasOne(mp => mp.Player).WithMany(p => p.MatchsPlayersHouse).HasForeignKey(p => p.PlayerId);

        }
    }
}
