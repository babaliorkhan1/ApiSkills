using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstError.Data.Migrations
{
    public partial class addedlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 13, 31, 38, 534, DateTimeKind.Utc).AddTicks(8511),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 23, 20, 2, 42, 269, DateTimeKind.Utc).AddTicks(7882));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 13, 31, 38, 534, DateTimeKind.Utc).AddTicks(8126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 23, 20, 2, 42, 269, DateTimeKind.Utc).AddTicks(7546));

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 23, 20, 2, 42, 269, DateTimeKind.Utc).AddTicks(7882),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 2, 13, 31, 38, 534, DateTimeKind.Utc).AddTicks(8511));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 23, 20, 2, 42, 269, DateTimeKind.Utc).AddTicks(7546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 2, 13, 31, 38, 534, DateTimeKind.Utc).AddTicks(8126));
        }
    }
}
