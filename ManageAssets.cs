using System.Drawing;

public class Computers : Assets
{
    public Computers(string type, string brand, string model, string office, DateOnly purchaseDate, double priceUSD, string currency, double localPrice) : base(type, brand, model, office, purchaseDate, priceUSD, currency, localPrice)
    {
    }
}
public class Phones : Assets
{
    public Phones(string type, string brand, string model, string office, DateOnly purchaseDate, double priceUSD, string currency, double localPrice) : base(type, brand, model, office, purchaseDate, priceUSD, currency, localPrice)
    {

    }
}
public class ManageAssets
{
    List<Assets> assets = new List<Assets>();
    public void AddAssets()
    {
        CurrencyConverter.FetchRates();
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Enter the asset type or enter Q to end the program!");
            Console.ResetColor();
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
            double localPrice = CurrencyConverter.Convert(priceUSD, "USD", currency);

            Console.Write("Enter the purchase date(MM-DD-YYYY): ");
            DateTime purchaseTime = Convert.ToDateTime(Console.ReadLine());
            DateOnly purchaseDate = DateOnly.FromDateTime(purchaseTime);

            if (assetType == "computer")
            {
                assets.Add(new Computers(assetType, assetBrand, assetModel, office, purchaseDate, priceUSD, currency, localPrice));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Asset added successfully!");
                Console.ResetColor();
            }
            else if (assetType == "phone")
            {
                assets.Add(new Phones(assetType, assetBrand, assetModel, office, purchaseDate, priceUSD, currency, localPrice));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Asset added successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid asset type. Please enter 'computer' or 'phone'.");
                Console.ResetColor();
            }
        }
    }
    public void ShowAssets()
    {
        Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price in USD".PadRight(20) + "Currency".PadRight(20) + "Local price".PadRight(20));
        int threeYears = 365 * 3;
        int threeMonths = 30 * 3;
        int sixMonths = 30 * 6;

        foreach (var a in assets)
        {
            int validDays = DateOnly.FromDateTime(DateTime.Now).DayNumber - a.PurchaseDate.DayNumber;
            ConsoleColor color = ConsoleColor.White;

            if (validDays > threeYears || threeYears - validDays <= threeMonths)
            {
                color = ConsoleColor.Red;
            }
            else if (threeYears - validDays <= sixMonths)
            {
                color = ConsoleColor.Yellow;
            }
            PrintAssets(a, color);

        }
        Sort();
    }
    public void Sort()
    {
        assets = (List<Assets>)assets.OrderBy(asset => asset.Office).ThenBy(asset => asset.GetType().Name).ThenByDescending(assets => assets.PurchaseDate).ToList();
    }
    public void PrintAssets(Assets a, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(a.Type.PadRight(20) + a.Brand.PadRight(20) + a.Model.PadRight(20) + a.Office.PadRight(20) + a.PurchaseDate.ToString().PadRight(20) + a.PriceUSD.ToString("0.00").PadRight(20) + a.Currency.PadRight(20) + a.LocalPrice.ToString("0.00").PadRight(20));
        Console.ResetColor();
    }
}