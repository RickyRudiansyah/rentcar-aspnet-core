using RentCar.Models;

namespace RentCar.ViewModels
{
    public class HomeViewModel
    {
        public CarSearchViewModel Search { get; set; } = new CarSearchViewModel();
        public List<MsCar> Cars { get; set; } = new List<MsCar>();
        public List<int> AvailableYears { get; set; } = new List<int>();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasSearched { get; set; } = false;
    }
}