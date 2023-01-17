using AzureRedisCacheApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzureRedisCacheApi.Persistence.Seed
{
	public static class DbInitializer
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>(x =>
			{
				x.HasData(new Book
				{
					Id = 1,
					Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
					Description = "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way.",
					Author = "Robert C. Martin (Author)",
					Publisher = "",
					Language = "English",
					Edition = "1st",
					ISBN10 = "9780132350884",
					ISBN13 = "978-0132350884",
					Published = DateTime.Parse("Aug 1, 2008")
				});
				
				x.HasData(new Book
				{
					Id = 2,
					Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
					Description = "Capturing a wealth of experience about the design of object-oriented software, four top-notch designers present a catalog of simple and succinct solutions to commonly occurring design problems. Previously undocumented, these 23 patterns allow designers to create more flexible, elegant, and ultimately reusable designs without having to rediscover the design solutions themselves.",
					Author = "Erich Gamma (Author), Richard Helm (Author), Ralph Johnson (Author), John Vlissides (Author), Grady Booch (Foreword)",
					Publisher = "Pearson",
					Language = "English",
					Edition = "1st",
					ISBN10 = "0201633612",
					ISBN13 = "978-0201633610",
					Published = DateTime.Parse("Oct 31, 1994")
				});
				
				x.HasData(new Book
				{
					Id = 3,
					Title = "Head First Design Patterns: A Brain-Friendly Guide",
					Description = "At any given moment, someone struggles with the same software design problems you have. And, chances are, someone else has already solved your problem. This edition of Head First Design Patterns—now updated for Java 8—shows you the tried-and-true, road-tested patterns used by developers to create functional, elegant, reusable, and flexible software.",
					Author = "Eric Freeman (Author), Bert Bates (Author), Kathy Sierra (Author), Elisabeth Robson (Author)",
					Publisher = "O'Reilly Media",
					Language = "English",
					Edition = "1st",
					ISBN10 = "9780596007126",
					ISBN13 = "978-0596007126",
					Published = DateTime.Parse("Oct 1, 2004")
				});
				
				x.HasData(new Book
				{
					Id = 4,
					Title = "The Pragmatic Programmer: From Journeyman to Master",
					Description = "Ward Cunningham Straight from the programming trenches, The Pragmatic Programmer cuts through the increasing specialization and technicalities of modern software development to examine the core process--taking a requirement and producing working, maintainable code that delights its users.",
					Author = "Andrew Hunt (Author), David Thomas (Author)",
					Publisher = "Addison-Wesley Professional",
					Language = "English",
					Edition = "1st",
					ISBN10 = "9780201616224",
					ISBN13 = "978-0201616224",
					Published = DateTime.Parse("Oct 30, 1999")
				});
			});
		}
	}
}
