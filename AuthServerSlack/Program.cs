using Microsoft.EntityFrameworkCore;
using SlackApi.Data.Repository;
using SlackApi.Data;
using SlackApi.Services.AuthService;
using SlackApi.Services.PostService;
using SlackApi.Services.RelationRequestService;
using SlackApi.Services.UserService;
using SlackApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRelationRequestRepository, RelationRequestRepository>();
builder.Services.AddScoped<ICredRepository, CredRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRelationRequestService, RelationRequestService>();
builder.Services.AddDbContext<SlackDbContext>((options) => {


    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"));



});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddCors((options) => {


    options.AddPolicy("AllowSpecificOrigins", builder => {

        builder.WithOrigins("http://localhost:5283","*").AllowAnyHeader().AllowAnyOrigin();
    
    });



});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
