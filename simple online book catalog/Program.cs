using Microsoft.EntityFrameworkCore;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Mappings;
using simple_online_book_catalog.Repository;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SimOnBookDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleOnlinebookCatalog")));

builder.Services.AddScoped<IAuthor, AuthorRepo>();
builder.Services.AddScoped<IGenres, GenresRepo>();
builder.Services.AddScoped<IBook, BookRepo>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
