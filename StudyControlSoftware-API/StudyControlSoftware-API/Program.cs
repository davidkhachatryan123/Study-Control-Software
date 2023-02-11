using StudyControlSoftware_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependencies();

builder.Services.ConfigureCORS();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositoryManager();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.ConfigureIdentity();

builder.Services.ConfigureControllers();

builder.Services.ConfigureHostedServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
