using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core2Identity.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ulke",
                columns: table => new
                {
                    UlkeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulke", x => x.UlkeId);
                });

            migrationBuilder.CreateTable(
                name: "Uye",
                columns: table => new
                {
                    UyeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(maxLength: 25, nullable: true),
                    DogumTarih = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    FUrl = table.Column<string>(nullable: true),
                    GUrl = table.Column<string>(nullable: true),
                    Onay = table.Column<int>(nullable: false),
                    ProfilFoto = table.Column<string>(nullable: true),
                    Sifre = table.Column<string>(maxLength: 100, nullable: true),
                    SoyAd = table.Column<string>(maxLength: 25, nullable: true),
                    TUrl = table.Column<string>(nullable: true),
                    Tarih = table.Column<DateTime>(nullable: false),
                    YaziSayisi = table.Column<int>(nullable: false),
                    YetkiId = table.Column<int>(nullable: false),
                    ÖzBilgi = table.Column<string>(nullable: true),
                    İnUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uye", x => x.UyeId);
                });

            migrationBuilder.CreateTable(
                name: "Sehir",
                columns: table => new
                {
                    SehirId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(nullable: true),
                    UlkeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehir", x => x.SehirId);
                    table.ForeignKey(
                        name: "FK_Sehir_Ulke_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulke",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yazi",
                columns: table => new
                {
                    YaziId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baslik = table.Column<string>(maxLength: 60, nullable: false),
                    BegenmeSayisi = table.Column<int>(nullable: false),
                    Goruntulenme = table.Column<int>(nullable: false),
                    Icerik = table.Column<string>(maxLength: 30000, nullable: false),
                    Onay = table.Column<bool>(nullable: false),
                    Resim = table.Column<string>(nullable: true),
                    SehirId = table.Column<int>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false),
                    UyeId = table.Column<int>(nullable: false),
                    YorumSayisi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazi", x => x.YaziId);
                    table.ForeignKey(
                        name: "FK_Yazi_Sehir_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehir",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yazi_Uye_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uye",
                        principalColumn: "UyeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Begen",
                columns: table => new
                {
                    BegenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UyeId = table.Column<int>(nullable: true),
                    YaziId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Begen", x => x.BegenId);
                    table.ForeignKey(
                        name: "FK_Begen_Uye_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uye",
                        principalColumn: "UyeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Begen_Yazi_YaziId",
                        column: x => x.YaziId,
                        principalTable: "Yazi",
                        principalColumn: "YaziId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Yorum",
                columns: table => new
                {
                    YorumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Icerik = table.Column<string>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false),
                    UyeId = table.Column<int>(nullable: true),
                    YaziId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorum", x => x.YorumId);
                    table.ForeignKey(
                        name: "FK_Yorum_Uye_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uye",
                        principalColumn: "UyeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yorum_Yazi_YaziId",
                        column: x => x.YaziId,
                        principalTable: "Yazi",
                        principalColumn: "YaziId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Begen_UyeId",
                table: "Begen",
                column: "UyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Begen_YaziId",
                table: "Begen",
                column: "YaziId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_UlkeId",
                table: "Sehir",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Yazi_SehirId",
                table: "Yazi",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Yazi_UyeId",
                table: "Yazi",
                column: "UyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_UyeId",
                table: "Yorum",
                column: "UyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_YaziId",
                table: "Yorum",
                column: "YaziId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Begen");

            migrationBuilder.DropTable(
                name: "Yorum");

            migrationBuilder.DropTable(
                name: "Yazi");

            migrationBuilder.DropTable(
                name: "Sehir");

            migrationBuilder.DropTable(
                name: "Uye");

            migrationBuilder.DropTable(
                name: "Ulke");
        }
    }
}
