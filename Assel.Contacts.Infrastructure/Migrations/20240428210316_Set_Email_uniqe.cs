using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assel.Contacts.Infrastructure.Migrations
{
    public partial class Set_Email_uniqe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOwnSubcategoryAllowed",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("db9d8f23-1202-436b-9eae-49aa590a1e2a"),
                column: "IsOwnSubcategoryAllowed",
                value: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("7c24fd27-34dd-4d51-b24f-e89b5f636b18"),
                column: "DateOfBirth",
                value: new DateTime(1984, 4, 28, 23, 3, 16, 219, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7"),
                column: "DateOfBirth",
                value: new DateTime(2002, 4, 28, 23, 3, 16, 219, DateTimeKind.Local).AddTicks(8008));

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_Email",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsOwnSubcategoryAllowed",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("7c24fd27-34dd-4d51-b24f-e89b5f636b18"),
                column: "DateOfBirth",
                value: new DateTime(1984, 4, 27, 22, 15, 7, 9, DateTimeKind.Local).AddTicks(7811));

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7"),
                column: "DateOfBirth",
                value: new DateTime(2002, 4, 27, 22, 15, 7, 9, DateTimeKind.Local).AddTicks(7768));
        }
    }
}
