var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS Policy
var SpecificOriginsPolicyName = "_specificOriginsPolicyName";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: SpecificOriginsPolicyName, policy =>
		{
			policy.WithOrigins("http://localhost:5173")
				.WithMethods("POST")
				.AllowAnyHeader();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(SpecificOriginsPolicyName);

app.MapPost("/api/links", Routers.PostLinks);

app.Run();
