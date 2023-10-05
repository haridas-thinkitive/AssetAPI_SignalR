using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetAlluser;
using TemplateTrack.Core.Interface.AssetOperation;
using TemplateTrack.Core.Interface.BatchAssetOp;
using TemplateTrack.Core.Interface.CheckInAsset;
using TemplateTrack.Core.Interface.IAssetAll;
using TemplateTrack.Core.Interface.LoginAsset;
using TemplateTrack.Core.Interface.Register;
using TemplateTrack.Core.Interface.TrackAssetInfo;
using TemplateTrack.Core.Services;
using TemplateTrack.Core.Services.AssetOperationService;
using TemplateTrack.Core.Services.AssetService;
using TemplateTrack.Core.Services.BatchAssetS;
using TemplateTrack.Core.Services.CheckInService;
using TemplateTrack.Core.Services.LoginService;
using TemplateTrack.Core.Services.RegistrationService;
using TemplateTrack.Core.Services.TrackAssetInfo;
using TemplateTrack.DataAccess.Model.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Connection String
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Identity Configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// HTTP Client
builder.Services.AddHttpClient();

// Scoped Services
builder.Services.AddScoped<IAllAssetUserService, AllAssetUserService>();
builder.Services.AddScoped<IAssetLogin, AssetLoginServices>();
builder.Services.AddScoped<IAllAsset, AllAssetService>();
builder.Services.AddScoped<IAssetOperation, AssetOperationServices>();
builder.Services.AddScoped<IBatchAsset, BatchAssetService>();
builder.Services.AddScoped<IcheckInAsset, CheckInAssetService>();
builder.Services.AddScoped<ITrackAssetInfo, TrackAssetInfoServices>();
builder.Services.AddScoped<IRegister, UserRegistrationService>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter The Valid Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .WithOrigins("http://localhost:4200", "http://localhost:51131")
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader()
    
    );

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<TableDataHub>("/TableDataHub");

app.Run();
