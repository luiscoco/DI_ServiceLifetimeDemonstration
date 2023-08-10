using ServiceLifetimeDemonstration;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;//when is on checks objects graph on app launch stage
});

#region dotnet 6 changes
//traditional way is to use Startup class;
//modern is to put services registrations and http pipeline setup here
//to return to the traditional way, please use the line below
//builder.WebHost.UseStartup<Startup>();
#endregion

#region  Add services to the container.

builder.Services.AddRazorPages();

#region lifetimes
//1. singleton - the same guid from request to request
//2. scoped - middleware and page give the same guid, which changes from request to request
//3. transient - middleware and page give different guids which changes from requests to request
#endregion
builder.Services.AddSingleton<IGuidService, GuidService>();
builder.Services.AddScoped<IGuidTrimmer, GuidTrimmer>();

#region scope validation
//scope validation is on by default in dev, off in prod as leads to performance degradation
//change ASPNETCORE_ENVIRONMENT for the demonstration
//also see line 4
#endregion

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<CustomMiddleware>();

app.MapRazorPages();

#endregion

app.Run();
