using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedPasswordFieldToUserDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.RenameColumn(
                name: "productQuantity",
                table: "Product",
                newName: "ProductQuantity");

            migrationBuilder.AddColumn<short>(
                name: "Password",
                table: "User",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Product",
                newName: "productQuantity");
        }
    }
}
