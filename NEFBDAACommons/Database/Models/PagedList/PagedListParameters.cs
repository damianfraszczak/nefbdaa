namespace NEFBDAACommons.Database.Models.PagedList
{
    public class PagedListParameters
    {
        //public IList<Order> Orders { get; set; } = new List<Order>();
        public Order Order { get; set; }
        public Page Page { get; set; } = new Page();
    }
}
