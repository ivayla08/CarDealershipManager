namespace CarDealershipManager.ViewModels.Owner
{
    public class OwnerIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CarDealershipNames { get; set; } = string.Empty;
    }
}
