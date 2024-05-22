﻿// <auto-generated />
using System;
using LANCommander.Client.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LANCommander.Client.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240522012114_InitialSeed")]
    partial class InitialSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("CategoryGame", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GamesId")
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriesId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("CategoryGame");
                });

            modelBuilder.Entity("CollectionGame", b =>
                {
                    b.Property<Guid>("CollectionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.HasKey("CollectionId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("CollectionGame");
                });

            modelBuilder.Entity("GameDeveloper", b =>
                {
                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.HasKey("DeveloperId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("GameDeveloper");
                });

            modelBuilder.Entity("GameGenre", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("TEXT");

                    b.HasKey("GamesId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GamePublisher", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("TEXT");

                    b.HasKey("GameId", "PublisherId");

                    b.HasIndex("PublisherId");

                    b.ToTable("GamePublisher");
                });

            modelBuilder.Entity("GameRedistributable", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RedistributableId")
                        .HasColumnType("TEXT");

                    b.HasKey("GameId", "RedistributableId");

                    b.HasIndex("RedistributableId");

                    b.ToTable("GameRedistributable");
                });

            modelBuilder.Entity("GameTag", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("TEXT");

                    b.HasKey("GamesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("GameTag");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Collection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Engine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BaseGameId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EngineId")
                        .HasColumnType("TEXT");

                    b.Property<long?>("IGDBId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InstallDirectory")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Installed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InstalledVersion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReleasedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Singleplayer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SortTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BaseGameId");

                    b.HasIndex("EngineId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Crc32")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FileId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.MultiplayerMode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GameId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinPlayers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NetworkProtocol")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Spectators")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("MultiplayerModes");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Redistributable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Redistributables");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CategoryGame", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectionGame", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameDeveloper", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameGenre", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamePublisher", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameRedistributable", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Redistributable", null)
                        .WithMany()
                        .HasForeignKey("RedistributableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameTag", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LANCommander.Client.Data.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Category", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Game", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", "BaseGame")
                        .WithMany("DependentGames")
                        .HasForeignKey("BaseGameId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("LANCommander.Client.Data.Models.Engine", "Engine")
                        .WithMany("Games")
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("BaseGame");

                    b.Navigation("Engine");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Media", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", "Game")
                        .WithMany("Media")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Game");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.MultiplayerMode", b =>
                {
                    b.HasOne("LANCommander.Client.Data.Models.Game", "Game")
                        .WithMany("MultiplayerModes")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Category", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Engine", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("LANCommander.Client.Data.Models.Game", b =>
                {
                    b.Navigation("DependentGames");

                    b.Navigation("Media");

                    b.Navigation("MultiplayerModes");
                });
#pragma warning restore 612, 618
        }
    }
}
