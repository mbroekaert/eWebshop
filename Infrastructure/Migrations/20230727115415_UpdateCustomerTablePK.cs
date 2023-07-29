using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateCustomerTablePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingAddress_Customer_CustomerId",
                table: "BillingAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddress_Customer_CustomerId",
                table: "ShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddress_CustomerId",
                table: "ShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_BillingAddress_CustomerId",
                table: "BillingAddress");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ShippingAddress");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "BillingAddress");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAuth0UserId",
                table: "ShippingAddress",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAuth0UserId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Auth0UserId",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Customer"
                );
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Customer",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "CustomerId",
            //    table: "Customer",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAuth0UserId",
                table: "BillingAddress",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Auth0UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_CustomerAuth0UserId",
                table: "ShippingAddress",
                column: "CustomerAuth0UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerAuth0UserId",
                table: "Order",
                column: "CustomerAuth0UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingAddress_CustomerAuth0UserId",
                table: "BillingAddress",
                column: "CustomerAuth0UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAddress_Customer_CustomerAuth0UserId",
                table: "BillingAddress",
                column: "CustomerAuth0UserId",
                principalTable: "Customer",
                principalColumn: "Auth0UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerAuth0UserId",
                table: "Order",
                column: "CustomerAuth0UserId",
                principalTable: "Customer",
                principalColumn: "Auth0UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddress_Customer_CustomerAuth0UserId",
                table: "ShippingAddress",
                column: "CustomerAuth0UserId",
                principalTable: "Customer",
                principalColumn: "Auth0UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingAddress_Customer_CustomerAuth0UserId",
                table: "BillingAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerAuth0UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddress_Customer_CustomerAuth0UserId",
                table: "ShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddress_CustomerAuth0UserId",
                table: "ShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerAuth0UserId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_BillingAddress_CustomerAuth0UserId",
                table: "BillingAddress");

            migrationBuilder.DropColumn(
                name: "CustomerAuth0UserId",
                table: "ShippingAddress");

            migrationBuilder.DropColumn(
                name: "CustomerAuth0UserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerAuth0UserId",
                table: "BillingAddress");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ShippingAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AlterColumn<int>(
            //    name: "CustomerId",
            //    table: "Customer",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Auth0UserId",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "BillingAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_CustomerId",
                table: "ShippingAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingAddress_CustomerId",
                table: "BillingAddress",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAddress_Customer_CustomerId",
                table: "BillingAddress",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddress_Customer_CustomerId",
                table: "ShippingAddress",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
