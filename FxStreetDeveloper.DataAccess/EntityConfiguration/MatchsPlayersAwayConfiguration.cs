using FxStreetDeveloper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FxStreetDeveloper.DataAccess.EntityConfiguration
{
    public class MatchsPlayersAwayConfiguration : IEntityTypeConfiguration<MatchsPlayersAway>
    {
        public void Configure(EntityTypeBuilder<MatchsPlayersAway> builder)
        {
            builder.ToTable("MatchsPlayersAway");
            builder.HasKey(mp => new { mp.MatchId, mp.PlayerId });
            builder.HasOne(mp => mp.Match).WithMany(m => m.AwayTeamPlayers).HasForeignKey(p => p.MatchId);

            builder.HasOne(mp => mp.Player).WithMany(p => p.MatchsPlayersAway).HasForeignKey(p => p.PlayerId);

        }
    }
}
