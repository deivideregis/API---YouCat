using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevIO.Data.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NumeroYouCat = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false),
                    Pergunta = table.Column<string>(type: "varchar(250)", nullable: false),
                    Resposta = table.Column<string>(type: "varchar(800)", nullable: true),
                    Explicacao = table.Column<string>(type: "varchar(800)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEstudo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EstudoId = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEstudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoEstudo_Estudos_EstudoId",
                        column: x => x.EstudoId,
                        principalTable: "Estudos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoEstudo_EstudoId",
                table: "TipoEstudo",
                column: "EstudoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoEstudo");

            migrationBuilder.DropTable(
                name: "Estudos");
        }
    }
}
