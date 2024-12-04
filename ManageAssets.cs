public class Computers : Assets
{
    public Computers(string type, string brand, string model, string office, string purchaseDate, double priceUSD, string currency, double localPrice = 0) : base(type, brand, model, office, purchaseDate, priceUSD, currency, localPrice)
    {
    }
}
public class Phones : Assets
{
    public Phones(string type, string brand, string model, string office, string purchaseDate, double priceUSD, string currency, double localPrice = 0) : base(type, brand, model, office, purchaseDate, priceUSD, currency, localPrice)
    {

    }
}
public class ManageAssets
{
    List<Assets> assets = new List<Assets>();
    public void AddAssets()
    {
        while (true)
        {
            Console.WriteLine("Enter the asset type or enter Q to end the program!");
            Console.Write("Enter the asset type, computer or phone: ");
            string assetType = Console.ReadLine().ToLower();
            if (assetType.ToLower() == "q")
            {
                break;
            }

            Console.Write("Enter the asset brand: ");
            string assetBrand = Console.ReadLine();

            Console.Write("Enter the asset model: ");
            string assetModel = Console.ReadLine();

            Console.Write("Enter the office, Sweden, Spain or USA: ");
            string office = Console.ReadLine().ToLower();
            string currency = office switch
            {
                "sweden" => "SEK",
                "spain" => "EUR",
                _ => "USD"
            };

            Console.Write("Enter the asset price in USD: ");
            double.TryParse(Console.ReadLine(), out double priceUSD);

            Console.Write("Enter the purchase date(MM/DD/YYYY): ");
            string purchaseDate = Console.ReadLine();

            if (assetType == "computer")
            {
                assets.Add(new Computers(assetType, assetBrand, assetModel, office, purchaseDate, priceUSD, currency));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Asset added successfully!");
                Console.ResetColor();
            }
            else if (assetType == "phone")
            {
                assets.Add(new Phones(assetType, assetBrand, assetModel, office, purchaseDate, priceUSD, currency));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Asset added successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Invalid asset type! Please enter 'computer' or 'phone");
            }
        }
        ShowAssets();
    }
    public void ShowAssets()
    {
        Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price in USD".PadRight(20) + "Currency".PadRight(20) + "Local price".PadRight(20));
        foreach (var asset in assets)
        {
            Console.WriteLine(asset.Show());
        }
        SortByClass();
        SortByDate();
    }
    public void SortByClass()
    {
        assets = assets.OrderBy(asset => asset.GetType().Name).ToList();
    }
    public void SortByDate()
    {
        assets = assets.OrderBy(assets => DateTime.Parse(assets.PurchaseDate)).ToList();
    }

}