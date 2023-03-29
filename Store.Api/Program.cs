using Store.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.MapControllers();
app.UseResponseCompression();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();