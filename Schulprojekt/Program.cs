using Microsoft.EntityFrameworkCore;
using Radzen;
using Schulprojekt.Components;
using Schulprojekt.Data;
using Schulprojekt.Services;

var builder = WebApplication.CreateBuilder(args);

// Add SQL Server connection
builder.Services.AddDbContextFactory<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped<ISpielerService, SpielerService>();
builder.Services.AddScoped<IQuestionSetService, QuestionSetService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IThemaService, ThemaService>();
builder.Services.AddScoped<ISpielerService, SpielerService>();
builder.Services.AddScoped<IQuestionSetProgressService, QuestionSetProgressService>();
builder.Services.AddScoped<QuizStateService>();
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

// Incase we have Seeder then can here the Migration run and Update Database.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

/// <author>Samer</author>
/// <summary>
/// Automatically opens the application in the default browser at http://localhost:5000
/// when the application starts, but only if not in the Development environment.
/// </summary>
if (!app.Environment.IsDevelopment())
{
    var url = "http://localhost:5000";

    app.Lifetime.ApplicationStarted.Register(() =>
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch { }
    });
}

app.Run();
