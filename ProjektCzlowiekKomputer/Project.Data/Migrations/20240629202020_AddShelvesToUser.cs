using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShelvesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Shelves_Guid",
                table: "Shelves",
                column: "Guid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserGuid",
                table: "AspNetUsers",
                column: "UserGuid");

            migrationBuilder.CreateTable(
                name: "UserShelves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShelvesId = table.Column<int>(type: "int", nullable: false),
                    ShelvesGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserShelves_AspNetUsers_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserShelves_Shelves_ShelvesGuid",
                        column: x => x.ShelvesGuid,
                        principalTable: "Shelves",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserShelves_ShelvesGuid",
                table: "UserShelves",
                column: "ShelvesGuid");

            migrationBuilder.CreateIndex(
                name: "IX_UserShelves_UserGuid",
                table: "UserShelves",
                column: "UserGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserShelves");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Shelves_Guid",
                table: "Shelves");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserGuid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "AspNetUsers");
        }
    }
}
