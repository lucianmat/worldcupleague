using Asp.Versioning.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureApiSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
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

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHttpLogging(); 

app.MapControllers();

app.Run();
