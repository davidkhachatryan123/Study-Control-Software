using StudyControlSoftware_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddApplicationConfiguration();

builder.Services.ConfigureCORS();
builder.Services.ConfigureForwardedHeaders();

builder.Services.ConfigureMapping();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositoryManager();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.ConfigureControllers();

builder.Services.ConfigureHostedServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseForwardedHeaders();
}

app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
