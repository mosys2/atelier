using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Services.Ateliers.Queries;
using Atelier.Application.Services.Ateliers.Commands;
using Atelier.Application.Services.Ateliers.Queries;
using Atelier.Application.Services.Auth;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Application.Services.Users.Commands.AddUser;
using Atelier.Application.Services.Users.Commands.DeleteToken;
using Atelier.Application.Services.Users.Commands.SaveToken;
using Atelier.Application.Services.Users.Queries.FindRefreshToken;
using Atelier.Application.Services.Users.Queries.ValidatorUsers;
using Atelier.Common.Constants;
using Atelier.Common.Helpers;
using Atelier.Domain.Entities.Users;
using Atelier.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Atelier.Application.Services.Branches.Commands.AddBranch;
using Atelier.Application.Services.Users.Commands.DeleteUser;
using Atelier.Application.Services.Users.Commands.EditUser;
using Atelier.Application.Services.Users.Queries.GetAllUser;
using Atelier.Application.Services.Users.Queries.GetDetailsUser;
using Atelier.Application.Services.Roles.Queries.GetRolesUser;
using Atelier.Application.Services.Roles.Queries.GetRoles;
using Atelier.Application.Services.Ateliers.Commands.RemoveAtelier;
using Atelier.Application.Services.Branches.Commands.RemoveBranch;
using Atelier.Application.Services.Ateliers.Queries.GetDetailAtelier;
using Atelier.Application.Services.Branches.Queries.GetDetailBranch;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));
builder.Services.AddIdentity<User, Role>(
    options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DatabaseContext>()
              .AddDefaultTokenProviders().AddRoles<Role>().AddErrorDescriber<CustomIdentityError>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthorization(option =>
{
   
   option.AddPolicy("BigAdminOrAdminOrSecretary", policy =>
   {
    policy.RequireRole(RoleesName.BigAdmin, RoleesName.Admin,RoleesName.Secretary);
    });
    option.AddPolicy(RoleesName.BigAdmin, policy =>{policy.RequireClaim(ClaimTypes.Role, RoleesName.BigAdmin);});
    option.AddPolicy(RoleesName.Admin, policy => { policy.RequireClaim(ClaimTypes.Role, RoleesName.Admin); });
    option.AddPolicy(RoleesName.Secretary, policy => { policy.RequireClaim(ClaimTypes.Role, RoleesName.Secretary); });
    option.AddPolicy(RoleesName.Employee, policy => { policy.RequireClaim(ClaimTypes.Role, RoleesName.Employee); });
    option.AddPolicy(RoleesName.Customer, policy => { policy.RequireClaim(ClaimTypes.Role, RoleesName.Customer); });

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

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IAddBigAdminService, AddBigAdminService>();
builder.Services.AddScoped<IGetAllAtelierBase, GetAllAtelierBase>();
builder.Services.AddScoped<IGetAllBranches, GetAllBranches>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAddAtelierService,AddAtelierService>();
builder.Services.AddScoped<ISaveUserTokenService, SaveUserTokenService>();
builder.Services.AddScoped<ITokenValidatorUserService, TokenValidatorUserService>();
builder.Services.AddScoped<IDeleteTokenUserService, DeleteTokenUserService>();
builder.Services.AddScoped<IFindRefreshTokenService, FindRefreshTokenService>();
builder.Services.AddScoped<IGetRolesUserService, GetRolesUserService>();
builder.Services.AddScoped<IAddBranchService, AddBranchService>();
builder.Services.AddScoped<IAddAdminService, AddAdminService>();
builder.Services.AddScoped<IAddSecretaryService, AddSecretaryService>();
builder.Services.AddScoped<IAddEmployeeService, AddEmployeeService>();
builder.Services.AddScoped<IAddCustomerService, AddCustomerService>();
builder.Services.AddScoped<IRemoveBigAdminService, RemoveBigAdminService>();
builder.Services.AddScoped<IRemoveAdminService, RemoveAdminService>();
builder.Services.AddScoped<IRemoveCustomerService, RemoveCustomerService>();
builder.Services.AddScoped<IRemoveEmployeeService, RemoveEmployeeService>();
builder.Services.AddScoped<IRemoveSecretaryService, RemoveSecretaryService>();
builder.Services.AddScoped<IEditAdminService, EditAdminService>();
builder.Services.AddScoped<IEditBigAdminService, EditBigAdminService>();
builder.Services.AddScoped<IEditCustomerService, EditCustomerService>();
builder.Services.AddScoped<IEditEmployeeService, EditEmployeeService>();
builder.Services.AddScoped<IEditSecretaryService, EditSecretaryService>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetDetailsUserService, GetDetailsUserService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRemoveAtelierService, RemoveAtelierService>();
builder.Services.AddScoped<IRemoveBranchService, RemoveBranchService>();
builder.Services.AddScoped<IGetDetailAtelierService, GetDetailAtelierService>();
builder.Services.AddScoped<IGetDetailBranchService, GetDetailBranchService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
                       var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorUserService>();
                       return tokenValidatorService.Execute(context);
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

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:44356",
                                              "http://www.google.com").AllowAnyMethod();
                      });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
