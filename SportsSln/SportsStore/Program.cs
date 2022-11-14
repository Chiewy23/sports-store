var builder = WebApplication.CreateBuilder(args);

/*
 * Set up the objects (a.k.a. services) to be used throughout
 * the application, which are accessed through dependency injection.
 * 
 * This specific call sets up the shared objects required by applications
 * using MVC framework and Razor view engine.
 */
builder.Services.AddControllersWithViews();

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
app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

app.Run();