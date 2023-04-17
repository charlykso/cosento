using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//for EF-core
builder.Services.AddDbContext<Cosento_DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cosento_AppCon"));
});

//For Json Serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

//For Automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

//enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowAllOrigin", options => options.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddScoped<ILevel, LevelServices>();
builder.Services.AddScoped<ICourse, CourseServices>();
builder.Services.AddScoped<ILecturer, LecturerServices>();
builder.Services.AddScoped<ISemester, SemesterServices>();
builder.Services.AddScoped<ILecturerCourse, LecturerCourseServices>();

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

app.UseCors("AllowAllOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
