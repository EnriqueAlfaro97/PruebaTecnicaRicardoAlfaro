using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaRicardoAlfaro.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarEstaCompletadaAEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstaCompletada",
                table: "Tareas",
                newName: "Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Tareas",
                newName: "EstaCompletada");
        }
    }
}
