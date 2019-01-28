using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GeziYazisiSitesi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ulkes",
                columns: table => new
                {
                    UlkeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkes", x => x.UlkeId);
                });

            migrationBuilder.CreateTable(
                name: "Uyes",
                columns: table => new
                {
                    UyeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyes", x => x.UyeID);
                });

            migrationBuilder.CreateTable(
                name: "Sehirs",
                columns: table => new
                {
                    SehirId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(nullable: true),
                    UlkeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirs", x => x.SehirId);
                    table.ForeignKey(
                        name: "FK_Sehirs_Ulkes_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkes",
                        principalColumn: "UlkeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yazis",
                columns: table => new
                {
                    YaziId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baslik = table.Column<string>(nullable: false),
                    BegenmeSayisi = table.Column<int>(nullable: false),
                    Goruntulenme = table.Column<int>(nullable: false),
                    Icerik = table.Column<string>(nullable: false),
                    Onay = table.Column<bool>(nullable: false),
                    Resim = table.Column<string>(nullable: false),
                    SehirId = table.Column<int>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false),
                    UyeId = table.Column<int>(nullable: false),
                    YorumSayisi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazis", x => x.YaziId);
                    table.ForeignKey(
                        name: "FK_Yazis_Sehirs_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirs",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yazis_Uyes_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uyes",
                        principalColumn: "UyeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sehirs_UlkeId",
                table: "Sehirs",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Yazis_SehirId",
                table: "Yazis",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Yazis_UyeId",
                table: "Yazis",
                column: "UyeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yazis");

            migrationBuilder.DropTable(
                name: "Sehirs");

            migrationBuilder.DropTable(
                name: "Uyes");

            migrationBuilder.DropTable(
                name: "Ulkes");
        }
    }
}
