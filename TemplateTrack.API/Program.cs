using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetAlluser;
using TemplateTrack.Core.Interface.AssetOperation;
using TemplateTrack.Core.Interface.BatchAssetOp;
using TemplateTrack.Core.Interface.CheckInAsset;
using TemplateTrack.Core.Interface.IAssetAll;
using TemplateTrack.Core.Interface.LoginAsset;
using TemplateTrack.Core.Interface.TrackAssetInfo;
using TemplateTrack.Core.Services;
using TemplateTrack.Core.Services.AssetOperationService;
using TemplateTrack.Core.Services.AssetService;
using TemplateTrack.Core.Services.BatchAssetS;
using TemplateTrack.Core.Services.CheckInService;
using TemplateTrack.Core.Services.LoginService;
using TemplateTrack.Core.Services.TrackAssetInfo;
using TemplateTrack.DataAccess.Model.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();
builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAllAssetUserService, AllAssetUserService>();
builder.Services.AddScoped<IAssetLogin, AssetLoginServices>();
builder.Services.AddScoped<IAllAsset, AllAssetService>();
builder.Services.AddScoped<IAssetOperation, AssetOperationServices>();
builder.Services.AddScoped<IBatchAsset, BatchAssetService>();
builder.Services.AddScoped<IcheckInAsset, CheckInAssetService>();
builder.Services.AddScoped<ITrackAssetInfo, TrackAssetInfoServices>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options


.WithOrigins("http://localhost:54400", "http://localhost:54494")
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
