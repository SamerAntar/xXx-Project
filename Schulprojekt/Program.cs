using Microsoft.EntityFrameworkCore;
using Radzen;
using Schulprojekt.Components;
using Schulprojekt.Data;
using Schulprojekt.Services;

var builder = WebApplication.CreateBuilder(args);

// Add SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ISpielerService, SpielerService>();
builder.Services.AddScoped<IQuestionSetService, QuestionSetService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IThemaService, ThemaService>();
builder.Services.AddScoped<ISpielerService, SpielerService>();
builder.Services.AddScoped<QuizStateService>();
builder.Services.AddScoped<FinalResultStateService>();
builder.Services.AddRadzenComponents();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
