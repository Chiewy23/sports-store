using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
	public static class SeedData {
		public static void EnsurePopulated(IApplicationBuilder app) {
			StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

			if (context.Database.GetPendingMigrations().Any()) {
				context.Database.Migrate();
			}

			if(!context.Products.Any()) {
				context.Products.AddRange(
					new Product {
						Name= "Kayak",
						Description=string.Empty,
						Category="Watersports",
						Price= 275,
					},
					new Product {
						Name="Lifejacket",
						Description = string.Empty,
						Category = "Watersports",
						Price = 275,
					},
					new Product {
						Name = "Soccer Ball",
						Description = string.Empty,
						Category = "Soccer",
						Price = 19.05m
					},
					new Product {
						Name = "Corner Flags",
						Description = string.Empty,
						Category = "Soccer",
						Price = 34.95m
					},
					new Product {
						Name = "Stadium",
						Description = string.Empty,
						Category = "Soccer",
						Price = 79500
					},
					new Product {
						Name = "Thinking Cap",
						Description = string.Empty,
						Category = "Chess",
						Price = 16
					},
					new Product {
						Name = "Unsteady Chair",
						Description = string.Empty,
						Category = "Chess",
						Price = 29.95m
					},
					new Product {
						Name = "Human Chess Board",
						Description = string.Empty,
						Category = "Chess",
						Price = 75
					},
					new Product {
						Name = "Supa Expensive Sunglasses",
						Description = string.Empty,
						Category = "Chess",
						Price = 400
					}
				);

				context.SaveChanges();
			}
		}
	}
}
