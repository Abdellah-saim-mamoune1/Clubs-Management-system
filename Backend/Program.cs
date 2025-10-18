using EventsManagement.Data;
using EventsManagement.Interfaces.Repositories.Authentication;
using EventsManagement.Interfaces.Repositories.Employee;
using EventsManagement.Interfaces.Repositories.Student;
using EventsManagement.Interfaces.Services;
using EventsManagement.Interfaces.Services.Employee;
using EventsManagement.Repositories;
using EventsManagement.Repositories.Club;
using EventsManagement.Repositories.Employee;
using EventsManagement.Repositories.Student;
using EventsManagement.Repositories.User;
using EventsManagement.Services.Club;
using EventsManagement.Services.Employee;
using EventsManagement.Services.Student;
using EventsManagement.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IStudentService, StudentService>(); 
builder.Services.AddScoped<IClubAdminRepository, ClubAdminRepository>();
builder.Services.AddScoped<IClubUserService, ClubUserService>();

//Repos
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>(); 
builder.Services.AddScoped<IClubAdminService,ClubAdminService>();
builder.Services.AddScoped<IClubUserRepository, ClubUserRepository>();


//EmployeeServices
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//EmployeeRepos
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),

        };

    });

builder.Services.AddHttpClient("ProgressAPI", client =>
{
    client.BaseAddress = new Uri("https://progres.mesrs.dz/api/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://clubs-orpin.vercel.app")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
