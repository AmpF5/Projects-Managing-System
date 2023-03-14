using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProjectsManagingSystem;
using ProjectsManagingSystem.Entities;
using ProjectsManagingSystem.Services.Member;
using ProjectsManagingSystem.Services.Project;
using ProjectsManagingSystem.Services.Task;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
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


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();