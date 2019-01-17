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
                    YazarUyeID = table.Column<int>(nullable: true),
                    baslik = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazis", x => x.YaziId);
                    table.ForeignKey(
                        name: "FK_Yazis_Uyes_YazarUyeID",
                        column: x => x.YazarUyeID,
                        principalTable: "Uyes",
                        principalColumn: "UyeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Yazis_YazarUyeID",
                table: "Yazis",
                column: "YazarUyeID");
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
