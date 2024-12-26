using Microsoft.AspNetCore.Authentication.Cookies;
using Zamdau.Interfaces;
using Zamdau.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ISellerService,SellerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddSession(option =>
{
    option.IdleTimeout =TimeSpan.FromHours(1);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

// Adicionar autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Buyer/Login"; // Caminho para a página de login
        options.LogoutPath = "/Buyer/Logout"; // Caminho para logout
        options.AccessDeniedPath = "/Buyer/AccessDenied"; // Caminho para acesso negado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Tempo de expiração do cookie
        options.SlidingExpiration = true; // Renova o cookie se o usuário estiver ativo
        options.Cookie.HttpOnly = true; // Protege contra acesso via scripts maliciosos
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requer HTTPS (recomendado em produção)
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
