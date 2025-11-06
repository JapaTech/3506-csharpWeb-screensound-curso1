using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PopularMusicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Musicas",
                new string[] { "Nome", "AnoLancamento" },
                new object[,]
                {
                    { "Imagine", 1982 },
                    { "Billie Jean", 1983 },
                    { "Bohemian Rhapsody", 1975 },
                    { "Like a Rolling Stone", 1965 },
                    { "Smells Like Teen Spirit", 1991 },
                    { "Erva Venenosa", 2000 },
                    { "Se...", 1992},
                    { "My Hero", 1997 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Musicas",
                keyColumn: "Nome",
                keyValues: new object[]
                {
                    "Imagine",
                    "Billie Jean",
                    "Bohemian Rhapsody",
                    "Like a Rolling Stone",
                    "Smells Like Teen Spirit",
                    "Erva Venenosa",
                    "Se...",
                    "My Hero"
                });
        }
    }
}
