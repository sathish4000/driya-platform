using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DRIYA.Platform.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDecimalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("1a6b74ca-26f2-4cb6-9754-2e15327e527f"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("5c4af8b1-64ca-405c-94bd-93fe437bd12b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("9a37e1b8-fdb7-435d-aecf-6115fdce8b14"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a9f94f1e-ff56-48b5-92e6-fa6ef0a85398"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("af0626f4-b2ab-4df5-afd4-8ad42e0ab077"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("b5a75ae9-5dd2-4a57-a9b8-c8689b61978c"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("cbe71b91-78cd-437b-baf5-8f523427074d"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("db861713-9542-485a-a2c5-360fe77b1fdf"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("0697f369-4ac9-44c2-a762-9a711f5fc9f5"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("d0f16e34-e034-490f-9aec-1600d5ef3d76"));

            migrationBuilder.DeleteData(
                table: "LicensePlans",
                keyColumn: "Id",
                keyValue: new Guid("ea64641f-dc97-43a0-b28b-90fb5ac8367b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01986367-0b38-438a-9f44-15eab7fdd50e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1c79f572-eefb-4492-89b5-d5971db791ac"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1f82b577-a00a-42c0-af35-9a781e00e9fb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2c31d2ca-8211-4a42-a2d8-60f5b72a501b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2f7d79a8-90ec-4b08-a69b-2db15bea8ac8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3870f6ea-a502-4aa5-8d28-a405d136b797"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("47133edf-f7a0-4715-9150-098b5bf08a2d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5b739450-06d5-4fd9-bea5-30fbda2217b7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("77ba2224-e6a1-4d5b-88d9-23b551c351a5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7ec4d941-225b-4c6b-8e2e-9a7e64e47183"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8d79dd95-228f-4fac-bb70-591e31351178"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8de5754e-9b63-4ddf-9de2-0a6c756f6934"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9ac9bb7e-8e91-41d7-a994-2df6de4bcd22"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ac6cd512-1db7-4129-930c-841046ee2d6d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("acc83e75-9e4d-45ca-a5aa-8577a4f3007e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e7bb773f-1d47-46fb-9c72-fa64bb268c2a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f7c160b9-2138-4f74-b6c3-50eca1423528"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fb38fa8e-940f-4046-b748-82ff293f33e0"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionFeePercentage",
                table: "PaymentGateways",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionFeePercentage",
                table: "PaymentGateways",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1344));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1533));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1543));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1553));

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefaultValue", "Description", "FeatureKey", "FeatureType", "IsActive", "IsSystemFeature", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1a6b74ca-26f2-4cb6-9754-2e15327e527f"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4340), null, "true", null, "api_access", "Boolean", true, true, "API Access", null, null },
                    { new Guid("5c4af8b1-64ca-405c-94bd-93fe437bd12b"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4355), null, "false", null, "advanced_security", "Boolean", true, true, "Advanced Security", null, null },
                    { new Guid("9a37e1b8-fdb7-435d-aecf-6115fdce8b14"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(3627), null, "true", null, "basic_analytics", "Boolean", true, true, "Basic Analytics", null, null },
                    { new Guid("a9f94f1e-ff56-48b5-92e6-fa6ef0a85398"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4352), null, "false", null, "sso_integration", "Boolean", true, true, "SSO Integration", null, null },
                    { new Guid("af0626f4-b2ab-4df5-afd4-8ad42e0ab077"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4342), null, "false", null, "white_label", "Boolean", true, true, "White Label", null, null },
                    { new Guid("b5a75ae9-5dd2-4a57-a9b8-c8689b61978c"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4338), null, "false", null, "custom_branding", "Boolean", true, true, "Custom Branding", null, null },
                    { new Guid("cbe71b91-78cd-437b-baf5-8f523427074d"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4351), null, "false", null, "priority_support", "Boolean", true, true, "Priority Support", null, null },
                    { new Guid("db861713-9542-485a-a2c5-360fe77b1fdf"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4334), null, "false", null, "advanced_analytics", "Boolean", true, true, "Advanced Analytics", null, null }
                });

            migrationBuilder.InsertData(
                table: "LicensePlans",
                columns: new[] { "Id", "AllowTrial", "CreatedAt", "CreatedBy", "Currency", "Description", "IsActive", "IsSystemPlan", "MaxApiCalls", "MaxStorageGB", "MaxUsers", "MonthlyPrice", "Name", "PlanCode", "TrialDays", "UpdatedAt", "UpdatedBy", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("0697f369-4ac9-44c2-a762-9a711f5fc9f5"), true, new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(2655), null, "USD", "Premium plan for enterprise customers", true, true, 100000, 1000, 100, 299.99m, "Premium", "premium", 14, null, null, 2999.99m },
                    { new Guid("d0f16e34-e034-490f-9aec-1600d5ef3d76"), true, new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(2651), null, "USD", "Professional plan for growing businesses", true, true, 10000, 100, 25, 99.99m, "Professional", "pro", 14, null, null, 999.99m },
                    { new Guid("ea64641f-dc97-43a0-b28b-90fb5ac8367b"), true, new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(2542), null, "USD", "Basic plan for small teams", true, true, 1000, 10, 5, 29.99m, "Basic", "basic", 14, null, null, 299.99m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsSystemPermission", "Name", "Resource", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("01986367-0b38-438a-9f44-15eab7fdd50e"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8940), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("1c79f572-eefb-4492-89b5-d5971db791ac"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8948), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("1f82b577-a00a-42c0-af35-9a781e00e9fb"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8906), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("2c31d2ca-8211-4a42-a2d8-60f5b72a501b"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8912), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("2f7d79a8-90ec-4b08-a69b-2db15bea8ac8"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8946), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("3870f6ea-a502-4aa5-8d28-a405d136b797"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8017), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("47133edf-f7a0-4715-9150-098b5bf08a2d"), "Delete", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8950), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("5b739450-06d5-4fd9-bea5-30fbda2217b7"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8945), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("77ba2224-e6a1-4d5b-88d9-23b551c351a5"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8903), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("7ec4d941-225b-4c6b-8e2e-9a7e64e47183"), "Delete", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8872), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("8d79dd95-228f-4fac-bb70-591e31351178"), "Delete", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8908), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("8de5754e-9b63-4ddf-9de2-0a6c756f6934"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8870), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("9ac9bb7e-8e91-41d7-a994-2df6de4bcd22"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8942), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("ac6cd512-1db7-4129-930c-841046ee2d6d"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8909), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("acc83e75-9e4d-45ca-a5aa-8577a4f3007e"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8915), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("e7bb773f-1d47-46fb-9c72-fa64bb268c2a"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8938), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("f7c160b9-2138-4f74-b6c3-50eca1423528"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8858), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("fb38fa8e-940f-4046-b748-82ff293f33e0"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8874), null, null, true, true, "Tenant Management", "Tenant", null, null }
                });
        }
    }
}
