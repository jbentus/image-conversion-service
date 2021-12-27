using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Imagination.Swagger
{
    public class ConvertOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId == "Convert")
            {
                // Required as ProducesResponseType does not generate mediaType
                // documentation per status response as expected. 
                operation.Responses["200"] = new OpenApiResponse
                {
                    Content = new Dictionary<string, OpenApiMediaType> {
                        ["image/jpeg"] = new OpenApiMediaType()
                     }
                };
                
                operation.RequestBody = new OpenApiRequestBody();
                operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType> ()
                {
                    ["application/octet-stream"] = new OpenApiMediaType { Schema = new() }
                };
                
                operation.RequestBody.Content.First().Value.Schema.Properties.Add("binary", new OpenApiSchema  
                {  
                    Type = "string",  
                    Format = "binary"  
                });
            }
        }
    }    
}

// c.MapType<FileStreamResult>(() => new OpenApiSchema { 
//                     Type = "string", Format = "binary", Default = new OpenApiString(""), Description = "binary" });
