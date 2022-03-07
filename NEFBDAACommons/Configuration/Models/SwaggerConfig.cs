using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Configuration.Models
{
  public class SwaggerConfig
  {
    public OpenApiInfo Info { get; set; } = new OpenApiInfo() { Version = "v1", Title = "Api" };
    public string Url { get; set; } = "/swagger/v1/swagger.json";
    public string Name { get; set; } = "API V1";
    public string SecurityName { get; set; } = "Bearer";

    public OpenApiSecurityScheme ApiKeyScheme { get; set; } = new OpenApiSecurityScheme
    {
      Description =
          "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\" \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      Scheme = "bearer",
      BearerFormat = "JWT",
      In = ParameterLocation.Header,

    };

    
    public OpenApiSecurityRequirement ApiRequirement { get; set; } = new OpenApiSecurityRequirement()
    {
      {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },

        },
        new List<string>()
      }
    };
  }
}
