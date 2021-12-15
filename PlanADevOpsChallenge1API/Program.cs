var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{//Added global error handler. If an unexpected error occurs will redirect to a raw error page.
//This only applies for non development environments (Running in the local workstation). 
app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
