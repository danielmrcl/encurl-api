using Encurl.Api.Config;

var specificOriginsPolicyName = "_specificOriginsPolicyName";

var builder = WebApplication.CreateBuilder(args);
ConfigureBuilder.Cors(builder, specificOriginsPolicyName);
ConfigureBuilder.Settings(builder);
ConfigureBuilder.Singletons(builder);
ConfigureBuilder.Monitoring(builder);
ConfigureBuilder.Caching(builder);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(specificOriginsPolicyName);

Middlewares.Configure(app);
Routes.Map(app);

app.Run();
