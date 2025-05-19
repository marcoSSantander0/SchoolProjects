using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
    
builder.Services.AddControllersWithViews();

// Add Razor Pages
builder.Services.AddScoped<InstructorData>();
builder.Services.AddScoped<AlumnoData>();
builder.Services.AddScoped<CursoData>();
builder.Services.AddScoped<GrupoData>();
builder.Services.AddScoped<GrupoCursoData>();
builder.Services.AddScoped<GrupoCursoAlumnoData>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{   
    //Verificar si los roles existen en la base de datos, sino se crean
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = new[] { "Admin", "Docente" };
    foreach (var role in roles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    //Asignar rol admin a usuario
    
    /*var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var adminUser = await userManager.FindByEmailAsync("zeromercy06@gmail.com");

    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }*/
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

await app.RunAsync();
