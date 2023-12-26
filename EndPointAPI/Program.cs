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
using Atelier.Application.Services.Ateliers.Commands.EditAtelier;
using Atelier.Application.Services.Branches.Commands.EditBranch;
using Microsoft.OpenApi.Models;
using Atelier.Application.Services.Auth.Commands;
using Atelier.Application.Services.Users.Commands.CheckToken;
using Atelier.Application.Services.Ateliers.Commands.ChangeStatusAtelier;
using Atelier.Application.Services.Branches.Commands.ChangeStatusBranch;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Ateliers.FacadPattern;
using Atelier.Application.Services.Branches.FacadPattern;
using Atelier.Application.Services.Users.Commands.ChangeStatusUser;
using Atelier.Application.Services.Users.FacadPattern;
using Atelier.Persistence.MongoDB;
using Atelier.Domain.MongoEntities;
using Atelier.Application.Services.TestMongo;
using Atelier.Application.Services.Banks.Commands;
using Atelier.Application.Services.Persons.Commands;
using Atelier.Application.Services.Jobs.Queries;
using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.PersonTypes.Commands;
using Atelier.Application.Services.PersonTypes.Queries;
using Atelier.Application.Services.Persons.Queries;

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
builder.Services.AddScoped<ISaveUserTokenService, SaveUserTokenService>();
builder.Services.AddScoped<ITokenValidatorUserService, TokenValidatorUserService>();
builder.Services.AddScoped<IDeleteTokenUserService, DeleteTokenUserService>();
builder.Services.AddScoped<IGetRolesUserService, GetRolesUserService>();
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
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<ICheckTokenUserService, CheckTokenUserService>();
builder.Services.AddScoped<IAddNewBankService, AddNewBankService>();
builder.Services.AddScoped<IAtelierFacad, AtelierFacad>();
builder.Services.AddScoped<IBranchFacad, BranchFacad>();
builder.Services.AddScoped<IUserFacad, UserFacad>();
builder.Services.AddScoped<IAddPersonService, AddPersonService>();
builder.Services.AddScoped<IAddJobService, AddJobService>();
builder.Services.AddScoped<IGetAllJobService, GetAllJobService>();
builder.Services.AddScoped<IAddPersonTypeService, AddPersonTypeService>();
builder.Services.AddScoped<IGetAllPersonTypeService, GetAllPersonType>();
builder.Services.AddScoped<IGetAllPersonService, GetAllPersonService>();
builder.Services.AddScoped<IEditPersonService, EditPersonService>();
builder.Services.AddScoped<IRemovePersonService, RemovePersonService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Mongo*****************************
builder.Services.AddMongo()
    .AddMongoRepository<Bank>("Bank")
    .AddMongoRepository<Cheque>("Cheque")
    .AddMongoRepository<Job>("Job")
    .AddMongoRepository<PersonType>("PersonType")
    .AddMongoRepository<Person>("Person");


builder.Services.AddControllers(option=>option.SuppressAsyncSuffixInActionNames = false);

//**************************************

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
builder.Services.AddSwaggerGen(c =>
{

    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApi.Bugeto.xml"), true);


    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Bugeto", Version = "v1" });
    //c.SwaggerDoc("v2", new OpenApiInfo { Title = "WebApi.Bugeto", Version = "v2" });


    //c.DocInclusionPredicate((doc, apiDescription) =>
    //{
    //    if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

    //    var version = methodInfo.DeclaringType
    //        .GetCustomAttributes<ApiVersionAttribute>(true)
    //        .SelectMany(attr => attr.Versions);

    //    return version.Any(v => $"v{v.ToString()}" == doc);
    //});

    var security = new OpenApiSecurityScheme
    {
        Name = "JWT Auth",
        Description = "توکن خود را وارد کنید- دقت کنید فقط توکن را وارد کنید",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(security.Reference.Id, security);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { security , new string[]{ } }
                });

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
