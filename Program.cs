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

builder.Services.AddSingleton(new LinkService());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(SpecificOriginsPolicyName);

var apiLinks = app.MapGroup("/api/links");

apiLinks.MapPost("", (CreateLinkDTO dto, LinkService service) =>
	Results.Ok(service.CreateLink(dto))
);

app.Run();
