using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Services.Auth;
using Atelier.Application.Services.Users.Commands.AddUser;
using Atelier.Common.Constants;
using Atelier.Common.Helpers;
using Atelier.Domain.Entities.Users;
using Atelier.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));
builder.Services.AddIdentity<User, Role>(
    options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DatabaseContext>()
              .AddDefaultTokenProviders().AddRoles<Role>().AddErrorDescriber<CustomIdentityError>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(RoleesName.BigAdmin, policy => policy.RequireRole(RoleesName.BigAdmin));
    option.AddPolicy(RoleesName.Admin, policy => policy.RequireRole(RoleesName.Admin));
    option.AddPolicy(RoleesName.Secretary, policy => policy.RequireRole(RoleesName.Secretary));
    option.AddPolicy(RoleesName.Employee, policy => policy.RequireRole(RoleesName.Employee));
    option.AddPolicy(RoleesName.Customer, policy => policy.RequireRole(RoleesName.Customer));
});
builder.Services.Configure<IdentityOptions>(option =>
{
    //UserSetting
    //option.User.AllowedUserNameCharacters = "abcd123";
    //option.User.RequireUniqueEmail = true;
    //Password Setting
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;//!@#$%^&*()_+
    option.Password.RequireUppercase = false;
    option.Password.RequiredLength = 6;
    //option.Password.RequiredUniqueChars = 1;

    ////Lokout Setting
    //option.Lockout.MaxFailedAccessAttempts = 3;
    //option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(10);

    ////SignIn Setting
    //option.SignIn.RequireConfirmedAccount = false;
    //option.SignIn.RequireConfirmedEmail = false;
    //option.SignIn.RequireConfirmedPhoneNumber = false;
});
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(configureOptions =>
           {
               configureOptions.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidIssuer = builder.Configuration["JWtConfig:issuer"],
                   ValidAudience = builder.Configuration["JWtConfig:audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWtConfig:Key"])),
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = true,
               };
               configureOptions.SaveToken = true; // HttpContext.GetTokenAsunc();
               configureOptions.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       //log 
                       //........
                       return Task.CompletedTask;
                   },
                   OnTokenValidated = context =>
                   {
                       //log
                       return Task.CompletedTask;
                   },
                   OnChallenge = context =>
                   {
                       return Task.CompletedTask;

                   },
                   OnMessageReceived = context =>
                   {
                       return Task.CompletedTask;

                   },
                   OnForbidden = context =>
                   {
                       return Task.CompletedTask;

                   }
               };

           });
builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IAddBigAdminService, AddBigAdminService>();
builder.Services.AddScoped<ILoginService, LoginService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
