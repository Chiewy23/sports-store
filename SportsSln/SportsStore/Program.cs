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
builder.Services.AddRazorPages();

// Session state is data associated with a series of requests made by a user.
// ASP.NET provides a range of different ways to store session state, including storing
// in memory (the approach used in this project.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// The AddScoped method specifies that the same object should be used to satisfy
// related requests for Cart instances. This means any Cart required by components
// handling the same HTTP request will receive the same object.
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

// Specifies the same object should always be used.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
 * Automatically associate requests with sessions when they arrive
 * from the client.
 */
app.UseSession();

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

	// Registers Razor Pages as endpoints which the URL routing system uses to handle requests.
	endpoints.MapRazorPages();
});

SeedData.EnsurePopulated(app);

app.Run();