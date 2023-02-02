using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Api.DAL.EF.Migrations
{
    public partial class AddFoodPriceAndQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("0f2c0750-4de7-4944-8581-8f038726203e"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("58cb9ade-2ee2-428b-b208-100fc981d29c"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"));

            migrationBuilder.AddColumn<decimal>(
                name: "FoodPrice",
                table: "FoodOrderNotes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FoodQuantity",
                table: "FoodOrderNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "FoodOrderNotes",
                columns: new[] { "Id", "FoodId", "FoodPrice", "FoodQuantity", "Note", "OrderId" },
                values: new object[,]
                {
                    { new Guid("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"), new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"), 16m, 1, "No onions please", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") },
                    { new Guid("0f2c0750-4de7-4944-8581-8f038726203e"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), 17m, 3, "No cheese please", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") },
                    { new Guid("58cb9ade-2ee2-428b-b208-100fc981d29c"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), 22m, 1, "", new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab") },
                    { new Guid("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), 13m, 2, "Double Cheese", new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab") },
                    { new Guid("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"), new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"), 12m, 5, "No ham please, extra corn", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("39ec26de-182e-4678-8c24-7c448be05a36"),
                columns: new[] { "CreatedDate", "DeliveryTime" },
                values: new object[] { new DateTime(2023, 2, 1, 17, 18, 14, 572, DateTimeKind.Local).AddTicks(6885), new DateTime(2023, 2, 1, 17, 18, 14, 574, DateTimeKind.Local).AddTicks(8636) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab"),
                columns: new[] { "CreatedDate", "DeliveryTime" },
                values: new object[] { new DateTime(2023, 2, 1, 17, 18, 14, 575, DateTimeKind.Local).AddTicks(5189), new DateTime(2023, 2, 1, 17, 18, 14, 575, DateTimeKind.Local).AddTicks(5220) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("0f2c0750-4de7-4944-8581-8f038726203e"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("58cb9ade-2ee2-428b-b208-100fc981d29c"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"));

            migrationBuilder.DropColumn(
                name: "FoodPrice",
                table: "FoodOrderNotes");

            migrationBuilder.DropColumn(
                name: "FoodQuantity",
                table: "FoodOrderNotes");

            migrationBuilder.InsertData(
                table: "FoodOrderNotes",
                columns: new[] { "Id", "FoodId", "Note", "OrderId" },
                values: new object[,]
                {
                    { new Guid("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"), new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "No onions please", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") },
                    { new Guid("0f2c0750-4de7-4944-8581-8f038726203e"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "No cheese please", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") },
                    { new Guid("58cb9ade-2ee2-428b-b208-100fc981d29c"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "", new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab") },
                    { new Guid("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "Double Cheese", new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab") },
                    { new Guid("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"), new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"), "No ham please, extra corn", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("39ec26de-182e-4678-8c24-7c448be05a36"),
                columns: new[] { "CreatedDate", "DeliveryTime" },
                values: new object[] { new DateTime(2022, 10, 8, 12, 58, 16, 715, DateTimeKind.Local).AddTicks(8146), new DateTime(2022, 10, 8, 12, 58, 16, 718, DateTimeKind.Local).AddTicks(2467) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab"),
                columns: new[] { "CreatedDate", "DeliveryTime" },
                values: new object[] { new DateTime(2022, 10, 8, 12, 58, 16, 718, DateTimeKind.Local).AddTicks(8711), new DateTime(2022, 10, 8, 12, 58, 16, 718, DateTimeKind.Local).AddTicks(8728) });
        }
    }
}
