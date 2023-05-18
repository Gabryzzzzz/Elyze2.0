// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Creazione = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    Data_Spegnimento = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    Stato = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    IdArea = table.Column<int>(type: "int", nullable: false),
                    Lingua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconaArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconaSdg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampiMa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCampo = table.Column<int>(type: "int", nullable: false),
                    MicroAreaId = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipologia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lingua = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    InizioValidita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FineValidita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UDM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UDMId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampiMa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceGri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stato = table.Column<bool>(type: "bit", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSpegnimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gruppo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attivo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gruppo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inserimenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCampo = table.Column<int>(type: "int", nullable: false),
                    ValoreCampo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    ValidatoreId = table.Column<int>(type: "int", nullable: true),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataInserimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataValidazione = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValoreValidato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stato = table.Column<int>(type: "int", nullable: true),
                    RepartoId = table.Column<int>(type: "int", nullable: false),
                    SocietaId = table.Column<int>(type: "int", nullable: false),
                    StabilimentoId = table.Column<int>(type: "int", nullable: false),
                    IdInserimento = table.Column<int>(type: "int", nullable: false),
                    MicroAreaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inserimenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lingue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lingue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MicroArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Creazione = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    Data_Spegnimento = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    Id_Gri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome_Tabella = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stato = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    Lingua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MicroAreaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operazione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatoreDiConversione = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operazione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reparto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StabilimentoId = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attivo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repository",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repository", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sede",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SedeN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsoNazione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attivo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    SocietaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ValutaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sede", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Societa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sede = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodiceIsoNazione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GruppoId = table.Column<int>(type: "int", nullable: false),
                    Attivo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Societa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatoSchede",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatoSchede", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitaMisura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Simbolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitaMisura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    Attivo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IdArea = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_Area = table.Column<int>(type: "int", nullable: false),
                    Id_Plant = table.Column<int>(type: "int", nullable: false),
                    Id_Societa = table.Column<int>(type: "int", nullable: false),
                    Id_Gruppo = table.Column<int>(type: "int", nullable: false),
                    Id_Reparto = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Valuta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Simbolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valuta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatoreDiConversione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUnitaDiMisuraPartenza = table.Column<int>(type: "int", nullable: true),
                    IdUnitaDiMisuraArrivo = table.Column<int>(type: "int", nullable: true),
                    IdOperazione = table.Column<int>(type: "int", nullable: true),
                    FattoreDiConversione = table.Column<decimal>(type: "decimal(18,5)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatoreDiConversione", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatoreDiConversione_UnitaMisura_Arrivo",
                        column: x => x.IdUnitaDiMisuraArrivo,
                        principalTable: "UnitaMisura",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperatoreDiConversione_UnitaMisura_Operazione",
                        column: x => x.IdOperazione,
                        principalTable: "Operazione",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperatoreDiConversione_UnitaMisura_Partenza",
                        column: x => x.IdUnitaDiMisuraPartenza,
                        principalTable: "UnitaMisura",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGruppo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GruppoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGruppo", x => new { x.UserId, x.GruppoId });
                    table.ForeignKey(
                        name: "FK_UserGruppo_Gruppo_GruppoId",
                        column: x => x.GruppoId,
                        principalTable: "Gruppo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGruppo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReparto",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RepartoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReparto", x => new { x.UserId, x.RepartoId });
                    table.ForeignKey(
                        name: "FK_UserReparto_Reparto_RepartoId",
                        column: x => x.RepartoId,
                        principalTable: "Reparto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReparto_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSede",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSede", x => new { x.UserId, x.SedeId });
                    table.ForeignKey(
                        name: "FK_UserSede_Sede_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSede_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSocieta",
                columns: table => new
                {
                    SocietaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocieta", x => new { x.UserId, x.SocietaId });
                    table.ForeignKey(
                        name: "FK_UserSocieta_Societa_SocietaId",
                        column: x => x.SocietaId,
                        principalTable: "Societa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSocieta_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperatoreDiConversione_IdOperazione",
                table: "OperatoreDiConversione",
                column: "IdOperazione");

            migrationBuilder.CreateIndex(
                name: "IX_OperatoreDiConversione_IdUnitaDiMisuraArrivo",
                table: "OperatoreDiConversione",
                column: "IdUnitaDiMisuraArrivo");

            migrationBuilder.CreateIndex(
                name: "IX_OperatoreDiConversione_IdUnitaDiMisuraPartenza",
                table: "OperatoreDiConversione",
                column: "IdUnitaDiMisuraPartenza");

            migrationBuilder.CreateIndex(
                name: "IX_UserGruppo_GruppoId",
                table: "UserGruppo",
                column: "GruppoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReparto_RepartoId",
                table: "UserReparto",
                column: "RepartoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSede_SedeId",
                table: "UserSede",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocieta_SocietaId",
                table: "UserSocieta",
                column: "SocietaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "CampiMa");

            migrationBuilder.DropTable(
                name: "Gri");

            migrationBuilder.DropTable(
                name: "Inserimenti");

            migrationBuilder.DropTable(
                name: "Lingue");

            migrationBuilder.DropTable(
                name: "MicroArea");

            migrationBuilder.DropTable(
                name: "OperatoreDiConversione");

            migrationBuilder.DropTable(
                name: "Repository");

            migrationBuilder.DropTable(
                name: "StatoSchede");

            migrationBuilder.DropTable(
                name: "UserArea");

            migrationBuilder.DropTable(
                name: "UserConfig");

            migrationBuilder.DropTable(
                name: "UserGruppo");

            migrationBuilder.DropTable(
                name: "UserReparto");

            migrationBuilder.DropTable(
                name: "UserSede");

            migrationBuilder.DropTable(
                name: "UserSocieta");

            migrationBuilder.DropTable(
                name: "Valuta");

            migrationBuilder.DropTable(
                name: "UnitaMisura");

            migrationBuilder.DropTable(
                name: "Operazione");

            migrationBuilder.DropTable(
                name: "Gruppo");

            migrationBuilder.DropTable(
                name: "Reparto");

            migrationBuilder.DropTable(
                name: "Sede");

            migrationBuilder.DropTable(
                name: "Societa");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
