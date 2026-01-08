namespace RentCar.ViewModels
{
    public class CarSearchViewModel
    {
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? YearFilter { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}