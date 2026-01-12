var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Points to your Training controller instead of Home
    app.UseExceptionHandler("/Training/Index");
    app.UseHsts();
}

// COMMENT THIS OUT for Codespaces/Mac to avoid port errors
// app.UseHttpsRedirection(); 

// ESSENTIAL: Allows the browser to see files in wwwroot
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Default route setup
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Training}/{action=Index}/{id?}");

app.Run();