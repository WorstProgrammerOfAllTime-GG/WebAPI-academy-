using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectAcademy.DBContext;
using ProjectAcademy.EndPointsAndControllers;
using ProjectAcademy.Services;
using ProjectAcademy.Validation;


var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
       
    };
});
builder.Services.AddSingleton<PostgresCreate>();
builder.Services.AddScoped<Authentication>();
builder.Services.AddScoped<Validator>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.MapAuth();
app.MapControllers();



app.Run();