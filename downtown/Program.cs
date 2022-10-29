using downtown.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//for dapper connection added in start up file in .net 5 or program.cs in .net 6
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
//enable cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseCors(x => x
       .SetIsOriginAllowed(origin => true)
       .AllowAnyMethod()
       .AllowAnyHeader()
       .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
