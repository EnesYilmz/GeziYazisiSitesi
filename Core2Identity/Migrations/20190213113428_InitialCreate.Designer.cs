﻿// <auto-generated />
using Core2Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Core2Identity.Migrations
{
    [DbContext(typeof(ApplicationIdentityDbContext))]
    [Migration("20190213113428_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core2Identity.Models.Uye", b =>
                {
                    b.Property<int>("UyeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ad")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .HasMaxLength(25);

                    b.Property<int>("Onay");

                    b.Property<string>("ProfilFoto");

                    b.Property<string>("Sifre")
                        .HasMaxLength(20);

                    b.Property<string>("SoyAd")
                        .HasMaxLength(20);

                    b.Property<DateTime>("Tarih");

                    b.Property<int>("YetkiId");

                    b.HasKey("UyeId");

                    b.ToTable("Uye");
                });

            modelBuilder.Entity("GeziYazisiSitesi.Modals.Sehir", b =>
                {
                    b.Property<int>("SehirId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ad");

                    b.Property<int>("UlkeId");

                    b.HasKey("SehirId");

                    b.HasIndex("UlkeId");

                    b.ToTable("Sehir");
                });

            modelBuilder.Entity("GeziYazisiSitesi.Modals.Ulke", b =>
                {
                    b.Property<int>("UlkeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ad")
                        .IsRequired();

                    b.HasKey("UlkeId");

                    b.ToTable("Ulke");
                });

            modelBuilder.Entity("GeziYazisiSitesi.Modals.Yazi", b =>
                {
                    b.Property<int>("YaziId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Baslik")
                        .IsRequired();

                    b.Property<int>("BegenmeSayisi");

                    b.Property<int>("Goruntulenme");

                    b.Property<string>("Icerik")
                        .IsRequired();

                    b.Property<bool>("Onay");

                    b.Property<string>("Resim");

                    b.Property<int>("SehirId");

                    b.Property<DateTime>("Tarih");

                    b.Property<int>("UyeId");

                    b.Property<int>("YorumSayisi");

                    b.HasKey("YaziId");

                    b.HasIndex("SehirId");

                    b.HasIndex("UyeId");

                    b.ToTable("Yazi");
                });

            modelBuilder.Entity("GeziYazisiSitesi.Modals.Sehir", b =>
                {
                    b.HasOne("GeziYazisiSitesi.Modals.Ulke", "Ulke")
                        .WithMany()
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeziYazisiSitesi.Modals.Yazi", b =>
                {
                    b.HasOne("GeziYazisiSitesi.Modals.Sehir", "Sehir")
                        .WithMany()
                        .HasForeignKey("SehirId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core2Identity.Models.Uye", "Uye")
                        .WithMany()
                        .HasForeignKey("UyeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
