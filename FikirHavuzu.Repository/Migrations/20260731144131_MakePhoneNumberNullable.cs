using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FikirHavuzu.Repository.Migrations
{
    /// <inheritdoc />
    public partial class MakePhoneNumberNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionDependency_Permissions_PermissionId",
                table: "PermissionDependency");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionDependency_Permissions_RequiredPermissionId",
                table: "PermissionDependency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionDependency",
                table: "PermissionDependency");

            migrationBuilder.RenameTable(
                name: "PermissionDependency",
                newName: "PermissionDependencies");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionDependency_RequiredPermissionId",
                table: "PermissionDependencies",
                newName: "IX_PermissionDependencies_RequiredPermissionId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionDependencies",
                table: "PermissionDependencies",
                columns: new[] { "PermissionId", "RequiredPermissionId" });

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7158));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7179));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7181));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7183));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7234));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 17, 41, 31, 78, DateTimeKind.Local).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$HaDeIxPwZdKsoMavZ/z1Iu3R11GjACqTqqMyt87gp/FQNvrSs5Kki");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionDependencies_Permissions_PermissionId",
                table: "PermissionDependencies",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionDependencies_Permissions_RequiredPermissionId",
                table: "PermissionDependencies",
                column: "RequiredPermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionDependencies_Permissions_PermissionId",
                table: "PermissionDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionDependencies_Permissions_RequiredPermissionId",
                table: "PermissionDependencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionDependencies",
                table: "PermissionDependencies");

            migrationBuilder.RenameTable(
                name: "PermissionDependencies",
                newName: "PermissionDependency");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionDependencies_RequiredPermissionId",
                table: "PermissionDependency",
                newName: "IX_PermissionDependency_RequiredPermissionId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionDependency",
                table: "PermissionDependency",
                columns: new[] { "PermissionId", "RequiredPermissionId" });

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1112));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1113));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1117));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1119));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$oRdgtBQSThMN2hx/.jGh0ezPKyRo2mY.tHPkwbO68mI.GpGijiiYO");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionDependency_Permissions_PermissionId",
                table: "PermissionDependency",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionDependency_Permissions_RequiredPermissionId",
                table: "PermissionDependency",
                column: "RequiredPermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
