using System.Text.Json.Serialization;

namespace Netrex.Frontend.Application.ViewModels.ProductManagement
{
    public class VmProductCategory
    {
        [property: JsonPropertyName("productCategoryId")]
        public Guid Id { get; set; }
        public string ProductCategoryName { get; set; }
        public string IconUrl { get; set; }  
    }
}
