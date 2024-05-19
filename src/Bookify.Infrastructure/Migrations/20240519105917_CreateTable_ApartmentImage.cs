using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable_ApartmentImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apartment_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    apartment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    extension = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    file_content = table.Column<byte[]>(type: "bytea", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apartment_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_apartment_image_apartments_apartment_id",
                        column: x => x.apartment_id,
                        principalTable: "apartments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_apartment_image_apartment_id",
                table: "apartment_image",
                column: "apartment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apartment_image");
        }
    }
}
