using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrometheonSuite.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicazione",
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
                    table.PrimaryKey("PK_Applicazione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Azienda",
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
                    table.PrimaryKey("PK_Azienda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtenteApplicazioneAzienda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AziendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicazioneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DefaultLocale = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteApplicazioneAzienda", x => x.Id);
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
                name: "Figura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApplicazioneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Figura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Figura_Applicazione_ApplicazioneId",
                        column: x => x.ApplicazioneId,
                        principalTable: "Applicazione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ruolo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApplicazioneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruolo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ruolo_Applicazione_ApplicazioneId",
                        column: x => x.ApplicazioneId,
                        principalTable: "Applicazione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AziendaApplicazione",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AziendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicazioneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AziendaApplicazione", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AziendaApplicazione_Applicazione_ApplicazioneId",
                        column: x => x.ApplicazioneId,
                        principalTable: "Applicazione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AziendaApplicazione_Azienda_AziendaId",
                        column: x => x.AziendaId,
                        principalTable: "Azienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UtenteApplicazioneAziendaFiguras",
                columns: table => new
                {
                    UtenteApplicazioneAziendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiguraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteApplicazioneAziendaFiguras", x => new { x.UtenteApplicazioneAziendaId, x.FiguraId });
                    table.ForeignKey(
                        name: "FK_UtenteApplicazioneAziendaFiguras_UtenteApplicazioneAzienda_UtenteApplicazioneAziendaId",
                        column: x => x.UtenteApplicazioneAziendaId,
                        principalTable: "UtenteApplicazioneAzienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiguraRuolos",
                columns: table => new
                {
                    FiguraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuoloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiguraRuolos", x => new { x.FiguraId, x.RuoloId });
                    table.ForeignKey(
                        name: "FK_FiguraRuolos_Figura_FiguraId",
                        column: x => x.FiguraId,
                        principalTable: "Figura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicazione_Code",
                table: "Applicazione",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Azienda_Code",
                table: "Azienda",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AziendaApplicazione_ApplicazioneId",
                table: "AziendaApplicazione",
                column: "ApplicazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_AziendaApplicazione_AziendaId_ApplicazioneId",
                table: "AziendaApplicazione",
                columns: new[] { "AziendaId", "ApplicazioneId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Figura_ApplicazioneId",
                table: "Figura",
                column: "ApplicazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_Figura_Code",
                table: "Figura",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ruolo_ApplicazioneId_Code",
                table: "Ruolo",
                columns: new[] { "ApplicazioneId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UtenteApplicazioneAzienda_UserId_AziendaId_ApplicazioneId",
                table: "UtenteApplicazioneAzienda",
                columns: new[] { "UserId", "AziendaId", "ApplicazioneId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AziendaApplicazione");

            migrationBuilder.DropTable(
                name: "FiguraRuolos");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Ruolo");

            migrationBuilder.DropTable(
                name: "UtenteApplicazioneAziendaFiguras");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Azienda");

            migrationBuilder.DropTable(
                name: "Figura");

            migrationBuilder.DropTable(
                name: "UtenteApplicazioneAzienda");

            migrationBuilder.DropTable(
                name: "Applicazione");
        }
    }
}
