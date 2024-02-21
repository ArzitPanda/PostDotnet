using Microsoft.EntityFrameworkCore;
using SlackApi;
using SlackApi.Data;
using SlackApi.Data.Repository;
using SlackApi.Data.Repository.SlackApi.Data.Repository;
using SlackApi.Filter;
using SlackApi.Services.AuthService;
using SlackApi.Services.PostService;
using SlackApi.Services.RelationRequestService;
using SlackApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddScoped<IPostService,PostService>();
builder.Services.AddScoped<IRelationRequestRepository,RelationRequestRepository>();
builder.Services.AddScoped<ICredRepository,CredRepository>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IRelationRequestService,RelationRequestService>();
builder.Services.AddDbContext<SlackDbContext>((options) => {


    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"));



});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
