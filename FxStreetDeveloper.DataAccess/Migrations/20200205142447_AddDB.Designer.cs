﻿// <auto-generated />
using System;
using FxStreetDeveloper.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FxStreetDeveloper.DataAccess.Migrations
{
    [DbContext(typeof(FxStreetDeveloperContext))]
    [Migration("20200205142447_AddDB")]
    partial class AddDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FxStreetDeveloper.Domain.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RedCards")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YellowCards")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("Manager_PK");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AwayTeamManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("HouseTeamManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RefereeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("Match_PK");

                    b.HasIndex("AwayTeamManagerId");

                    b.HasIndex("HouseTeamManagerId");

                    b.HasIndex("RefereeId");

                    b.ToTable("Matchs");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.MatchsPlayersAway", b =>
                {
                    b.Property<Guid>("MatchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MatchId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchsPlayersAway");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.MatchsPlayersHouse", b =>
                {
                    b.Property<Guid>("MatchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MatchId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchsPlayersHouse");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MinutesPlayed")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("RedCards")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YellowCards")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("Player_PK");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.Referee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MinutesPlayed")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("Referee_PK");

                    b.ToTable("Referees");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.Match", b =>
                {
                    b.HasOne("FxStreetDeveloper.Domain.Manager", "AwayTeamManager")
                        .WithMany()
                        .HasForeignKey("AwayTeamManagerId");

                    b.HasOne("FxStreetDeveloper.Domain.Manager", "HouseTeamManager")
                        .WithMany()
                        .HasForeignKey("HouseTeamManagerId");

                    b.HasOne("FxStreetDeveloper.Domain.Referee", "Referee")
                        .WithMany()
                        .HasForeignKey("RefereeId");
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.MatchsPlayersAway", b =>
                {
                    b.HasOne("FxStreetDeveloper.Domain.Match", "Match")
                        .WithMany("AwayTeamPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FxStreetDeveloper.Domain.Player", "Player")
                        .WithMany("MatchsPlayersAway")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FxStreetDeveloper.Domain.MatchsPlayersHouse", b =>
                {
                    b.HasOne("FxStreetDeveloper.Domain.Match", "Match")
                        .WithMany("HouseTeamPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FxStreetDeveloper.Domain.Player", "Player")
                        .WithMany("MatchsPlayersHouse")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
