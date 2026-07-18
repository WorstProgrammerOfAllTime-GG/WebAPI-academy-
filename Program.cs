using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ProjectAcademy.Authorization_and_authentication_JWT_approach_;
using ProjectAcademy.DBContext;
using ProjectAcademy.EndPointsAndControllers;
using ProjectAcademy.Jwt;
using ProjectAcademy.Middlewares;
using ProjectAcademy.RestHandlers;
using ProjectAcademy.Services;
using ProjectAcademy.Validation;


var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите JWT токен"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer", document),
                new List<string>()
            }
        });
});
builder.Services.AddSingleton<AuthenticationOptions>();
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthenticationOptions.ISSUER,

            ValidateAudience = true,
            ValidAudience = AuthenticationOptions.AUDIENCE,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new AuthenticationOptions(builder.Configuration)
                .GetSymmetricSecurityKey()
        };
    });
builder.Services.AddSingleton<PostgresConnectionProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<Validator>();
builder.Services.AddSingleton<AuthenticationOptions>();
builder.Services.AddSingleton<CreatorToken>();
builder.Services.AddScoped<ScheduleService>();
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.UseAuthentication();
app.UseAuthorization();
app.MapAuthEndpoints();
app.MapSchedule();
app.MapControllers();
app.Run();