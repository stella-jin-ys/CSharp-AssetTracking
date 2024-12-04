public class Assets
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }
    public double PriceUSD { get; set; }
    public string Currency { get; set; }
    public double LocalPrice { get; set; }
    public string PurchaseDate { get; set; }

    public Assets(string type, string brand, string model, string office, string purchaseDate, double priceUSD, string currency, double localPrice = 0)
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
    public string Show()
    {
        Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price in USD".PadRight(20) + "Currency".PadRight(20) + "Local price".PadRight(20));
        return Type.PadRight(20) + Brand.PadRight(20) + Model.PadRight(20) + Office.PadRight(20) + PurchaseDate.PadRight(20) + PriceUSD.ToString().PadRight(20) + Currency.PadRight(20) + LocalPrice.ToString().PadRight(20);
    }
}