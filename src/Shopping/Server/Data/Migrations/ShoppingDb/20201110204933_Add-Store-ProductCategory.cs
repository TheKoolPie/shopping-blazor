using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopping.Server.Data.Migrations.ShoppingDb
{
    public partial class AddStoreProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreProductCategories",
                columns: table => new
                {
                    StoreId = table.Column<string>(nullable: false),
                    ProductCategoryId = table.Column<string>(nullable: false),
                    RankingValue = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductCategories", x => new { x.ProductCategoryId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_StoreProductCategories_Categories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProductCategories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductCategories_ProductCategoryId",
                table: "StoreProductCategories",
                column: "ProductCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductCategories_StoreId",
                table: "StoreProductCategories",
                column: "StoreId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProductCategories");
        }
    }
}
