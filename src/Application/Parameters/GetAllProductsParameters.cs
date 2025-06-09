

namespace Application.Parameters
{
    //Esta clase marca los atributos por los que se puede filtrar 
    // en un get all (obtener todos) de productos. 
    public class GetAllProductsParameters : BaseRequestParameters
    {
        public string Name { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}