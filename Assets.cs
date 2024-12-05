public class Assets
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }
    public double PriceUSD { get; set; }
    public string Currency { get; set; }
    public double LocalPrice { get; set; }
    public DateOnly PurchaseDate { get; set; }

    public Assets(string type, string brand, string model, string office, DateOnly purchaseDate, double priceUSD, string currency, double localPrice)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Office = office;
        PriceUSD = priceUSD;
        Currency = currency;
        LocalPrice = localPrice;
        PurchaseDate = purchaseDate;
    }

}