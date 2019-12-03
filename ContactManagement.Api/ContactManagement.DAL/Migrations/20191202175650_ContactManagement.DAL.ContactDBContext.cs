using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManagement.DAL.Migrations
{
    public partial class ContactManagementDALContactDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StreetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Street = table.Column<string>(maxLength: 250, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: false),
                    City = table.Column<string>(maxLength: 150, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    VATNumber = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    GSMNumber = table.Column<string>(maxLength: 20, nullable: false),
                    IsFreelance = table.Column<bool>(nullable: false),
                    VATNumber = table.Column<string>(maxLength: 20, nullable: true),
                    AddressId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseAddress",
                columns: table => new
                {
                    AddressId = table.Column<long>(nullable: false),
                    EnterpriseId = table.Column<long>(nullable: false),
                    HeadOffice = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseAddress", x => new { x.AddressId, x.EnterpriseId });
                    table.ForeignKey(
                        name: "FK_EnterpriseAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnterpriseAddress_Enterprise_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactEnterprise",
                columns: table => new
                {
                    ContactId = table.Column<long>(nullable: false),
                    EnterpriseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEnterprise", x => new { x.ContactId, x.EnterpriseId });
                    table.ForeignKey(
                        name: "FK_ContactEnterprise_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactEnterprise_Enterprise_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AddressId",
                table: "Contact",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEnterprise_EnterpriseId",
                table: "ContactEnterprise",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseAddress_EnterpriseId",
                table: "EnterpriseAddress",
                column: "EnterpriseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactEnterprise");

            migrationBuilder.DropTable(
                name: "EnterpriseAddress");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Enterprise");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
