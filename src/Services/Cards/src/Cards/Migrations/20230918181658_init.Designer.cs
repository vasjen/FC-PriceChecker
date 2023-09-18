﻿// <auto-generated />
using Cards.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cards.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230918181658_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BuildingBlocks.Core.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DisplayedName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FbDataId")
                        .HasColumnType("integer");

                    b.Property<int>("FbId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PcId")
                        .HasColumnType("integer");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PsId")
                        .HasColumnType("integer");

                    b.Property<int>("Raiting")
                        .HasColumnType("integer");

                    b.Property<string>("Revision")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PcId");

                    b.HasIndex("PsId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("BuildingBlocks.Core.Models.Pc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LCPClosing")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice2")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice3")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice4")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice5")
                        .HasColumnType("integer");

                    b.Property<int>("MaxPrice")
                        .HasColumnType("integer");

                    b.Property<int>("MinPrice")
                        .HasColumnType("integer");

                    b.Property<int>("PRP")
                        .HasColumnType("integer");

                    b.Property<string>("updated")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pc");
                });

            modelBuilder.Entity("BuildingBlocks.Core.Models.Ps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LCPClosing")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice2")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice3")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice4")
                        .HasColumnType("integer");

                    b.Property<int>("LCPrice5")
                        .HasColumnType("integer");

                    b.Property<int>("MaxPrice")
                        .HasColumnType("integer");

                    b.Property<int>("MinPrice")
                        .HasColumnType("integer");

                    b.Property<int>("PRP")
                        .HasColumnType("integer");

                    b.Property<int>("updated")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Ps");
                });

            modelBuilder.Entity("BuildingBlocks.Core.Models.Card", b =>
                {
                    b.HasOne("BuildingBlocks.Core.Models.Pc", "PcPrices")
                        .WithMany()
                        .HasForeignKey("PcId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildingBlocks.Core.Models.Ps", "PsPrices")
                        .WithMany()
                        .HasForeignKey("PsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PcPrices");

                    b.Navigation("PsPrices");
                });
#pragma warning restore 612, 618
        }
    }
}