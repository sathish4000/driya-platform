using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DRIYA.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("02f438fc-8e81-4a56-8ddd-cd5b77df10ed"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("18e017bc-7a2d-4772-a4e9-67dd4f244032"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("62a7e20a-89bc-416e-abb5-de9cd03bf286"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("63623140-4fc3-4019-8ba2-0ff43c54c30f"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("71812898-2f06-4cbe-ab56-686096f9156f"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("7aaeadc7-6de0-4b25-8b19-b3f92f8c49ac"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("cd8acb6f-4ed7-4414-8380-45dbc652ed2c"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e766a30e-a5ef-4a47-a6d5-32731caa5fed"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("270006f9-8531-41b3-9c6a-ab7446c51b3e"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("968f03a1-6cf2-4dc9-8b6f-aa97ecf4b1db"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("bf7db261-f501-4586-ba39-e708b78d9e64"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("079578e1-3c2a-4df1-8cb6-5b6ebd523ebc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0b59ce9e-ab12-46af-943f-438bb4abd9eb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("21071b39-d9d4-4e63-814e-464f2ee0a129"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("25412988-4225-431b-b50c-8badd8e5a25e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("45986554-41eb-4cb9-b044-6d4b6423a021"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4cae40b5-9dc4-45cd-b871-1ffedad4e3e7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("53cf97f8-86ca-4696-9f9b-af354064c7e1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("585cbcf4-fbf4-441e-91ed-cbdcec029b11"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("737ba67a-fc76-42c6-8a7b-4b3818a46401"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("84d01ab3-620d-4961-9cdd-53dcae4d54c5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8cc640b3-b6da-48cd-94eb-a6005a9bc80b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8d30e90c-559b-4447-80ec-e45ac9541490"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8dedd7d1-a3d0-4ec1-b5ee-e2a09f0c434a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9e8cee46-adbe-49bf-88ac-6ad5cb368782"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a56699b2-1b21-4fa0-ad33-c32f0c8d03c6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bfd3c640-5f3f-46a3-b23e-34cf7c3af1f4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("da66ffae-649f-4e68-a6e3-334dd19d5ddb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ff1c851c-a5dc-4065-be8a-75321f96f202"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsMfaEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginIp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MfaSecretKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneConfirmedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AppKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Subdomain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrimaryColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserAccess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserAccess_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserAccess_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "AppKey", "CreatedAt", "Description", "Domain", "Icon", "IsActive", "Name", "PrimaryColor", "Status", "Subdomain", "UpdatedAt" },
                values: new object[] { new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), "platform", new DateTime(2025, 10, 5, 2, 46, 32, 193, DateTimeKind.Utc).AddTicks(5654), "Default platform application", null, "pi pi-cog", true, "DRIYA Platform", "#3b82f6", "Active", "app", new DateTime(2025, 10, 5, 2, 46, 32, 193, DateTimeKind.Utc).AddTicks(5763) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 2, 46, 32, 194, DateTimeKind.Utc).AddTicks(8714));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 2, 46, 32, 194, DateTimeKind.Utc).AddTicks(8932));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 2, 46, 32, 194, DateTimeKind.Utc).AddTicks(8937));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 2, 46, 32, 194, DateTimeKind.Utc).AddTicks(8951));

            migrationBuilder.InsertData(
                table: "LicensePlans",
                columns: new[] { "Id", "AllowTrial", "CreatedAt", "CreatedBy", "Currency", "Description", "IsActive", "IsSystemPlan", "MaxApiCalls", "MaxStorageGB", "MaxUsers", "MonthlyPrice", "Name", "PlanCode", "TrialDays", "UpdatedAt", "UpdatedBy", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("12308453-f1b0-4fbc-975f-1b64369ed0b3"), true, new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(3929), null, "USD", "Professional plan for growing businesses", true, true, 10000, 100, 25, 99.99m, "Professional", "pro", 14, null, null, 999.99m },
                    { new Guid("14af69ad-8b1a-40d2-b715-ea2b052e48b4"), true, new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(3820), null, "USD", "Basic plan for small teams", true, true, 1000, 10, 5, 29.99m, "Basic", "basic", 14, null, null, 299.99m },
                    { new Guid("87c677a0-6327-4e8f-8437-21bdad33eb9b"), true, new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(3932), null, "USD", "Premium plan for enterprise customers", true, true, 100000, 1000, 100, 299.99m, "Premium", "premium", 14, null, null, 2999.99m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsSystemPermission", "Name", "Resource", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("03e9e0f5-3c04-4825-94c1-8549d29071ab"), "Read", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(864), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("0635071b-8c81-4932-b235-ab197076a358"), "Read", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(928), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("08c457e0-19da-4ca2-9444-3afd54938fb6"), "Create", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(920), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("18b60084-cc6f-4eb7-ace7-320465b56184"), "Update", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(906), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("341caa3e-1b4d-43d0-b206-8bf5d8373136"), "Create", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(897), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("3ae1cadd-a463-4aa5-ac25-dc0c63c2eaab"), "Read", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(916), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("58058f70-b94c-4a44-b3da-a62359be8a49"), "Create", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(926), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("77b5ee9e-75ce-4161-a11f-889da0c4a2f5"), "Update", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(924), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("788f932d-471d-47a3-9a33-af4ab9754cd2"), "Create", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(912), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("7a554084-1897-4903-a441-5e680f32aff0"), "Delete", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(933), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("9c925429-05f7-4513-9686-c670c09249f6"), "Delete", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(870), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("ad370943-978a-4099-b284-116cde102f0e"), "Update", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(931), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("b43a18ef-911c-4080-9791-46f07c6a4594"), "Read", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(923), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("b6419610-1c84-4926-b3ad-1aa7e116e7a0"), "Update", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(918), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("c14a3f38-8e22-41ad-823e-18526f1d7fb1"), "Update", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(868), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("dc60d594-c5c3-452f-9567-f0c47f9e0c2e"), "Read", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(905), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("dd606174-b82a-4926-b4de-47a6a2cb37e9"), "Delete", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(908), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("e838282c-4e40-4266-8d45-7f40db206c24"), "Create", new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(192), null, null, true, true, "User Management", "User", null, null }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "ApplicationId", "CreatedAt", "CreatedBy", "DefaultValue", "Description", "FeatureKey", "FeatureType", "IsActive", "IsSystemFeature", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("15818f30-16e9-4212-8a12-ba924fe5c201"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5688), null, "false", null, "advanced_analytics", "Boolean", true, true, "Advanced Analytics", null, null },
                    { new Guid("232289cd-f56a-4fd4-b6f2-9f3af50414ee"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5692), null, "false", null, "custom_branding", "Boolean", true, true, "Custom Branding", null, null },
                    { new Guid("31b80617-7e21-4fdd-9164-87e115edb775"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5711), null, "false", null, "advanced_security", "Boolean", true, true, "Advanced Security", null, null },
                    { new Guid("32b5eaeb-efa3-45ed-83dc-eaeb6655845b"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5698), null, "true", null, "api_access", "Boolean", true, true, "API Access", null, null },
                    { new Guid("4fc5ba51-0fb0-4387-9d41-6d3f69465859"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5705), null, "false", null, "priority_support", "Boolean", true, true, "Priority Support", null, null },
                    { new Guid("55d7d3bf-d8af-487f-a48b-52eac9fa6f69"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5701), null, "false", null, "white_label", "Boolean", true, true, "White Label", null, null },
                    { new Guid("8e034cca-d596-4eaa-9cd2-dc169bf73b44"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(5708), null, "false", null, "sso_integration", "Boolean", true, true, "SSO Integration", null, null },
                    { new Guid("a600ee3b-29db-4084-9575-2a7acb661c8e"), new Guid("dbcc3473-bbdc-4263-a253-23cb50acbee6"), new DateTime(2025, 10, 5, 2, 46, 32, 195, DateTimeKind.Utc).AddTicks(4654), null, "true", null, "basic_analytics", "Boolean", true, true, "Basic Analytics", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ApplicationId",
                table: "Tenants",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ApplicationId",
                table: "Features",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppKey",
                table: "Applications",
                column: "AppKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Subdomain",
                table: "Applications",
                column: "Subdomain",
                unique: true,
                filter: "[Subdomain] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserAccess_ApplicationId",
                table: "ApplicationUserAccess",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserAccess_UserId",
                table: "ApplicationUserAccess",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Applications_ApplicationId",
                table: "Features",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Applications_ApplicationId",
                table: "Tenants",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Applications_ApplicationId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Applications_ApplicationId",
                table: "Tenants");

            migrationBuilder.DropTable(
                name: "ApplicationUserAccess");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_ApplicationId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Features_ApplicationId",
                table: "Features");

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("15818f30-16e9-4212-8a12-ba924fe5c201"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("232289cd-f56a-4fd4-b6f2-9f3af50414ee"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("31b80617-7e21-4fdd-9164-87e115edb775"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("32b5eaeb-efa3-45ed-83dc-eaeb6655845b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("4fc5ba51-0fb0-4387-9d41-6d3f69465859"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("55d7d3bf-d8af-487f-a48b-52eac9fa6f69"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("8e034cca-d596-4eaa-9cd2-dc169bf73b44"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a600ee3b-29db-4084-9575-2a7acb661c8e"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("12308453-f1b0-4fbc-975f-1b64369ed0b3"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("14af69ad-8b1a-40d2-b715-ea2b052e48b4"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("87c677a0-6327-4e8f-8437-21bdad33eb9b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("03e9e0f5-3c04-4825-94c1-8549d29071ab"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0635071b-8c81-4932-b235-ab197076a358"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("08c457e0-19da-4ca2-9444-3afd54938fb6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("18b60084-cc6f-4eb7-ace7-320465b56184"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("341caa3e-1b4d-43d0-b206-8bf5d8373136"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3ae1cadd-a463-4aa5-ac25-dc0c63c2eaab"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("58058f70-b94c-4a44-b3da-a62359be8a49"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("77b5ee9e-75ce-4161-a11f-889da0c4a2f5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("788f932d-471d-47a3-9a33-af4ab9754cd2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7a554084-1897-4903-a441-5e680f32aff0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9c925429-05f7-4513-9686-c670c09249f6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ad370943-978a-4099-b284-116cde102f0e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b43a18ef-911c-4080-9791-46f07c6a4594"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b6419610-1c84-4926-b3ad-1aa7e116e7a0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c14a3f38-8e22-41ad-823e-18526f1d7fb1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dc60d594-c5c3-452f-9567-f0c47f9e0c2e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dd606174-b82a-4926-b4de-47a6a2cb37e9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e838282c-4e40-4266-8d45-7f40db206c24"));

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Features");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMfaEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastLoginIp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MfaSecretKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneConfirmedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 44, 41, 388, DateTimeKind.Utc).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 44, 41, 388, DateTimeKind.Utc).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 44, 41, 388, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 44, 41, 388, DateTimeKind.Utc).AddTicks(7179));

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefaultValue", "Description", "FeatureKey", "FeatureType", "IsActive", "IsSystemFeature", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("02f438fc-8e81-4a56-8ddd-cd5b77df10ed"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1711), null, "false", null, "white_label", "Boolean", true, true, "White Label", null, null },
                    { new Guid("18e017bc-7a2d-4772-a4e9-67dd4f244032"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1719), null, "false", null, "priority_support", "Boolean", true, true, "Priority Support", null, null },
                    { new Guid("62a7e20a-89bc-416e-abb5-de9cd03bf286"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1722), null, "false", null, "sso_integration", "Boolean", true, true, "SSO Integration", null, null },
                    { new Guid("63623140-4fc3-4019-8ba2-0ff43c54c30f"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1689), null, "false", null, "advanced_analytics", "Boolean", true, true, "Advanced Analytics", null, null },
                    { new Guid("71812898-2f06-4cbe-ab56-686096f9156f"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1701), null, "true", null, "api_access", "Boolean", true, true, "API Access", null, null },
                    { new Guid("7aaeadc7-6de0-4b25-8b19-b3f92f8c49ac"), new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(9746), null, "true", null, "basic_analytics", "Boolean", true, true, "Basic Analytics", null, null },
                    { new Guid("cd8acb6f-4ed7-4414-8380-45dbc652ed2c"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1698), null, "false", null, "custom_branding", "Boolean", true, true, "Custom Branding", null, null },
                    { new Guid("e766a30e-a5ef-4a47-a6d5-32731caa5fed"), new DateTime(2025, 10, 5, 1, 44, 41, 391, DateTimeKind.Utc).AddTicks(1725), null, "false", null, "advanced_security", "Boolean", true, true, "Advanced Security", null, null }
                });

            migrationBuilder.InsertData(
                table: "LicensePlans",
                columns: new[] { "Id", "AllowTrial", "CreatedAt", "CreatedBy", "Currency", "Description", "IsActive", "IsSystemPlan", "MaxApiCalls", "MaxStorageGB", "MaxUsers", "MonthlyPrice", "Name", "PlanCode", "TrialDays", "UpdatedAt", "UpdatedBy", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("270006f9-8531-41b3-9c6a-ab7446c51b3e"), true, new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(7774), null, "USD", "Basic plan for small teams", true, true, 1000, 10, 5, 29.99m, "Basic", "basic", 14, null, null, 299.99m },
                    { new Guid("968f03a1-6cf2-4dc9-8b6f-aa97ecf4b1db"), true, new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(8043), null, "USD", "Professional plan for growing businesses", true, true, 10000, 100, 25, 99.99m, "Professional", "pro", 14, null, null, 999.99m },
                    { new Guid("bf7db261-f501-4586-ba39-e708b78d9e64"), true, new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(8049), null, "USD", "Premium plan for enterprise customers", true, true, 100000, 1000, 100, 299.99m, "Premium", "premium", 14, null, null, 2999.99m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsSystemPermission", "Name", "Resource", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("079578e1-3c2a-4df1-8cb6-5b6ebd523ebc"), "Read", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(485), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("0b59ce9e-ab12-46af-943f-438bb4abd9eb"), "Update", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(480), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("21071b39-d9d4-4e63-814e-464f2ee0a129"), "Create", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(492), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("25412988-4225-431b-b50c-8badd8e5a25e"), "Create", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(470), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("45986554-41eb-4cb9-b044-6d4b6423a021"), "Read", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(461), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("4cae40b5-9dc4-45cd-b871-1ffedad4e3e7"), "Delete", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(467), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("53cf97f8-86ca-4696-9f9b-af354064c7e1"), "Delete", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(537), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("585cbcf4-fbf4-441e-91ed-cbdcec029b11"), "Create", new DateTime(2025, 10, 5, 1, 44, 41, 389, DateTimeKind.Utc).AddTicks(7763), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("737ba67a-fc76-42c6-8a7b-4b3818a46401"), "Update", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(464), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("84d01ab3-620d-4961-9cdd-53dcae4d54c5"), "Update", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(490), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("8cc640b3-b6da-48cd-94eb-a6005a9bc80b"), "Delete", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(391), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("8d30e90c-559b-4447-80ec-e45ac9541490"), "Read", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(477), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("8dedd7d1-a3d0-4ec1-b5ee-e2a09f0c434a"), "Read", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(527), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("9e8cee46-adbe-49bf-88ac-6ad5cb368782"), "Create", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(394), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("a56699b2-1b21-4fa0-ad33-c32f0c8d03c6"), "Create", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(482), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("bfd3c640-5f3f-46a3-b23e-34cf7c3af1f4"), "Update", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(530), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("da66ffae-649f-4e68-a6e3-334dd19d5ddb"), "Read", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(374), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("ff1c851c-a5dc-4065-be8a-75321f96f202"), "Update", new DateTime(2025, 10, 5, 1, 44, 41, 390, DateTimeKind.Utc).AddTicks(387), null, null, true, true, "User Management", "User", null, null }
                });
        }
    }
}
