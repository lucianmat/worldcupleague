using Asp.Versioning.ApiExplorer;
using DrawService.Entities;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(o =>
{ 
    o.ConfigureHttpsDefaults(opts =>
    {   
        opts.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
        opts.AllowAnyClientCertificate();
        opts.ClientCertificateValidation = (cert, chain, policyErrors) =>
        {
            // Certificate validation logic here
            // Return true if the certificate is valid or false if it is invalid
            return true;
        };
    });
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

// use extensions to configure services
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureApiSwagger();

DrawService.ConfigurationManager.Initialize();
DbDrawRepository dbDrawRepository = new(DrawService.ConfigurationManager.ConnectionString!);
DrawService.DrawService drawService = new(dbDrawRepository);

builder.Services.AddSingleton<IDrawService>(drawService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} 
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

var descriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwaggerUI(c=>{
    c.RoutePrefix = string.Empty; // default request rerouted to "swagger"

    foreach (var description in descriptionProvider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", 
            description.GroupName.ToUpperInvariant());
    }
    
});

app.UseHttpsRedirection();
app.UseHsts();

app.UseAuthorization();
app.UseHttpLogging(); 

app.MapControllers();

app.Run();
