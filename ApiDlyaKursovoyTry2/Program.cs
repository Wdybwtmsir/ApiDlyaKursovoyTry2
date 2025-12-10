using ApiDlyaKursovoyTry2.Models;
using ApiDlyaKursovoyTry2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<NormalnayaKursovayaContext>(opt => opt.UseSqlServer(connectionString)).AddTransient<ArchiveService, ArchiveService>();
builder.Services.AddScoped<ArchiveService, ArchiveService>();
builder.Services.AddScoped<ClientService, ClientService>();
builder.Services.AddScoped<NumbersOtherService, NumbersOtherService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        // óêàçûâàåò, áóäåò ëè âàëèäèðîâàòüñÿ èçäàòåëü ïðè âàëèäàöèè òîêåíà
        ValidateIssuer = true,
        // ñòðîêà, ïðåäñòàâëÿþùàÿ èçäàòåëÿ
        ValidIssuer = AuthOptions.ISSUER,
        // áóäåò ëè âàëèäèðîâàòüñÿ ïîòðåáèòåëü òîêåíà
        ValidateAudience = true,
        // óñòàíîâêà ïîòðåáèòåëÿ òîêåíà
        ValidAudience = AuthOptions.AUDIENCE,
        // áóäåò ëè âàëèäèðîâàòüñÿ âðåìÿ ñóùåñòâîâàíèÿ
        ValidateLifetime = true,
        // óñòàíîâêà êëþ÷à áåçîïàñíîñòè
        IssuerSigningKey = AuthOptions.GetSimmetricSecurutyKey(),
        // âàëèäàöèÿ êëþ÷à áåçîïàñíîñòè
        ValidateIssuerSigningKey = true
    };
});
var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapPost("/login", async (Admin user, NormalnayaKursovayaContext db) =>
{
    Admin? admin = await db.Admins!.FirstOrDefaultAsync(p => p.Email == user.Email);
    string Password = AuthOptions.GetHash(user.Password);
    if (admin is null) return Results.Unauthorized();
    if (admin.Password != Password) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };
    var jwt = new JwtSecurityToken
    (
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSimmetricSecurutyKey(), SecurityAlgorithms.HmacSha256));
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = admin.Email
    };
    return Results.Json(response);
}
);
app.MapPost("/register", async (Admin user, NormalnayaKursovayaContext db) =>
{
    user.Password = AuthOptions.GetHash(user.Password);
    db.Admins.Add(user);
    await db.SaveChangesAsync();
    Admin createdUser = db.Admins.FirstOrDefault(p => p.Email == user.Email)!;
    return Results.Ok(createdUser);
});
var context = app.Services.CreateScope().ServiceProvider.
    GetRequiredService<NormalnayaKursovayaContext>();
SeedData.SeedDatabase(context);
app.Run();
public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretsecretkey!123";
    public static SymmetricSecurityKey GetSimmetricSecurutyKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    public static string GetHash(string plaintext)
    {
        var sha = new SHA1Managed();
        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
        return Convert.ToBase64String(hash);
    }
}