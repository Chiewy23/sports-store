using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

/*
 * Set up the objects (a.k.a. services) to be used throughout
 * the application, which are accessed through dependency injection.
 * 
 * This specific call sets up the shared objects required by applications
 * using MVC framework and Razor view engine.
 */
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(opts => {
	opts.UseSqlServer(
		config["ConnectionStrings:SportsStoreConnection"]
	);
});

/*
 * Create a service for the IStoreRepository interface
 * which uses EFStoreRepository as the implementation
 * class.
 */
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

/*
 * Display details of the exceptions which occur in the application.
 * Useful furing the development process, but not to be enabled in
 * deployed applications.
 */
app.UseDeveloperExceptionPage();

/*
 * Adds a simple message to HTTP reponses which would otherwise
 * have no body.
 */
app.UseStatusCodePages();

/*
 * Enables support for serving static content from the wwwroot folder.
 */
app.UseStaticFiles();

/*
 * These two methods add the endpoint routing feature to the request
 * pipeline, which is what matches HTTP requests to to the application
 * features (i.e. endpoints) able to produce responses for them.
 */
app.UseRouting();
app.UseEndpoints(endpoints => {
	// The ASP.NET Core routing feature makes it easy to change the URL scheme in an application.
	// This creates URLs which are more appealing to the user by following a more intuitive pattern.
	// For example: http://localhost/?productPage=2 becomes http://localhost/Page2

	/*
	 * /				List the first page of products from all categories.
	 * /Page2			Lists the specified page, showing items from all categories.
	 * /Soccer			Shows the first page of items from a specific category.
	 * /Soccer/Page2	Shows the specified page of items from the specified category.
	 */
	endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
	endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
	endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
	endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });
	endpoints.MapDefaultControllerRoute();
});

SeedData.EnsurePopulated(app);

app.Run();