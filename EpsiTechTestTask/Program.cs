using EpsiTechTestTask.Data;
using EpsiTechTestTask.Servises.Exeption;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ExceptionHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "EpsiTechTestTask.xml");
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AppDbContext>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EpsiTechTestTask api");
    c.RoutePrefix = string.Empty; 
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthorization();

app.MapControllers();

app.Run();