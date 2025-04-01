using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rinha_de_backend_2023.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueApelido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_People_Apelido",
                table: "People",
                column: "Apelido",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_Apelido",
                table: "People");
        }
    }
}
