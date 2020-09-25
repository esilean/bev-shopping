using Microsoft.OpenApi.Models;

namespace BevShopping.Shared.Infra.Swagger
{
    public class SwaggerOptions : OpenApiInfo
    {
        public string VersionName { get; set; } = "v1";
    }
}
