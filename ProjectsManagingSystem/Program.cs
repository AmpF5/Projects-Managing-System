using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectsManagingSystem;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Models.Member;
using ProjectsManagingSystem.Models.Validators;
using ProjectsManagingSystem.Services.Member;
using ProjectsManagingSystem.Services.Project;
using ProjectsManagingSystem.Services.Task;

var builder = WebApplication.CreateBuilder(args);
{

    var authenticationSettings = new AuthenticationSettings();

    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
        };
    });
    // Add services to the container.
    builder.Services.AddControllers().AddFluentValidation(); ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ProjectSystemDbContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDbConnection")));
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<IMemberService, MemberService>();
    builder.Services.AddScoped<ProjectSystemSeeder>();
    builder.Services.AddScoped<IPasswordHasher<Member>, PasswordHasher<Member>>();
    builder.Services.AddScoped<IValidator<MemberDto>, RegisterMemberDtoValidator>();
    builder.Services.AddHttpContextAccessor();
}

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ProjectSystemSeeder>();
seeder.Seed();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();