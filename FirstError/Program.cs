using FirstApi.Core.Repositories1;
using FirstApi.Data.Contexts;
using FirstApi.Data.Repositories.Implemantations;
using FirstApi.Service.Profiles.Categories;
using FirstApi.Service.Validations.Categories;
using FirstApi.Services.Implementations;
using FirstApi.Services.Interfaces;
using FirstError.Core.Entities;
using FirstError.Core.Repositories1;
using FirstError.Data.Repositoriess.Implemantations;
using FirstError.Service.Services.Implementations;
using FirstError.Service.Services.Interfaces;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));


});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    //istesek sertleri,validasiyni,buradada yaza bilerik

}).AddDefaultTokenProviders().AddEntityFrameworkStores<ApiDbContext>();


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //. Yani, uygulama JWT ile korunan kaynaklara erişim sağlamak istediğinde bu yapılandırma kullanılacak.

})
    .AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))

    };
});

builder.Services.AddControllers().AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());

builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<ILogService,LogService>();
builder.Services.AddHttpContextAccessor();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddCors(o => o.AddPolicy("P136", builder =>
{
    builder.AllowAnyOrigin() 
           .AllowAnyMethod()
           .AllowAnyHeader();//cors politikasi deyilir
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("admin_v1", new OpenApiInfo { Title = "admin_v1", Version = "v1" });
    c.SwaggerDoc("client_v1", new OpenApiInfo { Title = "client_v1", Version = "v1" });
    #region
    //deyeyki category uzerinde deyisiklik etdik ve her biri qalsin deye veya
    //evvelkine qayida biley deye v1,v2 deye adlandirilir
    #endregion




    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
builder.Services.AddFluentValidationRulesToSwagger();
var app = builder.Build();
app.UseCors("P136");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/admin_v1/swagger.json", "Admin Version 1");
        c.SwaggerEndpoint($"/swagger/client_v1/swagger.json", "Client Version 1");
    });
}




app.UseSerilogRequestLogging(); 
app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseAuthentication();    
app.UseAuthorization();
app.MapControllers();

app.Run();
