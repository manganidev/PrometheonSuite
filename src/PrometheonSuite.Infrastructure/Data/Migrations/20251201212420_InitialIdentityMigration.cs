using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrometheonSuite.Infrastructure.Identity.Data.Migrations;

  /// <inheritdoc />
  public partial class InitialIdentityMigration : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "Figures",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                  Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Figures", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Roles",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                  Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Roles", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Tenants",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                  Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Tenants", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "UserTenants",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),
                  DefaultLocale = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UserTenants", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Utenti",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                  Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                  Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  Attivo = table.Column<bool>(type: "bit", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Utenti", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "FigureRoles",
              columns: table => new
              {
                  FigureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_FigureRoles", x => new { x.FigureId, x.RoleId });
                  table.ForeignKey(
                      name: "FK_FigureRoles_Figures_FigureId",
                      column: x => x.FigureId,
                      principalTable: "Figures",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "UserTenantFigures",
              columns: table => new
              {
                  UserTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  FigureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UserTenantFigures", x => new { x.UserTenantId, x.FigureId });
                  table.ForeignKey(
                      name: "FK_UserTenantFigures_UserTenants_UserTenantId",
                      column: x => x.UserTenantId,
                      principalTable: "UserTenants",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "UserTenantRoles",
              columns: table => new
              {
                  UserTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_UserTenantRoles", x => new { x.UserTenantId, x.RoleId });
                  table.ForeignKey(
                      name: "FK_UserTenantRoles_UserTenants_UserTenantId",
                      column: x => x.UserTenantId,
                      principalTable: "UserTenants",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateIndex(
              name: "IX_Figures_Code",
              table: "Figures",
              column: "Code",
              unique: true);

          migrationBuilder.CreateIndex(
              name: "IX_Roles_Code",
              table: "Roles",
              column: "Code",
              unique: true);

          migrationBuilder.CreateIndex(
              name: "IX_Tenants_Code",
              table: "Tenants",
              column: "Code",
              unique: true);

          migrationBuilder.CreateIndex(
              name: "IX_UserTenants_UserId_TenantId",
              table: "UserTenants",
              columns: new[] { "UserId", "TenantId" },
              unique: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "FigureRoles");

          migrationBuilder.DropTable(
              name: "Roles");

          migrationBuilder.DropTable(
              name: "Tenants");

          migrationBuilder.DropTable(
              name: "UserTenantFigures");

          migrationBuilder.DropTable(
              name: "UserTenantRoles");

          migrationBuilder.DropTable(
              name: "Utenti");

          migrationBuilder.DropTable(
              name: "Figures");

          migrationBuilder.DropTable(
              name: "UserTenants");
      }
  }
