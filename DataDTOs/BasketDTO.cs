using System.Windows.Input;

namespace BuildingMaterials.DataDTOs
{
    public class BasketDTO
    {
        public int NumberId { get; set; }
        public int Counts { get; set; }
        public int ProviderId { get; set; }
        public decimal Fullprice { get; set; }
    }
}
