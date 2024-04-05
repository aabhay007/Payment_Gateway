using Microsoft.EntityFrameworkCore;
using Payment_Gateway;
using Payment_Gateway.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string cs = builder.Configuration.GetConnectionString("con");
builder.Services.AddEntityFrameworkSqlServer()
  .AddDbContext<AppDbContext>(options => options.UseSqlServer(cs, b =>
  b.MigrationsAssembly("Payment_Gateway")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
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
app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
