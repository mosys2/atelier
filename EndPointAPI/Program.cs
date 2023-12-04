using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Helpers;
using Atelier.Domain.Entities.Users;
using Atelier.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));
builder.Services.AddIdentity<User, Role>(
    options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DatabaseContext>()
              .AddDefaultTokenProviders().AddRoles<Role>().AddErrorDescriber<CustomIdentityError>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(RoleesName.Admin, policy => policy.RequireRole(RoleesName.Admin));
    option.AddPolicy(RoleesName.Secretary, policy => policy.RequireRole(RoleesName.Secretary));
    option.AddPolicy(RoleesName.Employee, policy => policy.RequireRole(RoleesName.Employee));
    option.AddPolicy(RoleesName.Customer, policy => policy.RequireRole(RoleesName.Customer));
});

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();


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
