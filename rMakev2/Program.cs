
using Blazorise.RichTextEdit;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using rMakev2.ViewModel;
using rMakev2.Services;
using Blazored.Toast;
using MudBlazor.Services;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using System.Text.Json.Serialization;
using rMakev2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<RmakeViewModel>();
builder.Services.AddScoped<RobinVM>();
builder.Services.AddScoped<AIChat>();
builder.Services.AddScoped<ProjectViewModel>();

builder.Services.AddBlazoredToast();
builder.Services
    .AddBlazorise()
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services
    .AddBlazoriseRichTextEdit(options => { options.UseBubbleTheme = true; });

builder.Services.AddControllersWithViews()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.MaximumReceiveMessageSize = 5 * 1024 * 1024; // 5MB
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://rcontentman.azurewebsites.net/",
                                "https://localhost:7267/");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapBlazorHub();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
