using Infrastructure.Repository;
using Infrastructure.Service;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// AddTransient: 每次请求时创建一个新的实例
//AddSingleton: 在应用程序的生命周期内只创建一个实例
// Http Request=> Single Object


builder.Services.AddDbContext<MovieShopDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MovieShopDbContext")
    )
);
//cookie based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options=>
{
    options.Cookie.Name="MovieShopAuthCookie";
    options.ExpireTimeSpan=TimeSpan.FromHours(1);
    options.LoginPath="/account/login";
}
);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
 
//Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Movie}/{action=TopRated}");

app.Run();
