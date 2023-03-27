using System.Reflection;

namespace BuildingMaterials.DataDTOs
{
    public class OrderProductsDTO
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductsCost { get; set; }
        public int Counts { get; set; }
        public string ProviderInn { get; set; }
        public string ProviderAddres { get; set; }
        public string ProviderName { get; set; }
        public string ProviderTitle { get; set; }
    }
}
