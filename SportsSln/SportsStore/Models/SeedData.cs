using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
	public static class SeedData {
		public static void EnsurePopulated(IApplicationBuilder app) {
			StoreDbContext context = app.ApplicationServices
										.CreateScope()
										.ServiceProvider
										.GetRequiredService<StoreDbContext>();

			if (context.Database.GetPendingMigrations().Any()) {
				context.Database.Migrate();
			}

			if(!context.Products.Any()) {
				context.Products.AddRange(
					new Product {
						Name= "Kayak",
						Description="Description",
						Category="Watersports",
						Price= 275,
					},
					new Product {
						Name="Lifejacket",
						Description = "Description",
						Category = "Watersports",
						Price = 275,
					},
					new Product {
						Name = "Soccer Ball",
						Description = "Description",
						Category = "Soccer",
						Price = 19.05m
					},
					new Product {
						Name = "Corner Flags",
						Description = "Description",
						Category = "Soccer",
						Price = 34.95m
					},
					new Product {
						Name = "Stadium",
						Description = "Description",
						Category = "Soccer",
						Price = 79500
					},
					new Product {
						Name = "Thinking Cap",
						Description = "Description",
						Category = "Chess",
						Price = 16
					},
					new Product {
						Name = "Unsteady Chair",
						Description = "Description",
						Category = "Chess",
						Price = 29.95m
					},
					new Product {
						Name = "Human Chess Board",
						Description = "Description",
						Category = "Chess",
						Price = 75
					},
					new Product {
						Name = "Supa Expensive Sunglasses",
						Description = "Description",
						Category = "Chess",
						Price = 400
					}
				);

				context.SaveChanges();
			}
		}
	}
}
