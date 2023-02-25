using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTful_API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class WriterControlle2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Writer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WrieterFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writer", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_WriterID",
                table: "Books",
                column: "WriterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Writer_WriterID",
                table: "Books",
                column: "WriterID",
                principalTable: "Writer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Writer_WriterID",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Writer");

            migrationBuilder.DropIndex(
                name: "IX_Books_WriterID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "WriterID",
                table: "Books");
        }
    }
}
