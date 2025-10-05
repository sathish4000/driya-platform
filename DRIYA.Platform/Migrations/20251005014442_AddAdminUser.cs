using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DRIYA.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("3a3141d1-9e63-4297-a668-3472d7b1fb3c"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("474bdbcb-358d-4c75-a00b-d2b3742c3072"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("642f405e-7179-4f73-a316-ccb48927e8aa"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("aa78daee-4db0-4d36-a847-20a646b940ae"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("bc010147-be82-4537-a679-2b143338bbaa"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("c49ca594-fab4-45f0-ae36-c7ac3dc9ba3d"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e7c2d432-c741-4bb7-a858-5f0c66c09e82"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("ef2c5c4a-6fab-4055-a7a5-d33b5c1752cd"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("581ce298-6dea-43de-b010-59afc9edc083"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("6c148d42-ec9c-4129-83a0-9ff252b306a2"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("6e070922-2105-4ac9-811e-543794cee64d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("011cd43a-db77-419f-ac20-150ad613d212"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01bcb4db-89c3-4e4a-806b-d754a98d35e6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("08a5a665-81b3-422e-8efd-49c085ff59e7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("17d23ce4-ca84-4e7c-b1e3-f2b7365de65a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1d893d87-8a06-4d78-b883-6683aa5043e3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("286acb41-bca4-4974-81d9-5b776a656b33"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2b52c9ce-ecf5-42f6-87be-bea249f8766a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("39ae8dfa-3c18-4593-952f-2403f1850ce9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3c0e6b33-8895-4eea-8fe6-2848c80aaf99"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5131c2e7-0207-455a-8ce9-3fcd271f3708"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5ce41b31-1b4b-4519-b9ce-220176c810e1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7802fe7f-cbbf-4097-9888-419c8be1a67a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("874899cb-2792-45cd-8415-7fe05a8c28ec"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9ca1e39f-3648-4aa9-8dc8-28218dd06ecb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b9d9ace2-da38-4805-9db3-ad7580f84092"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ca6f156a-ce9b-4834-ae9e-76c3496f864a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ce84a851-1611-498a-93c9-cfbc674a3109"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d5b5cfcd-5166-4d25-9317-352bf4e13778"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(830));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(1018));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(1023));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(1028));

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefaultValue", "Description", "FeatureKey", "FeatureType", "IsActive", "IsSystemFeature", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3a3141d1-9e63-4297-a668-3472d7b1fb3c"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1761), null, "false", null, "sso_integration", "Boolean", true, true, "SSO Integration", null, null },
                    { new Guid("474bdbcb-358d-4c75-a00b-d2b3742c3072"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1746), null, "false", null, "advanced_analytics", "Boolean", true, true, "Advanced Analytics", null, null },
                    { new Guid("642f405e-7179-4f73-a316-ccb48927e8aa"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1749), null, "false", null, "custom_branding", "Boolean", true, true, "Custom Branding", null, null },
                    { new Guid("aa78daee-4db0-4d36-a847-20a646b940ae"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1763), null, "false", null, "advanced_security", "Boolean", true, true, "Advanced Security", null, null },
                    { new Guid("bc010147-be82-4537-a679-2b143338bbaa"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1756), null, "false", null, "white_label", "Boolean", true, true, "White Label", null, null },
                    { new Guid("c49ca594-fab4-45f0-ae36-c7ac3dc9ba3d"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1759), null, "false", null, "priority_support", "Boolean", true, true, "Priority Support", null, null },
                    { new Guid("e7c2d432-c741-4bb7-a858-5f0c66c09e82"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1751), null, "true", null, "api_access", "Boolean", true, true, "API Access", null, null },
                    { new Guid("ef2c5c4a-6fab-4055-a7a5-d33b5c1752cd"), new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(1081), null, "true", null, "basic_analytics", "Boolean", true, true, "Basic Analytics", null, null }
                });

            migrationBuilder.InsertData(
                table: "LicensePlans",
                columns: new[] { "Id", "AllowTrial", "CreatedAt", "CreatedBy", "Currency", "Description", "IsActive", "IsSystemPlan", "MaxApiCalls", "MaxStorageGB", "MaxUsers", "MonthlyPrice", "Name", "PlanCode", "TrialDays", "UpdatedAt", "UpdatedBy", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("581ce298-6dea-43de-b010-59afc9edc083"), true, new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(380), null, "USD", "Premium plan for enterprise customers", true, true, 100000, 1000, 100, 299.99m, "Premium", "premium", 14, null, null, 2999.99m },
                    { new Guid("6c148d42-ec9c-4129-83a0-9ff252b306a2"), true, new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(377), null, "USD", "Professional plan for growing businesses", true, true, 10000, 100, 25, 99.99m, "Professional", "pro", 14, null, null, 999.99m },
                    { new Guid("6e070922-2105-4ac9-811e-543794cee64d"), true, new DateTime(2025, 10, 5, 1, 8, 30, 191, DateTimeKind.Utc).AddTicks(272), null, "USD", "Basic plan for small teams", true, true, 1000, 10, 5, 29.99m, "Basic", "basic", 14, null, null, 299.99m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsSystemPermission", "Name", "Resource", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("011cd43a-db77-419f-ac20-150ad613d212"), "Create", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7410), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("01bcb4db-89c3-4e4a-806b-d754a98d35e6"), "Create", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(6611), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("08a5a665-81b3-422e-8efd-49c085ff59e7"), "Update", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7408), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("17d23ce4-ca84-4e7c-b1e3-f2b7365de65a"), "Read", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7373), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("1d893d87-8a06-4d78-b883-6683aa5043e3"), "Delete", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7376), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("286acb41-bca4-4974-81d9-5b776a656b33"), "Read", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7411), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("2b52c9ce-ecf5-42f6-87be-bea249f8766a"), "Read", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7386), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("39ae8dfa-3c18-4593-952f-2403f1850ce9"), "Delete", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7363), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("3c0e6b33-8895-4eea-8fe6-2848c80aaf99"), "Read", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7348), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("5131c2e7-0207-455a-8ce9-3fcd271f3708"), "Read", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7381), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("5ce41b31-1b4b-4519-b9ce-220176c810e1"), "Create", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7365), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("7802fe7f-cbbf-4097-9888-419c8be1a67a"), "Update", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7351), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("874899cb-2792-45cd-8415-7fe05a8c28ec"), "Delete", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7417), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("9ca1e39f-3648-4aa9-8dc8-28218dd06ecb"), "Create", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7385), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("b9d9ace2-da38-4805-9db3-ad7580f84092"), "Update", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7374), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("ca6f156a-ce9b-4834-ae9e-76c3496f864a"), "Update", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7413), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("ce84a851-1611-498a-93c9-cfbc674a3109"), "Update", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7383), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("d5b5cfcd-5166-4d25-9317-352bf4e13778"), "Create", new DateTime(2025, 10, 5, 1, 8, 30, 190, DateTimeKind.Utc).AddTicks(7378), null, null, true, true, "Billing Management", "Billing", null, null }
                });
        }
    }
}
