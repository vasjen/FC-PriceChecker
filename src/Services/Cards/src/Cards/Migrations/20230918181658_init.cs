using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cards.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LCPrice = table.Column<int>(type: "integer", nullable: false),
                    LCPrice2 = table.Column<int>(type: "integer", nullable: false),
                    LCPrice3 = table.Column<int>(type: "integer", nullable: false),
                    LCPrice4 = table.Column<int>(type: "integer", nullable: false),
                    LCPrice5 = table.Column<int>(type: "integer", nullable: false),
                    updated = table.Column<string>(type: "text", nullable: false),
                    MinPrice = table.Column<int>(type: "integer", nullable: false),
                    MaxPrice = table.Column<int>(type: "integer", nullable: false),
                    PRP = table.Column<int>(type: "integer", nullable: false),
                    LCPClosing = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LCPrice = table.Column<int>(type: "integer", nullable: false),
                    LCPrice2 = table.Column<int>(type: "integer", nullable: false),
                    LCPrice3 = table.Column<int>(type: "integer", nullable: false),
                    LCPrice4 = table.Column<int>(type: "integer", nullable: false),
                    LCPrice5 = table.Column<int>(type: "integer", nullable: false),
                    updated = table.Column<int>(type: "integer", nullable: false),
                    MinPrice = table.Column<int>(type: "integer", nullable: false),
                    MaxPrice = table.Column<int>(type: "integer", nullable: false),
                    PRP = table.Column<int>(type: "integer", nullable: false),
                    LCPClosing = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FbId = table.Column<int>(type: "integer", nullable: false),
                    FbDataId = table.Column<int>(type: "integer", nullable: false),
                    DisplayedName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PcId = table.Column<int>(type: "integer", nullable: false),
                    PsId = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    Revision = table.Column<string>(type: "text", nullable: false),
                    Raiting = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Pc_PcId",
                        column: x => x.PcId,
                        principalTable: "Pc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_Ps_PsId",
                        column: x => x.PsId,
                        principalTable: "Ps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PcId",
                table: "Cards",
                column: "PcId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PsId",
                table: "Cards",
                column: "PsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Pc");

            migrationBuilder.DropTable(
                name: "Ps");
        }
    }
}
