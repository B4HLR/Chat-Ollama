var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<OllamaClient>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddTransient<ExplicadorDeTermos>();
builder.Services.AddTransient<AnaliseDeSentimento>();
builder.Services.AddTransient<ResumidorDeDocumentos>();

// Add services to the container
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
