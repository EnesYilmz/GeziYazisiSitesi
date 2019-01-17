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
                name: "Yazis",
                columns: table => new
                {
                    YaziId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baslik = table.Column<string>(nullable: true),
                    BegenmeSayisi = table.Column<int>(nullable: false),
                    Goruntulenme = table.Column<int>(nullable: false),
                    Icerik = table.Column<string>(nullable: true),
                    Onay = table.Column<bool>(nullable: false),
                    Resim = table.Column<string>(nullable: true),
                    Tarih = table.Column<DateTime>(nullable: false),
                    UyeId = table.Column<int>(nullable: false),
                    YorumSayisi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazis", x => x.YaziId);
                    table.ForeignKey(
                        name: "FK_Yazis_Uyes_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uyes",
                        principalColumn: "UyeID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Uyes");
        }
    }
}
