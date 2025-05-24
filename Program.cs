using api.config;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Policy
var specificOriginsPolicyName = "_specificOriginsPolicyName";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: specificOriginsPolicyName, policy =>
		{
			policy.WithOrigins("http://localhost:5173")
				.WithMethods("POST")
				.AllowAnyHeader();
		});
});

ConfigureBuilder.Settings(builder);
ConfigureBuilder.Singletons(builder);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(specificOriginsPolicyName);

Middlewares.Configure(app);
Routes.Map(app);

app.Run();
