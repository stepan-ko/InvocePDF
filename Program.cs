using InvocePDF.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



//My
builder.Services.AddScoped<PdfService>();
builder.Services.AddRazorPages();

// Указываем лицензию
QuestPDF.Settings.License = LicenseType.Community;

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
   
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.MapRazorPages();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
