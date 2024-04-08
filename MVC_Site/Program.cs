using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Site;
using MVC_Site.Data;
using MVC_Site.Models;
using MVC_Site.Services.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<ApplicationUser>();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc(x => x.EnableEndpointRouting = false );
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

app.MapGroup("/Account").MapIdentityApi<ApplicationUser>();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHsts();
app.UseHttpMethodOverride();

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
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.UseAntiforgery();
app.MapRazorPages();

app.UseMvcWithDefaultRoute();

app.Run();
