using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AzureRedisCacheApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISBN10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Edition", "ISBN10", "ISBN13", "Language", "Published", "Publisher", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, "Robert C. Martin (Author)", "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way.", "1st", "9780132350884", "978-0132350884", "English", new DateTime(2008, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0.0, "Clean Code: A Handbook of Agile Software Craftsmanship" },
                    { 2, "Erich Gamma (Author), Richard Helm (Author), Ralph Johnson (Author), John Vlissides (Author), Grady Booch (Foreword)", "Capturing a wealth of experience about the design of object-oriented software, four top-notch designers present a catalog of simple and succinct solutions to commonly occurring design problems. Previously undocumented, these 23 patterns allow designers to create more flexible, elegant, and ultimately reusable designs without having to rediscover the design solutions themselves.", "1st", "0201633612", "978-0201633610", "English", new DateTime(1994, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pearson", 0.0, "Design Patterns: Elements of Reusable Object-Oriented Software" },
                    { 3, "Eric Freeman (Author), Bert Bates (Author), Kathy Sierra (Author), Elisabeth Robson (Author)", "At any given moment, someone struggles with the same software design problems you have. And, chances are, someone else has already solved your problem. This edition of Head First Design Patterns—now updated for Java 8—shows you the tried-and-true, road-tested patterns used by developers to create functional, elegant, reusable, and flexible software.", "1st", "9780596007126", "978-0596007126", "English", new DateTime(2004, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "O'Reilly Media", 0.0, "Head First Design Patterns: A Brain-Friendly Guide" },
                    { 4, "Andrew Hunt (Author), David Thomas (Author)", "Ward Cunningham Straight from the programming trenches, The Pragmatic Programmer cuts through the increasing specialization and technicalities of modern software development to examine the core process--taking a requirement and producing working, maintainable code that delights its users.", "1st", "9780201616224", "978-0201616224", "English", new DateTime(1999, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Addison-Wesley Professional", 0.0, "The Pragmatic Programmer: From Journeyman to Master" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
