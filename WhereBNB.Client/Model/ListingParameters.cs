namespace WhereBNB.Client.Model
{
    public class ListingParameters
    {
        public string Type { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string Neighbourhood { get; set; }
        public int? ReviewFrom { get; set; }
        public int? ReviewTo { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
    }
}