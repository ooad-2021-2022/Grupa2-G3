using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD___Projektat___G3.Data.Migrations
{
    public partial class Druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cijena = table.Column<double>(type: "float", nullable: false),
                    kolicina = table.Column<double>(type: "float", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brojac = table.Column<int>(type: "int", nullable: false),
                    vlasnikKorisnik = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.id);
                    table.ForeignKey(
                        name: "FK_Artikal_User_vlasnikKorisnik",
                        column: x => x.vlasnikKorisnik,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikKompanija",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    nazivKompanije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikKompanija", x => x.id);
                    table.ForeignKey(
                        name: "FK_KorisnikKompanija_User_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NeregistrovaniKorisnik",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeregistrovaniKorisnik", x => x.id);
                    table.ForeignKey(
                        name: "FK_NeregistrovaniKorisnik_User_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrovaniKorisnik",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrovaniKorisnik", x => x.id);
                    table.ForeignKey(
                        name: "FK_RegistrovaniKorisnik_User_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtikalKategorija",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    katetegorija = table.Column<int>(type: "int", nullable: false),
                    ArtikalID = table.Column<int>(type: "int", nullable: false),
                    Kategorija = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikalKategorija", x => x.id);
                    table.ForeignKey(
                        name: "FK_ArtikalKategorija_Artikal_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_vlasnikKorisnik",
                table: "Artikal",
                column: "vlasnikKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikalKategorija_ArtikalID",
                table: "ArtikalKategorija",
                column: "ArtikalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "ArtikalKategorija");

            migrationBuilder.DropTable(
                name: "KorisnikKompanija");

            migrationBuilder.DropTable(
                name: "NeregistrovaniKorisnik");

            migrationBuilder.DropTable(
                name: "RegistrovaniKorisnik");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
