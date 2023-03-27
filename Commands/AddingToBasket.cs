using BuildingMaterials.ViewModels;

namespace BuildingMaterials.Commands
{
    public class AddingToBasket : CommandBase
    {
        public delegate void AddToBasket();
        public event AddToBasket AddedBasket;
        public AddingToBasket(ProductViewModel model)
        {
            AddedBasket += model.OnAddedToBusket;
        }
        public override void Execute(object parameter)
        {
            AddedBasket?.Invoke();
        }
    }
}
