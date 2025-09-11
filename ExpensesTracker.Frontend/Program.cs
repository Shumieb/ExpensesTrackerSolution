using ExpensesTracker.Frontend.Clients;
using ExpensesTracker.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var expensesTrackerAPIURL = builder.Configuration["ExpensesTrackerApiUrl"] ??
                        throw new Exception("ExpensesTrackerApiUrl is not set.");

builder.Services.AddHttpClient<ExpensesClient>(
    client => client.BaseAddress = new Uri(expensesTrackerAPIURL));

builder.Services.AddHttpClient<IncomeClient>(
    client => client.BaseAddress = new Uri(expensesTrackerAPIURL));

builder.Services.AddHttpClient<CategoriesClient>(
    client => client.BaseAddress = new Uri(expensesTrackerAPIURL));

builder.Services.AddHttpClient<FrequenciesClient>(
    client => client.BaseAddress = new Uri(expensesTrackerAPIURL));

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
