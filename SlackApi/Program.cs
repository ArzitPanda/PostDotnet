using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SlackApi;
using SlackApi.Data;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SlackApi.Data.Repository.SlackApi.Data.Repository;
using SlackApi.Filter;
using SlackApi.Services.AuthService;
using SlackApi.Services.FeedService;
using SlackApi.Services.PostService;
using SlackApi.Services.RelationRequestService;
using SlackApi.Services.RelationService;
using SlackApi.Services.UserService;
using SlackApi.Utils;
using SocialTree.Services.CommentService;
using SocialTree.Services.ConverterService;
using SocialTree.Services.LikeService;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<User>("Users");
    return builder.GetEdmModel();
}









builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(20)
        .Count()
        .Expand()
    )

    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard authorization using bearing schema",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey

    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});




builder.Services.AddCors((options) => {


    options.AddPolicy("AllowSpecificOrigins", builder => {

        builder.WithOrigins( "http://localhost:5002").AllowAnyHeader().AllowAnyMethod() ;

    });

   



});



builder.Services.AddCors((options) =>
{
    options.AddPolicy("FrontendAllow", builder =>
    {

        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials().AllowAnyMethod().SetIsOriginAllowed(hostName => true) ; 

    });
});

builder.Services.AddAuthentication((options) =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



}).AddJwtBearer((options) =>
{
    options.Authority = "http://localhost:5002"; // URL of your authentication server
    options.Audience = "http://localhost:5283"; // Audience of your API
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Validate the token's issuer
        ValidateAudience = true, // Validate the token's audience
        ValidateLifetime = true, // Validate the token's lifetime
        ValidateIssuerSigningKey = true, // Validate the token's signature

        ValidIssuer = "http://localhost:5002", // Expected issuer of the token
        ValidAudience = "http://localhost:5283", // Expected audience of the token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)) // Signing key for token validation
    };


});




builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<ImageUploadUtils>(provider =>
{
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var baseUrl = $"{request.Scheme}://{request.Host}";
    return new ImageUploadUtils(baseUrl);
});
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddScoped<IPostService,PostService>();
builder.Services.AddScoped<IRelationRequestRepository,RelationRequestRepository>();
builder.Services.AddScoped<ICredRepository,CredRepository>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IRelationRequestService,RelationRequestService>();
builder.Services.AddScoped<IRelationalRepository,RelationalRepository>();
builder.Services.AddScoped<IRelationService,RelationService>();
builder.Services.AddScoped<IFeedService,FeedService>();

builder.Services.AddScoped<IConverter,Converter>();

builder.Services.AddScoped<ILikeService,LikeService>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddDbContext<SlackDbContext>((options) => {


    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"));



});


builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(
    
    
    );
app.UseSession();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowSpecificOrigins");
app.UseCors("FrontendAllow");
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
