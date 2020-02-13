using Microsoft.EntityFrameworkCore.Migrations;

namespace DevItUp.Grain.API.Migrations.SqlServerMigrations
{
    public partial class AddEmailAddressToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("EmailAddress", "Users", "varchar(255)", unicode: false, maxLength: 255, nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Not figured out how to delete a column yet.
        }
    }
}
