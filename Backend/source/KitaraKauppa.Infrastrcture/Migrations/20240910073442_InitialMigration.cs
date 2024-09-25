using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KitaraKauppa.Infrastrcture.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    city_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_alt = table.Column<string>(type: "text", nullable: false),
                    extention = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    varient_type = table.Column<string>(type: "text", nullable: false),
                    orientation = table.Column<string>(type: "text", nullable: false),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    user_role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_user_active = table.Column<bool>(type: "boolean", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_user_roles_user_role_id",
                        column: x => x.user_role_id,
                        principalTable: "user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "color_product",
                columns: table => new
                {
                    colors_id = table.Column<Guid>(type: "uuid", nullable: false),
                    products_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_color_product", x => new { x.colors_id, x.products_id });
                    table.ForeignKey(
                        name: "fk_color_product_colors_colors_id",
                        column: x => x.colors_id,
                        principalTable: "colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_color_product_products_products_id",
                        column: x => x.products_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "image_product",
                columns: table => new
                {
                    images_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_image_product", x => new { x.images_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_image_product_images_images_id",
                        column: x => x.images_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_image_product_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_line2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    city_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_addresses_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_addresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_contact_numbers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_contact_numbers", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_contact_numbers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_credentials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_credentials", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_credentials_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    order_status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_user_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "user_addresses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_id = table.Column<Guid>(type: "uuid", nullable: false),
                    orientation = table.Column<string>(type: "text", nullable: false),
                    units = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.CheckConstraint("CK_OrderItem_Units", "\"units\" > 0");
                    table.ForeignKey(
                        name: "fk_order_items_colors_color_id",
                        column: x => x.color_id,
                        principalTable: "colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("4a02d3b7-c66d-4510-9ab5-191800cc3d49"), "Takamine" },
                    { new Guid("53479238-506e-4abd-940c-449bddb101fa"), "Sigma" },
                    { new Guid("74074506-e224-4d37-8286-308958a125cd"), "Yamaha" },
                    { new Guid("7e95231a-ed06-440a-b561-d8c2d8d2f97e"), "PRS" },
                    { new Guid("81a253d0-a07c-4cd5-a838-a200279cba3f"), "Ibanez" },
                    { new Guid("8996e7b6-755c-4cf8-8512-294c7916bdd4"), "Gibson" },
                    { new Guid("d38c4b9e-b13b-4aee-ad44-dd55db7e3919"), "Fender" },
                    { new Guid("d95ee07d-f51a-4322-b4ce-b1bdbf4443cb"), "Martin" }
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "city_name" },
                values: new object[,]
                {
                    { new Guid("0dfb4d4e-f694-4101-b0e6-e04c73c40584"), "Espoo" },
                    { new Guid("1f819ac4-52a1-4c27-ad03-3ba21159e85a"), "Helsinki" },
                    { new Guid("62046a0d-177f-4611-869b-cc14ff44ecef"), "Oulu" },
                    { new Guid("99950502-4beb-45a0-ab95-5285f0e9a517"), "Vantaa" },
                    { new Guid("ac0a350a-fdff-46d0-9839-e2057a81d62a"), "Tampere" },
                    { new Guid("b423dcc6-00d2-4a9a-871d-e8900fd48c97"), "Turku" }
                });

            migrationBuilder.InsertData(
                table: "colors",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("214ee9ef-c53a-4021-8378-fcbd67ab0cf0"), "White" },
                    { new Guid("32c1c9ba-bcfb-41bd-a1d5-12def60bc714"), "Black" },
                    { new Guid("5ac0cc75-623b-4968-a4f7-aeb38100077a"), "Red" },
                    { new Guid("7fdc99fb-0738-4eab-9692-93b3848a4f10"), "Yellow" },
                    { new Guid("85e730db-f1a1-47a8-b8c7-ffe52f1a511e"), "Green" },
                    { new Guid("d66fd662-8176-4b20-bbb1-0b4844a271d1"), "Blue" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "user_role_name" },
                values: new object[,]
                {
                    { new Guid("c2a1c420-c068-48b5-854c-ffb247904891"), "Admin" },
                    { new Guid("e08e5d7f-8d13-4e07-8ed0-29268b1a59d8"), "Customer" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_id", "created_at", "description", "orientation", "title", "unit_price", "varient_type" },
                values: new object[,]
                {
                    { new Guid("1edf02b3-9dd3-4625-aebc-326badf35cbc"), new Guid("53479238-506e-4abd-940c-449bddb101fa"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2664), "The Sigma 000M-15 is a 000 body size acoustic guitar with a solid mahogany top, back and sides. The 000M-15 is a 000 body size acoustic guitar with a solid mahogany top, back and sides. The 000M-15 is a 000 body size acoustic guitar with a solid mahogany top, back and sides.", "BothOptions", "Sigma 000M-15", 500m, "Accoustic" },
                    { new Guid("25435b04-9a85-4577-be4b-8816c287810c"), new Guid("d38c4b9e-b13b-4aee-ad44-dd55db7e3919"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2637), "The Fender Stratocaster is a model of electric guitar designed from 1952 into 1954 by Leo Fender, Bill Carson, George Fullerton and Freddie Tavares. The Fender Musical Instruments Corporation has continuously manufactured the Stratocaster from 1954 to the present. It is a double-cutaway guitar, with an extended top horn shape for balance. Along with the Gibson Les Paul and Fender Telecaster, it is one of the most-often emulated electric guitar shapes.", "RightHanded", "Fender Stratocaster", 1000m, "Electric" },
                    { new Guid("275b7cb3-df3d-4d11-bbc1-c0664e270f25"), new Guid("8996e7b6-755c-4cf8-8512-294c7916bdd4"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2647), "The Gibson Les Paul is a solid body electric guitar that was first sold by the Gibson Guitar Corporation in 1952. The Les Paul was designed by Gibson president Ted McCarty, factory manager John Huis and their team with input from and endorsement by guitarist Les Paul. Its design typically comprises a solid mahogany body with a carved maple top and a single cutaway, a mahogany set-in neck with a rosewood fretboard, two pickups with independent volume and tone controls, and a stoptail bridge, although variants exist.", "LeftHanded", "Gibson Les Paul", 1500m, "Electric" },
                    { new Guid("6e7fe926-0695-4b92-9e5d-f26fb0bfdead"), new Guid("81a253d0-a07c-4cd5-a838-a200279cba3f"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2652), "The Ibanez RG is a series of electric guitars produced by Hoshino Gakki and one of the best-selling superstrat-style hard rock/heavy metal guitars ever made. The first in the series, RG550, was originally released in 1987 and advertised as part of the Roadstar series. The RG series is a line of solid body electric guitars produced by Hoshino Gakki and sold under the Ibanez brand. The series was introduced in 1987 as the Roadstar (RG) series and expanded in 1992.", "BothOptions", "Ibanez RG", 800m, "Electric" },
                    { new Guid("a73368de-2bb9-4a1a-a08f-5b451f7a5515"), new Guid("74074506-e224-4d37-8286-308958a125cd"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2673), "The Yamaha FG800 is a dreadnought acoustic guitar with a solid spruce top and nato back and sides. The FG800 is a dreadnought acoustic guitar with a solid spruce top and nato back and sides. The FG800 is a dreadnought acoustic guitar with a solid spruce top and nato back and sides.", "LeftHanded", "Yamaha FG800", 400m, "Accoustic" },
                    { new Guid("bb5abee4-5507-48c6-a82d-e432967c5141"), new Guid("d95ee07d-f51a-4322-b4ce-b1bdbf4443cb"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2656), "The Martin D-28 is a dreadnought-style acoustic guitar made by C. F. Martin & Company of Nazareth, Pennsylvania. It is widely regarded as the instrument that set the standard for the dreadnought guitar style. The D-28 is the standard by which many steel-string guitars are measured and is often used as a reference for the sound of acoustic guitars.", "RightHanded", "Martin D-28", 2000m, "Accoustic" },
                    { new Guid("d81feab3-b560-44e6-b31b-704e08f5a591"), new Guid("4a02d3b7-c66d-4510-9ab5-191800cc3d49"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2669), "The Takamine GD20-NS is a dreadnought acoustic guitar with a solid cedar top and mahogany back and sides. The GD20-NS is a dreadnought acoustic guitar with a solid cedar top and mahogany back and sides. The GD20-NS is a dreadnought acoustic guitar with a solid cedar top and mahogany back and sides.", "RightHanded", "Takamine GD20-NS", 600m, "Accoustic" },
                    { new Guid("e8df56e4-d3b6-4587-9102-160651f476f8"), new Guid("7e95231a-ed06-440a-b561-d8c2d8d2f97e"), new DateTime(2024, 9, 10, 7, 34, 42, 47, DateTimeKind.Utc).AddTicks(2660), "The PRS Custom 24 is the original PRS—the guitar Paul Reed Smith took to his first tradeshow in 1985. A perennial favorite with musicians, this model features a patented PRS tremolo system, PRS Phase III locking tuners, and a pair of humbucking pickups. The Custom 24 is the original PRS. Since its introduction, it has offered a unique tonal option for serious players and defined PRS tone and playability.", "LeftHanded", "PRS Custom 24", 2500m, "Electric" }
                });

            migrationBuilder.InsertData(
                table: "color_product",
                columns: new[] { "colors_id", "products_id" },
                values: new object[,]
                {
                    { new Guid("214ee9ef-c53a-4021-8378-fcbd67ab0cf0"), new Guid("25435b04-9a85-4577-be4b-8816c287810c") },
                    { new Guid("214ee9ef-c53a-4021-8378-fcbd67ab0cf0"), new Guid("275b7cb3-df3d-4d11-bbc1-c0664e270f25") },
                    { new Guid("214ee9ef-c53a-4021-8378-fcbd67ab0cf0"), new Guid("a73368de-2bb9-4a1a-a08f-5b451f7a5515") },
                    { new Guid("214ee9ef-c53a-4021-8378-fcbd67ab0cf0"), new Guid("d81feab3-b560-44e6-b31b-704e08f5a591") },
                    { new Guid("32c1c9ba-bcfb-41bd-a1d5-12def60bc714"), new Guid("25435b04-9a85-4577-be4b-8816c287810c") },
                    { new Guid("32c1c9ba-bcfb-41bd-a1d5-12def60bc714"), new Guid("bb5abee4-5507-48c6-a82d-e432967c5141") },
                    { new Guid("32c1c9ba-bcfb-41bd-a1d5-12def60bc714"), new Guid("d81feab3-b560-44e6-b31b-704e08f5a591") },
                    { new Guid("5ac0cc75-623b-4968-a4f7-aeb38100077a"), new Guid("1edf02b3-9dd3-4625-aebc-326badf35cbc") },
                    { new Guid("5ac0cc75-623b-4968-a4f7-aeb38100077a"), new Guid("275b7cb3-df3d-4d11-bbc1-c0664e270f25") },
                    { new Guid("5ac0cc75-623b-4968-a4f7-aeb38100077a"), new Guid("6e7fe926-0695-4b92-9e5d-f26fb0bfdead") },
                    { new Guid("5ac0cc75-623b-4968-a4f7-aeb38100077a"), new Guid("a73368de-2bb9-4a1a-a08f-5b451f7a5515") },
                    { new Guid("7fdc99fb-0738-4eab-9692-93b3848a4f10"), new Guid("1edf02b3-9dd3-4625-aebc-326badf35cbc") },
                    { new Guid("7fdc99fb-0738-4eab-9692-93b3848a4f10"), new Guid("e8df56e4-d3b6-4587-9102-160651f476f8") },
                    { new Guid("85e730db-f1a1-47a8-b8c7-ffe52f1a511e"), new Guid("6e7fe926-0695-4b92-9e5d-f26fb0bfdead") },
                    { new Guid("85e730db-f1a1-47a8-b8c7-ffe52f1a511e"), new Guid("bb5abee4-5507-48c6-a82d-e432967c5141") },
                    { new Guid("d66fd662-8176-4b20-bbb1-0b4844a271d1"), new Guid("e8df56e4-d3b6-4587-9102-160651f476f8") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_color_product_products_id",
                table: "color_product",
                column: "products_id");

            migrationBuilder.CreateIndex(
                name: "ix_image_product_product_id",
                table: "image_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_color_id",
                table: "order_items",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_brand_id",
                table: "products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_addresses_city_id",
                table: "user_addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_addresses_user_id",
                table: "user_addresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_contact_numbers_user_id",
                table: "user_contact_numbers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_user_id",
                table: "user_credentials",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_credentials_user_name",
                table: "user_credentials",
                column: "user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_user_role_id",
                table: "users",
                column: "user_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "color_product");

            migrationBuilder.DropTable(
                name: "image_product");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "user_contact_numbers");

            migrationBuilder.DropTable(
                name: "user_credentials");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "user_addresses");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_roles");
        }
    }
}
