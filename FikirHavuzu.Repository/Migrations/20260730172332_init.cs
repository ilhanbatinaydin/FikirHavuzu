using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FikirHavuzu.Repository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionDependency",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RequiredPermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDependency", x => new { x.PermissionId, x.RequiredPermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionDependency_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionDependency_Permissions_RequiredPermissionId",
                        column: x => x.RequiredPermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetedBenefit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ideas_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ideas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdeaId = table.Column<int>(type: "int", nullable: false),
                    EvaluatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_Users_EvaluatedByUserId",
                        column: x => x.EvaluatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdeaDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdeaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaDocuments_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ürün" },
                    { 2, "Hizmet" },
                    { 3, "Süreç" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Fikirleri görüntüleme yetkisi.", "Idea.View" },
                    { 2, "Fikir oluşturma yetkisi.", "Idea.Create" },
                    { 3, "Fikirleri karara bağlama, açıklama yazma ve puanlama yetkisi.", "Idea.Evaluate" },
                    { 4, "Kullanıcı ekleme, güncelleme, pasife alma ve listeleme yetkisi.", "User.Manage" },
                    { 5, "Kullanıcılara yetki atama ve kaldırma yetkisi.", "Permission.Manage" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IdentityNumber", "IsActive", "LastName", "PasswordHash", "PhoneNumber", "RegistrationNumber" },
                values: new object[] { 1, "admin@fikirhavuzu.com", "Sistem", "11111111111", true, "Yöneticisi", "$2a$11$oRdgtBQSThMN2hx/.jGh0ezPKyRo2mY.tHPkwbO68mI.GpGijiiYO", "05555555555", "0001" });

            migrationBuilder.InsertData(
                table: "Ideas",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "TargetedBenefit", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1095), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 1", 1 },
                    { 2, 1, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1112), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 2", 1 },
                    { 3, 1, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1113), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 3", 1 },
                    { 4, 2, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1114), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 4", 1 },
                    { 5, 2, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1115), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 5", 1 },
                    { 6, 2, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1116), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 6", 1 },
                    { 7, 2, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1116), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 7", 1 },
                    { 8, 3, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1117), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 8", 1 },
                    { 9, 3, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1118), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 9", 1 },
                    { 10, 3, new DateTime(2026, 7, 30, 20, 23, 32, 280, DateTimeKind.Local).AddTicks(1119), "Bu mimariye geçiş yapmak daha iyi olur", "Mimari iyileştirmesi", "ASP.NET CORE MİMARİSİ 10", 1 }
                });

            migrationBuilder.InsertData(
                table: "PermissionDependency",
                columns: new[] { "PermissionId", "RequiredPermissionId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 3, 2 },
                    { 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 1, 0 },
                    { 3, 1, 0 },
                    { 4, 1, 0 },
                    { 5, 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatedByUserId",
                table: "Evaluations",
                column: "EvaluatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_IdeaId",
                table: "Evaluations",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaDocuments_IdeaId",
                table: "IdeaDocuments",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_CategoryId",
                table: "Ideas",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_UserId",
                table: "Ideas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDependency_RequiredPermissionId",
                table: "PermissionDependency",
                column: "RequiredPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "IdeaDocuments");

            migrationBuilder.DropTable(
                name: "PermissionDependency");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Ideas");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
